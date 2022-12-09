using CameraCLProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Business.Abstract
{
    public interface IFabricService
    {
        List<Fabric> GetFabric(string fabricPartiNo);

    }
}
