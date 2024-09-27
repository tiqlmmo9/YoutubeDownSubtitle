using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace YouTubeSubtitleDownloaderWinForm
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        // Update your Form1.cs file

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                cancellationTokenSource = new CancellationTokenSource();

                string apiKey = "AIzaSyA4HGKpLTz76bL-xaXKCzbu9DGI1YtSJVA";  // Replace with your Google API key

                string[] idStrs;
                string outputDirectory = outputPathTextBox.Text.Trim(); // Get the output directory from the TextBox

                if (radioPlaylist.Checked)
                {
                    idStrs = playlistIdTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                }
                else if (radioVideo.Checked)
                {
                    idStrs = videoIdTextBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    MessageBox.Show("Please select Playlist or Video.");
                    return;
                }

                if (idStrs.Length == 0)
                {
                    MessageBox.Show("Please enter IDs.");
                    return;
                }

                if (string.IsNullOrEmpty(outputDirectory))
                {
                    MessageBox.Show("Please enter an output directory.");
                    return;
                }

                if (radioPlaylist.Checked)
                    await ProcessForPlaylist(apiKey, idStrs, outputDirectory, cancellationTokenSource.Token);

                else if (radioVideo.Checked)
                    await ProcessForVideo(apiKey, idStrs, outputDirectory, cancellationTokenSource.Token);

                logTextBox.AppendText("DOWNLOAD COMPLETE!!!\r\n");
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while processing the request. Please check your input and try again.");
                return;
            }
        }

        private async Task ProcessForPlaylist(string apiKey, string[] idStrs, string outputDirectory, CancellationToken cancellationToken)
        {
            int folderIndex = 1;

            foreach (string playlistStr in idStrs)
            {
                string playlistId = ExtractPlaylistIdOrVideoId(playlistStr);

                if (string.IsNullOrEmpty(playlistId))
                {
                    MessageBox.Show("Please enter a playlist ID.");
                    return;
                }

                var (videos, playlistTitle) = await GetVideosAndTitleFromPlaylist(apiKey, playlistId);

                string playlistDirectory = Path.Combine(outputDirectory, $"{folderIndex:000}. {playlistTitle}");
                Directory.CreateDirectory(playlistDirectory);

                logTextBox.AppendText($"Videos in the playlist '{playlistTitle}':\r\n");

                int videoIndex = 1;

                // Collect tasks for extracting subtitles for each video
                var extractSubTasks = new List<Task>();
                foreach (var video in videos)
                {
                    var task = ExtractSubForVideo(playlistDirectory, video, videoIndex, cancellationToken);
                    extractSubTasks.Add(task);
                    videoIndex++;
                }

                // Wait for all tasks to complete
                await Task.WhenAll(extractSubTasks);

                folderIndex++;
            }
        }

        private async Task ProcessForVideo(string apiKey, string[] idStrs, string outputDirectory, CancellationToken cancellationToken)
        {
            var downloadTasks = idStrs.Select((videoStr, index) => DownloadAndExtractSubForVideo(apiKey, outputDirectory, videoStr, index + 1, cancellationToken));
            await Task.WhenAll(downloadTasks);
        }

        private async Task DownloadAndExtractSubForVideo(string apiKey, string outputDirectory, string videoStr, int videoIndex, CancellationToken cancellationToken)
        {
            string videoId = ExtractPlaylistIdOrVideoId(videoStr);

            if (string.IsNullOrEmpty(videoId))
            {
                MessageBox.Show("Invalid video ID.");
                return;
            }

            var (video, videoTitle) = await GetVideoAndTitle(apiKey, videoId, cancellationToken);

            await ExtractSubForVideo(outputDirectory, video, videoIndex, cancellationToken);
        }

        private async Task ExtractSubForVideo(string directory, VideoInfo video, int videoIndex, CancellationToken cancellationToken)
        {
            logTextBox.AppendText($"{videoIndex}. {video.Title} ({video.Id})\r\n");

            string url = $"https://youtubetranscript.com/?server_vid2={video.Id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url, cancellationToken);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    XDocument xmlDoc = XDocument.Parse(responseBody);

                    var transcriptText = new System.Text.StringBuilder();
                    foreach (var element in xmlDoc.Descendants("text"))
                    {
                        transcriptText.AppendLine(element.Value);
                    }

                    string sanitizedTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));
                    string videoDirectory = Path.Combine(directory, $"{videoIndex:000}. {sanitizedTitle}.txt");

                    await File.WriteAllTextAsync(videoDirectory, transcriptText.ToString(), cancellationToken);

                    logTextBox.AppendText($"Saved transcript to {videoDirectory}\r\n");
                }
                catch (HttpRequestException ex)
                {
                    logTextBox.AppendText("Request error: " + ex.Message + "\r\n");
                }
                catch (Exception ex)
                {
                    logTextBox.AppendText("Parsing error: " + ex.Message + "\r\n");
                }
            }
        }

        public static async Task<(List<VideoInfo>, string)> GetVideosAndTitleFromPlaylist(string apiKey, string playlistId)
        {
            List<VideoInfo> videos = new List<VideoInfo>();
            string playlistTitle = "";

            using (var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "YouTubePlaylistVideoIds"
            }))
            {
                var playlistRequest = youtubeService.Playlists.List("snippet");
                playlistRequest.Id = playlistId;
                var playlistResponse = await playlistRequest.ExecuteAsync();
                if (playlistResponse.Items.Count > 0)
                {
                    playlistTitle = playlistResponse.Items[0].Snippet.Title;
                }

                string nextPageToken = "";
                while (nextPageToken != null)
                {
                    var request = youtubeService.PlaylistItems.List("contentDetails,snippet");
                    request.PlaylistId = playlistId;
                    request.MaxResults = 50;
                    request.PageToken = nextPageToken;

                    var response = await request.ExecuteAsync();
                    foreach (var item in response.Items)
                    {
                        videos.Add(new VideoInfo
                        {
                            Id = item.ContentDetails.VideoId,
                            Title = item.Snippet.Title
                        });
                    }

                    nextPageToken = response.NextPageToken;
                }
            }

            return (videos, playlistTitle);
        }

        public static async Task<(VideoInfo, string)> GetVideoAndTitle(string apiKey, string videoId, CancellationToken cancellationToken)
        {
            VideoInfo video = null;
            string videoTitle = "";

            using (var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "YouTubeVideoInfo"
            }))
            {
                var videoRequest = youtubeService.Videos.List("snippet");
                videoRequest.Id = videoId;
                var videoResponse = await videoRequest.ExecuteAsync(cancellationToken);
                if (videoResponse.Items.Count > 0)
                {
                    videoTitle = videoResponse.Items[0].Snippet.Title;
                    video = new VideoInfo
                    {
                        Id = videoId,
                        Title = videoTitle
                    };
                }
            }

            return (video, videoTitle);
        }

        public static string RemoveInvalidPathChars(string title)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            return string.Concat(title.Select(c => invalidChars.Contains(c) ? '_' : c));
        }

        static string ExtractPlaylistIdOrVideoId(string playlistUrl)
        {
            // Split the URL by '=' and take the last part
            string[] parts = playlistUrl.Split('=');
            if (parts.Length > 1)
            {
                return parts[parts.Length - 1];
            }

            // If '=' is not found, split by '/' and take the last part
            parts = playlistUrl.Split('/');
            return parts[parts.Length - 1];
        }

        private void playlistIdLabel_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource != null && !cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Cancel();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class VideoInfo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
    }
}
