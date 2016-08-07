using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aoi3dLibrary
{
    public enum CheckMode3d
    {
        Line,
        Area,
    }

    public enum CheckType3d
    {
        SectionAbsHeight,
        SectionRefHeight,

        Volume,
    }

    class CheckSeries3dHeight : ICheckSeries3d
    {
        public CheckType3d Type { get; set; }
        public Rectangle Rect { get; set; }
    }
}
