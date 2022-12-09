using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspWebPreviewProject
{
    public partial class ImageTable : System.Web.UI.UserControl
    {
        public string FileName { get; set; }
        public string Meter { get; set; }
        public string TypeOfError { get; set; }
        public string Icon { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            LabelFileName.Text = FileName;
            LabelMeter.Text = Meter;
            LabelTypeOfError.Text = TypeOfError;
            IconImage.ImageUrl = Icon;
        }

        protected void BtnShow_Click(object sender, EventArgs e)
        {
            Response.Write("Clicked");
        }
    }
}