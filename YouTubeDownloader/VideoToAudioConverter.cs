using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;

namespace YouTubeDownloader
{
    public class VideoToAudioConverter
    {
        private MediaFile InputFile;
        private MediaFile OutputFile;

        public VideoToAudioConverter(string videoPath, string fileName, string audioExtensionToConvert)
        {
            InputFile = new MediaFile { Filename = videoPath + @"\" + fileName + audioExtensionToConvert };
            OutputFile = new MediaFile { Filename = videoPath + @"\" + fileName + ".mp3" };
        }

        public void ConvertVideoToAudioFile()
        {
            try
            {
                using (var engine = new Engine())
                {
                    engine.Convert(InputFile, OutputFile);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
