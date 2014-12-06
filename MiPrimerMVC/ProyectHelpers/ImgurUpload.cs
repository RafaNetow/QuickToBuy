using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MiPrimerMVC.ProyectHelpers
{
    public class ImgurUpload
    {
        private const string ClientId = "fccc9c092089aea";

        public static object UploadImage(byte[] imageBytes)
        {
            var w = new WebClient();
            w.Headers.Add("Authorization", "Client-ID " + ClientId);
            var keys = new System.Collections.Specialized.NameValueCollection();
            try
            {
                keys.Add("image", Convert.ToBase64String(imageBytes));
                var responseArray = w.UploadValues("https://api.imgur.com/3/image", keys);
                dynamic result = Encoding.ASCII.GetString(responseArray);
                var reg = new System.Text.RegularExpressions.Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);
                var url = match.ToString().Replace("\"", "").Replace("\\/", "/");
                return url;
            }
            catch (Exception s)
            {
                // MessageBox.Show("Something went wrong. " + s.Message);
                return "Failed!";
            }
        }

    }
}
