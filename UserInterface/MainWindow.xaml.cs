using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using YoutubeExtractor;
using YouTubeDownloader.Infrastructure;
using Downloader = YoutubeExtractor.Downloader;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int SelectedVideoIndex;
        private string YoutubeUrl;
        private List<VideoInfo> VideoWithQualities;
        private IDownloader Downloader;

        public MainWindow()
        {
            InitializeComponent();
            YoutubeUrl = String.Empty;
            VideoWithQualities = new List<VideoInfo>();
            Downloader = new MyYoutubeDownloader();
        }

        /// TODO: Upgrade this method to be Async and check the actual combo box items to readd them.
        private async void PopulateQuialitySelectorComboBox()
        {
            YoutubeUrl = tbYoutubeUrlToDownload.Text;

            CbVideoQualities.Items.Add("LOADING...");
            CbVideoQualities.SelectedIndex = 0;

            VideoWithQualities = await Task.Run(() => Downloader.RetrieveDownloadOptionsAsync(this.YoutubeUrl).Result.ToList());

            CbVideoQualities.Items.Clear();
            CbVideoQualities.SelectedValue = null;

            foreach (var actualItem in VideoWithQualities)
            {
                CbVideoQualities.Items.Add(actualItem);
            }
        }

        /// TODO: Make this method Async.
        private async void DownloadLink()
        {
            IDownloader downloader = new MyYoutubeDownloader();
            VideoInfo selectedVideo = await Task.Run(() => Downloader.RetrieveDownloadOptionsAsync(YoutubeUrl).Result.ToList()[SelectedVideoIndex]);
            downloader.DownloadAsync(selectedVideo);
        }

        private void btDownloadUrl_Copy_Click(object sender, RoutedEventArgs e)
        {
            SelectedVideoIndex = CbVideoQualities.SelectedIndex;
            LvDownloads.Items.Add(VideoWithQualities[SelectedVideoIndex]);
            DownloadLink();
        }

        /// TODO: perform a good condition to youtube Url via RegEx.
        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            string actualClipboardText = Clipboard.GetText();
            if (actualClipboardText.Contains("youtube"))
            {
                tbYoutubeUrlToDownload.Text = actualClipboardText;
                PopulateQuialitySelectorComboBox();
            }
        }

        private void btDownloadUrl_Click(object sender, RoutedEventArgs e)
        {
            PopulateQuialitySelectorComboBox();
        }
    }
}
