using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Kitware.VTK;

namespace VtkConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                LinePlotTest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void LinePlotTest()
        {
            vtkTable table = vtkTable.New();

            vtkFloatArray arrX = vtkFloatArray.New();
            table.AddColumn(arrX);

            vtkFloatArray arrSine = vtkFloatArray.New();
            table.AddColumn(arrSine);

            int numPoints = 100;

            table.SetNumberOfRows(100);

            for (int i = 0; i < numPoints; i++)
            {
                arrSine.SetValue(i, (float) Math.Cos(i));
            }

            table.Update();

            vtkContextView view = vtkContextView.New();
            view.GetRenderer().SetBackground(0,0,0);


            vtkChartXY chart = vtkChartXY.New();
            view.GetScene().AddItem(chart);


            chart.AddPlot(0).SetInput(table);

            view.GetInteractor().Initialize();
            view.GetInteractor().Start();


        }

        public static void FullscreenTest()
        {
            var sphere = vtkSphereSource.New();

            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(sphere.GetOutputPort());

            var actor = vtkActor.New();
            actor.SetMapper(mapper);

            var ren = vtkRenderer.New();
            ren.AddActor(actor);

            var renWin = vtkRenderWindow.New();
            renWin.AddRenderer(ren);

            var interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renWin);
            interactor.KeyPressEvt += MiddleButtonPress;

            renWin.SetFullScreen(1);
            renWin.Render();

            var th = new Thread(Thread1);
            th.IsBackground = true;
            th.Start(renWin);
            interactor.Start();
        }

        public static void Test()
        {
            var dataStrings = File.ReadAllLines("test.txt");


            var Points = vtkPoints.New();
            Points.SetNumberOfPoints(dataStrings.Length);

            var colorArray = vtkUnsignedCharArray.New();
            colorArray.SetNumberOfComponents(3);

            var imageData = vtkImageData.New();
            imageData.SetDimensions(2040, 2040, 0);
            imageData.SetScalarTypeToUnsignedChar();
            imageData.SetNumberOfScalarComponents(3);


            for (var i = 0; i < dataStrings.Length; i++)
            {
                var data = dataStrings[i].Split(' ');

                if (data.Length < 6)
                {
                    continue;
                }

                Points.SetPoint(i, int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]));
                colorArray.InsertNextTuple3(double.Parse(data[3]), double.Parse(data[4]), double.Parse(data[5]));
            }


            var PolyData = vtkPolyData.New();
            PolyData.SetPoints(Points);
            PolyData.GetPointData().SetScalars(colorArray);
            PolyData.Update();


            var filter = vtkVertexGlyphFilter.New();
            filter.SetInput(PolyData);
            filter.Update();


            var dataSetMapper = vtkPolyDataMapper2D.New();
            dataSetMapper.SetInput(filter.GetOutput());
            dataSetMapper.ScalarVisibilityOn();
            dataSetMapper.Update();

            var ImageDataActor = vtkActor2D.New();
            ImageDataActor.SetMapper(dataSetMapper);


            var ImageDataRenderer = vtkRenderer.New();
            ImageDataRenderer.SetBackground(0.2, 0.3, 0.5);
            ImageDataRenderer.AddActor(ImageDataActor);


            var renWin = vtkRenderWindow.New();
            renWin.AddRenderer(ImageDataRenderer);


            var interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renWin);

            renWin.Render();
            interactor.Start();
        }

        public static void Thread1(object o)
        {
            var renwin = o as vtkRenderWindow;
            while (true)
            {
                renwin.SetFullScreen(1);
                renwin.Render();
                Thread.Sleep(1000);
                renwin.SetFullScreen(0);
                renwin.Render();
                Thread.Sleep(1000);
            }
        }

        public static void MiddleButtonPress(object sender, EventArgs e)
        {
            var i = sender as vtkRenderWindowInteractor;
            Debug.Assert(i != null);
            if (i.GetRenderWindow().GetFullScreen() == 1)
            {
                i.GetRenderWindow().SetFullScreen(0);
                i.GetRenderWindow().Render();
            }
            else
            {
                i.GetRenderWindow().SetFullScreen(1);
                i.GetRenderWindow().Render();
            }
        }
    }
}