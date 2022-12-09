using Basler.Pylon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraCLProject.Business.Abstract
{
    public interface ICameraService
    {
        void StreamStart(string partiNo,string baseKod);
        void StreamPause();
        void StreamResume();
        void StreamStop();
        void CameraStartConfiguration(Camera[] camera);
        void CameraStopConfiguration(Camera[] camera);

    }
}
