namespace ReviveThis.Controls
{
  partial class UninstallManager
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UninstallManager));
      this.lstApplications = new System.Windows.Forms.ListBox();
      this.gbxGroup = new System.Windows.Forms.GroupBox();
      this.pnlMain = new System.Windows.Forms.Panel();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel3 = new System.Windows.Forms.Panel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.btnBack = new System.Windows.Forms.Button();
      this.lblCommand = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.lblName = new System.Windows.Forms.Label();
      this.btnRefresh = new System.Windows.Forms.Button();
      this.txtCommand = new System.Windows.Forms.TextBox();
      this.btnDelete = new System.Windows.Forms.Button();
      this.txtName = new System.Windows.Forms.TextBox();
      this.btnEdit = new System.Windows.Forms.Button();
      this.btnOpen = new System.Windows.Forms.Button();
      this.btnRun = new System.Windows.Forms.Button();
      this.lblCaption = new System.Windows.Forms.Label();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.muiElevated = new System.Windows.Forms.ToolStripMenuItem();
      this.gbxGroup.SuspendLayout();
      this.pnlMain.SuspendLayout();
      this.panel3.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // lstApplications
      // 
      this.lstApplications.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstApplications.FormattingEnabled = true;
      this.lstApplications.IntegralHeight = false;
      this.lstApplications.Location = new System.Drawing.Point(0, 0);
      this.lstApplications.Name = "lstApplications";
      this.lstApplications.Size = new System.Drawing.Size(418, 441);
      this.lstApplications.TabIndex = 0;
      // 
      // gbxGroup
      // 
      this.gbxGroup.Controls.Add(this.pnlMain);
      this.gbxGroup.Controls.Add(this.lblCaption);
      this.gbxGroup.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbxGroup.Location = new System.Drawing.Point(0, 0);
      this.gbxGroup.Name = "gbxGroup";
      this.gbxGroup.Padding = new System.Windows.Forms.Padding(8);
      this.gbxGroup.Size = new System.Drawing.Size(787, 510);
      this.gbxGroup.TabIndex = 1;
      this.gbxGroup.TabStop = false;
      this.gbxGroup.Text = "Add/Remove Programs Manager";
      // 
      // pnlMain
      // 
      this.pnlMain.Controls.Add(this.lstApplications);
      this.pnlMain.Controls.Add(this.splitter1);
      this.pnlMain.Controls.Add(this.panel3);
      this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pnlMain.Location = new System.Drawing.Point(8, 61);
      this.pnlMain.Name = "pnlMain";
      this.pnlMain.Size = new System.Drawing.Size(771, 441);
      this.pnlMain.TabIndex = 4;
      // 
      // splitter1
      // 
      this.splitter1.BackColor = System.Drawing.SystemColors.Control;
      this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
      this.splitter1.Location = new System.Drawing.Point(418, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 441);
      this.splitter1.TabIndex = 3;
      this.splitter1.TabStop = false;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.tableLayoutPanel2);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel3.Location = new System.Drawing.Point(421, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(350, 441);
      this.panel3.TabIndex = 2;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 6;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
      this.tableLayoutPanel2.Controls.Add(this.btnBack, 4, 5);
      this.tableLayoutPanel2.Controls.Add(this.lblCommand, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.btnSave, 2, 5);
      this.tableLayoutPanel2.Controls.Add(this.lblName, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnRefresh, 0, 5);
      this.tableLayoutPanel2.Controls.Add(this.txtCommand, 2, 1);
      this.tableLayoutPanel2.Controls.Add(this.btnDelete, 0, 2);
      this.tableLayoutPanel2.Controls.Add(this.txtName, 2, 0);
      this.tableLayoutPanel2.Controls.Add(this.btnEdit, 2, 2);
      this.tableLayoutPanel2.Controls.Add(this.btnOpen, 1, 3);
      this.tableLayoutPanel2.Controls.Add(this.btnRun, 4, 2);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 6;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(350, 441);
      this.tableLayoutPanel2.TabIndex = 7;
      // 
      // btnBack
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.btnBack, 2);
      this.btnBack.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnBack.Enabled = false;
      this.btnBack.Location = new System.Drawing.Point(235, 413);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(112, 25);
      this.btnBack.TabIndex = 9;
      this.btnBack.Text = "Back";
      this.btnBack.UseVisualStyleBackColor = true;
      // 
      // lblCommand
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.lblCommand, 2);
      this.lblCommand.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblCommand.Location = new System.Drawing.Point(3, 26);
      this.lblCommand.Name = "lblCommand";
      this.lblCommand.Size = new System.Drawing.Size(110, 26);
      this.lblCommand.TabIndex = 4;
      this.lblCommand.Text = "Uninstall Command:";
      this.lblCommand.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnSave
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.btnSave, 2);
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnSave.Location = new System.Drawing.Point(119, 413);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(110, 25);
      this.btnSave.TabIndex = 8;
      this.btnSave.Text = "Save List...";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // lblName
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.lblName, 2);
      this.lblName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblName.Location = new System.Drawing.Point(3, 0);
      this.lblName.Name = "lblName";
      this.lblName.Size = new System.Drawing.Size(110, 26);
      this.lblName.TabIndex = 2;
      this.lblName.Text = "Name:";
      this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // btnRefresh
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.btnRefresh, 2);
      this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnRefresh.Location = new System.Drawing.Point(3, 413);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(110, 25);
      this.btnRefresh.TabIndex = 7;
      this.btnRefresh.Text = "Refresh List";
      this.btnRefresh.UseVisualStyleBackColor = true;
      // 
      // txtCommand
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.txtCommand, 4);
      this.txtCommand.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtCommand.Location = new System.Drawing.Point(119, 29);
      this.txtCommand.Name = "txtCommand";
      this.txtCommand.ReadOnly = true;
      this.txtCommand.Size = new System.Drawing.Size(228, 20);
      this.txtCommand.TabIndex = 2;
      // 
      // btnDelete
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.btnDelete, 2);
      this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnDelete.Location = new System.Drawing.Point(3, 55);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(110, 25);
      this.btnDelete.TabIndex = 3;
      this.btnDelete.Text = "Delete Entry";
      this.btnDelete.UseVisualStyleBackColor = true;
      // 
      // txtName
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.txtName, 4);
      this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtName.Location = new System.Drawing.Point(119, 3);
      this.txtName.Name = "txtName";
      this.txtName.ReadOnly = true;
      this.txtName.Size = new System.Drawing.Size(228, 20);
      this.txtName.TabIndex = 1;
      // 
      // btnEdit
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.btnEdit, 2);
      this.btnEdit.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnEdit.Location = new System.Drawing.Point(119, 55);
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(110, 25);
      this.btnEdit.TabIndex = 4;
      this.btnEdit.Text = "Edit Command";
      this.btnEdit.UseVisualStyleBackColor = true;
      // 
      // btnOpen
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.btnOpen, 4);
      this.btnOpen.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnOpen.Location = new System.Drawing.Point(61, 86);
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(226, 25);
      this.btnOpen.TabIndex = 6;
      this.btnOpen.Text = "Open Programs and Features";
      this.btnOpen.UseVisualStyleBackColor = true;
      // 
      // btnRun
      // 
      this.tableLayoutPanel2.SetColumnSpan(this.btnRun, 2);
      this.btnRun.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnRun.Location = new System.Drawing.Point(235, 55);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(112, 25);
      this.btnRun.TabIndex = 5;
      this.btnRun.Text = "Execute";
      this.btnRun.UseVisualStyleBackColor = true;
      // 
      // lblCaption
      // 
      this.lblCaption.Dock = System.Windows.Forms.DockStyle.Top;
      this.lblCaption.Location = new System.Drawing.Point(8, 21);
      this.lblCaption.Name = "lblCaption";
      this.lblCaption.Size = new System.Drawing.Size(771, 40);
      this.lblCaption.TabIndex = 2;
      this.lblCaption.Text = resources.GetString("lblCaption.Text");
      this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muiElevated});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(143, 26);
      // 
      // muiElevated
      // 
      this.muiElevated.Checked = true;
      this.muiElevated.CheckOnClick = true;
      this.muiElevated.CheckState = System.Windows.Forms.CheckState.Checked;
      this.muiElevated.Name = "muiElevated";
      this.muiElevated.Size = new System.Drawing.Size(142, 22);
      this.muiElevated.Text = "Run Elevated";
      // 
      // UninstallManager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.gbxGroup);
      this.Name = "UninstallManager";
      this.Size = new System.Drawing.Size(787, 510);
      this.gbxGroup.ResumeLayout(false);
      this.pnlMain.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox lstApplications;
    private System.Windows.Forms.GroupBox gbxGroup;
    private System.Windows.Forms.Label lblCaption;
    private System.Windows.Forms.Button btnRefresh;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem muiElevated;
    private System.Windows.Forms.Panel pnlMain;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TextBox txtCommand;
    private System.Windows.Forms.Label lblCommand;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label lblName;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.Button btnOpen;
    private System.Windows.Forms.Splitter splitter1;
    private System.Windows.Forms.Button btnRun;
  }
}
