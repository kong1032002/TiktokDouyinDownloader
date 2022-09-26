using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace WebScrapper
{
    internal class DouyinDownloader : Downloader
    {
        public DouyinDownloader() : base()
        {

        }
        public DouyinDownloader(string url) : base(url)
        {

        }
        public override string api(string url)
        {
            string result = "https://api.douyin.wtf/api?url=";
            result += url;
            return result;
        }

        public override void createDownloadLink(string data)
        {
            Console.WriteLine("Đang tạo lien kết tải xuống!!!!!");
            JsonDocument document = JsonDocument.Parse(data);
            JsonElement rootElement = document.RootElement;
            string urlType = rootElement.GetProperty("url_type").ToString();
            if(urlType == "video")
            {
                string author = rootElement.GetProperty("video_author").ToString();
                string authorId = rootElement.GetProperty("video_author_id").ToString();
                string videoID = rootElement.GetProperty("video_aweme_id").ToString();
                string videoCover = rootElement.GetProperty("video_cover").ToString();
                string originCover = rootElement.GetProperty("video_origin_cover").ToString();
                string dynamicCover = rootElement.GetProperty("video_dynamic_cover").ToString();
                string nwmVideoHD = rootElement.GetProperty("nwm_video_url_1080p").ToString();
                string wmVideo = rootElement.GetProperty("wm_video_url").ToString();
                Console.WriteLine("Bắt đầu tải xuống!!!!!");
                Console.WriteLine("Đang tải ảnh thứ 1");
                downloadFile(videoCover, authorId, "pics\\" + videoID + "1.png").Wait();
                Console.WriteLine("Đang tải ảnh thứ 2");
                downloadFile(originCover, authorId, "pics\\" + videoID + "2.png").Wait();
                Console.WriteLine("Đang tải gif");
                downloadFile(dynamicCover, authorId, "gifs\\" + videoID + "1.webp").Wait();
                Console.WriteLine("Đang tải video");
                downloadFile(nwmVideoHD, authorId, "videos\\" + videoID + ".mp4").Wait();
                Console.WriteLine("Hoàn thành");
            } else if( urlType == "album" )
            {

            }
        }
    }
}
