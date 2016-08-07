using System;
using System.Reflection;
using System.Windows.Forms;
using Kitware.VTK;
using VtkDemo;

namespace Vtk
{
    public partial class VtkForm : Form
    {
        #region VtkForm

        public VtkForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            VtkControl.RenWinControl3D = renWinControl3D;
            VtkControl.RenWinControlHorizontal = renWinControlSectionRow;
            VtkControl.RenWinControlImage = renWinControlImage;
            VtkControl.RenWinControlVertical = renWinControlSectionCol;

            //renWinControl3D.RenderWindow.GetInteractor().MiddleButtonPressEvt += renWinControl3D_DoubleClick;
            //renWinControl3D.RenderWindow.GetInteractor().LeftButtonPressEvt += renWinControl3D_Click;
            //renWinControl3D.RenderWindow.GetInteractor().StartInteractionEvt += renWinControl3D_Enter;
            //renWinControl3D.RenderWindow.GetInteractor().EndInteractionEvt += renWinControl3D_Leave;


            //VtkControl.RenWinControlImage.RenderWindow.GetInteractor().SetInteractorStyle(vtkInteractorStyleImage.New());
        }

        #endregion

        #region ToolStripMenu

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "Data";
            openFileDialog1.Filter = "vtk|*.vtk|xyz|*.xyz";
            openFileDialog1.FileName = string.Empty;

            openFileDialog1.ShowDialog();
        }

        private void convertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();

            //VtkControl.ReadDataFromFile("test.txt");
            //VtkControl.SaveData(10, "test_saved.txt");
            //MessageBox.Show("convert finish");
        }

        #endregion

        #region ToolBar

        private void toolStripButtonResetRow_Click(object sender, EventArgs e)
        {
            VtkControl.UpdateHSection(VtkControl.HSectionNum = TestForm.imageRect.Height/2, TestForm.imageRect);
            VtkControl.UpdateSectionImage(TestForm.imageRect);
        }

        private void toolStripButtonResetCol_Click(object sender, EventArgs e)
        {
            VtkControl.UpdateVSection(VtkControl.VSectionNum = TestForm.imageRect.Width/2, TestForm.imageRect);
            VtkControl.UpdateSectionImage(TestForm.imageRect);
        }

        private void toolStripButtonSectionRowInc_Click(object sender, EventArgs e)
        {
            //VtkControl.UpdateHSection(VtkControl.HSectionNum+=10);
            VtkControl.UpdateHSection(VtkControl.HSectionNum += 5, TestForm.imageRect);
            VtkControl.UpdateSectionImage(TestForm.imageRect);
        }


        private void toolStripButtonSectionRowDec_Click(object sender, EventArgs e)
        {
            //VtkControl.UpdateHSection(VtkControl.HSectionNum -= 10);
            VtkControl.UpdateHSection(VtkControl.HSectionNum -= 5, TestForm.imageRect);
            VtkControl.UpdateSectionImage(TestForm.imageRect);

        }

        private void toolStripButtonSectionColInc_Click(object sender, EventArgs e)
        {
            //VtkControl.UpdateVSection(VtkControl.VSectionNum += 10);
            VtkControl.UpdateVSection(VtkControl.VSectionNum += 5, TestForm.imageRect);
            VtkControl.UpdateSectionImage(TestForm.imageRect);
        }

        private void toolStripButtonSectionColDec_Click(object sender, EventArgs e)
        {
            //VtkControl.UpdateVSection(VtkControl.VSectionNum -= 10);
            VtkControl.UpdateVSection(VtkControl.VSectionNum -= 5, TestForm.imageRect);
            VtkControl.UpdateSectionImage(TestForm.imageRect);
        }

        #endregion

        private void colorByHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VtkControl.SetColorByHeight(VtkControl.RectPolyData, TestForm.imageRect);

            renWinControl3D.RenderWindow.Render();
        }

        private void colorByImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VtkControl.SetColorByImage(VtkControl.RectPolyData, VtkControl.RectImageData, TestForm.imageRect);

            renWinControl3D.RenderWindow.Render();
        }

        private void meshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VtkControl.SetDelaunay2D(VtkControl.RectPolyData);

            renWinControl3D.RenderWindow.Render();
        }

        private void medianFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VtkControl.SetMedianFilter(ref VtkControl.RectPolyData, TestForm.imageRect);

            VtkControl.SetVertexGlyph(VtkControl.RectPolyData);

            VtkControl.UpdateHSection(VtkControl.HSectionNum, TestForm.imageRect);

            VtkControl.UpdateVSection(VtkControl.VSectionNum, TestForm.imageRect);


            renWinControl3D.RenderWindow.Render();
        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VtkControl.SetVertexGlyph(VtkControl.RectPolyData);

            renWinControl3D.RenderWindow.Render();
        }

        private void orgPolyDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VtkControl.SetVertexGlyph(VtkControl.RectPolyData);

            renWinControl3D.RenderWindow.Render();
        }

        private void toolStripButtonColorImage_Click(object sender, EventArgs e)
        {
            VtkControl.SetColorByImage(VtkControl.RectPolyData, VtkControl.RectImageData, TestForm.imageRect);

            renWinControl3D.RenderWindow.Render();
        }

        private void toolStripButtonColorHeight_Click(object sender, EventArgs e)
        {
            VtkControl.SetColorByHeight(VtkControl.RectPolyData, TestForm.imageRect);

            renWinControl3D.RenderWindow.Render();
        }

        private void toolStripButtonDelaunay_Click(object sender, EventArgs e)
        {
            VtkControl.SetDelaunay2D(VtkControl.RectPolyData);

            renWinControl3D.RenderWindow.Render();
        }

        private void toolStripButtonVertex_Click(object sender, EventArgs e)
        {
            VtkControl.SetVertexGlyph(VtkControl.RectPolyData);

            renWinControl3D.RenderWindow.Render();
        }

    }
}