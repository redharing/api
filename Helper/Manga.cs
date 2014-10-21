using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class Manga
    {
        //private const string url = "http://www.niceoppai.net/naruto/696/?all";
        private const string url = "http://www.niceoppai.net/onepiece/735/?all";

        
        public IList<string> GetImageByManga(string Name)
        {
            IList<string> images = new List<string>();
            var html = getHtml();

            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(html);

            var centerDom = htmlDoc.DocumentNode.SelectNodes("//center");

            var k = htmlDoc.DocumentNode.Descendants("div").Where(d =>
                        d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("wpm_pag")
                    ).ToList();


            var imageDom = k[0].SelectNodes("//img");
            foreach (var image in imageDom)
            {
                images.Add(image.Attributes["src"].Value);
            }

            //var image = 
            return images;
        }

        public string getHtml()
        {
            WebClient c = new WebClient();
            c.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var hostFile = new Dictionary<string, string>();
            string result = string.Empty;
            try
            {
                result = c.DownloadString(new Uri(url));

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

    }
}
