using CameraCLProject.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Entities.Concrete
{
    public class ErrorLog:IEntity
    {
        public ErrorLog(DateTime errorTime, string partiNo)
        {
            ErrorTime = errorTime;
            PartiNo = partiNo;
        }

        public int Id { get; set; }
        public DateTime ErrorTime { get; set; }
        public string PartiNo { get; set; }
    }
}
