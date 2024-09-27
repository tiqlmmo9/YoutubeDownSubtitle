using System;
using System.Windows.Forms;

namespace YouTubeSubtitleDownloaderWinForm
{
    partial class Form1 : Form
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnDownload;
        private Button btnCancel;
        private TextBox logTextBox;
        private TextBox playlistIdTextBox;
        private TextBox outputPathTextBox;
        private Label playlistIdLabel;
        private Label outputPathLabel;
        private RadioButton radioPlaylist;
        private RadioButton radioVideo;
        private TextBox videoIdTextBox;
        private Label videoIdLabel;
        private Button btnBrowse;
        private FolderBrowserDialog folderBrowserDialog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnDownload = new Button();
            btnCancel = new Button();
            logTextBox = new TextBox();
            playlistIdTextBox = new TextBox();
            outputPathTextBox = new TextBox();
            playlistIdLabel = new Label();
            outputPathLabel = new Label();
            radioPlaylist = new RadioButton();
            radioVideo = new RadioButton();
            videoIdTextBox = new TextBox();
            videoIdLabel = new Label();
            btnBrowse = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // btnDownload
            // 
            btnDownload.Location = new System.Drawing.Point(12, 193);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new System.Drawing.Size(416, 23);
            btnDownload.TabIndex = 0;
            btnDownload.Text = "Download Subtitles";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            btnCancel.Location = new System.Drawing.Point(462, 193);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(117, 23);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // logTextBox
            // 
            logTextBox.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            logTextBox.Location = new System.Drawing.Point(12, 238);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.Size = new System.Drawing.Size(567, 194);
            logTextBox.TabIndex = 1;
            // 
            // playlistIdTextBox
            // 
            playlistIdTextBox.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            playlistIdTextBox.Location = new System.Drawing.Point(12, 57);
            playlistIdTextBox.Multiline = true;
            playlistIdTextBox.Name = "playlistIdTextBox";
            playlistIdTextBox.ScrollBars = ScrollBars.Vertical;
            playlistIdTextBox.Size = new System.Drawing.Size(567, 75);
            playlistIdTextBox.TabIndex = 2;
            // 
            // outputPathTextBox
            // 
            outputPathTextBox.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            outputPathTextBox.Location = new System.Drawing.Point(12, 153);
            outputPathTextBox.Name = "outputPathTextBox";
            outputPathTextBox.Size = new System.Drawing.Size(416, 23);
            outputPathTextBox.TabIndex = 3;
            // 
            // playlistIdLabel
            // 
            playlistIdLabel.AutoSize = true;
            playlistIdLabel.Location = new System.Drawing.Point(12, 39);
            playlistIdLabel.Name = "playlistIdLabel";
            playlistIdLabel.Size = new System.Drawing.Size(84, 15);
            playlistIdLabel.TabIndex = 4;
            playlistIdLabel.Text = "Playlist URL(s):";
            // 
            // outputPathLabel
            // 
            outputPathLabel.AutoSize = true;
            outputPathLabel.Location = new System.Drawing.Point(12, 135);
            outputPathLabel.Name = "outputPathLabel";
            outputPathLabel.Size = new System.Drawing.Size(75, 15);
            outputPathLabel.TabIndex = 5;
            outputPathLabel.Text = "Output Path:";
            // 
            // radioPlaylist
            // 
            radioPlaylist.AutoSize = true;
            radioPlaylist.Checked = true;
            radioPlaylist.Location = new System.Drawing.Point(12, 12);
            radioPlaylist.Name = "radioPlaylist";
            radioPlaylist.Size = new System.Drawing.Size(62, 19);
            radioPlaylist.TabIndex = 6;
            radioPlaylist.TabStop = true;
            radioPlaylist.Text = "Playlist";
            radioPlaylist.UseVisualStyleBackColor = true;
            radioPlaylist.CheckedChanged += radioPlaylist_CheckedChanged;
            // 
            // radioVideo
            // 
            radioVideo.AutoSize = true;
            radioVideo.Location = new System.Drawing.Point(81, 12);
            radioVideo.Name = "radioVideo";
            radioVideo.Size = new System.Drawing.Size(55, 19);
            radioVideo.TabIndex = 7;
            radioVideo.TabStop = true;
            radioVideo.Text = "Video";
            radioVideo.UseVisualStyleBackColor = true;
            radioVideo.CheckedChanged += radioVideo_CheckedChanged;
            // 
            // videoIdTextBox
            // 
            videoIdTextBox.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            videoIdTextBox.Location = new System.Drawing.Point(12, 57);
            videoIdTextBox.Multiline = true;
            videoIdTextBox.Name = "videoIdTextBox";
            videoIdTextBox.ScrollBars = ScrollBars.Vertical;
            videoIdTextBox.Size = new System.Drawing.Size(567, 75);
            videoIdTextBox.TabIndex = 8;
            // 
            // videoIdLabel
            // 
            videoIdLabel.AutoSize = true;
            videoIdLabel.Location = new System.Drawing.Point(12, 39);
            videoIdLabel.Name = "videoIdLabel";
            videoIdLabel.Size = new System.Drawing.Size(77, 15);
            videoIdLabel.TabIndex = 4;
            videoIdLabel.Text = "Video URL(s):";
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            btnBrowse.Location = new System.Drawing.Point(462, 153);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new System.Drawing.Size(117, 23);
            btnBrowse.TabIndex = 10;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(595, 461);
            Controls.Add(radioVideo);
            Controls.Add(radioPlaylist);
            Controls.Add(outputPathLabel);
            Controls.Add(playlistIdLabel);
            Controls.Add(outputPathTextBox);
            Controls.Add(playlistIdTextBox);
            Controls.Add(videoIdTextBox);
            Controls.Add(videoIdLabel);
            Controls.Add(logTextBox);
            Controls.Add(btnDownload);
            Controls.Add(btnCancel);
            Controls.Add(btnBrowse);
            FormBorderStyle = FormBorderStyle.FixedSingle; // Set form border style to FixedSingle
            MaximizeBox = false; // Disable maximize box
            Name = "Form1";
            Text = "YouTube Subtitle Downloader";
            ResumeLayout(false);
            PerformLayout();
        }
        private void radioPlaylist_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPlaylist.Checked)
            {
                // Show playlist ID textbox and hide video ID textbox
                playlistIdLabel.Visible = true;
                playlistIdTextBox.Visible = true;
                videoIdTextBox.Visible = false;
                videoIdLabel.Visible = false;
            }
        }

        private void radioVideo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioVideo.Checked)
            {
                // Show video ID textbox and hide playlist ID textbox
                playlistIdLabel.Visible = false;
                playlistIdTextBox.Visible = false;
                videoIdTextBox.Visible = true;
                videoIdLabel.Visible = true;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                outputPathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        //private void btnDownload_Click(object sender, EventArgs e)
        //{
        //    // Download logic here
        //}

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    // Cancel logic here
        //}
    }
}
