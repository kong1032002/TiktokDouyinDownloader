using System.Text.Json;

namespace WebScrapper
{
    internal class TiktokDownloader : Downloader
    {
        public TiktokDownloader() : base()
        {

        }
        public TiktokDownloader(string url) : base(url) { }
        public override string api(string url)
        {
            string result = url.Replace("video/","");
            if (url.Contains("?"))
            {
                url = url.Substring(0, url.IndexOf("?"));
            }
            result = result.Replace("com/", "com/node/share/video/");
            return result;
        }
        public override void createDownloadLink(string data)
        {
            JsonDocument document = JsonDocument.Parse(data);
            JsonElement element = document.RootElement;
            JsonElement videoInfo = element.GetProperty("itemInfo").GetProperty("itemStruct").GetProperty("video");
            string authorId = element.GetProperty("itemInfo").GetProperty("itemStruct").GetProperty("author").GetProperty("uniqueId").ToString();
            string idVideo = videoInfo.GetProperty("id").ToString(); // id
            string cover = videoInfo.GetProperty("cover").ToString(); //png
            string reflowCover = videoInfo.GetProperty("reflowCover").ToString(); //png
            string originCover = videoInfo.GetProperty("originCover").ToString(); //png
            string dynamicCover = videoInfo.GetProperty("dynamicCover").ToString(); //gif
            string playAddress = videoInfo.GetProperty("playAddr").ToString(); //mp4
            string downloadAddress = videoInfo.GetProperty("downloadAddr").ToString(); //mp4
            Console.WriteLine("Đang tải ảnh thứ 1");
            downloadFile(cover, authorId, "pics\\" + idVideo + "1.png").Wait();
            Console.WriteLine("Đang tải ảnh thứ 2");
            downloadFile(originCover, authorId, "pics\\" + idVideo + "2.png").Wait();
            Console.WriteLine("Đang tải ảnh thứ 3");
            downloadFile(reflowCover, authorId, "pics\\" + idVideo + "3.png").Wait();
            Console.WriteLine("Đang tải gif");
            downloadFile(dynamicCover, authorId, "gifs\\" + idVideo + "1.webp").Wait();
            Console.WriteLine("Đang tải video");
            downloadFile(playAddress, authorId,"videos\\" + idVideo + ".mp4").Wait();
            Console.WriteLine("Hoàn thành");
        }
    }
}
