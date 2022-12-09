using CameraCLProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.DataAccess.Abstract
{
    public interface ILogDal
    {
        void Add(Log log);
        void GetWithDate(string date);
    }
}
