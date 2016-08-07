using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Kitware.VTK;

namespace Vtk
{
    public class VtkControl
    {
        #region RenderWindow

        public static RenderWindowControl RenWinControl3D;
        public static RenderWindowControl RenWinControlImage;
        public static RenderWindowControl RenWinControlVertical;
        public static RenderWindowControl RenWinControlHorizontal;

        #endregion

        #region Image Params

        public static int ImageWidth = 2040;
        public static int ImageHeight = 2040;

        #endregion

        #region Section Data

        public static int HSectionNum;
        public static int VSectionNum;

        public static vtkTable HTable;
        public static vtkFloatArray HArray;
        public static vtkFloatArray HZArray;
        public static vtkChartXY HChart;
        public static vtkContextView HView;

        public static vtkTable VTable;
        public static vtkFloatArray VArray;
        public static vtkFloatArray VZArray;
        public static vtkChartXY VChart;
        public static vtkContextView VView;

        #endregion

        #region Data

        public static vtkImageViewer2 ImageViewer;

        public static vtkPolyData PolyData;
        public static vtkImageData ImageData;

        public static Rectangle ImageRect;
        public static vtkPolyData RectPolyData;
        public static vtkImageData RectImageData;
        public static vtkImageData SectionImageData;

        #endregion

        #region Load and Save Data

        public static void ReadDataFromFile(string filename)
        {
            try
            {
                SimplePointFile.GetImageInfo(filename);

                ImageWidth = SimplePointFile.ImageWidth;
                ImageHeight = SimplePointFile.ImageHeight;

                //Console.WriteLine("ImageWidth:" + ImageWidth);
                //Console.WriteLine("ImageHeight:" + ImageHeight);

                var points = vtkPoints.New();

                var colorArray = vtkUnsignedCharArray.New();
                colorArray.SetNumberOfComponents(3);

                ImageData = vtkImageData.New();
                ImageData.SetExtent(0, ImageWidth - 1, 0, ImageHeight - 1, 0, 0);
                ImageData.SetNumberOfScalarComponents(1);
                ImageData.SetScalarTypeToUnsignedChar();


                SimplePointFile.OpenReadFile(filename);
                double[] data;
                while ((data = SimplePointFile.ReadLine()) != null)
                {
                    points.InsertNextPoint(data[0], data[1], data[2]);
                    colorArray.InsertNextTuple3(data[3], data[4], data[5]);

                    ImageData.SetScalarComponentFromDouble((int) data[0], (int) data[1], 0, 0, data[3]);
                }

                PolyData = vtkPolyData.New();
                PolyData.SetPoints(points);
                PolyData.GetPointData().SetScalars(colorArray);
                PolyData.Update();

                //Console.WriteLine("PolyData & ImageData Load.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                SimplePointFile.CloseReadFile();
            }
        }

        public static void UpdateDepthFormFile(string filename)
        {
          
            var points = vtkPoints.New();

            SimplePointFile.OpenReadFile(filename);
            double[] data;
            while ((data = SimplePointFile.ReadShortLine()) != null)
            {
                points.InsertNextPoint(data[0], data[1], data[2]);
            }

            PolyData.SetPoints(points);
            PolyData.Update();

            SimplePointFile.CloseReadFile();
            
        }

