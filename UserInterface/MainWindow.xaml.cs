using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YoutubeExtractor;
using YouTubeDownloader.Infrastructure;
using Downloader = YoutubeExtractor.Downloader;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// TODO: IMPLEMENT PROGRESS BAR.
    /// </summary>
    public partial class MainWindow : Window
    {
        private int SelectedVideoIndex;
        private string YoutubeUrl;
        private List<VideoInfo> VideoWithQualities;
        private SortedDictionary<VideoInfo, ProgressBar> VideosInListView;
        private IDownloader Downloader;

        public MainWindow()
        {
            InitializeComponent();
            YoutubeUrl = String.Empty;
            VideoWithQualities = new List<VideoInfo>();
            //VideosInListView = new SortedDictionary<VideoInfo, ProgressBar>();
            Downloader = new MyYoutubeDownloader();
        }

        private async void PopulateQuialitySelectorComboBox()
        {
            YoutubeUrl = tbYoutubeUrlToDownload.Text;

            CbVideoQualities.Items.Add("LOADING...");
            CbVideoQualities.SelectedIndex = 0;

            VideoWithQualities = await Task.Run(() => Downloader.RetrieveDownloadOptionsAsync(this.YoutubeUrl).Result.ToList());

            //VideosInListView = await Task.Run(() => Downloader.RetrieveDownloadOptionsAsync(this.YoutubeUrl).Result.ToList());

            CbVideoQualities.Items.Clear();
            CbVideoQualities.SelectedValue = null;

            foreach (var actualItem in VideoWithQualities)
            {
                CbVideoQualities.Items.Add(actualItem);
                //VideosInListView.Add(actualItem, new ProgressBar());
            }
        }

        private async void DownloadLink()
        {
            IDownloader downloader = new MyYoutubeDownloader();
            VideoInfo selectedVideo = await Task.Run(() => Downloader.RetrieveDownloadOptionsAsync(YoutubeUrl).Result.ToList()[SelectedVideoIndex]);
            //await Task.Run((() => downloader.DownloadAsync(selectedVideo)));
            downloader.DownloadAsync(selectedVideo);
        }

        private void btDownloadUrl_Copy_Click(object sender, RoutedEventArgs e)
        {
            SelectedVideoIndex = CbVideoQualities.SelectedIndex;
            LvDownloads.Items.Add(VideoWithQualities[SelectedVideoIndex]);
            //LvDownloads.Items.Add(VideosInListView.ElementAt(SelectedVideoIndex)); TODO: CHANGE COLLECTION TYPE OR CREATE A PERSONALIZATED OBJECT.
            DownloadLink();
        }

        private void btDownloadUrl_Click(object sender, RoutedEventArgs e)
        {
            PopulateQuialitySelectorComboBox();
        }

        /// TODO: perform a good condition to youtube Url via RegEx.
        private void tbYoutubeUrlToDownload_GotFocus(object sender, RoutedEventArgs e)
        {
            string actualClipboardText = Clipboard.GetText();
            if (actualClipboardText.Contains("youtube"))
            {
                tbYoutubeUrlToDownload.Text = actualClipboardText;
                PopulateQuialitySelectorComboBox();
            }
        }

        private void MenuItem_SavePath_Click(object sender, RoutedEventArgs e)
        {

            }

        private void MenuItem_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
