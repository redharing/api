using HtmlAgilityPack;
using Model;
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
        private const string urlmangaName = "http://www.niceoppai.net/manga_list/all/any/name-az/{0}/";
        private const string url = "http://www.niceoppai.net/{0}/{1}/?all";
        private const string urlThaiCartoon = "http://www.thai-cartoon.com/read-{0}-chapter-{1}.html";

        public IList<string> GetImageByManga(string Name, double chapter, int webType)
        {
            List<string> images = new List<string>();

            var html = getHtml(Name, chapter, webType);
            if (webType == 1)
            {
                return getimages(html);
            }

            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(html);

            var centerDom = htmlDoc.DocumentNode.SelectNodes("//center");

            var k = htmlDoc.DocumentNode.Descendants("div").Where(d =>
                        d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("wpm_pag mng_rdr")
                    ).ToList();


            var imageDom = k[0].Descendants("img");
            if (imageDom != null)
            {
                foreach (var image in imageDom)
                {
                    string imagePath = image.Attributes["src"].Value;
                    if (!images.Exists(x => x == imagePath))
                    {
                        images.Add(imagePath);
                    }
                }
            }
            return images;
        }

        public string getHtml(string mangaName, double chapter, int web)
        {
            WebClient c = new WebClient();
            c.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var hostFile = new Dictionary<string, string>();
            string result = string.Empty;
            try
            {
                if (web == 1)
                {
                    result = c.DownloadString(new Uri(string.Format(urlThaiCartoon, mangaName, chapter)));
                }

                else
                {
                    result = c.DownloadString(new Uri(string.Format(url, mangaName, chapter)));
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        private string getHtmlByUrl(string url)
        {
            WebClient c = new WebClient();
            c.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            string result = string.Empty;
            try
            {
                c.Encoding = Encoding.UTF8;
                result = c.DownloadString(new Uri(url));
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public IList<MangaObject> getHtmlManga(int page)
        {
            IList<MangaObject> mangas = new List<MangaObject>();
            WebClient c = new WebClient();
            c.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            var hostFile = new Dictionary<string, string>();
            string result = string.Empty;
            try
            {
                result = c.DownloadString(new Uri(string.Format(urlmangaName, page)));

                var htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(result);



                var k = htmlDoc.DocumentNode.Descendants("div").Where(d =>
                            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("nde")
                        ).ToList();
                foreach (var c1 in k)
                {
                    var cvr = c1.Descendants("div").Where(d =>
                            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("cvr")
                        ).ToList();
                    var det = c1.Descendants("div").Where(d =>
                            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("det")
                        ).ToList();

                    var imageManga = cvr[0].Descendants("img").ToList()[0].Attributes["src"].Value;
                    var nameManga = det[0].Descendants("a").ToList()[0].Attributes["href"].Value;
                    var displayManga = det[0].Descendants("a").ToList()[0].InnerHtml;

                    int index = nameManga.IndexOf(".net") + ".net".Length + 1;
                    nameManga = nameManga.Substring(index, nameManga.Length - 1 - index);
                    var manga = new MangaObject()
                    {
                        ImagePath = imageManga,
                        Name = nameManga,
                        NameDisplay = displayManga
                    };
                    mangas.Add(manga);
                }


                return mangas;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public IList<string> getimages(string html)
        {
            string text = "%3C%69%6D%67%20%73%72%63%3D";
            IList<string> images = new List<string>();
            for (int index = 0; ; index += text.Length)
            {
                index = html.IndexOf(text, index);
                if (index == -1)
                    return images;
                else
                {
                    var indexJpg = html.IndexOf(".jpg", index);
                    string image = html.Substring(indexJpg - 26, 30);
                    images.Add(image);
                }
            }
        }

        public double getLastestByManga(string MangaName)
        {
            double chapter = -1;
            var domain = "http://www.niceoppai.net/{0}";
            var url = string.Format(domain, MangaName);
            var html = getHtmlByUrl(url);

            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(html);

            var ul = htmlDoc.DocumentNode.Descendants("ul").Where(d =>
                        d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lst")
                    ).FirstOrDefault();
            var li = ul.Descendants("li").Where(d =>
                        d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lng_")
                    ).FirstOrDefault();

            if (li != null)
            {
                var aManga = li.Descendants("a").FirstOrDefault();
                var Mangahref = aManga.Attributes["href"].Value.Trim();
                int l = Mangahref.Split('/').Count();
                string chapterString = Mangahref.Split('/')[l - 2];
                double.TryParse(chapterString, out chapter);
            }

            return chapter;
        }

        public NewReleaseManga getNewReleaseByMangaChapter(Model.Manga Manga)
        {
            double chapter = -1;
            var domain = "http://www.niceoppai.net/{0}";
            var url = string.Format(domain, Manga.Name);
            var html = getHtmlByUrl(url);
            var htmlDoc = new HtmlDocument();
            NewReleaseManga newRelease = null;
            htmlDoc.LoadHtml(html);

            var ul = htmlDoc.DocumentNode.Descendants("ul").Where(d =>
                        d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lst")
                    ).FirstOrDefault();
            var li = ul.Descendants("li").Where(d =>
                        d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lng_")
                    ).FirstOrDefault();

            if (li != null)
            {
                var aManga = li.Descendants("a").FirstOrDefault();
                var Mangahref = aManga.Attributes["href"].Value.Trim();
                var MangaChapterName = aManga.Attributes["title"].Value.Trim();
                int l = Mangahref.Split('/').Count();
                string chapterString = Mangahref.Split('/')[l - 2];
                newRelease = new NewReleaseManga()
                {
                    Chapter = chapterString,
                    ChapterName = MangaChapterName,
                    MangaId = Manga.Id,
                    MangaName = Manga.Name,
                    ModifyDate = DateTime.Now
                };
            }

            return newRelease;
        }

        public IList<MangaChapter> getChapterByManga(string MangaName, int Id)
        {
            int pagemax = 4;
            int chapter = 0;
            IList<MangaChapter> c = new List<MangaChapter>();
            for (var page = 1; page <= pagemax; page++)
            {
                var domain = "http://www.niceoppai.net/{0}/chapter-list/{1}/";
                var url = string.Format(domain, MangaName, page);
                var html = getHtmlByUrl(url);

                var htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(html);

                var ul = htmlDoc.DocumentNode.Descendants("ul").Where(d =>
                            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lst")
                        ).FirstOrDefault();
                var li = ul.Descendants("li").Where(d =>
                            d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("lng_")
                        );

                if (li != null)
                {
                    foreach (var l in li)
                    {
                        var aManga = l.Descendants("a").FirstOrDefault();
                        var MangaChapterName = aManga.Attributes["title"].Value;
                        var Mangahref = aManga.Attributes["href"].Value.Trim();
                        string chapterString = Mangahref.Substring(Mangahref.Length - 4, 3);
                        int.TryParse(chapterString, out chapter);

                        var c1 = new MangaChapter()
                        {
                            MangaId = Id,
                            ChapterId = chapter,
                            ChapterName = MangaChapterName,
                        };
                        c.Add(c1);
                    }

                }
            }
            return c;
        }
    }
}
