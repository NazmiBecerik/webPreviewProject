using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;

namespace aspWebPreviewProject
{
    public partial class PreviewPage : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            Button2.Visible = false;
        }
        public static string globalPath = @"C:\inetpub\wwwroot\AmesCameraProject\";
        public static string pathLbox1 = "";
        public static string pathLbox2 = "";
        public static int typeForGet;
        public static int controlIsFirstTime = 0;
        public static int isGetValues = 1;

        //protected void doubleClick(object sender, EventArgs e)
        //{
        //    if (ListBox1.SelectedItem != null)
        //    {
        //        ListBox3.Items.Clear();
        //        ListBox2.Items.Clear();
        //        pathLbox1 = ListBox1.SelectedItem.Text.ToString();
        //        GetSubDirectories();


        //    }
        //}

        protected void ListBox2Click(object sender, EventArgs e)
        {
            typeForGet = 1;
            files = null;
            controlIsFirstTime = 0;
            if (ListBox2.SelectedItem != null)
            {
                bool directoryIsThere = Directory.Exists(@"C:\inetpub\wwwroot\AmesCameraProject\" + ListBox2.SelectedItem.Text + "");
                if (directoryIsThere == true)
                {
                    // GetPartiValues(ListBox2.SelectedItem.Text);
                    DirectoryInfo directoryForFilledTable = new DirectoryInfo(@"C:\inetpub\wwwroot\AmesCameraProject\" + ListBox2.SelectedItem.Text);
                    // files = directoryForFilledTable.GetFiles();
                    FilledTableWithCalendar(globalPath + ListBox2.SelectedItem.Text);
                    Calendar1.Visible = true;
                    ListBox2.Visible = true;
                    date.Visible = true;
                    PartiNumber.Visible = true;
                    Button2.Visible = true;
                    btnIleri.Visible = true;

                    firstIndex = 0;
                    if (firstIndex == 0)
                    {
                        btnGeri.Visible = false;
                    }
                    if (files.Length <= 10)
                    {
                        btnIleri.Visible = false;
                    }

                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Uyarı','Bu Partide Hata Bulunamadı','warning')", true);
                }
            }
        }

        //public void OpenImage()
        //{
        //    string FilePath = "http://localhost/GORUNTULER/" + ListBox1.SelectedItem.Value + "/" + ListBox2.SelectedItem.Text + "/" + ListBox3.SelectedItem.Text;

        //    Response.Write("<script>");
        //    Response.Write("window.open('" + FilePath + "','top', 'height=700,width=800,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no,status=yes');");
        //    Response.Write("</script>");
        //}

        protected void ListBox3Click(object sender, EventArgs e)
        {
            //if (ListBox3.SelectedItem != null)
            //{

            //    string FilePath = "http://localhost/GORUNTULER/" + ListBox1.SelectedItem.Value + "/" + ListBox2.SelectedItem.Text + "/" + ListBox3.SelectedItem.Text;
            //    Response.Write("<script>");
            //    Response.Write("window.open('" + FilePath + "','top', 'height=700,width=800,resizable=yes,scrollbars=yes,toolbar=yes,menubar=no,location=no,directories=no,status=yes');");
            //    Response.Write("</script>");

            //}
        }

        //       public void FilledTable(string path)
        //       {
        //          DirectoryInfo directoriesForFilledTable = new DirectoryInfo(@"C:\inetpub\wwwroot\AmesCameraProject\" + ListBox1.SelectedItem.Text + "\\" + ListBox2.SelectedItem.Text);
        //           var files = directoriesForFilledTable.GetFiles();
        //           TableRow tRow = new TableRow();
        //           tRow.Attributes.CssStyle["margin-bottom"] = "10px";
        //           table1.Rows.Add(tRow); 
        //           int row = 0;
        //           int cells = 0;
        //           for (int i = 0; i < files.Length; i++)
        //           {

        //               if (i % 5 == 0)
        //               {
        //                   TableRow tRow2 = new TableRow();
        //                   tRow2.Attributes.CssStyle["margin-bottom"] = "10px";
        //                   table1.Rows.Add(tRow2);
        //                   if (i != 0)
        //                   {
        //                       row = row + 1;
        //                       cells = 0;
        //                   }
        //               }
        //               if ((files[i].Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
        //               {
        //                 // var filePath = FileExtensionConverter.ExtensionConverter(files[i].FullName);
        //                   TableCell tCell = new TableCell();                   
        //                   table1.Rows[row].Cells.Add(tCell);

        //                   ImageButton imgBtn = new ImageButton();
        //                   Label lbl = new Label();
        //                   lbl.Text = files[i].Name;
        //                   //imgBtn.ImageUrl = "http://localhost/GORUNTULER/" + ListBox1.SelectedItem.Text.ToString() + '/' + ListBox2.SelectedItem.Text.ToString() + '/' + i;
        //                   //imgBtn.ImageUrl = "http://localhost/GORUNTULER/512002-002/01.01.2022/images.png";

        //                   System.Net.WebRequest request = System.Net.WebRequest.Create(@"http://localhost/AmesCameraProject/" + ListBox1.SelectedItem.Text+ "//" + ListBox2.SelectedItem.Text + "/" + files[i].Name);
        //                   //System.Net.WebRequest request = System.Net.WebRequest.Create(filePath);
        //                   System.Net.WebResponse response = request.GetResponse();
        //                   System.IO.Stream responseStream = response.GetResponseStream();
        //                   Bitmap bitmap2 = new Bitmap(responseStream);

        //                   var imageHeight = 1200;
        //                   var imageWidth = 1920;

        //                   System.IO.MemoryStream ms = new MemoryStream();
        //                   bitmap2.Save(ms, ImageFormat.Jpeg);
        //                   byte[] byteImage = ms.ToArray();
        //                   var SigBase64 = Convert.ToBase64String(byteImage);
        //                   imgBtn.ImageUrl = "data:image/png;base64," + SigBase64;
        //                   //imgBtn.ImageUrl = @"http://localhost/GORUNTULER/" + ListBox1.SelectedItem.Text + "//" + ListBox2.SelectedItem.Text + "/"+files[i].Name;
        //                  // imgBtn.ImageUrl = @"http://localhost/GORUNTULER/" + ListBox1.SelectedItem.Text + "//" + ListBox2.SelectedItem.Text + "/"+fileNameWithExtension;

        //                   imgBtn.Height = imageHeight/5;
        //                   imgBtn.Width = imageWidth/5;
        //                   imgBtn.ID = "imgBtn" + i;
        //                   imgBtn.OnClientClick = "OpenWindow('" + SigBase64 + "');return false;";
        //                   //imgBtn.OnClientClick = "OpenWindow('" + ListBox1.SelectedItem.Text + "','" + ListBox2.SelectedItem.Text + "','" + files[i].Name+"');return false;///";
        ////çalışankodblogu  imgBtn.OnClientClick = "OpenWindow('" + ListBox1.SelectedItem.Text + "','" + ListBox2.SelectedItem.Text + "','" + files[i].Name+"');return false;";
        //                   //imgBtn.OnClientClick = "OpenWindow('" + ListBox1.SelectedItem.Text + "','" + ListBox2.SelectedItem.Text + "','" + files[i].Name + "');return false;";

        //                   //imgBtn.CommandArgument = @"http://localhost/GORUNTULER/" + ListBox1.SelectedItem.Text + "//" + ListBox2.SelectedItem.Text + "/" + files[i].Name;
        //                   //imgBtn.Command += new CommandEventHandler(OnImageClick);
        //                   //imgBtn.Attributes.Add("onclick", "return false;");
        //                   //imgBtn.Attributes.Add("AutoPostBack", "false");
        //                   //table1.Rows[0].Cells[i].Text = string.Format("<img src='' width='200' height ='200' id ='photoReview' runat='server'></img>");
        //                   table1.Rows[row].Cells[cells].Controls.Add(imgBtn);
        //                   table1.Rows[row].Cells[cells].Controls.Add(lbl);
        //                   table1.Rows[row].Cells[cells].Attributes.CssStyle["text-align"] = "center";
        //                   //   fabricTypeDiv.InnerHtml += "<li class='list-group-item' onClick=\"alert('selam')\" >" + dir.ToString() + "</li>";
        //               }
        //               //ListBox1.Items.Add(dirArray[i].ToString());
        //               cells = cells + 1;

        //           }

        //       }
        public static string fileName = "";
        protected void OnImageClick(object sender, CommandEventArgs e)
        {
            fileName = e.CommandName;

        }

        //public void GetDirectories()
        //{
        //    DirectoryInfo dirInfo = new DirectoryInfo(@"C:\inetpub\wwwroot\AmesCameraProject");
        //    DirectoryInfo[] dirArray = dirInfo.GetDirectories();
        //    foreach (var directory in dirArray)
        //    {
        //        ListBox1.Items.Add(directory.Name);
        //    }                  
        //}


        public void GetPartiValues(string _partiNo)
        {
            string connectionString = @"Server=sun0231\SQLEXPRESS;Database=AMESLOG;Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select * from dbo.PartiLog where PartiNo = '" + _partiNo + "'", connection);
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.HasRows == true)
                {
                    GridView1.DataSource = dataReader;
                    GridView1.DataBind();
                }
            }
        }

        public void FilledTableWithCalendar(string path)
        {
            try
            {
                if (controlIsFirstTime == 0)
                {
                    bool directoryIsThere = Directory.Exists(@"C:\inetpub\wwwroot\AmesCameraProject\" + ListBox2.SelectedItem.Text + "");
                    DirectoryInfo directoryForFilledTable = new DirectoryInfo(@"C:\inetpub\wwwroot\AmesCameraProject\" + ListBox2.SelectedItem.Text);
                    if (directoryIsThere == true)
                    {
                        files = directoryForFilledTable.GetFiles();
                        controlIsFirstTime++;
                        if (files == null)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Uyarı','Bu Partide Hata Bulunamadı','warning')", true);
                        }
                        if (files.Length > 10)
                        {
                            firstIndex = 0;
                            lastIndex = 10;
                        }
                        else
                        {
                            firstIndex = 0;
                            lastIndex = files.Length;
                        }
                    }
                }
                if (isGetValues == 1)
                {
                    TableRow tRow = new TableRow();
                    tRow.Attributes.CssStyle["margin-bottom"] = "10px";
                    table1.Rows.Add(tRow);
                    int row = 0;
                    int cells = 0;
                    for (int i = firstIndex; i < lastIndex; i++)
                    {
                        ImageTable userControl = new ImageTable();
                        userControl.FileName = "asdasd";
                        userControl.Meter = "240";
                        userControl.TypeOfError = "delik";
                        placeHolderForUserControl.Controls.Add(userControl);
                        if (i % 5 == 0)
                        {
                            TableRow tRow2 = new TableRow();
                            tRow2.Attributes.CssStyle["margin-bottom"] = "10px";
                            table1.Rows.Add(tRow2);
                            if (i != 0)
                            {
                                row = row + 1;
                                cells = 0;
                            }
                        }
                        if ((files[i].Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                        {
                            TableCell tCell = new TableCell();

                            table1.Rows[row].Cells.Add(tCell);

                            ImageButton imgBtn = new ImageButton();

                            Label lbl = new Label();

                            fileName = files[i].Name;

                            lbl.Text = "<h6>" + files[i].Name + "</h6>";

                            System.Net.WebRequest request = System.Net.WebRequest.Create(@"http://localhost/AmesCameraProject" + "//" + ListBox2.SelectedItem.Text + "/" + files[i].Name);

                            System.Net.WebResponse response = request.GetResponse();

                            System.IO.Stream responseStream = response.GetResponseStream();

                            Bitmap bitmap2 = new Bitmap(responseStream);

                            var imageHeight = 1200;

                            var imageWidth = 1920;

                            System.IO.MemoryStream ms = new MemoryStream();

                            bitmap2.Save(ms, ImageFormat.Jpeg);

                            byte[] byteImage = ms.ToArray();

                            var SigBase64 = Convert.ToBase64String(byteImage);

                            imgBtn.ImageUrl = "data:image/png;base64," + SigBase64;

                            imgBtn.Height = imageHeight / 6;

                            imgBtn.Width = imageWidth / 6;

                            imgBtn.ID = "imgBtn" + i;

                            imgBtn.OnClientClick = "OpenWindow('" + SigBase64 + "');return false;";

                            //table1.Rows[row].Cells[cells].Controls.Add(imgBtn);
                            table1.Rows[row].Cells[cells].Controls.Add(placeHolderForUserControl);

                            //  table1.Rows[row].Cells[cells].Controls.Add(lbl);
                        }
                        cells = cells + 1;

                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Uyarı','Beklenmedik Bir Hata İle Karşılaşıldı ','warning')", true);
            }
        }
        public static int firstIndex = 0;
        public static int lastIndex = 10;
        public static FileInfo[] files = null;


        public void FilledTableWithPartiNo(string partiNo)
        {
            try
            {
                if (controlIsFirstTime == 0)
                {
                    DirectoryInfo directoryForFilledTable = new DirectoryInfo(@"C:\inetpub\wwwroot\AmesCameraProject\" + partiNo);
                    bool directoryIsThere = Directory.Exists(@"C:\inetpub\wwwroot\AmesCameraProject\" + partiNo + "");
                    if (directoryIsThere == true)
                    {
                        //GetPartiValues(partiNo);
                        files = directoryForFilledTable.GetFiles();
                        controlIsFirstTime++;
                        if (files == null)
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Uyarı','Bu Partide Hata Bulunamadı','warning')", true);
                        }
                        if (files.Length > 10)
                        {
                            firstIndex = 0;
                            lastIndex = 10;
                        }
                        else
                        {
                            firstIndex = 0;
                            lastIndex = files.Length;
                        }
                    }
                    else
                    {
                        btnIleri.Visible = false;
                        btnGeri.Visible = false;
                        GridView1.Visible = false;
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Uyarı','Hatalı Parti Numarası Girdiniz','warning')", true);
                    }

                }
                if (isGetValues == 1)
                {

                    TableRow tRow = new TableRow();
                    tRow.Attributes.CssStyle["margin-bottom"] = "10px";
                    table1.Rows.Add(tRow);
                    int row = 0;
                    int cells = 0;
                    for (int i = 0; i < files.Length; i++)
                    {



                        fileName = files[i].FullName.Split('\\').Last();

                        TableRow tRow2 = new TableRow();
                        tRow2.Attributes.CssStyle["margin-bottom"] = "10px";
                        table1.Rows.Add(tRow2);
                        if (i % 3 == 0)
                        {
                            row = row + 1;
                            cells = 0;
                        }

                        if ((files[i].Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                        {
                            TableCell tCell = new TableCell();
                            table1.Rows[row].Cells.Add(tCell);
                            var control = (ImageTable)LoadControl("~/ImageTable.ascx");
                            control.FileName = files[i].Name;
                            control.Meter = "Metresi : 240";
                            control.TypeOfError = " Hata Türü : Yapıştırma";
                            control.Icon = @"http://localhost/AmesCameraProject/" + "icos" + "/" + files[i].Name;
                            control.Icon = @"http://localhost/AmesCameraProject/Icons/1.png";

                            table1.Rows[row].Cells[cells].Controls.Add(control);

                            //System.Net.WebRequest request = System.Net.WebRequest.Create(@"http://localhost/AmesCameraProject/" + partiNo + "/" + files[i].Name);
                            //System.Net.WebRequest request = System.Net.WebRequest.Create(@"http://localhost/AmesCameraProject/Icons/1.png");
                            //System.Net.WebResponse response = request.GetResponse();
                            //System.IO.Stream responseStream = response.GetResponseStream();
                            //Bitmap bitmap2 = new Bitmap(responseStream);
                            //System.IO.MemoryStream ms = new MemoryStream();
                            //bitmap2.Save(ms, ImageFormat.png);
                            //byte[] byteImage = ms.ToArray();
                            //var SigBase64 = Convert.ToBase64String(byteImage);
                            // control.Icon = "data:image/png;base64," + SigBase64;
                            //control.Icon = @"D:/Icons/1.png";
                            //  imgBtn.Height = imageHeight ;
                            //  imgBtn.Width = imageWidth ;
                            // imgBtn.ID = "imgBtn" + i;
                            //imgBtn.OnClientClick = "OpenWindow('" + SigBase64 + "','" + files[i].Name + "');return false;";
                            // imgBtn.OnClientClick = "OpenWindow('" + TextBox1.Text + "/" + files[i].Name + "');return false;";
                            //table1.Rows[row].Cells[cells].Controls.Add(imgBtn);
                            //Page.LoadControl("ImageTable.ascx");
                            //table1.Rows[row].Cells[cells].Controls.Add(placeHolderForUserControl);
                            // placeHolderForUserControl.Controls.Add(control);
                            //table1.Rows[row].Cells[cells].Controls.Add(lbl);
                        }
                        cells = cells + 1;

                    }

                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Uyarı','Beklenmedik Bir Hata İle Karşılaşıldı','warning')", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            typeForGet = 2;
            controlIsFirstTime = 0;
            string partiNo = TextBox1.Text;
            FilledTableWithPartiNo(partiNo);
            // GetDirectories();
            GridView1.Visible = true;
            Calendar1.Visible = false;
            ListBox2.Visible = false;
            date.Visible = false;
            PartiNumber.Visible = false;
            Button2.Visible = true;
            btnIleri.Visible = true;
            firstIndex = 0;
            if (firstIndex == 0)
            {
                btnGeri.Visible = false;
            }
            if (files.Length <= 10)
            {
                btnIleri.Visible = false;
            }


        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

            ListBox2.Items.Clear();
            string day = Calendar1.SelectedDate.Day.ToString();
            string month = Calendar1.SelectedDate.Month.ToString();
            string year = Calendar1.SelectedDate.Year.ToString();

            string date = year + "/" + month + "/" + day;
         //    GetWithDate(date);

        }
        public void GetWithDate(string date)
        {
            string connectionString = @"Server=sun0231\SQLEXPRESS;Database=AMESLOG;Trusted_Connection=True";
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    String query = "SELECT PartiNo FROM PartiLog WHERE CAST(StartingDate AS date) ='" + date + "'";
                    con.Open();

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        SqlDataReader rdr = command.ExecuteReader();
                        while (rdr.Read())
                        {
                            var findByValue = ListBox2.Items.FindByValue(rdr.GetString(0));
                            if (findByValue == null)
                            {
                                ListBox2.Items.Add(rdr.GetString(0));
                            }
                        }

                        rdr.Close();
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (ListBox2.Items.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "k", "swal('Uyarı','Seçtiğiniz Tarihte Hatalı Parti Bulunamadı','warning')", true);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            btnGeri.Visible = false;
            btnIleri.Visible = true;
            Calendar1.Visible = true;
            ListBox2.Visible = true;
            date.Visible = true;
            PartiNumber.Visible = true;
        }
        protected void btnIleri_Click(object sender, EventArgs e)
        {
            Button2.Visible = true;

            firstIndex = firstIndex + 10;
            lastIndex = lastIndex + 10;
            if (lastIndex > files.Length)
            {
                lastIndex = files.Length;

                btnIleri.Visible = false;
            }
            if (typeForGet == 2)
            {
                FilledTableWithPartiNo(TextBox1.Text);
            }
            else
            {
                FilledTableWithCalendar(globalPath + ListBox2.SelectedItem.Text);
            }
            if (firstIndex > 0)
            {
                btnGeri.Visible = true;
            }
        }

        protected void btnGeri_Click(object sender, EventArgs e)
        {
            Button2.Visible = true;
            firstIndex = firstIndex - 10;
            lastIndex = lastIndex - 10;
            if (lastIndex != 10)
            {
                while (lastIndex % 10 != 0)
                {
                    lastIndex++;
                }

            }
            if (firstIndex < 0 || firstIndex == 0)
            {
                firstIndex = 0;
                lastIndex = 10;
            }
            if (firstIndex == 0)
            {
                btnGeri.Visible = false;
                btnIleri.Visible = true;
            }
            if ((lastIndex >= 10) && (files.Length > 10))
            {
                btnIleri.Visible = true;
            }
            if (typeForGet == 2)
            {
                FilledTableWithPartiNo(TextBox1.Text);
            }
            else
            {
                FilledTableWithCalendar(globalPath + ListBox2.SelectedItem.Text);
            }


        }
    }
}