using System;
using System.IO;
using Kitware.VTK;

namespace Vtk
{
    internal class SimplePointPreprocess : IVtkPreprocess
    {
        private vtkColorSeries colors;
        private vtkPoints points;


        public vtkPolyData PolyData;

        public vtkPolyDataMapper polyDataMapper;
        private vtkSimplePointsReader reader;
        
        
        public vtkActor Actor { get; set; }
        public vtkRenderer Renderer { get; set; }
        public RenderWindowControl RenderWindowControl { get; set; }

        public IVtkPreprocess SetData(string filename)
        {
            try
            {
                reader = vtkSimplePointsReader.New();
                reader.SetFileName(filename);
                reader.Update();

                return this;
            }
            catch (Exception)
            {
                reader.FastDelete();
                throw;
            }
        }

        public IVtkPreprocess SetDecimate()
        {
            throw new NotImplementedException();
        }

        public IVtkPreprocess SetMapper()
        {
            polyDataMapper = vtkPolyDataMapper.New();
            polyDataMapper.SetInput(reader.GetOutput());
            polyDataMapper.Update();

            return this;
        }

        public void SetMapper(vtkPolyData data)
        {
            vtkVertexGlyphFilter filter = vtkVertexGlyphFilter.New();
            filter.SetInput(data);
            filter.Update();


            polyDataMapper = vtkPolyDataMapper.New();
            polyDataMapper.SetInputConnection(filter.GetOutputPort());
            polyDataMapper.Update();
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
            Actor.GetProperty().SetPointSize(1.5f);


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

        public void ReadDataByLine(string filename)
        {
            try
            {
                var dataStrings = File.ReadAllLines(filename);
                points = vtkPoints.New();
                points.SetNumberOfPoints(dataStrings.Length);

                var colorArray = vtkUnsignedCharArray.New();
                colorArray.SetNumberOfComponents(3);

                for (var i = 0; i < dataStrings.Length; i++)
                {
                    var data = dataStrings[i].Split(' ');

                    if (data.Length<6)
                    {
                        continue;
                    }

                    points.SetPoint(i, int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]));
                    colorArray.InsertNextTuple3(double.Parse(data[3]), double.Parse(data[4]), double.Parse(data[5]));
                }

                PolyData = vtkPolyData.New();
                PolyData.SetPoints(points);
                PolyData.GetPointData().SetScalars(colorArray);
                PolyData.Update();
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveData(string filename)
        {
            var writer = vtkPolyDataWriter.New();
            writer.SetFileName(filename);
            writer.SetInput(reader.GetOutput());
            writer.Update();
        }

        public void SetCamera()
        {
            var cam = Renderer.GetActiveCamera();
            cam.SetViewAngle(40);
            cam.Azimuth(10);
            Renderer.SetActiveCamera(cam);
        }
    }
}