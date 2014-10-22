using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job
{
    class Program
    {
        static void Main(string[] args)
        {
            using( var ctx = new Job.mangaEntities())
            {
                var manganame = "baki-son-of-ogre";
                int end = 312;
                var helper = new Helper.Manga();
                IList<MangaImage> mangaImage = new List<MangaImage>();
                for (int i = 0; i < end; i++)
                {
                    var images = helper.GetImageByManga(manganame, end - i);
                    for (int j = 0; j < images.Count; j++)
			        {
                        var result = new MangaImage()
                        {
                            MangaId = 6,
                            Chapter = end - i,
                            Page = j + 1,
                            ImagePath = images[j]
                        };
                        mangaImage.Add(result);
			        }
                }

                ctx.MangaImages.AddRange(mangaImage);
                ctx.SaveChanges();
            }
        }
    }
}
