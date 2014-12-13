using MangaModel;
using MangaModel.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetMangaSeed
{
    class Program : IDisposable
    {
        const string url = "http://49.213.18.94/mangaseed/cartoon.php/androidapiv2.0/";
        static void Main(string[] args)
        {
            using (var db = new mangaEntities())
            {
                var allM = db.MangaSeed.Where(m=>m.C_id > 749);
                int i = 1;
                int allManga = allM.Count();
                foreach (var m in allM)
                {
                    var allC = db.MangaSeedChapter.Where(c=>c.mangaid == m.mangaid).OrderBy(c=>c.part);
                    var lastC = allC.ToList().Last().part;
                    foreach (var c in allC)
	                {
                        MangaChapterImage image = new MangaChapterImage(url);

                        image.GetChapterDetail(m.mangaid, c.part);
                        image.SaveImage();
                        Console.WriteLine("Get " + m.name + " Chapter  " + c.part + "/" + lastC);
	                }
                  
                }
                db.SaveChanges();
                Console.WriteLine("Save All success");
            }
        }

        private static List<MangaSeedChapter> GetMangaChapter(int mangaId)
        {
            string getChapterUrl = url + "getcartoonpart/" + mangaId;
            var result = GetJson(getChapterUrl);
            var allChapter = JsonConvert.DeserializeObject<SeedResuls>(result);
            allChapter.result.ForEach(c => c.mangaid = mangaId);
            return allChapter.result;
        }

        private static List<MangaSeedImage> GetChapterDetail(int mangaId, string chapter)
        {
            string getChapterUrl = url + "getcartoondetail/" + chapter + "/" + mangaId;
            var result = GetJson(getChapterUrl);
            var allChapter = JsonConvert.DeserializeObject<SeedImage>(result);
            List<MangaSeedImage> allImage = new List<MangaSeedImage>();
            int i = 1;
            foreach (var imageUrl in allChapter.result)
            {
                allImage.Add(new MangaSeedImage()
                {
                    Chapter = chapter,
                    ImagePath = imageUrl.img,
                    MangaId = mangaId,
                    Page = i

                });
                i++;
            }
            return allImage;
        }

        private static string GetJson(string url)
        {
            WebClient c = new WebClient();
            c.Headers.Add("user-agent", "Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
            string result = string.Empty;
            try
            {
                c.Encoding = Encoding.UTF8;
                result = c.DownloadString(new Uri(url + "?nogzip=1"));
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
