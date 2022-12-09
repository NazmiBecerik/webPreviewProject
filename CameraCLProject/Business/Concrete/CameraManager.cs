using Basler.Pylon;
using CameraCLProject.Business.Abstract;
using CameraCLProject.Core;
using CameraCLProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CameraCLProject.Business.Concrete
{
    public class CameraManager : ICameraService
    {
        int ramCacheClear = 0;
        Bitmap[] compareBitmaps = new Bitmap[4];
        public static bool ImageCheckOnStart;
        string Camera11Id = "24150711";
        string Camera09Id = "24150709";
        int errorCount = 0;
        Camera[] cameras = new Camera[2];
        Camera camera09 = new Camera("24150709");
        Camera camera11 = new Camera("24150711");
        public Bitmap bitmap;
        public Rectangle section;
        string gun = DateTime.Now.Day.ToString();
        string ay = DateTime.Now.Month.ToString();
        string yil = DateTime.Now.Year.ToString();
        DateTime now = DateTime.Now;
        IRequestService _requestService;

        public CameraManager(IRequestService requestService)
        {
            var cameraInfoFilter09 = new Dictionary<string, string> {

              {CameraInfoKey.ModelName, "acA1920-40gc"},
              {CameraInfoKey.DeviceType, DeviceType.GigE},
              {CameraInfoKey.SerialNumber,"24150709"},
              {CameraInfoKey.DeviceIpAddress,"169.254.182.85"}
            };
            // camera09 = new Camera(cameraInfoFilter09, CameraSelectionStrategy.FirstFound);

            var cameraInfoFilter11 = new Dictionary<string, string> {

              {CameraInfoKey.ModelName, "acA1920-40gc"},
              {CameraInfoKey.DeviceType, DeviceType.GigE},
              {CameraInfoKey.SerialNumber,"24150711"},
              {CameraInfoKey.DeviceIpAddress,"169.254.184.85"}
            };
            // camera11 = new Camera(cameraInfoFilter11, CameraSelectionStrategy.FirstFound);

            cameras[0] = camera09;
            cameras[1] = camera11;
            _requestService = requestService;
        }
        static bool ThreadCheck = false;
        static bool status = true;
        static bool resume = true;
        public static Bitmap croppedBitmap = null;
        public static Bitmap compareBitmap = null;
        public static bool autoStop = false;

        public void StreamStart(string partiNo, string baseKod)
        {
            try
            {
                bool ImageCheckOnStart = false;
                int compareCount = 0;

                CameraStartConfiguration(cameras);
                cameras[0].StreamGrabber.Start();
                cameras[1].StreamGrabber.Start();
                while (status)
                {



                    for (int i = 0; i < cameras.Length; i++)
                    {

                        string dtNow = Core.Utilities.GetDateTime();
                        if (cameras[i].IsOpen == true)
                        {
                            IGrabResult grabResult = cameras[i].StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);

                            using (grabResult)
                            {
                                if (grabResult.GrabSucceeded)
                                {


                                    if (resume == true)
                                    {

                                        bitmap = Core.Utilities.PhoteResize2(grabResult, bitmap, section, dtNow);
                                        // croppedBitmap = bitmap;
                                        // croppedBitmap = Core.Utilities.Crop(croppedBitmap, new Rectangle(new Point(500, 270), new Size(700, 700)));



                                        //  bitmap = Core.Utilities.PhoteResize3(grabResult, bitmap, section);

                                        if (i == 0)
                                        {
                                            //  compareBitmap = Core.Utilities.Crop(bitmap, new Rectangle(new Point(1700, 0), new Size(220, 1200)));
                                            //  compareBitmap.Save(@"C:\Users\amescam.SUN\Desktop\compare\1.tiff", ImageFormat.Tiff);
                                            //  if (compareCount == 4)
                                            //  {
                                            //      compareCount = 0;
                                            //  }
                                            //  compareBitmaps[compareCount] = compareBitmap;

                                            //  if (compareCount == 3)
                                            //   {
                                            //       ThreadCheck = true;
                                            //        Thread thread = new Thread(() => DifferencePercentage(compareBitmaps));
                                            //       thread.Start();
                                            //  }

                                            //   compareCount++;

                                            // errorCount = _requestService.SendRequest(Core.Utilities.ImageToByte(croppedBitmap), Camera09Id, baseKod);
                                            // if ((RequestManager.ImageCheckOnStart == true) && errorCount > 0)
                                            // {
                                            if (bitmap != null)
                                            {
                                                SavePhoto(partiNo, dtNow, bitmap, Camera09Id);
                                            }


                                            //

                                            // }
                                        }
                                        else
                                        {
                                            // errorCount = _requestService.SendRequest(Core.Utilities.ImageToByte(croppedBitmap), Camera09Id, baseKod);
                                            //  if ((RequestManager.ImageCheckOnStart == true) && errorCount > 0)
                                            //  {
                                            if (bitmap != null)
                                            {
                                                SavePhoto(partiNo, dtNow, bitmap, Camera11Id);
                                            }



                                            //  }


                                        }

                                    }
                                }


                            }
                        }
                        ramCacheClear++;
                        if (ramCacheClear % 50 == 0)
                        {
                            long usedMemory = GC.GetTotalMemory(true);

                        }


                    }

                }
            }
            catch (Exception ex)
            {
                string dateTime = Utilities.GetDateTime();
                Utilities.GlobalExceptionLogger(ex, dateTime);
            }
         





        }




        public void DifferencePercentage(Bitmap[] bitmaps)
        {
            Bitmap bmpClone1 = bitmaps[0];
            Bitmap bmpClon11 = bmpClone1;
            Bitmap bmpClone2 = bitmaps[1];
            Bitmap bmpClone3 = bitmaps[2];
            Bitmap bmpClone4 = bitmaps[3];
            if (ThreadCheck == true)
            {
                if ((bmpClone1.Size != bmpClone2.Size) || (bmpClone2.Size != bmpClone3.Size) || (bmpClone3.Size != bmpClone4.Size))
                {
                    Console.Error.WriteLine("Images are of different sizes");
                    return;
                }

                float diff = 0;
                float diff1 = 0;
                float diff2 = 0;


                for (int y = 0; y < bmpClone1.Height; y++)
                {
                    for (int x = 0; x < bmpClone1.Width; x++)
                    {

                        Color pixel1 = bmpClone1.GetPixel(x, y);
                        Color pixel2 = bmpClone2.GetPixel(x, y);
                        Color pixel3 = bmpClone3.GetPixel(x, y);
                        Color pixel4 = bmpClone4.GetPixel(x, y);

                        diff += Math.Abs(pixel1.R - pixel2.R);
                        diff += Math.Abs(pixel1.G - pixel2.G);
                        diff += Math.Abs(pixel1.B - pixel2.B);

                        diff1 += Math.Abs(pixel2.R - pixel3.R);
                        diff1 += Math.Abs(pixel2.G - pixel3.G);
                        diff1 += Math.Abs(pixel2.B - pixel3.B);

                        diff2 += Math.Abs(pixel3.R - pixel4.R);
                        diff2 += Math.Abs(pixel3.G - pixel4.G);
                        diff2 += Math.Abs(pixel3.B - pixel4.B);
                    }
                }

                float difff = 100 * (diff / 255) / (bmpClon11.Width * bmpClon11.Height * 3);
                float difff1 = 100 * (diff1 / 255) / (bmpClon11.Width * bmpClon11.Height * 3);
                float difff2 = 100 * (diff2 / 255) / (bmpClon11.Width * bmpClon11.Height * 3);

                float ort = (difff + difff1 + difff2) / 3;

                //Console.WriteLine("diff: {0} %", 100 * (diff / 255) / (img1.Width * img1.Height * 3));
                if (ort < 10.0)
                {
                    //img1Clone.Save(@"D:\duranlar\" + j + ".tiff", System.Drawing.Imaging.ImageFormat.Tiff);
                    //  img2Clone.Save(@"D:\duranlar\" + (j+3) + ".tiff", System.Drawing.Imaging.ImageFormat.Tiff);
                    //  img3Clone.Save(@"D:\duranlar\" + (j+2) + ".tiff", System.Drawing.Imaging.ImageFormat.Tiff);
                    //  img4Clone.Save(@"D:\duranlar\" + (j+1) + ".tiff", System.Drawing.Imaging.ImageFormat.Tiff);
                    // Console.WriteLine("Durdu : " + ort);

                    Console.WriteLine("Durdu : " + ort);

                    //    string url = "https://localhost:44399/HomePage.aspx/CallPauseInCL";
                    //WebRequest request = WebRequest.Create("https://localhost:44399/HomePage.aspx/CallPauseInCL");
                    //request.Credentials = CredentialCache.DefaultCredentials;
                    //HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
                    //Console.WriteLine(httpWebResponse.StatusDescription);
                    //Stream dataStream = httpWebResponse.GetResponseStream();
                    //StreamReader reader = new StreamReader(dataStream);
                    //string responseFromServer = reader.ReadToEnd();
                    //Console.WriteLine(responseFromServer);
                    //reader.Close();
                    //dataStream.Close();
                    //httpWebResponse.Close();
                    //throw new Exception("ErrorMSGBegin DURDU ErrorMSGEnd");
                    autoStop = true;


                    HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"http://10.80.3.87/kumasSecim/Webmethods.asmx");
                    //SOAPAction    
                    Req.Headers.Add(@"SOAPAction:http://tempuri.org/CallPauseInCL");
                    //Content_type    
                    Req.ContentType = "text/xml;charset=\"utf-8\"";
                    Req.Accept = "text/xml";
                    //HTTP method    
                    Req.Method = "POST";

                    HttpWebRequest request = Req;

                    XmlDocument SOAPReqBody = new XmlDocument();
                    //SOAP Body Request  
                    SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>  
                    <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-   instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">  
                     <soap:Body>  
                        <CallPauseInCL xmlns=""http://tempuri.org/""/>
                      </soap:Body>  
                    </soap:Envelope>");

                    using (Stream stream = request.GetRequestStream())
                    {
                        SOAPReqBody.Save(stream);
                    }
                    //Geting response from request  
                    using (WebResponse Serviceres = request.GetResponse())
                    {
                        using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                        {
                            //reading stream  
                            var ServiceResult = rd.ReadToEnd();
                            //writting stream result on console  
                            Console.WriteLine(ServiceResult);
                            Console.ReadLine();
                        }
                    }

                    /* var theWebRequest = HttpWebRequest.Create("http://10.80.3.87/kumasSecim/Webmethods.asmx?op=CallPauseInCL");
                     theWebRequest.Method = "POST";
                     theWebRequest.ContentType = "application/json; charset=utf-8";
                     theWebRequest.Headers.Add(HttpRequestHeader.Pragma, "no-cache");

                     using (var writer = theWebRequest.GetRequestStream())
                     {
                         //string send = null;
                         //send = "{\"partiNo\":\"" + RequestManager._fabricId + "\"}";

                         //var data = Encoding.ASCII.GetBytes(send);

                         //writer.Write(data, 0, data.Length);
                     }

                     try
                     {
                         var theWebResponse = (HttpWebResponse)theWebRequest.GetResponse();
                         var theResponseStream = new StreamReader(theWebResponse.GetResponseStream());

                         string result = theResponseStream.ReadToEnd();
                     }
                     catch (Exception)
                     {

                     }*/



                }
            }

        }

        private static void SavePhoto(string partiNo, string dtNow, Image fullImage, string cameraId)
        {
            string path = @"D:/Goruntuler/" + partiNo + "/" + dtNow + "__" + cameraId + ".tiff";

            if (Directory.Exists(@"D:/Goruntuler/" + partiNo))
            {

                //  fullImage.Save(@"D:/Goruntuler/" + partiNo + "/" + dtNow + "__" + cameraId + ".tiff", ImageFormat.Tiff);
                if (path.Contains(cameraId) == true)
                {
                    fullImage.Save(path, ImageFormat.Tiff);
                }
                else
                {
                    File.WriteAllText(@"D:\count.txt", dtNow);
                }
            }
            else
            {
                Directory.CreateDirectory(@"D:/Goruntuler/" + partiNo);


            }
        }

        public void CameraStartConfiguration(Camera[] camera)
        {
            try
            {
                for (int i = 0; i < camera.Length; i++)
                {
                    if (camera[i].IsOpen == false)
                    {

                        camera[i].Open();
                        camera[i].Parameters[PLCamera.UserSetSelector].SetValue(PLCamera.UserSetSelector.UserSet2);
                        camera[i].Parameters[PLCamera.UserSetLoad].Execute();
                        status = true;

                    }
                    else
                    {
                        camera[i].Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void StreamPause()
        {
            resume = false;
        }

        public void StreamResume()
        {
            resume = true;
        }

        public void StreamStop()
        {
            status = false;
            CameraStopConfiguration(cameras);
        }

        public void CameraStopConfiguration(Camera[] camera)
        {

            try
            {
                for (int i = 0; i < camera.Length; i++)
                {
                    camera[i].StreamGrabber.Stop();
                    camera[i].Close();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
