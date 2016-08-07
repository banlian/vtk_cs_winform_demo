using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Kitware.VTK;
using log4net;

namespace Vtk
{
    public class VtkProcess
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static RenderWindowControl RenderWindowControl;
        public static vtkRenderer Renderer;

        ~VtkProcess()
        {
        }

        #region Test Process

        public static void SimplePointPreprocess()
        {
            var filePath = Path.Combine(DirManage.PointDataDir, @"points.txt");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("file not exist");
                return;
            }

            var pro = new SimplePointPreprocess
            {
                RenderWindowControl = RenderWindowControl
            };

            pro.SetData(filePath).SetMapper().SetActor().SetRenderWindow();

            pro.SetCamera();


            pro.SaveData("Test.data");
        }

        public static void PolyDataPointReader(string fileName)
        {
            var pro = new PolyDataPointPreprocess()
            {
                RenderWindowControl = RenderWindowControl
            };

            pro.SetData(fileName);
            pro.SetMapper();
            pro.SetActor();
            pro.SetRenderWindow();
        }


        public static void PlyPointsReader()
        {
            var filePath = Path.Combine(DirManage.Root, @"Data\bunny.ply");

            var reader = vtkPLYReader.New();
            reader.SetFileName(filePath);
            reader.Update();


            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(reader.GetOutputPort());


            var actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetPointSize(10);

            // renderer
            Renderer = RenderWindowControl.RenderWindow.GetRenderers().GetFirstRenderer();
            // set background color
            Renderer.SetBackground(0.2, 0.3, 0.4);
            // add our actor to the renderer
            Renderer.AddActor(actor);
            Renderer.AddActor(new vtkAxesActor());
        }

        public static void GreedyTerrainDecimation()
        {
            // Create an image
            var image = vtkImageData.New();
            image.SetDimensions(3, 3, 1);
            image.SetNumberOfScalarComponents(1);
            image.SetScalarTypeToUnsignedChar();
            var dims = image.GetDimensions();
            unsafe
            {
                for (var i = 0; i < dims[0]; i++)
                {
                    for (var j = 0; j < dims[1]; j++)
                    {
                        var ptr = (byte*) image.GetScalarPointer(i, j, 0);
                        *ptr = (byte) vtkMath.Round(vtkMath.Random(0, 100));
                    }
                }
            }
            var decimation = vtkGreedyTerrainDecimation.New();
            decimation.SetInputConnection(image.GetProducerPort());
            decimation.Update();
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(decimation.GetOutputPort());
            // actor
            var actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetInterpolationToFlat();
            actor.GetProperty().EdgeVisibilityOn();
            actor.GetProperty().SetEdgeColor(1, 0, 0);

            // get a reference to the renderwindow of our renderWindowControl1
            var renderWindow = RenderWindowControl.RenderWindow;
            // renderer
            var renderer = renderWindow.GetRenderers().GetFirstRenderer();
            // set background color
            renderer.SetBackground(0.2, 0.3, 0.4);
            // add our actor to the renderer
            renderer.AddActor(actor);
        }

        public static void vtkPolyDataConnectivityFilter_SpecifiedRegion()
        {
            // Small sphere (first region)
            var sphereSource1 = vtkSphereSource.New();
            sphereSource1.Update();

            // Large sphere (second region)
            var sphereSource2 = vtkSphereSource.New();
            sphereSource2.SetRadius(10);
            sphereSource2.SetCenter(25, 0, 0);
            sphereSource2.Update();

            var appendFilter = vtkAppendPolyData.New();
            appendFilter.AddInputConnection(sphereSource1.GetOutputPort());
            appendFilter.AddInputConnection(sphereSource2.GetOutputPort());
            appendFilter.Update();

            var connectivityFilter = vtkPolyDataConnectivityFilter.New();
            connectivityFilter.SetInputConnection(appendFilter.GetOutputPort());
            connectivityFilter.SetExtractionModeToSpecifiedRegions();
            connectivityFilter.AddSpecifiedRegion(1); //select the region to extract here
            connectivityFilter.Update();

            // Create a mapper and actor for original data
            var originalMapper = vtkPolyDataMapper.New();
            originalMapper.SetInputConnection(appendFilter.GetOutputPort());
            originalMapper.Update();

            var originalActor = vtkActor.New();
            originalActor.SetMapper(originalMapper);

            // Create a mapper and actor for extracted data
            var extractedMapper = vtkPolyDataMapper.New();
            extractedMapper.SetInputConnection(connectivityFilter.GetOutputPort());
            extractedMapper.Update();

            var extractedActor = vtkActor.New();
            extractedActor.GetProperty().SetColor(1, 0, 0);
            extractedActor.SetMapper(extractedMapper);
            // get a reference to the renderwindow of our renderWindowControl1
            var renderWindow = RenderWindowControl.RenderWindow;
            // renderer
            var renderer = renderWindow.GetRenderers().GetFirstRenderer();
            // set background color
            renderer.SetBackground(0.2, 0.3, 0.4);
            // add our actor to the renderer
            //renderer.AddActor(originalActor);
            renderer.AddActor(extractedActor);
        }

        public static void DijkstraGraphGeodesicPath()
        {
            // Create a sphere
            var sphereSource = vtkSphereSource.New();
            sphereSource.Update();

            var dijkstra = vtkDijkstraGraphGeodesicPath.New();
            dijkstra.SetInputConnection(sphereSource.GetOutputPort());
            dijkstra.SetStartVertex(0);
            dijkstra.SetEndVertex(22);
            dijkstra.Update();

            // Create a mapper and actor
            var pathMapper = vtkPolyDataMapper.New();
            pathMapper.SetInputConnection(dijkstra.GetOutputPort());

            var pathActor = vtkActor.New();
            pathActor.SetMapper(pathMapper);
            pathActor.GetProperty().SetColor(1, 0, 0); // Red
            pathActor.GetProperty().SetLineWidth(4);

            // Create a mapper and actor
            var mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(sphereSource.GetOutputPort());

            var actor = vtkActor.New();
            actor.SetMapper(mapper);

            // get a reference to the renderwindow of our renderWindowControl1
            var renderWindow = RenderWindowControl.RenderWindow;
            // renderer
            var renderer = renderWindow.GetRenderers().GetFirstRenderer();
            // set background color
            renderer.SetBackground(0.3, 0.6, 0.3);
            // add our actor to the renderer
            renderer.AddActor(actor);
            renderer.AddActor(pathActor);
        }

        public static void SurfaceReconstruction()
        {
            var reader = vtkPolyDataReader.New();

            reader.SetFileName(DirManage.PointDataDir + "\\points.txt");
            reader.Update();

            var points = reader.GetOutput().GetPoints();

            var polyData = vtkPolyData.New();
            polyData.SetPoints(points);

            var surfaceReconstructionFilter = vtkSurfaceReconstructionFilter.New();
            surfaceReconstructionFilter.SetInput(polyData);

            var surface = vtkContourFilter.New();
            surface.SetInputConnection(surfaceReconstructionFilter.GetOutputPort());
            surface.SetValue(0, 0.0);

            var mapper = vtkPolyDataMapper.New();
            mapper.SetInput(surface.GetOutput());


            var actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(0, 0, 1);

            var ren = RenderWindowControl.RenderWindow.GetRenderers().GetFirstRenderer();
            ren.AddActor(actor);
        }

        public static void Canvas2D()
        {
            var imgCanvas = vtkImageCanvasSource2D.New();
            //imgCanvas.SetScalarType(VTK_DOUBLE);
            imgCanvas.SetExtent(0, 500, 0, 500, 0, 0);

            imgCanvas.SetDrawColor(66.0);
            imgCanvas.FillBox(0, 500, 0, 500);
            imgCanvas.SetDrawColor(19);
            imgCanvas.FillBox(20, 300, 40, 400);
            imgCanvas.SetDrawColor(0.0);
            imgCanvas.DrawCircle(400, 360, 80.0);

            var viewer = vtkImageViewer.New();
            viewer.SetInput(imgCanvas.GetOutput());
            viewer.SetColorWindow(256);
            viewer.SetColorLevel(127.5);
            viewer.Render();
        }


        public static void PolyData2Image()
        {
            var sph = vtkSphereSource.New();
            sph.SetRadius(20);
            sph.SetPhiResolution(30);
            sph.SetThetaResolution(30);

            var pd = sph.GetOutput();
            sph.Update();


            var whiteImage = vtkImageData.New();
            var bounds = pd.GetBounds();
            double[] spacing = {0.5, 0.5, 0.5};
            whiteImage.SetSpacing(0.5, 0.5, 0.5);

            int[] dim;

            whiteImage.SetDimensions(1, 1, 1);
            whiteImage.SetExtent(0, 5, 0, 5, 0, 5);

            whiteImage.SetOrigin(0, 0, 0);
            whiteImage.SetScalarTypeToUnsignedChar();
            whiteImage.AllocateScalars();


            var inval = (char) 255;
            var outval = (char) 0;
            var count = whiteImage.GetNumberOfPoints();
            for (var i = 0; i < count; i++)
            {
                whiteImage.GetPointData().GetScalars().SetTuple1(i, inval);
            }

            var pol2stenc = vtkPolyDataToImageStencil.New();
            pol2stenc.SetInput(pd);

            pol2stenc.SetOutputOrigin(0, 0, 0);
            pol2stenc.SetOutputSpacing(0.5, 0.5, 0.5);
            pol2stenc.SetOutputWholeExtent(0, 5, 0, 5, 0, 5);
            pol2stenc.Update();

            var imgStencil = vtkImageStencil.New();
            imgStencil.SetInput(whiteImage);
            imgStencil.SetStencil(pol2stenc.GetOutput());
            imgStencil.ReverseStencilOff();
            imgStencil.SetBackgroundValue(outval);
            imgStencil.Update();

            var w = vtkMetaImageWriter.New();
            w.SetFileName("test.img");
            w.SetInput(imgStencil.GetOutput());
            w.Write();
        }

        #endregion
    }
}