        public static void SaveData(int ratio, string filename)
        {
            try
            {
                SimplePointFile.OpenWriteFile(filename);

                var color = PolyData.GetPointData().GetScalars();

                for (var i = 0; i < ImageHeight; i += ratio)
                {
                    for (var j = 0; j < ImageWidth; j += ratio)
                    {
                        var data = PolyData.GetPoint(i*ImageWidth + j);
                        var c = color.GetComponent(i*ImageWidth + j, 0);
                        SimplePointFile.Writeline(new[] {data[0]/ratio, data[1]/ratio, data[2], c, c, c});
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                SimplePointFile.CloseWriteFile();
            }
        }


        public static void SaveRectData(string filename, vtkPolyData polyData, Rectangle rect)
        {
            try
            {
                SimplePointFile.OpenWriteFile(filename);

                var color = polyData.GetPointData().GetScalars();

                for (var i = 0; i < rect.Height; i++)
                {
                    for (var j = 0; j < rect.Width; j++)
                    {
                        var data = polyData.GetPoint(i*rect.Width + j);
                        var c = color.GetComponent(i*rect.Width + j, 0);

                        SimplePointFile.Writeline(new[] {data[0], data[1], data[2], c, c, c});
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                SimplePointFile.CloseWriteFile();
            }
        }


        public static void SavePolyData(string filename, vtkPolyData polydata)
        {
            var w = vtkXMLPolyDataWriter.New();
            w.SetInput(polydata);
            w.SetFileName(filename);
            w.Update();
        }

        public static void SaveImageData(string imgname, vtkImageData imageData)
        {
            var bmpWriter = vtkBMPWriter.New();
            bmpWriter.SetInput(imageData);
            bmpWriter.SetFileName(imgname);
            bmpWriter.Update();
        }

        #endregion

        #region

        #region PolyDataPoint Methods

        public static void SetMedianFilter(ref vtkPolyData polyData, Rectangle rect)
        {
            var image = vtkImageData.New();
            image.SetExtent(0, rect.Width - 1, 0, rect.Height - 1, 0, 0);
            image.SetNumberOfScalarComponents(1);
            image.SetScalarTypeToDouble();

            for (int i = 0; i < rect.Width*rect.Height; i++)
            {
                double[] d = polyData.GetPoint(i);
                image.SetScalarComponentFromDouble(i%rect.Width, i/rect.Width, 0, 0, d[2]);
            }
            image.Update();


            var cast = vtkImageCast.New();
            cast.SetInput(image);
            cast.SetOutputScalarTypeToUnsignedChar();
            cast.Update();

            var w = vtkBMPWriter.New();
            w.SetInput(cast.GetOutput());
            w.SetFileName("imgcastbyheight_beforefilter.bmp");
            w.Update();

            var medianFilter = vtkImageGaussianSmooth.New();
            medianFilter.SetInput(image);
            medianFilter.SetDimensionality(2);
            medianFilter.SetRadiusFactor(10);
            medianFilter.SetStandardDeviation(3);
            medianFilter.Update();
            //var medianFilter = vtkImageHybridMedian2D.New();
            //medianFilter.SetInput(image);
            //medianFilter.Update();

            var cast2 = vtkImageCast.New();
            cast2.SetInput(medianFilter.GetOutput());
            cast2.SetOutputScalarTypeToUnsignedChar();
            cast2.Update();

            var w2 = vtkBMPWriter.New();
            w2.SetInput(cast2.GetOutput());
            w2.SetFileName("imgcastbyheight_afterfilter.bmp");
            w2.Update();

            for (int i = 0; i < rect.Height; i++)
            {
                for (int j = 0; j < rect.Width; j++)
                {
                    double d = medianFilter.GetOutput().GetScalarComponentAsFloat(j, i, 0, 0);
                    polyData.GetPoints().SetPoint(i*rect.Width + j, j, i, (int) d);
                }
            }

            polyData.Update();
        }

        public static void SetVertexGlyph(vtkPolyData polyData)
        {
            var filter = vtkVertexGlyphFilter.New();
            filter.SetInput(polyData);
            filter.Update();

            var polyDataMapper = vtkPolyDataMapper.New();
            polyDataMapper.SetInputConnection(filter.GetOutputPort());
            polyDataMapper.SetScalarModeToDefault();
            polyDataMapper.SetScalarRange(0, 255);
            polyDataMapper.SetScalarVisibility(1);
            polyDataMapper.Update();

            var polyDataActor = vtkActor.New();
            polyDataActor.SetMapper(polyDataMapper);
            polyDataActor.GetProperty().SetPointSize(1.5f);
            polyDataActor.SetPosition(-polyDataActor.GetCenter()[0], -polyDataActor.GetCenter()[1],
                -polyDataActor.GetCenter()[2]);

            var polyDataRenderer = RenWinControl3D.RenderWindow.GetRenderers().GetFirstRenderer();
            polyDataRenderer.RemoveAllViewProps();
            polyDataRenderer.AddActor(polyDataActor);
            polyDataRenderer.SetBackground(1, 1, 1);
            polyDataRenderer.ResetCamera();
            polyDataRenderer.Render();
        }

        public static void SetDelaunay2D(vtkPolyData polyData)
        {
            var delaunay = vtkDelaunay2D.New();
            delaunay.SetTolerance(0.001);
            delaunay.SetInput(polyData);
            delaunay.Update();

            var triDataMapper = vtkPolyDataMapper.New();
            triDataMapper.SetInputConnection(delaunay.GetOutputPort());
            triDataMapper.Update();

            var polyDataActor = vtkActor.New();
            polyDataActor.SetMapper(triDataMapper);
            polyDataActor.SetPosition(-polyDataActor.GetCenter()[0], -polyDataActor.GetCenter()[1],
                -polyDataActor.GetCenter()[2]);

            var polyDataRenderer = RenWinControl3D.RenderWindow.GetRenderers().GetFirstRenderer();
            polyDataRenderer.RemoveAllViewProps();
            polyDataRenderer.LightFollowCameraOn();
            polyDataRenderer.SetBackground(1, 1, 1);
            polyDataRenderer.SetAmbient(1, 1, 1);
            polyDataRenderer.AddActor(polyDataActor);
            polyDataRenderer.ResetCamera();
            polyDataRenderer.Render();
        }

        #endregion

        #region ImageViewer Methods

        public static void SetImageView(vtkImageData image)
        {
            if (image == null)
            {
                image = vtkImageData.New();
            }

            if (ImageViewer == null)
            {
                ImageViewer = vtkImageViewer2.New();
            }

            ImageViewer.SetInput(image);
            ImageViewer.GetRenderer().SetBackground(1, 1, 1);

            RenWinControlImage.RenderWindow.GetRenderers().GetFirstRenderer().RemoveAllViewProps();

            ImageViewer.SetRenderWindow(RenWinControlImage.RenderWindow);
            ImageViewer.GetRenderer().ResetCamera();

            RenWinControlImage.RenderWindow.GetInteractor().SetInteractorStyle(new vtkInteractorStyleImage());
            RenWinControlImage.RenderWindow.Render();
        }

        public static void UpdateSectionImage(Rectangle imageRect)
        {
            if (SectionImageData == null)
            {
                SectionImageData = vtkImageData.New();
            }
            SectionImageData.DeepCopy(RectImageData);

            for (int i = 0; i < imageRect.Width; i++)
            {
                SectionImageData.GetPointData().GetScalars().SetTuple1(HSectionNum*imageRect.Width + i, 255);
            }

            for (int i = 0; i < imageRect.Height; i++)
            {
                SectionImageData.GetPointData().GetScalars().SetTuple1(i*imageRect.Width + VSectionNum, 255);
            }

            SectionImageData.Update();

            UpdateImageView();
        }

        public static void UpdateImageView()
        {
            if (ImageViewer != null)
            {
                ImageViewer.SetInput(SectionImageData);
                ImageViewer.Render();
            }
        }

        #endregion

        #region Section Methods

        public static void SetHSectionPoints()
        {
            HZArray = vtkFloatArray.New();
            HZArray.SetName("HZArray");
            HZArray.SetNumberOfComponents(1);

            HArray = vtkFloatArray.New();
            HArray.SetName("HArray");
            HArray.SetNumberOfComponents(1);

            for (var i = 0; i < ImageWidth; ++i)
            {
                HArray.InsertNextTuple1(i);
                HZArray.InsertNextTuple1(PolyData.GetPoint(HSectionNum*ImageWidth + i)[2]);
            }
        }

        public static void SetHSectionPoints(Rectangle rect)
        {
            HZArray = vtkFloatArray.New();
            HZArray.SetName("HZArray");
            HZArray.SetNumberOfComponents(1);

            HArray = vtkFloatArray.New();
            HArray.SetName("HArray");
            HArray.SetNumberOfComponents(1);

            HSectionNum = rect.Height/2;

            for (var i = 0; i < rect.Width; ++i)
            {
                HArray.InsertNextTuple1(i);
                HZArray.InsertNextTuple1(RectPolyData.GetPoint(HSectionNum*rect.Width + i)[2]);
            }
        }

        public static void SetVSectionPoints()
        {
            VArray = vtkFloatArray.New();
            VArray.SetName("VArray");
            VArray.SetNumberOfComponents(1);

            VZArray = vtkFloatArray.New();
            VZArray.SetName("VZArray");
            VZArray.SetNumberOfComponents(1);


            for (var i = 0; i < ImageHeight; i++)
            {
                VArray.InsertNextTuple1(i);
                VZArray.InsertNextTuple1(PolyData.GetPoint(VSectionNum + i*ImageWidth)[2]);
            }
        }

        public static void SetVSectionPoints(Rectangle rect)
        {
            VArray = vtkFloatArray.New();
            VArray.SetName("VArray");
            VArray.SetNumberOfComponents(1);

            VZArray = vtkFloatArray.New();
            VZArray.SetName("VZArray");
            VZArray.SetNumberOfComponents(1);

            VSectionNum = rect.Width/2;

            for (var i = 0; i < rect.Height; i++)
            {
                VArray.InsertNextTuple1(i);
                VZArray.InsertNextTuple1(RectPolyData.GetPoint(VSectionNum + i*rect.Width)[2]);
            }
        }

        public static void UpdateHSection(int h)
        {
            if (h < 0 || h >= ImageHeight)
            {
                MessageBox.Show("Row Num is Over Range");
                return;
            }

            for (var i = 0; i < ImageWidth; ++i)
            {
                HZArray.SetTuple1(i, PolyData.GetPoint(h*ImageWidth + i)[2]);
            }

            HTable.Modified();

            RenWinControlHorizontal.RenderWindow.Render();
        }

        public static void UpdateHSection(int h, Rectangle rect)
        {
            if (h < 0 || h >= rect.Height)
            {
                MessageBox.Show("Row Num is Over Range");
                return;
            }

            for (var i = 0; i < rect.Width; ++i)
            {
                HZArray.SetTuple1(i, RectPolyData.GetPoint(h*rect.Width + i)[2]);
            }

            HTable.Modified();
            HView.Render();

            //RenWinControlHorizontal.RenderWindow.Render();
        }

        public static void UpdateVSection(int v)
        {
            if (v < 0 || v >= ImageWidth)
            {
                MessageBox.Show("Col Num is Over Range");
                return;
            }

            for (var i = 0; i < ImageHeight; i++)
            {
                VZArray.SetTuple1(i, PolyData.GetPoint(v + i*ImageWidth)[2]);
            }

            VTable.Modified();

            RenWinControlVertical.RenderWindow.Render();
        }

        public static void UpdateVSection(int v, Rectangle rect)
        {
            if (v < 0 || v >= rect.Width)
            {
                MessageBox.Show("Col Num is Over Range");
                return;
            }

            for (var i = 0; i < rect.Height; i++)
            {
                VZArray.SetTuple1(i, RectPolyData.GetPoint(v + i*rect.Width)[2]);
            }

            VTable.Modified();
            VView.Render();

            //RenWinControlVertical.RenderWindow.Render();
        }

        public static void SetHChart()
        {
            if (HArray == null || HZArray == null)
            {
                MessageBox.Show("array is null");
                return;
            }

            HTable = vtkTable.New();
            HTable.AddColumn(HArray);
            HTable.AddColumn(HZArray);

            HChart = vtkChartXY.New();
            var line = HChart.AddPlot(0);
            line.SetInput(HTable, 0, 1);

            HView = vtkContextView.New();
            HView.GetScene().AddItem(HChart);

            RenWinControlHorizontal.RenderWindow.GetRenderers().GetFirstRenderer().RemoveAllViewProps();

            HView.SetRenderWindow(RenWinControlHorizontal.RenderWindow);
            RenWinControlHorizontal.RenderWindow.Render();
        }

        public static void SetVChart()
        {
            if (VZArray == null || VArray == null)
            {
                MessageBox.Show("array is null");
                return;
            }

            VTable = vtkTable.New();
            VTable.AddColumn(VZArray);
            VTable.AddColumn(VArray);

            VChart = vtkChartXY.New();
            var line = VChart.AddPlot(0);
            line.SetInput(VTable, 0, 1);

            VView = vtkContextView.New();
            VView.GetScene().AddItem(VChart);

            RenWinControlVertical.RenderWindow.GetRenderers().GetFirstRenderer().RemoveAllViewProps();

            VView.SetRenderWindow(RenWinControlVertical.RenderWindow);
            RenWinControlVertical.RenderWindow.Render();
        }

        #endregion

        public static void Set3DModel()
        {
            SetVertexGlyph(PolyData);


            SetImageView(ImageData);

            SetHSectionPoints();
            SetVSectionPoints();

            SetHChart();
            SetVChart();
        }


        public static void SetRectModel(Rectangle rect)
        {
            SetVertexGlyph(RectPolyData);

            ImageRect = rect;
            HSectionNum = rect.Height/2;
            VSectionNum = rect.Width/2;

            UpdateSectionImage(rect);
            SetImageView(SectionImageData);

            SetHSectionPoints(rect);
            SetVSectionPoints(rect);

            SetHChart();
            SetVChart();
        }

        #endregion

        #region Rect Data Color Mapper

        public static void SetColorByHeight(vtkPolyData data, Rectangle rect)
        {
            double zmin = data.GetPoint(0)[2];
            double zmax = zmin;
            for (int i = 0; i < rect.Width*rect.Height; i++)
            {
                double z = data.GetPoint(i)[2];
                if (z < zmin)
                {
                    zmin = z;
                }

                if (z > zmax)
                {
                    zmax = z;
                }
            }

            //Console.WriteLine("Zmin:" + zmin + " Zmax:" + zmax);

            vtkColorTransferFunction colorFunction = vtkColorTransferFunction.New();
            colorFunction.AddRGBPoint(zmin, 0, 0, 1);
            colorFunction.AddRGBPoint(zmax, 1, 0, 0);

            //Console.WriteLine("Color:" + colorFunction.GetColor(0)[0] + " "
            //                  + colorFunction.GetColor(0)[1] + " "
            //                  + colorFunction.GetColor(0)[2]);

            for (int i = 0; i < rect.Width*rect.Height; i++)
            {
                double z = data.GetPoint(i)[2];
                double[] c = colorFunction.GetColor(z);
                data.GetPointData().GetScalars().SetTuple3(i, c[0]*255, c[1]*255, c[2]*255);
            }

            data.Modified();
        }

        public static void SetColorByImage(vtkPolyData data, vtkImageData image, Rectangle rect)
        {
            for (int i = 0; i < rect.Width*rect.Height; i++)
            {
                float c = image.GetScalarComponentAsFloat(i%rect.Width, i/rect.Width, 0, 0);
                data.GetPointData().GetScalars().SetTuple3(i, c, c, c);
            }

            data.Modified();
        }

        #endregion

        #region Rect Data

        public static void GetRectData(vtkPolyData data, Rectangle rect)
        {
            var points = vtkPoints.New();
            var colorArray = vtkUnsignedCharArray.New();
            colorArray.SetNumberOfComponents(3);

            RectImageData = vtkImageData.New();
            RectImageData.SetExtent(0, rect.Size.Width - 1, 0, rect.Size.Height - 1, 0, 0);
            RectImageData.SetNumberOfScalarComponents(1);
            RectImageData.SetScalarTypeToUnsignedChar();

            for (var i = rect.Top; i < rect.Bottom; i++)
            {
                for (var j = rect.Left; j < rect.Right; j++)
                {
                    double[] p = data.GetPoint(i*ImageWidth + j);
                    points.InsertNextPoint(j - rect.Left, i - rect.Top, p[2]);

                    double[] c = data.GetPointData().GetScalars().GetTuple3(i*ImageWidth + j);
                    colorArray.InsertNextTuple3(c[0], c[1], c[2]);

                    RectImageData.SetScalarComponentFromDouble(j - rect.Left, i - rect.Top, 0, 0, c[0]);
                }
            }


            RectPolyData = vtkPolyData.New();
            RectPolyData.SetPoints(points);
            RectPolyData.GetPointData().SetScalars(colorArray);
            RectPolyData.Update();
        }

        #endregion
    }
}