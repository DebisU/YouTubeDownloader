using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoutubeExtractor;
using YouTubeDownloader;
using YouTubeDownloader.Infrastructure;
using Downloader = YouTubeDownloader.Infrastructure.Downloader;
using YouTubeDownloader = YouTubeDownloader.Infrastructure;

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
        private VideoInfo VideoToDownload;
        private IDownloader Downloader;

        public MainWindow()
        {
            InitializeComponent();
            YoutubeUrl = String.Empty;
            VideoWithQualities = new List<VideoInfo>();
            Downloader = new Downloader();
        }

        /// TODO: Upgrade this method to be Async and check the actual combo box items to readd them.
        private void PopulateQuialitySelectorComboBox()
        {
            YoutubeUrl = tbYoutubeUrlToDownload.Text;
            VideoWithQualities = Downloader.RetrieveDownloadOptions(this.YoutubeUrl).Result.ToList();
            foreach (var actualItem in VideoWithQualities)
            {
                CbVideoQualities.Items.Add(actualItem);
            }
        }

        /// TODO: Make this method Async.
        private async void DownloadLink()
        {
            IDownloader downloader = new Downloader();
            Downloader.RetrieveDownloadOptions(YoutubeUrl);
            VideoInfo selectedVideo = Downloader.RetrieveDownloadOptions(YoutubeUrl).Result.ToList()[SelectedVideoIndex];
            downloader.DownloadAsync(selectedVideo);
            //downloader.Download(selectedVideo);
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
                //PopulateQuialitySelectorComboBox();
            }
        }

        private void btDownloadUrl_Click(object sender, RoutedEventArgs e)
        {
            PopulateQuialitySelectorComboBox();
        }
    }
}
