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
    public class ErrorLogManager : IErrorLogService
    {
        private IErrorLogDal _logDal;

        public ErrorLogManager(IErrorLogDal logDal)
        {
            _logDal = logDal;
        }

        public void Add(ErrorLog log)
        {
            _logDal.Add(log);
        }
    }
}
