using CameraCLProject.Business.Abstract;
using CameraCLProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Business.Concrete
{
    public class FabricManager:IFabricService
    {

        List<Fabric> fabrics;
        public FabricManager()
        {
            fabrics = new List<Fabric>();
        }
        public List<Fabric> GetFabric(string fabricPartiNo)
        {
            string cs = ConfigurationManager.ConnectionStrings["MyConnection"].ToString();
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    using (SqlCommand com = new SqlCommand())
                    {
                        com.CommandText = string.Format("select * from  NETSIS.AMES.dbo.disun_parti where Parti_No like '{0}%'", fabricPartiNo);
                        com.Connection = con;
                        con.Open();
                        SqlDataReader reader = com.ExecuteReader();
                        while (reader.Read())
                        {
                            fabrics.Add(new Fabric
                            {
                                PartiNo = reader.GetString(0),
                                ArticleKod = reader.GetString(1),
                                ArticleTanim = reader.GetString(2),
                                BaseKod = reader.GetString(3),
                                Gramaj = reader.GetDecimal(4),
                                En = reader.GetDecimal(5),
                                CariUnvan = reader.GetString(6)
                            });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex.Message);
            }
            return fabrics;
        }

        public void SaveFabricLog()
        {
            string cs = ConfigurationManager.ConnectionStrings["MyConnection1"].ToString();
        }
    }
}
