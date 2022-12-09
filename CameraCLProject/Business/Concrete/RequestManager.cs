using CameraCLProject.Business.Abstract;
using CameraCLProject.Core;
using CameraCLProject.DataAccess.Concrete;
using CameraCLProject.Entities.Concrete;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Business.Concrete
{
    public class RequestManager : IRequestService
    {
        public static int errorCounter = 0;
        public static bool ImageCheckOnStart = false;
        public static Bitmap compareBitmap1;
        public static string _fabricId;
        public RequestManager()
        {

        }
    
        public int SendRequest(byte[] image, string cameraId, string fabricId)
        {
            _fabricId = fabricId;
            JsonSerializer serializer = new JsonSerializer();
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:5000/predict");
            httpWebRequest.Proxy = null;
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    camera_id = cameraId,
                    kumas_id = fabricId,
                    timestamp = 1000
                });

                //var bjson = Encoding.UTF8.GetBytes(json);
                var bjson = Encoding.UTF8.GetBytes(json);
                string split = "split";
                var splitByte = Encoding.UTF8.GetBytes(split);
                byte[] result1 = new byte[bjson.Length + splitByte.Length];
                byte[] result2 = new byte[bjson.Length + splitByte.Length + image.Length];
                result1 = Core.Utilities.Combine(bjson, splitByte);
                result2 = Core.Utilities.Combine(result1, image);
                
                using (Stream stream1 = httpWebRequest.GetRequestStream())
                {
                   
                    stream1.Write(result2, 0, result2.Length);

                    stream1.Close();
                }
                using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    
                    using (Stream stream2 = httpResponse.GetResponseStream())
                    {
                        var sonuc = (new StreamReader(stream2)).ReadToEnd();
                        Result result = JsonConvert.DeserializeObject<Result>(sonuc);
                        if (result.ErrorCount == 0)
                        {
                            ImageCheckOnStart = true;
                        }
                        if (result.ErrorCount > 0)
                        {                           
                            errorCounter++;
                        }
                        return result.ErrorCount;
                    }
                }
            }
        }
    }
}
