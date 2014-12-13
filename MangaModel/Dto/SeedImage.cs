using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaModel.Dto
{
    public class SeedImage
    {
        public string status { get; set; }
        public List<ImagePath> result { get; set; }
    }

    public class ImagePath
    {
        public string img { get; set; }
    }
}
