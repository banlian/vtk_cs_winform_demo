namespace Vtk
{
    partial class VtkForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VtkForm));
            this.renWinControl3D = new Kitware.VTK.RenderWindowControl();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonResetRow = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResetCol = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSectionRowInc = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSectionRowDec = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSectionColInc = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSectionColDec = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonColorHeight = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonColorImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelaunay = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonVertex = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.renWinControlSectionCol = new Kitware.VTK.RenderWindowControl();
            this.renWinControlImage = new Kitware.VTK.RenderWindowControl();
            this.renWinControlSectionRow = new Kitware.VTK.RenderWindowControl();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // renWinControl3D
            // 
            this.renWinControl3D.AddTestActors = false;
            this.renWinControl3D.BackColor = System.Drawing.Color.Black;
            this.renWinControl3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renWinControl3D.Location = new System.Drawing.Point(472, 3);
            this.renWinControl3D.Name = "renWinControl3D";
            this.renWinControl3D.Size = new System.Drawing.Size(464, 210);
            this.renWinControl3D.TabIndex = 0;
            this.renWinControl3D.TestText = null;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgress});
            this.statusStrip.Location = new System.Drawing.Point(0, 457);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(939, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size(0, 17);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonResetRow,
            this.toolStripButtonResetCol,
            this.toolStripButtonSectionRowInc,
            this.toolStripButtonSectionRowDec,
            this.toolStripButtonSectionColInc,
            this.toolStripButtonSectionColDec,
            this.toolStripButtonColorHeight,
            this.toolStripButtonColorImage,
            this.toolStripButtonDelaunay,
            this.toolStripButtonVertex});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(939, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonResetRow
            // 
            this.toolStripButtonResetRow.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResetRow.Image")));
            this.toolStripButtonResetRow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResetRow.Name = "toolStripButtonResetRow";
            this.toolStripButtonResetRow.Size = new System.Drawing.Size(69, 22);
            this.toolStripButtonResetRow.Text = "ResetH";
            this.toolStripButtonResetRow.Click += new System.EventHandler(this.toolStripButtonResetRow_Click);
            // 
            // toolStripButtonResetCol
            // 
            this.toolStripButtonResetCol.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResetCol.Image")));
            this.toolStripButtonResetCol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResetCol.Name = "toolStripButtonResetCol";
            this.toolStripButtonResetCol.Size = new System.Drawing.Size(68, 22);
            this.toolStripButtonResetCol.Text = "ResetV";
            this.toolStripButtonResetCol.Click += new System.EventHandler(this.toolStripButtonResetCol_Click);
            // 
            // toolStripButtonSectionRowInc
            // 
            this.toolStripButtonSectionRowInc.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSectionRowInc.Image")));
            this.toolStripButtonSectionRowInc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSectionRowInc.Name = "toolStripButtonSectionRowInc";
            this.toolStripButtonSectionRowInc.Size = new System.Drawing.Size(55, 22);
            this.toolStripButtonSectionRowInc.Text = "H++";
            this.toolStripButtonSectionRowInc.Click += new System.EventHandler(this.toolStripButtonSectionRowInc_Click);
            // 
            // toolStripButtonSectionRowDec
            // 
            this.toolStripButtonSectionRowDec.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSectionRowDec.Image")));
            this.toolStripButtonSectionRowDec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSectionRowDec.Name = "toolStripButtonSectionRowDec";
            this.toolStripButtonSectionRowDec.Size = new System.Drawing.Size(47, 22);
            this.toolStripButtonSectionRowDec.Text = "H--";
            this.toolStripButtonSectionRowDec.Click += new System.EventHandler(this.toolStripButtonSectionRowDec_Click);
            // 
            // toolStripButtonSectionColInc
            // 
            this.toolStripButtonSectionColInc.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSectionColInc.Image")));
            this.toolStripButtonSectionColInc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSectionColInc.Name = "toolStripButtonSectionColInc";
            this.toolStripButtonSectionColInc.Size = new System.Drawing.Size(54, 22);
            this.toolStripButtonSectionColInc.Text = "V++";
            this.toolStripButtonSectionColInc.Click += new System.EventHandler(this.toolStripButtonSectionColInc_Click);
            // 
            // toolStripButtonSectionColDec
            // 
            this.toolStripButtonSectionColDec.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSectionColDec.Image")));
            this.toolStripButtonSectionColDec.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSectionColDec.Name = "toolStripButtonSectionColDec";
            this.toolStripButtonSectionColDec.Size = new System.Drawing.Size(46, 22);
            this.toolStripButtonSectionColDec.Text = "V--";
            this.toolStripButtonSectionColDec.Click += new System.EventHandler(this.toolStripButtonSectionColDec_Click);
            // 
            // toolStripButtonColorHeight
            // 
            this.toolStripButtonColorHeight.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonColorHeight.Image")));
            this.toolStripButtonColorHeight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonColorHeight.Name = "toolStripButtonColorHeight";
            this.toolStripButtonColorHeight.Size = new System.Drawing.Size(98, 22);
            this.toolStripButtonColorHeight.Text = "ColorHeight";
            this.toolStripButtonColorHeight.Click += new System.EventHandler(this.toolStripButtonColorHeight_Click);
            // 
            // toolStripButtonColorImage
            // 
            this.toolStripButtonColorImage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonColorImage.Image")));
            this.toolStripButtonColorImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonColorImage.Name = "toolStripButtonColorImage";
            this.toolStripButtonColorImage.Size = new System.Drawing.Size(97, 22);
            this.toolStripButtonColorImage.Text = "ColorImage";
            this.toolStripButtonColorImage.Click += new System.EventHandler(this.toolStripButtonColorImage_Click);
            // 
            // toolStripButtonDelaunay
            // 
            this.toolStripButtonDelaunay.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelaunay.Image")));
            this.toolStripButtonDelaunay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelaunay.Name = "toolStripButtonDelaunay";
            this.toolStripButtonDelaunay.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonDelaunay.Text = "Delaunay";
            this.toolStripButtonDelaunay.Click += new System.EventHandler(this.toolStripButtonDelaunay_Click);
            // 
            // toolStripButtonVertex
            // 
            this.toolStripButtonVertex.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonVertex.Image")));
            this.toolStripButtonVertex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonVertex.Name = "toolStripButtonVertex";
            this.toolStripButtonVertex.Size = new System.Drawing.Size(65, 22);
            this.toolStripButtonVertex.Text = "Vertex";
            this.toolStripButtonVertex.Click += new System.EventHandler(this.toolStripButtonVertex_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.renWinControlSectionCol, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.renWinControlImage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.renWinControl3D, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.renWinControlSectionRow, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(939, 432);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // renWinControlSectionCol
            // 
            this.renWinControlSectionCol.AddTestActors = false;
            this.renWinControlSectionCol.BackColor = System.Drawing.Color.Black;
            this.renWinControlSectionCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renWinControlSectionCol.Location = new System.Drawing.Point(472, 219);
            this.renWinControlSectionCol.Name = "renWinControlSectionCol";
            this.renWinControlSectionCol.Size = new System.Drawing.Size(464, 210);
            this.renWinControlSectionCol.TabIndex = 3;
            this.renWinControlSectionCol.TestText = null;
            // 
            // renWinControlImage
            // 
            this.renWinControlImage.AddTestActors = false;
            this.renWinControlImage.BackColor = System.Drawing.Color.Black;
            this.renWinControlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renWinControlImage.Location = new System.Drawing.Point(3, 219);
            this.renWinControlImage.Name = "renWinControlImage";
            this.renWinControlImage.Size = new System.Drawing.Size(463, 210);
            this.renWinControlImage.TabIndex = 2;
            this.renWinControlImage.TestText = null;
            // 
            // renWinControlSectionRow
            // 
            this.renWinControlSectionRow.AddTestActors = false;
            this.renWinControlSectionRow.BackColor = System.Drawing.Color.Black;
            this.renWinControlSectionRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renWinControlSectionRow.Location = new System.Drawing.Point(3, 3);
            this.renWinControlSectionRow.Name = "renWinControlSectionRow";
            this.renWinControlSectionRow.Size = new System.Drawing.Size(463, 210);
            this.renWinControlSectionRow.TabIndex = 1;
            this.renWinControlSectionRow.TestText = null;
            // 
            // VtkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 479);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Name = "VtkForm";
            this.Text = "MODEL VIEWER";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonResetRow;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Kitware.VTK.RenderWindowControl renWinControlSectionCol;
        private Kitware.VTK.RenderWindowControl renWinControlImage;
        private Kitware.VTK.RenderWindowControl renWinControlSectionRow;
        private System.Windows.Forms.ToolStripStatusLabel statusProgress;
        private System.Windows.Forms.ToolStripButton toolStripButtonResetCol;
        private System.Windows.Forms.ToolStripButton toolStripButtonSectionRowInc;
        private System.Windows.Forms.ToolStripButton toolStripButtonSectionRowDec;
        private System.Windows.Forms.ToolStripButton toolStripButtonSectionColInc;
        private System.Windows.Forms.ToolStripButton toolStripButtonSectionColDec;
        private System.Windows.Forms.ToolStripButton toolStripButtonColorHeight;
        private System.Windows.Forms.ToolStripButton toolStripButtonColorImage;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelaunay;
        private System.Windows.Forms.ToolStripButton toolStripButtonVertex;
        public Kitware.VTK.RenderWindowControl renWinControl3D;

    }
}

