using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeDownloader.Infrastructure
{
    public class DownloadItem : INotifyPropertyChanged
    {
        private double _percent;
        public double Percent
        {
            get { return _percent; }
            set
            {
                if (_percent.Equals(value))
                    return;

                _percent = value;
                OnPropertyChanged("Percent");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
