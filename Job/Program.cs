using System;
using System.Collections.Generic;
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

            using (var ctx = new Job.mangaEntities())
            {
                int webType = 0;
                var mangaAll = (from all in ctx.Mangas
                               where all.Id > 96
                               select all).ToList();
                IList<MangaImage> mangaImage = new List<MangaImage>();
                foreach (var m in mangaAll)
                {
                    var manganame = m.Name;
                    int end = 100;
                    var helper = new Helper.Manga();
                    Console.WriteLine(m.Name);
                    for (int i = 0; i < end; i++)
                    {
                        var images = helper.GetImageByManga(manganame, end - i, webType);
                        for (int j = 0; j < images.Count; j++)
                        {
                            var result = new MangaImage()
                            {
                                MangaId = m.Id,
                                Chapter = end - i,
                                Page = j + 1,
                                ImagePath = images[j]
                            };
                            mangaImage.Add(result);
                        }
                    }
                    ctx.MangaImages.AddRange(mangaImage);
                    ctx.SaveChanges();
                    mangaImage.Clear();
                }
                
            }
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
    }
}
