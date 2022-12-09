using CameraCLProject.Entities.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Entities.Concrete
{
    public class Result : IEntity
    {
        [JsonProperty("defect_count")]
        public int ErrorCount { get; set; }

        [JsonProperty("defect_coordinates")]
        public List<string[]> Coordinates { get; set; }
    }
}
