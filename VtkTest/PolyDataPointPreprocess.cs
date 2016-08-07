using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;

namespace Vtk
{
    class PolyDataPointPreprocess :IVtkPreprocess
    {

        private vtkPolyDataReader reader;
        public vtkPolyDataMapper polyDataMapper;


        public vtkActor Actor { get; set; }
        public vtkRenderer Renderer { get; set; }
        public RenderWindowControl RenderWindowControl { get; set; }

        public IVtkPreprocess SetData(string filename)
        {
            reader = vtkPolyDataReader.New();
            reader.SetFileName(filename);
            reader.Update();

            return this;
        }

        public IVtkPreprocess SetDecimate()
        {
            throw new NotImplementedException();
        }

        public IVtkPreprocess SetMapper()
        {
           polyDataMapper = vtkPolyDataMapper.New();
            polyDataMapper.SetInputConnection(reader.GetOutputPort());
            polyDataMapper.Update();

            return this;
        }

        public IVtkPreprocess SetFilter()
        {
            throw new NotImplementedException();
        }

        public IVtkPreprocess SetReconModel()
        {
            throw new NotImplementedException();
        }

        public IVtkPreprocess SetActor()
        {
            Actor = vtkActor.New();
            Actor.SetMapper(polyDataMapper);

            return this;
        }

        public IVtkPreprocess SetRenderWindow()
        {
            Renderer = RenderWindowControl.RenderWindow.GetRenderers().GetFirstRenderer();
            Renderer.SetBackground(0, 0, 0);
            Renderer.AddActor(Actor);
            RenderWindowControl.Refresh();

            return this;
        }
    }
}
