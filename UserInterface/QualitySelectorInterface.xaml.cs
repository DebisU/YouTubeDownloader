using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for QualitySelectorInterface.xaml
    /// </summary>
    public partial class QualitySelectorInterface : Window
    {
        private string youtubeUrl;

        public QualitySelectorInterface(string youtubeUrl)
        {
            InitializeComponent();
            this.youtubeUrl = youtubeUrl;

            //TODO: se how to perform this call without adding the NuGet package called: Newtonsoft.Json 
            List<string> videoQualities = YouTubeDownloader.UserInterface.RetrieveDownloadOptionsAsString(youtubeUrl).ToList();

        }
        public QualitySelectorInterface()
        {
            InitializeComponent();
        }
    }
}
