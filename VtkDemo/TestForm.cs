using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vtk;
using Aoi3dLibrary;
using Kitware.VTK;
using System.Runtime.InteropServices;


namespace VtkDemo
{
    public partial class TestForm : Form
    {
        private VtkForm vtk = new VtkForm();

        private Point rectPointStart;

        private Point rectPointEnd;

        private Rectangle rectSelected;

        public static Rectangle imageRect;

        private Graphics g;


        private bool IsLeftDown;

        public TestForm()
        {
            InitializeComponent();
        }


        private void TestForm_Load(object sender, EventArgs e)
        {
            string format = "E:/Data20150814/test{0}/Depth_Reconsutructed.txt";

            //comboBox3DModel.Items.Add(string.Format(format, 5));
            comboBox3DModel.Items.Add(string.Format(Config.RuntimeDataFormat, 1));
            comboBox3DModel.SelectedIndex = 0;

            textBoxComponentName.Text = "test";

            rectPointEnd = new Point(0, 0);
            rectPointStart = new Point(0, 0);
            rectSelected = new Rectangle(0, 0, 0, 0);
            imageRect = new Rectangle(0, 0, 0, 0);
        }

        private void buttonSetModel_Click(object sender, EventArgs e)
        {
            buttonAddModel.BackColor = Color.LightCoral;

            Application.DoEvents();

            if (comboBox3DModel.Text.Length <= 0)
            {
                MessageBox.Show("No File Selected!");
                return;
            }

            if (!File.Exists(comboBox3DModel.Text))
            {
                MessageBox.Show("File Not Exist!");
                return;
            }
            //Console.WriteLine(comboBox3DModel.Text);

            VtkControl.ReadDataFromFile(comboBox3DModel.Text);


            VtkControl.SaveImageData(comboBox3DModel.Text + ".img.bmp", VtkControl.ImageData);


            pictureBoxImage.Image = Image.FromFile(comboBox3DModel.Text + ".img.bmp");
            pictureBoxImage.SizeMode = PictureBoxSizeMode.StretchImage;


            richTextBox1.Clear();
            richTextBox1.AppendText("ImageWidth:" + VtkControl.ImageWidth + "\r\n");
            richTextBox1.AppendText("ImageHeight:" + VtkControl.ImageHeight + "\r\n");


            //Console.WriteLine("Load Model Finish.");
            buttonAddModel.BackColor = Color.LightGreen;
        }

        private void buttonAddSeries_Click(object sender, EventArgs e)
        {
            if (VtkControl.ImageData == null || VtkControl.PolyData == null)
            {
                MessageBox.Show("Please Load Model First!");
                return;
            }

            if (vtk.IsDisposed)
            {
                vtk = new VtkForm();
            }
            vtk.Show();


            imageRect = new Rectangle()
            {
                X = (int) (((float) rectSelected.Left)/pictureBoxImage.Width*VtkControl.ImageWidth),
                Y =
                    VtkControl.ImageHeight -
                    (int) (((float) rectSelected.Bottom)/pictureBoxImage.Height*VtkControl.ImageHeight),
                Width = (int) (((float) rectSelected.Width)/pictureBoxImage.Width*VtkControl.ImageWidth),
                Height = (int) (((float) rectSelected.Height)/pictureBoxImage.Height*VtkControl.ImageHeight),
            };


            if (imageRect.Width*imageRect.Height == 0)
            {
                imageRect = new Rectangle()
                {
                    X = 0,
                    Y = 0,
                    Width = VtkControl.ImageWidth,
                    Height = VtkControl.ImageHeight
                };

                VtkControl.RectPolyData = vtkPolyData.New();
                VtkControl.RectPolyData.DeepCopy(VtkControl.PolyData);
                VtkControl.RectImageData = vtkImageData.New();
                VtkControl.RectImageData.DeepCopy(VtkControl.ImageData);

                VtkControl.SetRectModel(new Rectangle(0, 0, VtkControl.ImageWidth, VtkControl.ImageHeight));
            }
            else
            {
                VtkControl.GetRectData(VtkControl.PolyData, imageRect);

                VtkControl.SetRectModel(imageRect);
            }

            vtk.renWinControl3D.RenderWindow.Render();
        }


        private void buttonSaveComponentData_Click(object sender, EventArgs e)
        {
            if (textBoxComponentName.Text.Length <= 0)
            {
                MessageBox.Show("No File Name!");
                return;
            }

            if (VtkControl.RectImageData ==null || VtkControl.RectPolyData ==null)
            {
                MessageBox.Show("No Data Selected!");
                return;
            }

            if (imageRect.Width*imageRect.Height ==0)
            {
                imageRect = new Rectangle()
                {
                    X = 0,
                    Y = 0,
                    Width = VtkControl.ImageWidth,
                    Height = VtkControl.ImageHeight
                };
            }

            VtkControl.SaveRectData(Config.RuntimeData + textBoxComponentName.Text + ".txt", VtkControl.RectPolyData, imageRect);
            VtkControl.SaveImageData(Config.RuntimeData + textBoxComponentName.Text + ".bmp", VtkControl.RectImageData);
            VtkControl.SavePolyData(Config.RuntimeData + textBoxComponentName.Text + ".poly.txt", VtkControl.RectPolyData);

            MessageBox.Show("Data Saved.");
        }

        private void comboBox3DModel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void pictureBoxImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsLeftDown == false)
            {
                IsLeftDown = true;

                rectPointStart = e.Location;
                rectPointEnd = e.Location;
                pictureBoxImage.Invalidate();
            }
        }

        private void pictureBoxImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsLeftDown)
            {
                rectPointEnd = e.Location;
                pictureBoxImage.Invalidate();
            }
        }

        private void pictureBoxImage_MouseUp(object sender, MouseEventArgs e)
        {
            IsLeftDown = false;
            pictureBoxImage.Invalidate();
        }

        private void pictureBoxImage_Paint(object sender, PaintEventArgs e)
        {
            rectSelected = new Rectangle(Math.Min(rectPointStart.X, rectPointEnd.X),
                Math.Min(rectPointStart.Y, rectPointEnd.Y),
                Math.Abs(rectPointStart.X - rectPointEnd.X), Math.Abs(rectPointStart.Y - rectPointEnd.Y));

            g = e.Graphics;
            g.DrawRectangle(new Pen(Color.White), rectSelected);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton r = (RadioButton) sender;

            if (r.Checked == false)
            {
                return;
            }

            int index = int.Parse(r.Text);


            if (!File.Exists(string.Format(Config.RuntimeDataFormat, index)))
            {
                MessageBox.Show("Data File Not Exists : " + string.Format(Config.RuntimeDataFormat, index));

                if (File.Exists(Config.RuntimeData + "black.bmp"))
                {
                    pictureBoxPreview.Image = Image.FromFile(Config.RuntimeData + "black.bmp");
                    pictureBoxPreview.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                return;
            }

            comboBox3DModel.Text = string.Format(Config.RuntimeDataFormat, index);

            if (File.Exists(comboBox3DModel.Text + ".img.bmp"))
            {
                pictureBoxPreview.Image = Image.FromFile(Config.RuntimeData + "test" + index + ".bmp");
                pictureBoxPreview.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void buttonUpdateDepth0_Click(object sender, EventArgs e)
        {

            VtkControl.UpdateDepthFormFile(@".\Data\test1\Depth0.txt");

        }

        private void buttonUpdateDepth1_Click(object sender, EventArgs e)
        {
            VtkControl.UpdateDepthFormFile(@".\Data\test1\Depth1.txt");
        }
    }
}