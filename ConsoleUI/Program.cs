using CameraCLProject.Business.Concrete;
using CameraCLProject.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            LogManager logManager = new LogManager(new LogDal());
            logManager.GetWithDate("2022/09/19");
            Console.Read();
        }
    }
}
