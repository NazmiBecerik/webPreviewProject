using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CameraCLProject;
using CameraCLProject.Business.Concrete;
using Basler.Pylon;
using CameraCLProject.Entities.Concrete;
using CameraCLProject.Business.Abstract;
using CameraCLProject.DataAccess.Concrete;
using System.Threading;

namespace kumasSecim
{
    public partial class HomePage : System.Web.UI.Page
    {
        public static CameraManager camera;
        public static string _partiNo;
        public static string _articleKod;
        public static string _articleTanim;
        public static string _baseKod;
        public static decimal _gramaj;
        public static decimal _en;
        public static string _cariUnvan;
        public static bool isStopped;
        //public static HomePage homePage;
        public static DateTime _startingTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            //homePage = new HomePage();
            camera = new CameraManager(new RequestManager());

        }

        [WebMethod]
        public static List<Fabric> GetFabric(string partiNo)
        {
            // 3 KARAKTERLİ VERİLER VAR
            if (partiNo.Length >= 3)
            {
                FabricManager manager = new FabricManager();
                return manager.GetFabric(partiNo);
            }
            else
            {
                return null;
            }
        }
        [WebMethod]
        public static void CallResumeInCL()
        {
            camera.StreamResume();
        }

        [WebMethod]
        public static void CallStartInCL(string partiNo, string articleKod, string articleTanim, string baseKod, decimal gramaj, decimal en, string cariUnvan)
        {
            try
            {
                isStopped = true;
                _startingTime = DateTime.Now;
                _partiNo = partiNo;
                _articleKod = articleKod;
                _articleTanim = articleTanim;
                _baseKod = baseKod;
                _gramaj = gramaj;
                _en = en;
                _cariUnvan = cariUnvan;
                Thread thread = new Thread(() => camera.StreamStart(partiNo, baseKod));
                thread.Start();

            }
            catch (Exception ex)
            {

                throw ex;
            }
      



            return;

        }

        [WebMethod(EnableSession = true)]
        public static void CallPauseInCL()
        {
            
            camera.StreamPause();

        }

        [WebMethod]
        public static void CallStopInCL(string partiNo)
        {
            int errorCount = CameraCLProject.Business.Concrete.RequestManager.errorCounter;

            LogManager logManager = new LogManager(new LogDal());

            logManager.Add(new Log(_partiNo, _startingTime, errorCount, DateTime.Now, _articleKod, _articleTanim, _baseKod, _en, _gramaj, _cariUnvan));

            CameraCLProject.Core.Utilities.KumasBilgisiLogJsonFile((@"D:/Goruntuler/" + partiNo), partiNo, _articleKod, _articleTanim, _baseKod, _gramaj, _en, _cariUnvan);

            camera.StreamStop();

        }
    }
}