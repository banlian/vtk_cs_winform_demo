using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Aoi3dLibrary
{
    public interface ICheckSeries3d
    {
        CheckType3d Type { get; set; }

        Rectangle Rect { get; set; }


    }
}
