using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CustomerService.Models
{
    public class ImageDisplay
    {
        private string DisplayImages(byte[] img , string filename)
        {
            MemoryStream ms = new MemoryStream(img, 0, img.Length);
            ms.Write(img, 0, img.Length);
            Image image = System.Drawing.Image.FromStream(ms, true);
            string savepath = string.Format( "~/Content/images/customerimages/{0}",filename);
            string path = Server.MapPath(savepath);
            image.Save(path);
            return path;
        }
    }
    //https://www.codeproject.com/Articles/196618/C-SQLite-Storing-Images
}