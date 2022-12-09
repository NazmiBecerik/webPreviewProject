using CameraCLProject.DataAccess.Abstract;
using CameraCLProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.DataAccess.Concrete
{
    public class ErrorLogDal : IErrorLogDal
    {
        public void Add(ErrorLog log)
        {
            string connectionString = @"Server=sun0231\SQLEXPRESS;Database=AMESLOG;Trusted_Connection=True";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String query = "INSERT INTO dbo.PartiErrorTimeLogs VALUES (@PartiNo,@ErrorTime)";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@PartiNo", log.PartiNo);
                        command.Parameters.AddWithValue("@ErrorTime", log.ErrorTime);
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
