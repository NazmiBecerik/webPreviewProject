using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace aspWebPreviewProject
{
    public static class FileExtensionConverter
    {
        static string fileNameWithExtension2;
        public static string ExtensionConverter(string fileNameWithExtension)
        {
            
            string fileName = "";
            if (fileNameWithExtension.Contains(".tiff"))
            {
               var index = fileNameWithExtension.IndexOf(".tiff");
               fileName = fileNameWithExtension.Remove(index);
               string extension = ".png";
               fileNameWithExtension2 = fileName + extension;
               //fileNameWithExtension2 = Path.Combine(fileName, extension);
                Bitmap.FromFile(fileNameWithExtension).Save(fileNameWithExtension2,System.Drawing.Imaging.ImageFormat.Png);
                return fileNameWithExtension2;
            }
            else
            {
                return fileNameWithExtension;
            }
        }
    }
}
