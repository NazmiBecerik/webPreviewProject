using CameraCLProject.Business.Abstract;
using CameraCLProject.DataAccess.Abstract;
using CameraCLProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Business.Concrete
{
    public class LogManager : ILogService
    {
        private ILogDal _logDal;
        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }
        public void Add(Log log)
        {
            _logDal.Add(log);
        }

        public void GetWithDate(string date)
        {
            _logDal.GetWithDate(date);
        }
    }
}
