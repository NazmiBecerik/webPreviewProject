using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspWebPreviewProject
{
    public partial class ViewPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FileName"] != null && Request.QueryString["FileName"].ToString() != "")
            {
                string fileName = Request.QueryString["FileName"].ToString();

                /*if (!new Regex("^[0-9A-Za-z]*$").IsMatch(fileName))
                {
                    throw new Exception("Security Exception");
                }*/

                byte[] buffer = new byte[1024];

                buffer = File.ReadAllBytes("C:/inetpub/wwwroot/AmesCameraProject/" + fileName);

                Byte[] jpegBytes;

                using (MemoryStream inStream = new MemoryStream(buffer))
                using (MemoryStream outStream = new MemoryStream())
                {
                    System.Drawing.Bitmap.FromStream(inStream).Save(outStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    jpegBytes = outStream.ToArray();
                }

                Response.ContentType = "image/jpeg";
                Response.AddHeader("content-length", jpegBytes.Length.ToString());
                Response.BinaryWrite(jpegBytes);
                Response.End();
            }
            else
            {
                Response.ContentType = "text/plain";
                Response.Write("Aradığınız doküman bulunamadı.");
                Response.End();
            }

        }

    }
}
