namespace Vtk
{
    partial class VtkUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.renWinControlSectionCol = new Kitware.VTK.RenderWindowControl();
            this.renWinControlImage = new Kitware.VTK.RenderWindowControl();
            this.renWinControlModel = new Kitware.VTK.RenderWindowControl();
            this.renWinControlSectionRow = new Kitware.VTK.RenderWindowControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(799, 474);
            this.splitContainer1.SplitterDistance = 683;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.renWinControlSectionCol, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.renWinControlImage, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.renWinControlModel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.renWinControlSectionRow, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(683, 474);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // renWinControlSectionCol
            // 
            this.renWinControlSectionCol.AddTestActors = false;
            this.renWinControlSectionCol.BackColor = System.Drawing.Color.Black;
            this.renWinControlSectionCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renWinControlSectionCol.Location = new System.Drawing.Point(344, 240);
            this.renWinControlSectionCol.Name = "renWinControlSectionCol";
            this.renWinControlSectionCol.Size = new System.Drawing.Size(336, 231);
            this.renWinControlSectionCol.TabIndex = 3;
            this.renWinControlSectionCol.TestText = null;
            // 
            // renWinControlImage
            // 
            this.renWinControlImage.AddTestActors = false;
            this.renWinControlImage.BackColor = System.Drawing.Color.Black;
            this.renWinControlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renWinControlImage.Location = new System.Drawing.Point(3, 240);
            this.renWinControlImage.Name = "renWinControlImage";
            this.renWinControlImage.Size = new System.Drawing.Size(335, 231);
            this.renWinControlImage.TabIndex = 2;
            this.renWinControlImage.TestText = null;
            // 
            // renWinControlModel
            // 
            this.renWinControlModel.AddTestActors = false;
            this.renWinControlModel.BackColor = System.Drawing.Color.Black;
            this.renWinControlModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renWinControlModel.Location = new System.Drawing.Point(344, 3);
            this.renWinControlModel.Name = "renWinControlModel";
            this.renWinControlModel.Size = new System.Drawing.Size(336, 231);
            this.renWinControlModel.TabIndex = 1;
            this.renWinControlModel.TestText = null;
            // 
            // renWinControlSectionRow
            // 
            this.renWinControlSectionRow.AddTestActors = false;
            this.renWinControlSectionRow.BackColor = System.Drawing.Color.Black;
            this.renWinControlSectionRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renWinControlSectionRow.Location = new System.Drawing.Point(3, 3);
            this.renWinControlSectionRow.Name = "renWinControlSectionRow";
            this.renWinControlSectionRow.Size = new System.Drawing.Size(335, 231);
            this.renWinControlSectionRow.TabIndex = 0;
            this.renWinControlSectionRow.TestText = null;
            // 
            // VtkUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "VtkUserControl";
            this.Size = new System.Drawing.Size(799, 474);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Kitware.VTK.RenderWindowControl renWinControlSectionCol;
        private Kitware.VTK.RenderWindowControl renWinControlImage;
        private Kitware.VTK.RenderWindowControl renWinControlModel;
        private Kitware.VTK.RenderWindowControl renWinControlSectionRow;
    }
}
