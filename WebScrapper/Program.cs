using WebScrapper;
using System.Text.Json;
using System.Net;
using System.Windows;


Console.OutputEncoding = System.Text.Encoding.UTF8;
Downloader downloader;

void multiSession()
{
    Console.WriteLine(". Nhập lần lượt từng link\n" +
        "Nhấn enter 2 lần để thoát");
    List<string> urlList = new List<string>();
    bool isNext = true;
    while(isNext)
    {
        Console.Write("-");
        string url = Console.ReadLine();
        if (url == "")
        {
            isNext = false;
            continue;
        }
        urlList.Add(url);
    }
    foreach(string url in urlList)
    {
        if(url.ToLower().Contains("tiktok"))
        {
            downloader = new TiktokDownloader(url);
        } 
        else if(url.ToLower().Contains("douyin"))
        {
            downloader = new DouyinDownloader(url);
        }
    }
}

void singleSession()
{
    Console.WriteLine(". Lựa chọn nền tảng\n" +
    "1. Tiktok\n" +
    "2. Douyin");
    switch (Console.ReadLine())
    {
        case "1":
            downloader = new TiktokDownloader();
            break;
        case "2":
            downloader = new DouyinDownloader();
            break;
    }
}

bool isNext = true;
while(isNext)
{
    Console.WriteLine("Chọn cách thức nhập:\n" +
        "1. Nhập nhiều link\n" +
        "2. Nhập từng link\n" +
        "3. Nhập bất kỳ để thoát");
    switch(Console.ReadKey().Key)
    {
        case ConsoleKey.D1:
            multiSession();
            break;
        case ConsoleKey.D2:
            singleSession();
            break;
        default:
            isNext = false;
            break;
    }
    Console.WriteLine("Nhấn phím bất kì để tiếp tục");
    Console.ReadKey();
    Console.Clear();
}
