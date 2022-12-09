using CameraCLProject.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Entities.Concrete
{
    public class Fabric:IEntity
    {
        public string PartiNo { get; set; }
        public string ArticleKod { get; set; }
        public string ArticleTanim { get; set; }
        public string BaseKod { get; set; }
        public decimal Gramaj { get; set; }
        public decimal En { get; set; }
        public string CariUnvan { get; set; }
    }
}
