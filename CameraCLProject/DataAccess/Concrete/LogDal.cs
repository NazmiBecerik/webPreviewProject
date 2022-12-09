using CameraCLProject.DataAccess.Abstract;
using CameraCLProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.DataAccess.Concrete
{
    public class LogDal : ILogDal
    {
        string connectionString = @"Server=sun0231\SQLEXPRESS;Database=AMESLOG;Trusted_Connection=True";

        public void Add(Log log)
        {        

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String query = "INSERT INTO dbo.PartiLog VALUES (@PartiNo,@StartingDate,@EndDate,@ErrorCount,@ArticleKod,@ArticleTanim,@BaseKod,@Gramaj,@En,@CariUnvan)";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@PartiNo", log.PartiNo);
                        command.Parameters.AddWithValue("@StartingDate", log.StartingDate);
                        command.Parameters.AddWithValue("@ErrorCount", log.ErrorCount);
                        command.Parameters.AddWithValue("@EndDate", log.EndDate);
                        command.Parameters.AddWithValue("@ArticleKod", log.ArticleKod);
                        command.Parameters.AddWithValue("@ArticleTanim", log.ArticleTanim);
                        command.Parameters.AddWithValue("@BaseKod", log.BaseKod);
                        command.Parameters.AddWithValue("@Gramaj", log.Gramaj);
                        command.Parameters.AddWithValue("@En", log.En);
                        command.Parameters.AddWithValue("@CariUnvan", log.CariUnvan);
                        command.ExecuteNonQuery();
                        con.Close();

                    }                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex.Message);
            }
        }

        public void GetWithDate(string date)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String query = "SELECT PartiNo FROM PartiLog WHERE CAST(StartingDate AS date) ='"+date+"'";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        SqlDataReader rdr = command.ExecuteReader();
                        while (rdr.Read())
                        {
                            Console.WriteLine(rdr.GetValue(0));
                        }
                        rdr.Close();
                        command.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error {0}", ex.Message);
            }
        }
    }
}
