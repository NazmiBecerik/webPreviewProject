using Basler.Pylon;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CameraCLProject.Core
{
    public static class Utilities
    {
    
        public static void KumasBilgisiLogJsonFile(string savePath,string partiNo,string articleKod,string articleTanim,string baseKod,decimal gramaj,decimal en,string cariUnvan)
        {

            JObject kumasBilgisi = new JObject(
                new JProperty("PartiNo",partiNo),
                new JProperty("ArticleKod", articleKod),
                new JProperty("ArticleTanim", articleTanim),
                new JProperty("BaseKod", baseKod),
                new JProperty("Gramaj", gramaj),
                new JProperty("En", en),
                new JProperty("CariUnvan", cariUnvan));

          File.WriteAllText(savePath+"/kumasBilgisi.json", kumasBilgisi.ToString());

            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(savePath + "/kumasBilgisi.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                kumasBilgisi.WriteTo(writer);
            }

        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);

            return bytes;
        }

        public static Bitmap[] UnlockBits(Bitmap[] bitmaps)
        {
            for (int i = 0; i < bitmaps.Length; i++)
            {
                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, bitmaps[i].Width, bitmaps[i].Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bitmaps[i].LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bitmaps[i].PixelFormat);

                // Get the address of the first line.
                IntPtr ptr = bmpData.Scan0;

                // Declare an array to hold the bytes of the bitmap.
               int bytes = Math.Abs(bmpData.Stride) * bitmaps[i].Height;
                byte[] rgbValues = new byte[bytes];

                // Copy the RGB values into the array.
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                // Set every third value to 255. A 24bpp bitmap will look red.  
                for (int counter = 2; counter < rgbValues.Length; counter += 3)
                   rgbValues[counter] = 255;

                // Copy the RGB values back to the bitmap
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

                // Unlock the bits.
                bitmaps[i].UnlockBits(bmpData);
            }

            return bitmaps;
        }

        public static Bitmap Crop( Bitmap source, Rectangle section)
        {
            var bitmap = new Bitmap(section.Width, section.Height);
            using (var graphic = Graphics.FromImage(bitmap))
            {
                graphic.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);
                return bitmap;
            }
        }

        public static Bitmap PhoteResize2(IGrabResult grabResult,  Bitmap _bitmap,  Rectangle _section,string dtNow )
        {
            try
            {
                PixelDataConverter converter = new PixelDataConverter();
                _bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                BitmapData bmpData = _bitmap.LockBits(new Rectangle(0, 0, grabResult.Width, grabResult.Height), ImageLockMode.ReadWrite, _bitmap.PixelFormat);
                converter.OutputPixelFormat = PixelType.BGRA8packed;
                IntPtr ptrBmp = bmpData.Scan0;
                converter.Convert(ptrBmp, bmpData.Stride * grabResult.Height, grabResult);
                _bitmap.UnlockBits(bmpData);
                _section = new Rectangle(new Point(0, 0), new Size(1920, 1200));
                return Crop(_bitmap, _section);
            }
            catch (Exception ex)
            {
                _bitmap = null;
                //string logTexts = File.ReadAllText(@"C:\Log Kayıtları\Camera.txt");
                // File.WriteAllText(@"C:\Log Kayıtları\Camera.txt", logTexts + "\n" + ex.Message + " " + dtNow);
                GlobalExceptionLogger(ex, dtNow);
                return _bitmap;
                //Console.Error.WriteLine(ex.Message);
            

                //throw ;
            }
    
        }

        public static void GlobalExceptionLogger(Exception ex,string dtNow)
        {
            string logTexts = File.ReadAllText(@"C:\Log Kayıtları\Camera.txt");
            File.WriteAllText(@"C:\Log Kayıtları\Camera.txt", logTexts + "\n" + ex.Message + " " + dtNow);
        }

        public static Bitmap PhoteResize(IGrabResult grabResult, Bitmap _bitmap)
        {
            Rectangle _section;
            PixelDataConverter converter = new PixelDataConverter();
            _bitmap = new Bitmap(1920, 1200, PixelFormat.Format32bppRgb);
            BitmapData bmpData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, _bitmap.PixelFormat);
            converter.OutputPixelFormat = PixelType.BGRA8packed;
            IntPtr ptrBmp = bmpData.Scan0;
            
            if(grabResult.Container == null)
            {
            }
            else
            {

                converter.Convert(ptrBmp, bmpData.Stride * _bitmap.Height, grabResult);
                _bitmap.UnlockBits(bmpData);
                _section = new Rectangle(new Point(0, 0), new Size(1920, 1200));
            }


            return _bitmap;



        }


        public static Bitmap PhoteResize4(IGrabResult grabResult, Bitmap _bitmap)
        {
           
           
            
                PixelDataConverter converter = new PixelDataConverter();
                _bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                BitmapData bmpData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, _bitmap.PixelFormat);
                converter.OutputPixelFormat = PixelType.BGRA8packed;
                IntPtr ptrBmp = bmpData.Scan0;
                converter.Convert(ptrBmp, bmpData.Stride * _bitmap.Height, grabResult);
                _bitmap.UnlockBits(bmpData);
                return _bitmap;
            
          
        }

        public static Bitmap PhoteResize3(IGrabResult grabResult, Bitmap bitmap,  Rectangle section)
        {
            PixelDataConverter converter = new PixelDataConverter();
            bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            converter.OutputPixelFormat = PixelType.BGRA8packed;
            IntPtr ptrBmp = bmpData.Scan0;
            converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult);
            bitmap.UnlockBits(bmpData);
            section = new Rectangle(new Point(0, 0), new Size(1920, 1200));
            return bitmap;
        }

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static string GetDateTime()
        {
            Regex r = null;
            DateTime dateTime = DateTime.Now;
            int milisecond = dateTime.Millisecond;
            string dateTimeNow = dateTime.ToString();
            string dateTimeNowWithMs = dateTimeNow + milisecond;
            string filePath = dateTimeNowWithMs.Replace(" ", "");
            filePath = filePath.Replace(":", ".");
        //    string filePath = r.Replace(dateTimeNowWithMs, ".");
            return filePath;
        }
    }
}
