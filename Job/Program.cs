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
        static Helper.Manga helper = new Helper.Manga();
        static void Main(string[] args)
        {
            var mangaError = new List<Manga>();
            var newReleases = new List<NewReleaseManga>();
            using (var ctx = new Model.mangaEntities())
            {
                var mangaAll = (from all in ctx.Manga

                                select all).ToList();
                int mangaAllCount = mangaAll.Count();
                int i = 1;
                foreach (var m in mangaAll)
                {
                    Console.WriteLine("get html {1} r{0}  /{2}   ", i, m.Name, mangaAllCount);
                    try
                    {
                        var newRelease = helper.getNewReleaseByMangaChapter(m);
                        
                        if (!newReleases.Exists(x => x.MangaId == newRelease.MangaId))
                        {
                            newReleases.Add(newRelease);
                            
                        }
                        if (newRelease != null)
                        {
                            Console.WriteLine("save {1} r{0}  /{2}   ", i, newRelease.MangaName, mangaAllCount);
                            var theLastest = ctx.NewReleaseManga.SingleOrDefault(r => r.MangaId == newRelease.MangaId);
                            if (theLastest != null)
                            {
                                theLastest.ChapterName = newRelease.ChapterName;
                                theLastest.ChapterImagePath = newRelease.ChapterImagePath;
                                theLastest.ModifyDate = newRelease.ModifyDate;
                                theLastest.Chapter = newRelease.Chapter;
                                //var images = getMangaDetail(newRelease);
                                //if (images != null && images.Count > 0)
                                //{
                                //    theLastest.ChapterImagePath = images[0].ImagePath;
                                //    ctx.MangaImage.AddRange(images);
                                //}
                            }
                            else
                            {
                                ctx.NewReleaseManga.Add(newRelease);
                                
                            }
                            var images = helper.GetImageByManga(newRelease.MangaName, Convert.ToDouble(newRelease.Chapter), 0);
                            for (int k = 0; k < images.Count; k++)
                            {
                                var result = new MangaImage()
                                {
                                    MangaId = newRelease.MangaId,
                                    Chapter = Convert.ToInt32(newRelease.Chapter),
                                    Page = k + 1,
                                    ImagePath = images[k]
                                };
                                ctx.MangaImage.Add(result);
                            }
                        }
                        try
                        {
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        mangaError.Add(m);
                    }

                    i++;
                }

                Console.Write("Save All Success");
            }

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

            //using (var ctx = new Model.mangaEntities())
            //{
            //    int webType = 0;
            //    var mangaAll = (from all in ctx.Manga
            //                    where all.Id > 92
            //                    orderby all.Id
            //                    select all).ToList();
            //    IList<MangaImage> mangaImage = new List<MangaImage>();
            //    IList<MangaChapter> mangachapter = new List<MangaChapter>();
            //    foreach (var m in mangaAll)
            //    {
            //        var helper = new Helper.Manga();
            //        var manganame = m.Name;
            //        var mangaid = m.Id;


            //        mangachapter = helper.getChapterByManga(manganame, mangaid);
            //        //double end = Convert.ToInt32(helper.getLastestByManga(manganame));
            //        Console.WriteLine(m.Name);
            //        //if (end == 0)
            //        //{
            //        //    Console.WriteLine(m.Name + " not found");
            //        //}


            //        //for (int i = 0; i <= end; i++)
            //        //{
            //        //    Console.Write("\r{0}/{1}   ", i,end);
            //        //    var images = helper.GetImageByManga(manganame, end - i, webType);
            //        //    for (int j = 0; j < images.Count; j++)
            //        //    {
            //        //        var result = new MangaImage()
            //        //        {
            //        //            MangaId = m.Id,
            //        //            Chapter = Convert.ToInt32(end) - i,
            //        //            Page = j + 1,
            //        //            ImagePath = images[j]
            //        //        };
            //        //        mangaImage.Add(result);
            //        //    }
            //        //}
            //        ctx.MangaChapter.AddRange(mangachapter);
            //        ctx.SaveChanges();
            //        Console.WriteLine(mangachapter.Count);
            //        mangachapter.Clear();
            //    }

            //}
        }

        static void getlastestMangaChapter()
        {
            var helper = new Helper.Manga();

            double result = helper.getLastestByManga("onepiece");


        }

        static void getLastestManga()
        {

        }

        static List<MangaImage> getMangaDetail(NewReleaseManga newRelease)
        {
            List<MangaImage> mangaDetail = new List<MangaImage>();
            try
            {
                var images = helper.GetImageByManga(newRelease.MangaName, Convert.ToDouble(newRelease.Chapter), 0);
                for (int k = 0; k < images.Count; k++)
                {


                    var result = new MangaImage()
                    {
                        MangaId = newRelease.MangaId,
                        Chapter = Convert.ToInt32(newRelease.Chapter),
                        Page = k + 1,
                        ImagePath = images[k]
                    };
                    mangaDetail.Add(result);
                }

            }
            catch (Exception)
            {
                return null;
            }
            return mangaDetail;
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
