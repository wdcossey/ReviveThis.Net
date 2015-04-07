namespace ReviveThis.Forms
{
  partial class FrmMain
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.addinManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.betterListView1 = new ComponentOwl.BetterListView.BetterListView();
      this.betterListViewColumnHeader1 = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
      this.colImage = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
      this.colInformation = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
      this.iglStatus = new System.Windows.Forms.ImageList(this.components);
      this.button3 = new System.Windows.Forms.Button();
      this.cmsAnalyse = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.uninstallManager1 = new ReviveThis.Controls.UninstallManager();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.betterListView1)).BeginInit();
      this.cmsAnalyse.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 34);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "&Scan";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(93, 34);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(75, 23);
      this.button2.TabIndex = 2;
      this.button2.Text = "Modules";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(650, 24);
      this.menuStrip1.TabIndex = 3;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(131, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // toolsToolStripMenuItem
      // 
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addinManagerToolStripMenuItem});
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
      this.toolsToolStripMenuItem.Text = "&Tools";
      // 
      // addinManagerToolStripMenuItem
      // 
      this.addinManagerToolStripMenuItem.Name = "addinManagerToolStripMenuItem";
      this.addinManagerToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
      this.addinManagerToolStripMenuItem.Text = "&Add-In Manager...";
      this.addinManagerToolStripMenuItem.Click += new System.EventHandler(this.addinManagerToolStripMenuItem_Click);
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
      this.aboutToolStripMenuItem.Text = "&About";
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
      // 
      // betterListView1
      // 
      this.betterListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.betterListView1.CheckBoxes = ComponentOwl.BetterListView.BetterListViewCheckBoxes.TwoState;
      this.betterListView1.Columns.Add(this.betterListViewColumnHeader1);
      this.betterListView1.Columns.Add(this.colImage);
      this.betterListView1.Columns.Add(this.colInformation);
      this.betterListView1.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.Disable;
      this.betterListView1.ImageList = this.iglStatus;
      this.betterListView1.Location = new System.Drawing.Point(12, 63);
      this.betterListView1.Name = "betterListView1";
      this.betterListView1.ShowDefaultGroupHeader = false;
      this.betterListView1.ShowGroups = true;
      this.betterListView1.ShowToolTips = true;
      this.betterListView1.ShowToolTipsColumns = true;
      this.betterListView1.ShowToolTipsGroups = true;
      this.betterListView1.Size = new System.Drawing.Size(626, 346);
      this.betterListView1.TabIndex = 5;
      this.betterListView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.betterListView1_MouseClick);
      this.betterListView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.betterListView1_MouseDoubleClick);
      // 
      // betterListViewColumnHeader1
      // 
      this.betterListViewColumnHeader1.Name = "betterListViewColumnHeader1";
      this.betterListViewColumnHeader1.Text = "Results";
      this.betterListViewColumnHeader1.Width = 500;
      // 
      // colImage
      // 
      this.colImage.AlignHorizontal = ComponentOwl.BetterListView.TextAlignmentHorizontal.Center;
      this.colImage.AllowResize = false;
      this.colImage.Name = "colImage";
      this.colImage.Style = ComponentOwl.BetterListView.BetterListViewColumnHeaderStyle.Nonclickable;
      this.colImage.Width = 28;
      // 
      // colInformation
      // 
      this.colInformation.Name = "colInformation";
      this.colInformation.Text = "Information";
      // 
      // iglStatus
      // 
      this.iglStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iglStatus.ImageStream")));
      this.iglStatus.TransparentColor = System.Drawing.Color.Transparent;
      this.iglStatus.Images.SetKeyName(0, "cross_octagon.png");
      this.iglStatus.Images.SetKeyName(1, "exclamation_octagon_fram.png");
      this.iglStatus.Images.SetKeyName(2, "accept.png");
      this.iglStatus.Images.SetKeyName(3, "help.png");
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(174, 34);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(75, 23);
      this.button3.TabIndex = 6;
      this.button3.Text = "Analyse";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // cmsAnalyse
      // 
      this.cmsAnalyse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
      this.cmsAnalyse.Name = "cmsAnalyse";
      this.cmsAnalyse.Size = new System.Drawing.Size(97, 26);
      // 
      // testToolStripMenuItem
      // 
      this.testToolStripMenuItem.Name = "testToolStripMenuItem";
      this.testToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
      this.testToolStripMenuItem.Text = "&Test";
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(21, 72);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(594, 337);
      this.tabControl1.TabIndex = 7;
      // 
      // tabPage1
      // 
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(586, 311);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "tabPage1";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.uninstallManager1);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(586, 311);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // uninstallManager1
      // 
      this.uninstallManager1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.uninstallManager1.Location = new System.Drawing.Point(3, 3);
      this.uninstallManager1.Name = "uninstallManager1";
      this.uninstallManager1.Size = new System.Drawing.Size(580, 305);
      this.uninstallManager1.TabIndex = 0;
      // 
      // FrmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(650, 421);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.betterListView1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "FrmMain";
      this.Text = "ReviveThis.Net";
      this.Load += new System.EventHandler(this.FrmMain_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.betterListView1)).EndInit();
      this.cmsAnalyse.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addinManagerToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private ComponentOwl.BetterListView.BetterListView betterListView1;
    private ComponentOwl.BetterListView.BetterListViewColumnHeader colResult;
    private System.Windows.Forms.Button button3;
    private ComponentOwl.BetterListView.BetterListViewColumnHeader colImage;
    private System.Windows.Forms.ImageList iglStatus;
    private ComponentOwl.BetterListView.BetterListViewColumnHeader colInformation;
    private ComponentOwl.BetterListView.BetterListViewColumnHeader betterListViewColumnHeader1;
    private System.Windows.Forms.ContextMenuStrip cmsAnalyse;
    private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private Controls.UninstallManager uninstallManager1;






  }
}

