using System.Collections.Generic;

namespace YouTubeDownloader
{
    public interface IQualitySelector
    {
        IEnumerable<string> RetrieveDownloadOptionsAsString(string youtubeUrl);
    }
}
