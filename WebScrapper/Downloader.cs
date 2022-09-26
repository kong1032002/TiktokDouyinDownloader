using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper
{
    public abstract class Downloader
    {
        public HttpClient client = new HttpClient();
        public Downloader()
        {
            Console.WriteLine("Bắt đầu");
            Console.Write("Nhập link video/album: ");
            string url = Console.ReadLine();
            Console.WriteLine("Đang gửi request.....");
            try
            {
                var task = getRequest(url);
                task.Wait();
                Console.WriteLine("Đang tạo link tải xuống........");
                createDownloadLink(task.Result);
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        public Downloader(string url)
        {
            Console.WriteLine("Bắt đầu");
            Console.WriteLine("Đang gửi request.....");
            try
            {
                var task = getRequest(url);
                task.Wait();
                Console.WriteLine("Đang tạo link tải xuống........");
                createDownloadLink(task.Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        public abstract string api(string url);
        public abstract void createDownloadLink(string data);
        public async Task downloadFile(string uri, string author, string fileName)
        {
            string path = @"D:\_Pic\Pic\";
            if (!Directory.Exists(path + author))
            {
                DirectoryInfo directory = Directory.CreateDirectory(path + author);
            }
            if (!Directory.Exists(path + author + "\\videos\\")) ;
            {
                DirectoryInfo directory = Directory.CreateDirectory(path + author + "\\videos\\");
            }
            if (!Directory.Exists(path + author + "\\pics\\")) ;
            {
                DirectoryInfo directory = Directory.CreateDirectory(path + author + "\\pics\\");
            }
            if (!Directory.Exists(path + author + "\\gifs\\")) ;
            {
                DirectoryInfo directory = Directory.CreateDirectory(path + author + "\\gifs\\");
            }
            Stream stream = await client.GetStreamAsync(uri);
            string filePath = @"D:\_Pic\Pic\" + author + "\\" + fileName;
            if (!File.Exists(filePath))
            {
                FileStream file = new FileStream(filePath, FileMode.Create);
                await stream.CopyToAsync(file);
            }
            else
            {
                Console.WriteLine("-------Đã tồn tại tập tin {0}", filePath);
            }
        }
        public async Task<string> getRequest(string url)
        {
            Console.WriteLine("Đang tải dữ liệu");
            string acceptLanguage = "vi,en-US;q=0.9,en;q=0.8";
            string accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 Edg/105.0.1343.33";
            string responseString;
            try
            {
                HttpRequestMessage request;
                HttpResponseMessage response;
                client = new HttpClient();
                request = new HttpRequestMessage(HttpMethod.Get, api(url));
                request.Headers.Add("accept", accept);
                request.Headers.Add("user-agent", userAgent);
                request.Headers.Add("accept-language", acceptLanguage);
                request.Headers.Add("cookie", "uid_tt_ss=e5a635763a8ab696d88bf38c158c45b0ffb5dad5bc5dde6c6a2bc4ac9d78e43d; sessionid_ss=44389ca7fbfcde450ff9547134bc8e76; tta_attr_id=0.1649260333.7083519194049822721; tta_attr_id_mirror=0.1649260333.7083519194049822721; _gid=GA1.2.2002006941.1649260333; _gat_UA-143770054-3=1; uid_tt=e5a635763a8ab696d88bf38c158c45b0ffb5dad5bc5dde6c6a2bc4ac9d78e43d; sid_tt=44389ca7fbfcde450ff9547134bc8e76; sessionid=44389ca7fbfcde450ff9547134bc8e76; xgplayer_user_id=679811474284; xgplayer_device_id=56814406663; _gac_UA-143770054-3=1.1656210802.EAIaIQobChMIq-HA9onK-AIV0AtcCh13jgD8EAEYASAAEgKaYvD_BwE; tta_attr_ga_cid=94461731.1649260333; MONITOR_WEB_ID=34869c1f-ba26-4c30-8f90-c96898e85014; MONITOR_DEVICE_ID=d659ab97-2693-4c10-a13c-aef71fd5fb68; sid_guard=44389ca7fbfcde450ff9547134bc8e76%7C1662651854%7C5184000%7CMon%2C+07-Nov-2022+15%3A44%3A14+GMT; sid_ucp_v1=1.0.0-KDdmOGM0MzA3MGFmNjFmZDVjMDc0MzEyNTFjOGNhODI0Y2Q3MTAyNmQKIAiCgIa-9OjQgFwQzpvomAYYswsgDDC0jIXgBTgHQPQHEAEaA3NnMSIgNDQzODljYTdmYmZjZGU0NTBmZjk1NDcxMzRiYzhlNzY; ssid_ucp_v1=1.0.0-KDdmOGM0MzA3MGFmNjFmZDVjMDc0MzEyNTFjOGNhODI0Y2Q3MTAyNmQKIAiCgIa-9OjQgFwQzpvomAYYswsgDDC0jIXgBTgHQPQHEAEaA3NnMSIgNDQzODljYTdmYmZjZGU0NTBmZjk1NDcxMzRiYzhlNzY; passport_csrf_token=95ddb008afea4c474882267458eef7ea; passport_csrf_token_default=95ddb008afea4c474882267458eef7ea; _ga=GA1.1.94461731.1649260333; _fbp=fb.1.1662920108170.874903449; _tt_enable_cookie=1; _ga_BZBQ2QHQSP=GS1.1.1662920108.1.1.1662920183.0.0.0; _ttp=2EdKnL9VN3KO8TTrwxxLZ2aRDK0; _tea_utm_cache_3053={%22utm_source%22:%22copy%22%2C%22utm_medium%22:%22android%22%2C%22utm_campaign%22:%22client_share%22}; store-idc=useast2a; store-country-code=vn; store-country-code-src=uid; tt-target-idc=alisg; cmpl_token=AgQQAPOFF-RMpYw_reDP81k7-8b0ElmTf4AOYMVpTg; __tea_cache_tokens_1988={%22_type_%22:%22default%22%2C%22user_unique_id%22:%227036712935419037186%22%2C%22timestamp%22:1663767562921}; tt_csrf_token=gsprvyTu-sSwsyQrIQl0szezNm4nHmb1QsDQ; _abck=AC548F53DE9CCD0B960C1DDD55E0EC97~-1~YAAQ5FFNG+/21zqDAQAAhUWcdwhYcjJgRvZygldc93UlfJoRE7LkX0r2YAyGwsqhCYMPjW1eYRWGIziZmCgedQfWHlCjneCnyJavmJD66Wg2IIR3HkwfbHjiah+W3h5LtH27yz5t5waQrn+dRQbSAa/8tvax/JeiRosyueOgjOVNKmoW8RtQEiu50WCL6bxrVYVquMxZaxsgi6AmUp+OKTEeHxJ2YGMAZbrvCkThmC95NdQfUlJ3ZAcY4cjsVkPhTfldNX+95S7AH6FeTFzyKqxU7DKNQLznjTQd8ctwfLGIpCM5Xsa4rkABj5JdWNf8riuPb4HzhP577vXjrWPfPn/uH+MZ/K3iCWcRLl7AARkiESRogkDW6z+838r5pfiFtfJbYhXipEjQuKs=~-1~-1~-1; ak_bmsc=7078F817F3C569834165F3766DB4D627~000000000000000000000000000000~YAAQ5FFNG/D21zqDAQAAhUWcdxFqVq54oFw8zvq6SLfSb0OT3yPCU1q0hV9+CwXjmiVCV5zGs2rQkUOLMwM9ypt1Sj1Gf6MLll8a6BIg6B9ynY0jTxJY4nxVa6Q50L+EOps4Br725pZ7ic68/V6Ukf309/rbLG3J5C4tVLxVXob6FoIFFdNEiL5fZ2NnTX+Ur+pQ8bjg+FbgKn8D1aJF+8Ux2kCZ0gO0LFwo36FSl3AF/rAF+QXCyO/6ZFIM+111Ss7V0Esqb4kGWHCjVEu7IrVJ2e7/BFXphSUqMsZj7bfxaD6JiihwDLRthinolbwTf8HWqTqIJCUWgEtKWxTTyh8qiHI41QDV/hXZc5qM+sVpYcNFyMK/uTceakzNGWFyJmuHrEoKEQZRtE8=; bm_sz=52437B8F54DBDD949F647C423996B18D~YAAQ5FFNG/L21zqDAQAAhUWcdxFZ/Wm950EvqWfr5U2AHjlU6TgPOTizrZ1O330+hl19s+s8clmZ/5dCA5Cc5DrN7x2pMNLuaDH/+JlXsuX0nTrIC2vWmarh6qPzqpb+Lk7mJ14pzyCq1C1LNlrXsO2yKYAWkk19Eh18Z5uVqY8gxbD8Iv/KIRmQ1JE6Whu4NSwUNTwN5hH8Bl4jFeVGS37yoPH7ghsRS8XPmARfZ7KnV3N6xj0c9WPmBaCrk5LU0BgLvkS7zQGvVUlnZya0IDKBU3GGyr04BGvT1ZTWs0tI9Go=~4599863~3485752; passport_fe_beating_status=true; odin_tt=3f9ef2caad4ba8fd8c66a5003a2d03c8f9110aa7d468727ba7991e116aaad0add6735fa0fc2673983bb40fcdb7778990ae350c7e2a6f2a600296dc716869837c15a52a5f9b63d33d38b3cab34f32f745; ttwid=1%7Cx5m802QeUgykMKSKiXjg0wGUSb7dmr-YTugooHubOiU%7C1664159076%7Cf5b3bd333283ea88594e66169e192ef89fbcd2232ac2bd456bbf5caae1c03dc3; x-jupiter-uuid=16641592684537260; bm_sv=8E5D661CD42A286418ED6F53EF7E626F~YAAQxVFNG09NWUyDAQAADFWidxGXlMoisGiPRXbLud3RfMkvYi9Wi3mqJtq4R+GSSWBA2XNRNE8juT8E6LWJaOWzAiuUUm2uWxO6P/MXUe9gUlUfQn+xfsmddektG9/xHy0Jd3NjgFhz7LECQIB97+pmPUItByTEoOD1wJPUzhonnrO0DcGd1D0Z86mNK5WgDU/MBzmCw5FhfVlEr5airqWuUDV2RL6tGUz0XxjH5EM4YuZdLMRooOWe0bKkqTDnmQ==~1; msToken=hWIpvobgdIb_SOC03PnaQEQ0M6M5D0-2_amY00Z1YdoDyfd8gd-MwtnuldzU-WZEwH5CnEE_CmV4CX0XY3BpA1kNx_8rGLTf-eb2sBI60J4wBZFGrxGAnFh5HQybH1q7-ALBybqw; msToken=gU0d_op-LaZino4JpyhBJS-pHxVIMZCsVcdVTetK-41VNeT_Knpj6XWkytvCcXsqEmp1rinVPzgAL-bvFrrp3vXP2_wEL3NGEac8PV9-kDSWldGdmPNhnr_62n4nAtCsvt062M0v");
                response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responseString = await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Không thể gửi đi gói tin");
            }
            Console.WriteLine("Tải dữ liệu thành công");
            return responseString;
        }
    }
}
