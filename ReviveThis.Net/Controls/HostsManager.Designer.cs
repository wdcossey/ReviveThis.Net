namespace ReviveThis.Controls
{
  partial class HostsManager
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
      this.lstHosts = new System.Windows.Forms.ListBox();
      this.gbxGroup = new System.Windows.Forms.GroupBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.lblCaption = new System.Windows.Forms.Label();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnToggle = new System.Windows.Forms.Button();
      this.btnNotepad = new System.Windows.Forms.Button();
      this.cmsNotepad = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.muiElevated = new System.Windows.Forms.ToolStripMenuItem();
      this.btnBack = new System.Windows.Forms.Button();
      this.gbxGroup.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.cmsNotepad.SuspendLayout();
      this.SuspendLayout();
      // 
      // lstHosts
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.lstHosts, 5);
      this.lstHosts.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstHosts.FormattingEnabled = true;
      this.lstHosts.IntegralHeight = false;
      this.lstHosts.Location = new System.Drawing.Point(3, 24);
      this.lstHosts.Name = "lstHosts";
      this.lstHosts.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lstHosts.Size = new System.Drawing.Size(547, 268);
      this.lstHosts.TabIndex = 0;
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
      this.gbxGroup.Text = "Hosts File Manager";
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 5;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Controls.Add(this.btnDelete, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.lblCaption, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.lstHosts, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.btnToggle, 1, 3);
      this.tableLayoutPanel1.Controls.Add(this.btnNotepad, 2, 3);
      this.tableLayoutPanel1.Controls.Add(this.btnBack, 3, 3);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 21);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(553, 347);
      this.tableLayoutPanel1.TabIndex = 4;
      // 
      // label1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.label1, 5);
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 295);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(547, 21);
      this.label1.TabIndex = 3;
      this.label1.Text = "Note: changes to the hosts file take effect when you restart your browser.";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblCaption
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.lblCaption, 5);
      this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblCaption.Location = new System.Drawing.Point(3, 0);
      this.lblCaption.Name = "lblCaption";
      this.lblCaption.Size = new System.Drawing.Size(547, 21);
      this.lblCaption.TabIndex = 2;
      this.lblCaption.Text = "Hosts file located at: C:\\WINDOWS\\hosts";
      this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnDelete
      // 
      this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnDelete.Location = new System.Drawing.Point(3, 319);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(100, 25);
      this.btnDelete.TabIndex = 1;
      this.btnDelete.Text = "Delete line(s)";
      this.btnDelete.UseVisualStyleBackColor = true;
      // 
      // btnToggle
      // 
      this.btnToggle.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnToggle.Location = new System.Drawing.Point(109, 319);
      this.btnToggle.Name = "btnToggle";
      this.btnToggle.Size = new System.Drawing.Size(100, 25);
      this.btnToggle.TabIndex = 2;
      this.btnToggle.Text = "Toggle line(s)";
      this.btnToggle.UseVisualStyleBackColor = true;
      // 
      // btnNotepad
      // 
      this.btnNotepad.AutoSize = true;
      this.btnNotepad.ContextMenuStrip = this.cmsNotepad;
      this.btnNotepad.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnNotepad.Location = new System.Drawing.Point(215, 319);
      this.btnNotepad.Name = "btnNotepad";
      this.btnNotepad.Size = new System.Drawing.Size(116, 25);
      //this.btnNotepad.SplitMenuStrip = this.cmsNotepad;
      this.btnNotepad.TabIndex = 3;
      this.btnNotepad.Text = "Open in Notepad";
      this.btnNotepad.UseVisualStyleBackColor = true;
      // 
      // cmsNotepad
      // 
      this.cmsNotepad.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muiElevated});
      this.cmsNotepad.Name = "contextMenuStrip1";
      this.cmsNotepad.Size = new System.Drawing.Size(153, 48);
      // 
      // muiElevated
      // 
      this.muiElevated.Name = "muiElevated";
      this.muiElevated.Size = new System.Drawing.Size(142, 22);
      this.muiElevated.Text = "Run Elevated";
      // 
      // btnBack
      // 
      this.btnBack.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnBack.Enabled = false;
      this.btnBack.Location = new System.Drawing.Point(337, 319);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(75, 25);
      this.btnBack.TabIndex = 4;
      this.btnBack.Text = "Back";
      this.btnBack.UseVisualStyleBackColor = true;
      // 
      // HostsManager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.gbxGroup);
      this.Name = "HostsManager";
      this.Size = new System.Drawing.Size(569, 376);
      this.gbxGroup.ResumeLayout(false);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.cmsNotepad.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListBox lstHosts;
    private System.Windows.Forms.GroupBox gbxGroup;
    private System.Windows.Forms.Label lblCaption;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Button btnToggle;
    private System.Windows.Forms.Button btnNotepad;
    private System.Windows.Forms.ContextMenuStrip cmsNotepad;
    private System.Windows.Forms.ToolStripMenuItem muiElevated;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
  }
}
