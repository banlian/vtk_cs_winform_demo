using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace Vtk
{
    interface IVtkPreprocess
    {
        vtkActor Actor { get; set; }
        vtkRenderer Renderer { get; set; }
        RenderWindowControl RenderWindowControl { get; set; }

        IVtkPreprocess SetData(string filename);

        IVtkPreprocess SetDecimate();

        IVtkPreprocess SetMapper();

        IVtkPreprocess SetFilter();

        IVtkPreprocess SetReconModel();

        IVtkPreprocess SetActor();

        IVtkPreprocess SetRenderWindow();
    }
}
