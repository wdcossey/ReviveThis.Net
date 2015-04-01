using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PSTaskDialog
{
  public partial class FrmTaskDialog : Form
  {
    #region PRIVATE consts

    private const int DEFAULT_PADDING = 8;

    #endregion
    //--------------------------------------------------------------------------------
    #region PRIVATE members
    //--------------------------------------------------------------------------------
    eSysIcons _mMainIcon = eSysIcons.Question;
    eSysIcons _mFooterIcon = eSysIcons.Warning;

    string _mMainInstruction = "Main Instruction Text";
    int _mMainInstructionHeight;
    readonly Font _mMainInstructionFont = new Font("Arial", 11.75F, FontStyle.Regular, GraphicsUnit.Point, 0);

    readonly List<RadioButton> _mRadioButtonCtrls = new List<RadioButton>();
    string _mRadioButtons = "";
    int _mInitialRadioButtonIndex;

    //List<Button> _mCmdButtons = new List<Button>();
    string _mCommandButtons;
    int _mCommandButtonClicked = -1;

    int _mDefaultButtonIndex;
    Control _mFocusControl;

    eTaskDialogButtons _mButtons = eTaskDialogButtons.YesNoCancel;

    bool _mExpanded;
    readonly bool _mIsVista;
    #endregion

    //--------------------------------------------------------------------------------
    #region PROPERTIES
    //--------------------------------------------------------------------------------
    public eSysIcons MainIcon { get { return _mMainIcon; } set { _mMainIcon = value; } }
    public eSysIcons FooterIcon { get { return _mFooterIcon; } set { _mFooterIcon = value; } }

    public string Title
    {
      get { return Text; }
      set { Text = value; }
    }

    public string MainInstruction
    {
      get { return _mMainInstruction; }
      set
      {
        _mMainInstruction = value;
        Invalidate();
      }
    }

    public string Content
    {
      get { return lbContent.Text; }
      set { lbContent.Text = value; }
    }

    public string ExpandedInfo
    {
      get { return lbExpandedInfo.Text; }
      set { lbExpandedInfo.Text = value; }
    }

    public string Footer
    {
      get { return lbFooter.Text; }
      set { lbFooter.Text = value; }
    }

    public int DefaultButtonIndex
    {
      get { return _mDefaultButtonIndex; }
      set { _mDefaultButtonIndex = value; }
    }

    public string RadioButtons
    {
      get { return _mRadioButtons; }
      set { _mRadioButtons = value; }
    }

// ReSharper disable once ConvertToAutoProperty
    public int InitialRadioButtonIndex
    {
      get { return _mInitialRadioButtonIndex; }
      set { _mInitialRadioButtonIndex = value; }
    }

    public int RadioButtonIndex
    {
      get
      {
        //foreach (var rb in _mRadioButtonCtrls.Where(rb => rb.Checked))
        //  return (int)rb.Tag;
        //  return -1;
        var first = _mRadioButtonCtrls.FirstOrDefault(rb => rb.Checked);
        return first != null ? (int) first.Tag : -1;
      }
    }

    public string CommandButtons { get { return _mCommandButtons; } set { _mCommandButtons = value; } }
    public int CommandButtonClickedIndex { get { return _mCommandButtonClicked; } }

    public eTaskDialogButtons Buttons { get { return _mButtons; } set { _mButtons = value; } }

    public string VerificationText { get { return cbVerify.Text; } set { cbVerify.Text = value; } }
    public bool VerificationCheckBoxChecked { get { return cbVerify.Checked; } set { cbVerify.Checked = value; } }

    public bool Expanded { get { return _mExpanded; } set { _mExpanded = value; } }
    #endregion

    //--------------------------------------------------------------------------------
    #region CONSTRUCTOR
    //--------------------------------------------------------------------------------
    public FrmTaskDialog()
    {
      InitializeComponent();

      _mIsVista = VistaTaskDialog.IsAvailableOnThisOS;
      if (!_mIsVista && cTaskDialog.UseToolWindowOnXP) // <- shall we use the smaller toolbar?
          FormBorderStyle = FormBorderStyle.FixedToolWindow;

      MainInstruction = "Main Instruction";
      Content = "";
      ExpandedInfo = "";
      Footer = "";
      VerificationText = "";
    }
    #endregion 

    //--------------------------------------------------------------------------------
    #region BuildForm
    // This is the main routine that should be called before .ShowDialog()
    //--------------------------------------------------------------------------------
    bool _mFormBuilt;
    public void BuildForm()
    {
      var formHeight = 0;

      // Setup Main Instruction
      switch (_mMainIcon)
      {
        case eSysIcons.Information: imgMain.Image = SystemIcons.Information.ToBitmap(); break;
        case eSysIcons.Question: imgMain.Image = SystemIcons.Question.ToBitmap(); break;
        case eSysIcons.Warning: imgMain.Image = SystemIcons.Warning.ToBitmap(); break;
        case eSysIcons.Error: imgMain.Image = SystemIcons.Error.ToBitmap(); break;
      }

      //AdjustLabelHeight(lbMainInstruction);
      //pnlMainInstruction.Height = Math.Max(41, lbMainInstruction.Height + 16);
      if (_mMainInstructionHeight == 0)
        GetMainInstructionTextSizeF();
      pnlMainInstruction.Height = Math.Max(41, _mMainInstructionHeight + 16);

      formHeight += pnlMainInstruction.Height;

      #region Setup Content
      var setupContent = !string.IsNullOrEmpty(Content);
      pnlContent.Visible = setupContent;
      if (setupContent)
      {
        AdjustLabelHeight(lbContent);
        pnlContent.Height = lbContent.Height + 4;
        formHeight += pnlContent.Height;
      }
      #endregion

      var showVerifyCheckbox = (cbVerify.Text != "");
      cbVerify.Visible = showVerifyCheckbox;

      #region Setup Expanded Info and Buttons panels
      if (string.IsNullOrEmpty(ExpandedInfo))
      {
        pnlExpandedInfo.Visible = false;
        lbShowHideDetails.Visible = false;
        cbVerify.Top = 12;
        pnlButtons.Height = 40;
      }
      else
      {
        AdjustLabelHeight(lbExpandedInfo);
        pnlExpandedInfo.Height = lbExpandedInfo.Height + 4;
        pnlExpandedInfo.Visible = _mExpanded;
        lbShowHideDetails.Text = (_mExpanded ? "        Hide details" : "        Show details");
        lbShowHideDetails.ImageIndex = (_mExpanded ? 0 : 3);
        if (!showVerifyCheckbox)
          pnlButtons.Height = 40;
        if (_mExpanded)
          formHeight += pnlExpandedInfo.Height;
      }
      #endregion

      #region Setup RadioButtons
      var setupRadioButtons = !string.IsNullOrEmpty(_mRadioButtons);
      pnlRadioButtons.Visible = setupRadioButtons;
      if (setupRadioButtons)
      {
        var pnlHeight = 12;

        int[] i = { 0 };

        foreach (var btn in _mRadioButtons.Split(new[] { '|' }).Select(text =>
          new RadioButton
          {
            Text = text,
            Parent = pnlRadioButtons,
            Tag = i[0],
          }))
        {
          btn.Location = new Point(60, 4 + (i[0] * btn.Height));
          btn.Checked = (_mDefaultButtonIndex == i[0]);
          btn.Width = ClientSize.Width - btn.Left - DEFAULT_PADDING;
          pnlHeight += btn.Height;
          _mRadioButtonCtrls.Add(btn);
          i[0]++;
        }

        pnlRadioButtons.Height = pnlHeight;
        formHeight += pnlRadioButtons.Height;
      }
      #endregion

      #region Setup CommandButtons
      var setupCommandButtons = !string.IsNullOrEmpty(_mCommandButtons);
      pnlCommandButtons.Visible = setupCommandButtons;
      if (setupCommandButtons)
      {
        var pnlHeight = 16;
        int[] t = { DEFAULT_PADDING };
        int[] i = { 0 };

        foreach (var btn in _mCommandButtons.Split(new[] {'|'}).Select(text =>
          new CommandButton
          {
            Parent = pnlCommandButtons,
            Location = new Point(50, t[0]),
            Text = text,
            Tag = i[0],
          }))
        {
          if (_mIsVista) // <- tweak font if vista
            btn.Font = new Font(btn.Font, FontStyle.Regular);

          btn.Size = new Size(ClientSize.Width - btn.Left - DEFAULT_PADDING, btn.GetBestHeight());
          t[0] += btn.Height;
          pnlHeight += btn.Height;

          btn.Click += CommandButton_Click;
          if (i[0] == _mDefaultButtonIndex)
            _mFocusControl = btn;

          i[0]++;
        }

        pnlCommandButtons.Height = pnlHeight;
        formHeight += pnlCommandButtons.Height;
      }
      #endregion

      // Setup Buttons
      switch (_mButtons)
      {
        case eTaskDialogButtons.YesNo:
          bt1.Visible = false;
          bt2.Text = "&Yes";
          bt2.DialogResult = DialogResult.Yes;
          bt3.Text = "&No";
          bt3.DialogResult = DialogResult.No;
          AcceptButton = bt2;
          CancelButton = bt3;
          break;
        case eTaskDialogButtons.YesNoCancel:
          bt1.Text = "&Yes";
          bt1.DialogResult = DialogResult.Yes;
          bt2.Text = "&No";
          bt2.DialogResult = DialogResult.No;
          bt3.Text = "&Cancel";
          bt3.DialogResult = DialogResult.Cancel;
          AcceptButton = bt1;
          CancelButton = bt3;
          break;
        case eTaskDialogButtons.OKCancel:
          bt1.Visible = false;
          bt2.Text = "&OK";
          bt2.DialogResult = DialogResult.OK;
          bt3.Text = "&Cancel";
          bt3.DialogResult = DialogResult.Cancel;
          AcceptButton = bt2;
          CancelButton = bt3;
          break;
        case eTaskDialogButtons.OK:
          bt1.Visible = false;
          bt2.Visible = false;
          bt3.Text = "&OK";
          bt3.DialogResult = DialogResult.OK;
          AcceptButton = bt3;
          CancelButton = bt3;
          break;
        case eTaskDialogButtons.Close:
          bt1.Visible = false;
          bt2.Visible = false;
          bt3.Text = "&Close";
          bt3.DialogResult = DialogResult.Cancel;
          CancelButton = bt3;
          break;
        case eTaskDialogButtons.Cancel:
          bt1.Visible = false;
          bt2.Visible = false;
          bt3.Text = "&Cancel";
          bt3.DialogResult = DialogResult.Cancel;
          CancelButton = bt3;
          break;
        case eTaskDialogButtons.None:
          bt1.Visible = false;
          bt2.Visible = false;
          bt3.Visible = false;
          break;
      }

      ControlBox = (Buttons == eTaskDialogButtons.Cancel ||
                         Buttons == eTaskDialogButtons.Close ||
                         Buttons == eTaskDialogButtons.OKCancel ||
                         Buttons == eTaskDialogButtons.YesNoCancel);

      if (!showVerifyCheckbox && ExpandedInfo == "" && _mButtons == eTaskDialogButtons.None)
        pnlButtons.Visible = false;
      else
        formHeight += pnlButtons.Height;

      pnlFooter.Visible = (Footer != "");
      if (Footer != "")
      {
        AdjustLabelHeight(lbFooter);
        pnlFooter.Height = Math.Max(28, lbFooter.Height + 16);
        switch (_mFooterIcon)
        {
          case eSysIcons.Information:
            // SystemIcons.Information.ToBitmap().GetThumbnailImage(16, 16, null, IntPtr.Zero);
            imgFooter.Image = ResizeBitmap(SystemIcons.Information.ToBitmap(), 16, 16);
            break;
          case eSysIcons.Question:
            // SystemIcons.Question.ToBitmap().GetThumbnailImage(16, 16, null, IntPtr.Zero);
            imgFooter.Image = ResizeBitmap(SystemIcons.Question.ToBitmap(), 16, 16);
            break;
          case eSysIcons.Warning:
            // SystemIcons.Warning.ToBitmap().GetThumbnailImage(16, 16, null, IntPtr.Zero);
            imgFooter.Image = ResizeBitmap(SystemIcons.Warning.ToBitmap(), 16, 16);
            break;
          case eSysIcons.Error:
            // SystemIcons.Error.ToBitmap().GetThumbnailImage(16, 16, null, IntPtr.Zero);
            imgFooter.Image = ResizeBitmap(SystemIcons.Error.ToBitmap(), 16, 16);
            break;
        }
        formHeight += pnlFooter.Height;
      }

      ClientSize = new Size(ClientSize.Width, formHeight);

      _mFormBuilt = true;
    }

    //--------------------------------------------------------------------------------
    static Image ResizeBitmap(Image srcImg, int newWidth, int newHeight)
    {
      var percentWidth = (newWidth / (float)srcImg.Width);
      var percentHeight = (newHeight / (float)srcImg.Height);

      var resizePercent = (percentHeight < percentWidth ? percentHeight : percentWidth);

      var w = (int)(srcImg.Width * resizePercent);
      var h = (int)(srcImg.Height * resizePercent);
      var b = new Bitmap(w, h);
      
      using (var g = Graphics.FromImage(b))
      {
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        g.DrawImage(srcImg, 0, 0, w, h);
      }
      return b;
    }
    
    //--------------------------------------------------------------------------------
    // utility function for setting a Label's height
    static void AdjustLabelHeight(Label lb)
    {
      var text = lb.Text;
      var textFont = lb.Font;
      var layoutSize = new SizeF(lb.ClientSize.Width, 5000.0F);
      using (var g = Graphics.FromHwnd(lb.Handle))
      {
        var stringSize = g.MeasureString(text, textFont, layoutSize);
        lb.Height = (int) stringSize.Height + 4;
      }
    }
    #endregion

    //--------------------------------------------------------------------------------
    #region EVENTS
    //--------------------------------------------------------------------------------
    void CommandButton_Click(object sender, EventArgs e)
    {
     	_mCommandButtonClicked = (int)((CommandButton)sender).Tag;
      DialogResult = DialogResult.OK;
    }

    //--------------------------------------------------------------------------------
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
    }

    //--------------------------------------------------------------------------------
    protected override void OnShown(EventArgs e)
    {
      if (!_mFormBuilt)
        throw new Exception("frmTaskDialog : Please call .BuildForm() before showing the TaskDialog");
      base.OnShown(e);
    }

    //--------------------------------------------------------------------------------
    private void lbDetails_MouseEnter(object sender, EventArgs e)
    {
      lbShowHideDetails.ImageIndex = (_mExpanded ? 1 : 4);
    }

    //--------------------------------------------------------------------------------
    private void lbDetails_MouseLeave(object sender, EventArgs e)
    {
      lbShowHideDetails.ImageIndex = (_mExpanded ? 0 : 3);
    }

    //--------------------------------------------------------------------------------
    private void lbDetails_MouseUp(object sender, MouseEventArgs e)
    {
      lbShowHideDetails.ImageIndex = (_mExpanded ? 1 : 4);
    }

    //--------------------------------------------------------------------------------
    private void lbDetails_MouseDown(object sender, MouseEventArgs e)
    {
      lbShowHideDetails.ImageIndex =(_mExpanded ? 2 : 5);
    }

    private void RepositionWindow()
    {
      var workingArea = Screen.GetWorkingArea(this);
      if (Top + Height > workingArea.Height)
      {
        Top -= Top + Height - workingArea.Height;
      }
      if (Left + Width > workingArea.Width)
      {
        Left -= Left + Width - workingArea.Width;
      } 
      if (Top < 0)
      {
        Top = 0;
      }
      if (Left < 0)
      {
        Left = 0;
      }
    }
    //--------------------------------------------------------------------------------
    private void lbDetails_Click(object sender, EventArgs e)
    {
      _mExpanded = !_mExpanded;
      pnlExpandedInfo.Visible = _mExpanded;
      lbShowHideDetails.Text = (_mExpanded ? "        Hide details" : "        Show details");
      if (_mExpanded)
        Height += pnlExpandedInfo.Height;
      else
        Height -= pnlExpandedInfo.Height;

      RepositionWindow();
    }

    //--------------------------------------------------------------------------------
    const int MAIN_INSTRUCTION_LEFT_MARGIN = 46;
    const int MAIN_INSTRUCTION_RIGHT_MARGIN = 8;

    SizeF GetMainInstructionTextSizeF()
    {
      var mzSize = new SizeF(pnlMainInstruction.Width - MAIN_INSTRUCTION_LEFT_MARGIN - MAIN_INSTRUCTION_RIGHT_MARGIN, 5000.0F);
      using (var g = Graphics.FromHwnd(Handle))
      {
        var textSize = g.MeasureString(_mMainInstruction, _mMainInstructionFont, mzSize);
        _mMainInstructionHeight = (int) textSize.Height;
        return textSize;
      }
    }

    private void pnlMainInstruction_Paint(object sender, PaintEventArgs e)
    {
      var szL = GetMainInstructionTextSizeF();
      e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
      e.Graphics.DrawString(_mMainInstruction, _mMainInstructionFont, new SolidBrush(Color.DarkBlue), new RectangleF(new PointF(MAIN_INSTRUCTION_LEFT_MARGIN, 10), szL));
    }

    //--------------------------------------------------------------------------------
    private void frmTaskDialog_Shown(object sender, EventArgs e)
    {
      if (cTaskDialog.PlaySystemSounds)
      {
        switch (_mMainIcon)
        {
          case eSysIcons.Error: System.Media.SystemSounds.Hand.Play(); break;
          case eSysIcons.Information: System.Media.SystemSounds.Asterisk.Play(); break;
          case eSysIcons.Question: System.Media.SystemSounds.Asterisk.Play(); break;
          case eSysIcons.Warning: System.Media.SystemSounds.Exclamation.Play(); break;
        }
      }
      if (_mFocusControl != null)
        _mFocusControl.Focus();
    }

    #endregion

    //--------------------------------------------------------------------------------
  }
}
