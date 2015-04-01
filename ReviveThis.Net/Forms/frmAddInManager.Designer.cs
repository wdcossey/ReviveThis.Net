namespace ReviveThis.Forms
{
  partial class FrmAddInManager
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.betterListView1 = new ComponentOwl.BetterListView.BetterListView();
      this.betterListViewColumnHeader1 = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
      this.betterListViewColumnHeader2 = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
      this.betterListViewColumnHeader3 = new ComponentOwl.BetterListView.BetterListViewColumnHeader();
      this.lblDescription = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.betterListView1)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this.betterListView1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.lblDescription, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 2);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 8);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(568, 445);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // betterListView1
      // 
      this.betterListView1.CheckBoxes = ComponentOwl.BetterListView.BetterListViewCheckBoxes.TwoState;
      this.betterListView1.Columns.AddRange(new object[] {
            this.betterListViewColumnHeader1,
            this.betterListViewColumnHeader2,
            this.betterListViewColumnHeader3});
      this.tableLayoutPanel1.SetColumnSpan(this.betterListView1, 2);
      this.betterListView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.betterListView1.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.Disable;
      this.betterListView1.Location = new System.Drawing.Point(3, 3);
      this.betterListView1.Name = "betterListView1";
      this.betterListView1.ShowDefaultGroupHeader = false;
      this.betterListView1.ShowGroups = true;
      this.betterListView1.ShowToolTips = true;
      this.betterListView1.Size = new System.Drawing.Size(562, 312);
      this.betterListView1.TabIndex = 0;
      this.betterListView1.SelectedIndexChanged += new System.EventHandler(this.betterListView1_SelectedIndexChanged);
      // 
      // betterListViewColumnHeader1
      // 
      this.betterListViewColumnHeader1.Name = "betterListViewColumnHeader1";
      this.betterListViewColumnHeader1.Text = "Name";
      this.betterListViewColumnHeader1.Width = 200;
      // 
      // betterListViewColumnHeader2
      // 
      this.betterListViewColumnHeader2.AlignHorizontal = ComponentOwl.BetterListView.TextAlignmentHorizontal.Center;
      this.betterListViewColumnHeader2.Name = "betterListViewColumnHeader2";
      this.betterListViewColumnHeader2.Text = "Version";
      this.betterListViewColumnHeader2.Width = 120;
      // 
      // betterListViewColumnHeader3
      // 
      this.betterListViewColumnHeader3.Name = "betterListViewColumnHeader3";
      this.betterListViewColumnHeader3.Text = "Author";
      this.betterListViewColumnHeader3.Width = 200;
      // 
      // lblDescription
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.lblDescription, 2);
      this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblDescription.Location = new System.Drawing.Point(3, 318);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(562, 21);
      this.lblDescription.TabIndex = 1;
      this.lblDescription.Text = "Description:";
      this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // textBox1
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 2);
      this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.textBox1.Location = new System.Drawing.Point(3, 342);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBox1.Size = new System.Drawing.Size(562, 100);
      this.textBox1.TabIndex = 1;
      // 
      // FrmAddInManager
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 461);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "FrmAddInManager";
      this.Padding = new System.Windows.Forms.Padding(8);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add-In Manager";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.betterListView1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Label lblDescription;
    private ComponentOwl.BetterListView.BetterListViewColumnHeader betterListViewColumnHeader1;
    private ComponentOwl.BetterListView.BetterListViewColumnHeader betterListViewColumnHeader2;
    private ComponentOwl.BetterListView.BetterListViewColumnHeader betterListViewColumnHeader3;
    private ComponentOwl.BetterListView.BetterListView betterListView1;
    private System.Windows.Forms.TextBox textBox1;
  }
}