using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Helper;

namespace API.Controllers
{
    public class ReaderController : ApiController
    {
        private bool _isValid = true;
        private const string fileVersion = "http://www.niceoppai.net/naruto/696/?all";

        public IList<string> Get()
        {
            if (_isValid)
            {
                var manga = new Manga();
                return manga.GetImageByManga("naruto");
            }
            return null;
        }

        void c_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Result))
            {
                var resultSplit = e.Result.Split('\n');
                if (resultSplit.Count() > 1)
                {
                    
                }
            }
        }
    }
}
