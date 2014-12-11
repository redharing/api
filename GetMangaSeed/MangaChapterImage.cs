using MangaModel;
using MangaModel.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMangaSeed
{
    public class MangaChapterImage
    {
        public string Url { get; set; }
        public int MangaId { get; set; }
        public string Chapter { get; set; }
        public List<MangaSeedImage> Images { get; set; }
        public MangaChapterImage(string url)
        {
            Url = url;
        }

        public void GetChapterDetail(int mangaId, string chapter)
        {
            string getChapterUrl = Url + "getcartoondetail/" + chapter + "/" + mangaId;
            var result = Helper.GetJson(getChapterUrl);
            var allChapter = JsonConvert.DeserializeObject<SeedImage>(result);
            int i = 1;
            foreach (var imageUrl in allChapter.result)
            {
                if (Images == null)
                {
                    Images = new List<MangaSeedImage>();
                }

                Images.Add(new MangaSeedImage()
                {
                    Chapter = chapter,
                    ImagePath = imageUrl.img,
                    MangaId = mangaId,
                    Page = i

                });
                i++;
            }
        }

        public bool SaveImage()
        {
            using (var db = new mangaEntities())
            {
                db.MangaSeedImages.AddRange(Images);
                return db.SaveChanges() > 0;
            }
        }

    }
}
