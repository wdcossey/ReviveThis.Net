namespace ReviveThis.Controls
{
  partial class ServicesManager
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServicesManager));
      this.gbxGroup = new System.Windows.Forms.GroupBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.cmsNotepad = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.muiElevated = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
      this.tsbExport = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.tsbStart = new System.Windows.Forms.ToolStripButton();
      this.tsbStop = new System.Windows.Forms.ToolStripButton();
      this.tsbPause = new System.Windows.Forms.ToolStripButton();
      this.tsbReStart = new System.Windows.Forms.ToolStripButton();
      this.ltvServices = new System.Windows.Forms.ListView();
      this.colImage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.colStartupType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.gbxGroup.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.cmsNotepad.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // gbxGroup
      // 
      this.gbxGroup.Controls.Add(this.tableLayoutPanel1);
      this.gbxGroup.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbxGroup.Location = new System.Drawing.Point(0, 0);
      this.gbxGroup.Name = "gbxGroup";
      this.gbxGroup.Padding = new System.Windows.Forms.Padding(8);
      this.gbxGroup.Size = new System.Drawing.Size(569, 376);
      this.gbxGroup.TabIndex = 1;
      this.gbxGroup.TabStop = false;
      this.gbxGroup.Text = "Windows® Services Manager";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 5;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.ltvServices, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 21);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(553, 347);
      this.tableLayoutPanel1.TabIndex = 4;
      // 
      // cmsNotepad
      // 
      this.cmsNotepad.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muiElevated});
      this.cmsNotepad.Name = "contextMenuStrip1";
      this.cmsNotepad.Size = new System.Drawing.Size(143, 26);
      // 
      // muiElevated
      // 
      this.muiElevated.Name = "muiElevated";
      this.muiElevated.Size = new System.Drawing.Size(142, 22);
      this.muiElevated.Text = "Run Elevated";
      // 
      // toolStrip1
      // 
      this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
      this.tableLayoutPanel1.SetColumnSpan(this.toolStrip1, 5);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbExport,
            this.toolStripSeparator1,
            this.tsbStart,
            this.tsbStop,
            this.tsbPause,
            this.tsbReStart});
      this.toolStrip1.Location = new System.Drawing.Point(0, 0);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(553, 25);
      this.toolStrip1.TabIndex = 5;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // tsbRefresh
      // 
      this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
      this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbRefresh.Name = "tsbRefresh";
      this.tsbRefresh.Size = new System.Drawing.Size(23, 22);
      this.tsbRefresh.Text = "Refresh";
      this.tsbRefresh.ToolTipText = "Refresh Services List";
      // 
      // tsbExport
      // 
      this.tsbExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbExport.Image = ((System.Drawing.Image)(resources.GetObject("tsbExport.Image")));
      this.tsbExport.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbExport.Name = "tsbExport";
      this.tsbExport.Size = new System.Drawing.Size(23, 22);
      this.tsbExport.Text = "Export";
      this.tsbExport.ToolTipText = "Export Services List";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
      // 
      // tsbStart
      // 
      this.tsbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbStart.Image = ((System.Drawing.Image)(resources.GetObject("tsbStart.Image")));
      this.tsbStart.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbStart.Name = "tsbStart";
      this.tsbStart.Size = new System.Drawing.Size(23, 22);
      this.tsbStart.Text = "Start";
      this.tsbStart.ToolTipText = "Start Service";
      // 
      // tsbStop
      // 
      this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
      this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbStop.Name = "tsbStop";
      this.tsbStop.Size = new System.Drawing.Size(23, 22);
      this.tsbStop.Text = "Stop";
      this.tsbStop.ToolTipText = "Stop Service";
      // 
      // tsbPause
      // 
      this.tsbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbPause.Image = ((System.Drawing.Image)(resources.GetObject("tsbPause.Image")));
      this.tsbPause.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbPause.Name = "tsbPause";
      this.tsbPause.Size = new System.Drawing.Size(23, 22);
      this.tsbPause.Text = "Pause";
      this.tsbPause.ToolTipText = "Pause Service";
      // 
      // tsbReStart
      // 
      this.tsbReStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tsbReStart.Image = ((System.Drawing.Image)(resources.GetObject("tsbReStart.Image")));
      this.tsbReStart.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.tsbReStart.Name = "tsbReStart";
      this.tsbReStart.Size = new System.Drawing.Size(23, 22);
      this.tsbReStart.Text = "Restart";
      this.tsbReStart.ToolTipText = "Restart Service";
      // 
      // ltvServices
      // 
      this.ltvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colImage,
            this.colName,
            this.colFileName,
            this.colStartupType});
      this.tableLayoutPanel1.SetColumnSpan(this.ltvServices, 5);
      this.ltvServices.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ltvServices.FullRowSelect = true;
      this.ltvServices.HideSelection = false;
      this.ltvServices.Location = new System.Drawing.Point(3, 28);
      this.ltvServices.Name = "ltvServices";
      this.ltvServices.Size = new System.Drawing.Size(547, 316);
      this.ltvServices.TabIndex = 6;
      this.ltvServices.UseCompatibleStateImageBehavior = false;
      this.ltvServices.View = System.Windows.Forms.View.Details;
      // 
      // colImage
      // 
      this.colImage.Text = "";
      this.colImage.Width = 25;
      // 
      // colName
      // 
      this.colName.Text = "Name";
      this.colName.Width = 140;
      // 
      // colFileName
      // 
      this.colFileName.Text = "File Name";
      this.colFileName.Width = 260;
      // 
      // colStartupType
      // 
      this.colStartupType.Text = "Startup Type";
      this.colStartupType.Width = 120;
      // 
      // ServicesManager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.gbxGroup);
      this.Name = "ServicesManager";
      this.Size = new System.Drawing.Size(569, 376);
      this.gbxGroup.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.cmsNotepad.ResumeLayout(false);
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbxGroup;
    private System.Windows.Forms.ContextMenuStrip cmsNotepad;
    private System.Windows.Forms.ToolStripMenuItem muiElevated;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton tsbRefresh;
    private System.Windows.Forms.ToolStripButton tsbExport;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton tsbStart;
    private System.Windows.Forms.ToolStripButton tsbStop;
    private System.Windows.Forms.ToolStripButton tsbPause;
    private System.Windows.Forms.ToolStripButton tsbReStart;
    private System.Windows.Forms.ListView ltvServices;
    private System.Windows.Forms.ColumnHeader colImage;
    private System.Windows.Forms.ColumnHeader colName;
    private System.Windows.Forms.ColumnHeader colFileName;
    private System.Windows.Forms.ColumnHeader colStartupType;
  }
}
