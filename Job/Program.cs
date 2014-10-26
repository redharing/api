using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    class Program
    {
        static IList<Manga> ms = new List<Manga>();
        static void Main(string[] args)
        {
            
            //for (int i = 3; i <= 21; i++)
            //{
            //    Console.WriteLine(i);
            //    getManga(i);

            //}
            //using( var ctx = new Job.mangaEntities())
            //{
            //    ctx.Mangas.AddRange(ms);
            //    ctx.SaveChanges();
            //}

            using (var ctx = new Model.mangaEntities())
            {
                int webType = 0;
                var mangaAll = (from all in ctx.Manga
                                where all.Id > 92
                                orderby all.Id
                                select all).ToList();
                IList<MangaImage> mangaImage = new List<MangaImage>();
                IList<MangaChapter> mangachapter = new List<MangaChapter>();
                foreach (var m in mangaAll)
                {
                    var helper = new Helper.Manga();
                    var manganame = m.Name;
                    var mangaid = m.Id;


                    mangachapter = helper.getChapterByManga(manganame, mangaid);
                    //double end = Convert.ToInt32(helper.getLastestByManga(manganame));
                    Console.WriteLine(m.Name);
                    //if (end == 0)
                    //{
                    //    Console.WriteLine(m.Name + " not found");
                    //}

                    
                    //for (int i = 0; i <= end; i++)
                    //{
                    //    Console.Write("\r{0}/{1}   ", i,end);
                    //    var images = helper.GetImageByManga(manganame, end - i, webType);
                    //    for (int j = 0; j < images.Count; j++)
                    //    {
                    //        var result = new MangaImage()
                    //        {
                    //            MangaId = m.Id,
                    //            Chapter = Convert.ToInt32(end) - i,
                    //            Page = j + 1,
                    //            ImagePath = images[j]
                    //        };
                    //        mangaImage.Add(result);
                    //    }
                    //}
                    ctx.MangaChapter.AddRange(mangachapter);
                    ctx.SaveChanges();
                    Console.WriteLine(mangachapter.Count);
                    mangachapter.Clear();
                }

            }
        }

        static void getlastestManga()
        {
            var helper = new Helper.Manga();

            double result = helper.getLastestByManga("onepiece");


        }

        static void getManga(int page)
        {
            var helper = new Helper.Manga();

            var result = helper.getHtmlManga(page);
            foreach (var item in result)
            {
                var m = new Manga()
                {
                    ImagePath = item.ImagePath,
                    Name = item.Name,
                    NameDisplay = item.NameDisplay,
                    IsEnable = true
                };
                ms.Add(m);
            }
            Console.WriteLine(result.Count);
        }

        public static void Log(string logMessage)
        {
            if (!string.IsNullOrEmpty(logMessage.Trim()))
            {
                var logFilePath = File.AppendText("c:\\job\\log.txt");
                logFilePath.AutoFlush = true;
                logFilePath.Write("\r\nLog Entry : ");
                logFilePath.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                logFilePath.WriteLine("  :{0}", logMessage);
                logFilePath.WriteLine("-------------------------------");
                logFilePath.Close();
            }
        }
    }
}
