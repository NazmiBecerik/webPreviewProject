using CameraCLProject.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Entities.Concrete
{
    public class Log : IEntity
    {
        public Log(string partiNo, DateTime startingDate, int errorCount, DateTime endDate, string articleKod, string articleTanim, string baseKod, decimal en,decimal gramaj, string cariUnvan)
        {
            PartiNo = partiNo;
            StartingDate = startingDate;
            ErrorCount = errorCount;
            EndDate = endDate;
            ArticleKod = articleKod;
            ArticleTanim = articleTanim;
            BaseKod = baseKod;
            Gramaj = gramaj;
            En = en;
            CariUnvan = cariUnvan;

        }

        public int Id { get; set; }
        public string PartiNo { get; set; }
        public DateTime StartingDate { get; set; }
        public int ErrorCount { get; set; }
        public DateTime EndDate { get; set; }
        public string ArticleKod { get; set; }
        public string ArticleTanim { get; set; }
        public string BaseKod { get; set; }
        public decimal Gramaj { get; set; }
        public decimal En { get; set; }
        public string CariUnvan { get; set; }
    }
}
