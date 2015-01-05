using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetMangaSeed
{
    public class Helper
    {
        public static string GetJson(string url)
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

        public static string GetImage(string url, string filename)
        {
            WebClient c = new WebClient();
            c.Headers.Add("user-agent", "Mozilla/5.0 (Linux; U; Android 4.0.3; ko-kr; LG-L160L Build/IML74K) AppleWebkit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
            string result = string.Empty;
            try
            {
                //c.Encoding = Encoding.UTF8;
                c.DownloadFile(new Uri(url), filename);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
