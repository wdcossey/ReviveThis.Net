using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace PSTaskDialog
{
  public partial class CommandButton : Button
  {
    //--------------------------------------------------------------------------------
    #region PRIVATE MEMBERS
    //--------------------------------------------------------------------------------
    Image _normalImage = null;
    Image _hoverImage = null;
    private Image _grayImage = null;

    const int LEFT_MARGIN = 10;
    const int TOP_MARGIN = 10;
    const int ARROW_WIDTH = 19;

    enum eButtonState
    {
      Normal,
      MouseOver, 
      Down,
    }

    eButtonState _mState = eButtonState.Normal;
    private bool _tabKeyFocus = false;
    #endregion

    //--------------------------------------------------------------------------------
    #region PUBLIC PROPERTIES
    //--------------------------------------------------------------------------------
    // Override this to make sure the control is invalidated (repainted) when 'Text' is changed
    public override string Text
    {
      get { return base.Text; }
      set
      {
        base.Text = value;
        if (m_autoHeight)
          this.Height = GetBestHeight();
        this.Invalidate(); 
      }
    }

    // SmallFont is the font used for secondary lines
    Font m_smallFont;
    public Font SmallFont { get { return m_smallFont; } set { m_smallFont = value; } }

    // AutoHeight determines whether the button automatically resizes itself to fit the Text
    bool m_autoHeight = true;
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool AutoHeight { get { return m_autoHeight; } set { m_autoHeight = value; if (m_autoHeight) this.Invalidate(); } }

    #endregion

    //--------------------------------------------------------------------------------
    #region CONSTRUCTOR
    //--------------------------------------------------------------------------------
    public CommandButton()
    {
      InitializeComponent();
      //this.DoubleBuffered = true;
      base.Font = new Font("Arial", 11.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
      m_smallFont = new Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (byte)0);
    }
    
    #endregion

    //--------------------------------------------------------------------------------
    #region PUBLIC ROUTINES
    //--------------------------------------------------------------------------------
    public int GetBestHeight()
    {
      return (TOP_MARGIN * 2) + (int)GetSmallTextSizeF.Height + (int)GetLargeTextSizeF.Height;
    }
    #endregion

    //--------------------------------------------------------------------------------
    #region PRIVATE ROUTINES
    //--------------------------------------------------------------------------------
    private string _largeText = string.Empty;
    string GetLargeText
    {
      get
      {
        //if (!string.IsNullOrEmpty(_largeText))
        //  return _largeText;

        return _largeText = Text.Split(new[] {'\n'}).FirstOrDefault();
      }
    }

    private string _smallText = string.Empty;
    string GetSmallText
    {
      get
      {
        if (!Text.Contains("\n"))
          return _smallText = string.Empty;

        //if (!string.IsNullOrEmpty(_smallText))
        //  return _smallText;

        var array = Text.Split(new[] {'\n'});
        return _smallText = string.Join("\n", array, 1, array.Length - 1);
      }
    }

    private SizeF? _largeTextSizeF = null;
    SizeF GetLargeTextSizeF
    {
      get
      {
        //if (_largeTextSizeF.HasValue)
        //  return _largeTextSizeF.Value;

        const int PADDING = LEFT_MARGIN + ARROW_WIDTH + 5;
        var mzSize = new SizeF(ClientSize.Width - PADDING - LEFT_MARGIN, 5000.0F); // presume RIGHT_MARGIN = LEFT_MARGIN
        using (var g = Graphics.FromHwnd(Handle))
        {
          var textSize = g.MeasureString(GetLargeText, base.Font, mzSize);
          return (_largeTextSizeF = textSize).Value;
        }
      }
    }

    private SizeF? _smallTextSizeF = null;
    SizeF GetSmallTextSizeF
    {
      get
      {
        //if (_smallTextSizeF.HasValue)
        //  return _smallTextSizeF.Value;

        var smallText = GetSmallText;
        if (string.IsNullOrEmpty(smallText))
          return (_smallTextSizeF = new SizeF(0, 0)).Value;

        const int PADDING = LEFT_MARGIN + ARROW_WIDTH + 8; // <- indent small text slightly more
        var mzSize = new SizeF(ClientSize.Width - PADDING - LEFT_MARGIN, 5000.0F); // presume RIGHT_MARGIN = LEFT_MARGIN
        using (var g = Graphics.FromHwnd(Handle))
        {
          var textSize = g.MeasureString(smallText, m_smallFont, mzSize);
          return (_smallTextSizeF = textSize).Value;
        }
      }
    }

    private Image GetImage
    {
      get
      {
        if (!Enabled)
          return _grayImage = _grayImage ?? GetGrayscale(_normalImage);

        switch (_mState)
        {
          case eButtonState.MouseOver:
            return _hoverImage;

            //case eButtonState.Normal:
            //case eButtonState.Down:
          default:
            return _normalImage;
        }
      }
    }

    #endregion

    //--------------------------------------------------------------------------------
    #region OVERRIDEs
    //--------------------------------------------------------------------------------
    protected override void OnCreateControl()
    {
      base.OnCreateControl();

      var resNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

      var normalImage =
        this.GetType()
          .Assembly.GetManifestResourceStream(resNames.FirstOrDefault(f => f.EndsWith("green_arrow1.png")));

      if (normalImage != null)
        _normalImage = new Bitmap(normalImage);//new Bitmap(Assembly.GetExecutingAssembly().GetType(), "green_arrow1.png");

      var hoverImage =
        this.GetType()
          .Assembly.GetManifestResourceStream(resNames.FirstOrDefault(f => f.EndsWith("green_arrow2.png")));

      if (hoverImage != null)
        _hoverImage = new Bitmap(hoverImage);// new Bitmap(Assembly.GetExecutingAssembly().GetType(), "green_arrow2.png");
    }

    protected override void OnPaintBackground(PaintEventArgs pevent)
    {
      //pevent.Graphics.FillRectangle(new SolidBrush(this.Parent.BackColor), ClientRectangle);
      //base.OnPaintBackground(pevent);
    }

    //--------------------------------------------------------------------------------
    protected override void OnPaint(PaintEventArgs e)
    {
      //base.OnPaint(e); //draws the regular background stuff

      //e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

      LinearGradientBrush brush;
      const LinearGradientMode MODE = LinearGradientMode.Vertical;

      var newRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
      var text_color = SystemColors.WindowText;

      //var img = _normalImage;

      if (Enabled)
      {
        switch (_mState)
        {
          case eButtonState.Normal:
            //e.Graphics.FillRectangle(Brushes.White, newRect);
            //if (base.Focused)
            //  e.Graphics.DrawRectangle(new Pen(Color.SkyBlue, 1), newRect);
            //else
            //  e.Graphics.DrawRectangle(new Pen(Color.White, 1), newRect);
            //text_color = Color.DarkBlue;
            
            
            // e.Graphics.FillRectangle(SystemBrushes.Window, ClientRectangle);
            e.Graphics.FillRectangle(new SolidBrush(this.Parent.BackColor), ClientRectangle);
            if (base.Focused)
              DrawHighlight(e.Graphics);
            
              DrawNormalState(e.Graphics);
            break;

          case eButtonState.MouseOver:
          //  brush = new LinearGradientBrush(newRect, Color.White, Color.WhiteSmoke, mode);
          //  e.Graphics.FillRectangle(brush, newRect);
          //  e.Graphics.DrawRectangle(new Pen(Color.Silver, 1), newRect);
          //  img = imgArrow2;
          //  text_color = Color.Blue;
            //img = _hoverImage;
            //e.Graphics.FillRectangle(Brushes.White, ClientRectangle);
            e.Graphics.FillRectangle(SystemBrushes.Control/*Brushes.White*/, ClientRectangle);

            DrawHoverState(e.Graphics);
            break;

          case eButtonState.Down:
          //  brush = new LinearGradientBrush(newRect, Color.WhiteSmoke, Color.White, mode);
          //  e.Graphics.FillRectangle(brush, newRect);
          //  e.Graphics.DrawRectangle(new Pen(Color.DarkGray, 1), newRect);
          //  text_color = Color.DarkBlue;
            e.Graphics.FillRectangle(SystemBrushes.Control, ClientRectangle);
            DrawPushedState(e.Graphics);
            break;
        }
      }
      else
      {
        //brush = new LinearGradientBrush(newRect, Color.WhiteSmoke, Color.Gainsboro, MODE);
        //e.Graphics.FillRectangle(brush, newRect);
        //e.Graphics.DrawRectangle(new Pen(Color.DarkGray, 1), newRect);
        //text_color = Color.DarkBlue;

        e.Graphics.FillRectangle(new SolidBrush(this.Parent.BackColor), ClientRectangle);
        DrawNormalState(e.Graphics);
      }

      //string largetext = this.GetLargeText();
      //string smalltext = this.GetSmallText();

      //SizeF szL = GetLargeTextSizeF;
      ////e.Graphics.DrawString(largetext, base.Font, new SolidBrush(text_color), new RectangleF(new PointF(LEFT_MARGIN + imgArrow1.Width + 5, TOP_MARGIN), szL));
      //TextRenderer.DrawText(e.Graphics, largetext, base.Font, new Rectangle(LEFT_MARGIN + imgArrow1.Width + 5, TOP_MARGIN, (int)szL.Width, (int)szL.Height), text_color, TextFormatFlags.Default);

      //if (smalltext != "")
      //{
      //  SizeF szS = GetSmallTextSizeF();
      //  e.Graphics.DrawString(smalltext, m_smallFont, new SolidBrush(text_color), new RectangleF(new PointF(LEFT_MARGIN + imgArrow1.Width + 8, TOP_MARGIN + (int)szL.Height), szS));
      //}

      //e.Graphics.DrawImage(img, new Point(LEFT_MARGIN, TOP_MARGIN + (int)(szL.Height / 2) - (int)(img.Height / 2)));
    }

    //--------------------------------------------------------------------------------
    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.Enabled)
        _mState = eButtonState.Normal;
      this.Invalidate();

      base.OnMouseLeave(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      this.Invalidate();
      base.OnGotFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (_mState != eButtonState.Normal)
      {
        _mState = eButtonState.Normal;
        this.Invalidate();
      }

      if (_tabKeyFocus)
        _tabKeyFocus = false;

      base.OnLostFocus(e);
    }

    //--------------------------------------------------------------------------------
    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.Enabled && _mState != eButtonState.Down)
        _mState = eButtonState.MouseOver;
      this.Invalidate();

      base.OnMouseEnter(e);
    }

    //--------------------------------------------------------------------------------
    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.Enabled)
      {
        _mState = this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position) ? eButtonState.MouseOver : eButtonState.Normal;

        //if (_tabKeyFocus && _mState == eButtonState.MouseOver)
        //  _tabKeyFocus = false;

      }
      this.Invalidate();

      base.OnMouseUp(e);
    }

    //--------------------------------------------------------------------------------
    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.Enabled)
        _mState = eButtonState.Down;
      this.Invalidate();

      base.OnMouseDown(e);
    }

    //--------------------------------------------------------------------------------
    protected override void OnSizeChanged(EventArgs e)
    {
      if (m_autoHeight)
      {
        int h = GetBestHeight();
        if (this.Height != h)
        {
          this.Height = h;
          return;
        }
      }
      base.OnSizeChanged(e);
    }

    protected override void OnKeyDown(KeyEventArgs kevent)
    {
      if (kevent.KeyCode == Keys.Space || kevent.KeyCode == Keys.Return)
      {
        _mState = eButtonState.Down;
        this.Invalidate();
      }
      base.OnKeyDown(kevent);
    }

    protected override void OnKeyUp(KeyEventArgs kevent)
    {
      if (kevent.KeyCode == Keys.Space || kevent.KeyCode == Keys.Return)
      {
        _mState = eButtonState.Normal;
        this.Invalidate();
      }
      //if (kevent.KeyCode == Keys.Tab)
      //{
      //  _tabKeyFocus = true;
      //  this.Invalidate();
      //}
      base.OnKeyUp(kevent);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
    }

    #endregion

    //--------------------------------------------------------------------------------

    #region Drawing Methods-------------------------

    //Draws the light-blue rectangle around the button when it is focused (by Tab for example)
    private void DrawHighlight(Graphics g)
    {
      //The outline is drawn inside the button
      //using (var innerRegion = RoundedRect(this.Width - 3, this.Height - 3, 3))
      using (var innerRegion = RoundedRect(this.Width - 1, this.Height - 1, 3))
      {
        //----Shift the inner region inwards
        var translate = new Matrix();
        //translate.Translate(1, 1);
        translate.Translate(0, 0);
        innerRegion.Transform(translate);
        translate.Dispose();
        //-----

        using (var inlinePen = new Pen(Color.FromArgb(127, SystemColors.Highlight) /*Color.FromArgb(192, 233, 243)*/))
          //Light-blue
        {
          g.SmoothingMode = SmoothingMode.HighQuality;

          g.DrawPath(inlinePen,
            innerRegion);
        }
      }
    }

    ////Draws the button when the mouse is over it
    private void DrawHoverState(Graphics g)
    {
      using (var outerRegion = RoundedRect(this.Width - 1, this.Height - 1, 3))
      using (var innerRegion = RoundedRect(this.Width - 3, this.Height - 3, 2))
      {
        var backgroundRect = new Rectangle(1, 1, this.Width - 2, (int) (this.Height*0.75f) - 2);

        using (var outlinePen = new Pen(Color.FromArgb(127, SystemColors.ControlDark)/*Color.FromArgb(189, 189, 189)*/)) //SystemColors.ControlDark
        using (var inlinePen = new Pen(Color.FromArgb(245, SystemColors.Window)/*Color.FromArgb(245, 255, 255, 255)*/)) //Slightly transparent white
          //Gradient brush for the background, goes from white to transparent 75% of the way down
        using (var backBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, backgroundRect.Height),
          SystemColors.Window, Color.Transparent) { WrapMode = WrapMode.TileFlipX })
        {
          //----Shift the inner region inwards
          using (var translate = new Matrix())
          {
            translate.Translate(1, 1);
            innerRegion.Transform(translate);
          }

          //keeps the gradient smooth incase of the glitch where there's an extra gradient line
          g.SmoothingMode = SmoothingMode.AntiAlias;

          g.FillRectangle(backBrush, backgroundRect);
          g.DrawPath(inlinePen, innerRegion);
          g.DrawPath(outlinePen, outerRegion);

          //Text/Image
          //offset = 0; //Text/Image doesn't move
          DrawForeground(g);
        }
      }
    }

    //Draws the button when it's clicked down
    private void DrawPushedState(Graphics g)
    {
      var backgroundRect = new Rectangle(0, 0, this.Width - 3, 5/*(int) (this.Height*0.75f) - 3*/);

      using (var outerRegion = RoundedRect(this.Width - 1, this.Height - 1, 3))
      using (var innerRegion = RoundedRect(this.Width - 3, this.Height - 3, 2))
      using (var outlinePen = new Pen(SystemColors.ControlDark /*Color.FromArgb(167, 167, 167)*/))
        //Outline is darker than normal
      using (var inlinePen = new Pen(SystemColors.ControlLight /*Color.FromArgb(227, 227, 227)*/)) //Darker white
      //using (var backBrush = new SolidBrush(SystemColors.Control /* Color.FromArgb(234, 234, 234)*/))
      using (var backBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, backgroundRect.Height),
         Color.FromArgb(127, SystemColors.ControlDark), Color.Transparent) { WrapMode = WrapMode.TileFlipX })
      {
        //----Shift the inner region inwards
        using (var translate = new Matrix())
        {
          translate.Translate(1, 1);
          innerRegion.Transform(translate);
        }
        //-----
        //var backgroundRect = new Rectangle(1, 1, Width - 3, Height - 3);

        g.SmoothingMode = SmoothingMode.HighQuality;

        g.DrawPath(inlinePen, innerRegion);
        g.FillRectangle(backBrush, backgroundRect);
        g.DrawPath(outlinePen, outerRegion);

        //Text/Image
        //offset = 1; //moves image inwards 1 pixel (x and y) to create the illusion that the button was pushed
        DrawForeground(g);
      }

    }

    //Draws the button in it's regular state
    private void DrawNormalState(Graphics g)
    {
      //Nothing needs to be drawn but the text and image

      //Text/Image
      //offset = 0; //Text/Image doesn't move
      DrawForeground(g);
    }

    //Draws Text and Image
    private void DrawForeground(Graphics g)
    {

      var image = GetImage;
      var imageSize = image != null ? image.Size : new Size(24, 24);

      var largeText = GetLargeText;
      var smallText = GetSmallText;

      //Make sure drawing is of good quality
      g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
      g.PixelOffsetMode = PixelOffsetMode.HighQuality;

      //Image Coordinates-------------------------------
      var imageLeft = 9;
      var imageTop = 0;
      //

      //Text Layout--------------------------------
      //Gets the width/height of the text once it's drawn out
      var headerLayout = g.MeasureString(largeText, this.Font);
      var descriptLayout = g.MeasureString(smallText, SmallFont);

      //Merge the two sizes into one big rectangle
      var totalRect = new Rectangle(0, 0, (int) Math.Max(headerLayout.Width, descriptLayout.Width),
        (int) (headerLayout.Height + descriptLayout.Height) - 4);

      //Align the total rectangle-------------------------
      if (image != null)
        totalRect.X = imageLeft + imageSize.Width + 5; //consider the image is there
      else
        totalRect.X = 20;

      totalRect.Y = (this.Height/2) - (totalRect.Height/2); //center vertically  
      //---------------------------------------------------

      ////Align the top of the image---------------------
      //if (image != null)
      //{
      //  switch (imageAlign)
      //  {
      //    case VerticalAlign.Top:
      //      imageTop = totalRect.Y;
      //      break;
      //    case VerticalAlign.Middle:
      //      imageTop = totalRect.Y + (totalRect.Height / 2) - (imageSize.Height / 2);
      //      break;
      //    case VerticalAlign.Bottom:
      //      imageTop = totalRect.Y + totalRect.Height - imageSize.Height;
      //      break;
      //    default:
      //      break;
      //  }
      //}
      ////-----------------------------------------------

      //Brushes--------------------------------
      // Determine text color depending on whether the control is enabled or not
      var textColor = this.Enabled ? /*_mState == eButtonState.MouseOver ? SystemColors.HotTrack :*/ this.ForeColor : Color.FromArgb(127, SystemColors.GrayText) ;

      var offset = _mState == eButtonState.Down ? 1 : 0;

      using (var textBrush = new SolidBrush(textColor))
      {
        var szL = GetLargeTextSizeF;
        var format = new StringFormat {HotkeyPrefix = HotkeyPrefix.Show};
        g.DrawString(largeText, base.Font, textBrush, new RectangleF(new PointF(LEFT_MARGIN + imageSize.Width + 5 + offset, TOP_MARGIN + offset), szL), format);

        //TextRenderer.DrawText(g, largeText, base.Font,
        //  new Rectangle(LEFT_MARGIN + imageSize.Width + 5 + offset, TOP_MARGIN + offset, (int)szL.Width, (int)szL.Height),
        //  textBrush.Color, TextFormatFlags.Default);
        //g.DrawString(largeText, this.Font, textBrush, totalRect.Left + offset, totalRect.Top + offset);

        if (!string.IsNullOrEmpty(smallText))
        {
          var szS = GetSmallTextSizeF;
          g.DrawString(smallText, m_smallFont, this.Enabled ? SystemBrushes.GrayText : textBrush,
            new RectangleF(
              new PointF(LEFT_MARGIN + imageSize.Width + 8 + offset, TOP_MARGIN + (int) szL.Height + offset), szS));
        }
        //g.DrawString(smallText, descriptFont, textBrush, totalRect.Left + 1 + offset,
        //  totalRect.Bottom - (int)descriptLayout.Height + offset);

        //Note: the + 1 in "totalRect.Left + 1 + offset" compensates for GDI+ inconsistency

        if (image == null)
          return;

        g.DrawImage(image, new Point(LEFT_MARGIN + offset, TOP_MARGIN + (int)(szL.Height / 2) - (imageSize.Height / 2) + offset));

      }

      if (this.Focused && _tabKeyFocus)
      {
        g.SmoothingMode = SmoothingMode.None;
        ControlPaint.DrawFocusRectangle(g, new Rectangle(4, 4, Width - 6, Height - 6));
      }
    }

    #endregion

    #region Helper Methods--------------------------
    private static GraphicsPath RoundedRect(int width, int height, int radius)
    {
      var baseRect = new RectangleF(0, 0, width, height);
      var diameter = radius * 2.0f;
      var sizeF = new SizeF(diameter, diameter);
      var arc = new RectangleF(baseRect.Location, sizeF);
      var path = new GraphicsPath();

      // top left arc 
      path.AddArc(arc, 180, 90);

      // top right arc 
      arc.X = baseRect.Right - diameter;
      path.AddArc(arc, 270, 90);

      // bottom right arc 
      arc.Y = baseRect.Bottom - diameter;
      path.AddArc(arc, 0, 90);

      // bottom left arc
      arc.X = baseRect.Left;
      path.AddArc(arc, 90, 90);

      path.CloseFigure();
      return path;
    }

    private static Bitmap GetGrayscale(Image original)
    {
      //Set up the drawing surface
      var grayscale = new Bitmap(original.Width, original.Height);
      using (var g = Graphics.FromImage(grayscale))
      {
        //Grayscale Color Matrix
        var colorMatrix = new ColorMatrix(/*new float[][]
        {
          new float[] {0.3f, 0.3f, 0.3f, 0, 0},
          new float[] {0.59f, 0.59f, 0.59f, 0, 0},
          new float[] {0.11f, 0.11f, 0.11f, 0, 0},
          new float[] {0, 0, 0, 1, 0},
          new float[] {0, 0, 0, 0, 1}
        }*/);

        colorMatrix.Matrix33 = 0.5f;

        //Create attributes
        var att = new ImageAttributes();
        att.SetColorMatrix(colorMatrix);

        //Draw the image with the new attributes
        g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width,
          original.Height, GraphicsUnit.Pixel, att);
      }
      return grayscale;
    }
    #endregion
  }
}
