using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CP
{
    public class ColorPicker : Form
    {
        //#==================================================================== CONSTANTS
        private const int COLOR_COUNT = 360;

        //#==================================================================== ENUM
        private enum ControlState
        {
            None, Donut, Triangle
        }

        //#==================================================================== CONTROLS
        private NumericUpDown _numHue = new NumericUpDown();
        private NumericUpDown _numSaturation = new NumericUpDown();
        private NumericUpDown _numBrightness = new NumericUpDown();
        private NumericUpDown _numRed = new NumericUpDown();
        private NumericUpDown _numGreen = new NumericUpDown();
        private NumericUpDown _numBlue = new NumericUpDown();
        private Button _btnOK = new Button();

        //#==================================================================== VARIABLES
        private Bitmap _colorWheel;
        private ControlState _controlState = ControlState.None;
        private Color _oldColor;
        private bool _isManualChange = false; // set to true when manually changing numericupdown values

        //#==================================================================== INITIALIZE
        public ColorPicker() : this(Color.White)
        {
        }
        public ColorPicker(Color defaultColor)
        {
            this.ClientSize = new Size(250, 340);

            _numHue.DecimalPlaces = _numSaturation.DecimalPlaces = _numBrightness.DecimalPlaces = 2;
            _numHue.Maximum = 359.99m;
            _numHue.Location = new Point(30, Y + Diameter + 10);
            _numHue.Width = _numSaturation.Width = _numBrightness.Width = _numRed.Width = _numGreen.Width = _numBlue.Width = 60;
            _numHue.ValueChanged += numHSB_ValueChanged;
            _numSaturation.Location = new Point(_numHue.Left, _numHue.Bottom + 10);
            _numSaturation.ValueChanged += numHSB_ValueChanged;
            _numBrightness.Location = new Point(_numHue.Left, _numSaturation.Bottom + 10);
            _numBrightness.ValueChanged += numHSB_ValueChanged;
            _numRed.Maximum = _numGreen.Maximum = _numBlue.Maximum = 255;
            _numRed.Location = new Point(_numHue.Right + 30, Y + Diameter + 10);
            _numRed.ValueChanged += numRGB_ValueChanged;
            _numGreen.Location = new Point(_numSaturation.Right + 30, _numRed.Bottom + 10);
            _numGreen.ValueChanged += numRGB_ValueChanged;
            _numBlue.Location = new Point(_numBrightness.Right + 30, _numGreen.Bottom + 10);
            _numBlue.ValueChanged += numRGB_ValueChanged;
            _btnOK.Location = new Point(_numRed.Right + 10, Y + Diameter + 8);
            _btnOK.Text = "OK";
            _btnOK.Width = ClientSize.Width - _numRed.Right - 10 - X;
            _btnOK.Click += btnOK_Click;

            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Color Picker";
            this.Controls.Add(_numHue);
            this.Controls.Add(_numSaturation);
            this.Controls.Add(_numBrightness);
            this.Controls.Add(_numRed);
            this.Controls.Add(_numGreen);
            this.Controls.Add(_numBlue);
            this.Controls.Add(_btnOK);

            SelectedColor = _oldColor = defaultColor;
            InitializeColorWheel();
        }
        public void InitializeColorWheel()
        {
            _colorWheel = new Bitmap(Diameter, Diameter);
            Point center = new Point(Radius, Radius);
            using (Graphics g = Graphics.FromImage(_colorWheel))
            using (PathGradientBrush brush = new PathGradientBrush(GetDonutPoints(Radius + 1, center)))
            using (SolidBrush backBrush = new SolidBrush(this.BackColor))
            {
                brush.CenterPoint = center;
                brush.CenterColor = Color.White;
                brush.SurroundColors = GetDonutColors();

                // drawing the donut
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillEllipse(brush, 0, 0, Radius * 2 - 1, Radius * 2 - 1);
                g.FillEllipse(backBrush, Thickness - 1, Thickness - 1, Diameter - Thickness * 2 + 1, Diameter - Thickness * 2 + 1);
            }
        }

        //#==================================================================== FINALIZING
        protected override void Dispose(bool disposing)
        {
 	         base.Dispose(disposing);
             _colorWheel.Dispose();
        }
        //#==================================================================== FUNCTIONS
        private Point[] GetDonutPoints(int radius, Point center)
        {
            Point[] points = new Point[COLOR_COUNT];
            for (int degree = 0; degree < points.Length; degree++)
                points[degree] = GetPoint(radius, center, degree);
            return points;
        }
        private Point GetPoint(int radius, Point center, int degree)
        {
            float radians = (float)((degree * Math.PI) / 180);
            int x = (int)(center.X + radius * Math.Cos(radians));
            int y = (int)(center.Y + radius * Math.Sin(radians));
            return new Point(x, y);
        }
        private Color[] GetDonutColors()
        {
            Color[] colors = new Color[COLOR_COUNT];
            for (int degree = 0; degree < colors.Length; degree++)
                colors[degree] = new ColorEx.HSB(degree, 100, 100).ToColor();
            return colors;
        }
        private bool IsInsideDonut(Point pt)
        {
            float distance = (float)Math.Sqrt(Math.Pow(pt.Y - Center.Y, 2) + Math.Pow(pt.X - Center.X, 2));
            return (distance > InnerRadius && distance < Radius);
        }

        private bool IsInsideTriangle(Point pt)
        {
            if (!(pt.Y > TriangleY && pt.Y < TriangleY + TriangleHeight))
                return false; // check vertical point
            float yPercent = (pt.Y - TriangleY) / (float)TriangleHeight;
            float length = TriangleWidth * yPercent;
            return (pt.X > Center.X - length / 2 && pt.X < Center.X + length / 2); // check horizontal point
        }

        private float GetHue(Point pt)
        {
            float radian = (float)Math.Atan2(pt.Y - Center.Y, pt.X - Center.X);
            float degree = (float)((radian * 180) / Math.PI);
            return (degree < 0 ? degree + 360 : degree);
        }
        private float GetSaturation(Point pt)
        {
            float yPercent = (pt.Y - TriangleY) / (float)TriangleHeight;
            float length = TriangleWidth * yPercent;
            return (pt.X - Center.X + length / 2) / length * 100;
        }
        private float GetBrightness(Point pt)
        {
            return (pt.Y - TriangleY) / (float)TriangleHeight * 100;
        }

        //#==================================================================== PROPERTIES
        private int X
        {
            get { return 10; }
        }
        private int Y
        {
            get { return 10; }
        }
        private int Diameter
        {
            get { return ClientSize.Width - X * 2; }
        }
        private int Radius
        {
            get { return Diameter / 2; }
        }
        private int InnerRadius
        {
            get { return Radius - Thickness; }
        }
        private int Thickness
        {
            get { return 25; }
        }
        private Point Center
        {
            get { return new Point(X + Radius, Y + Radius); }
        }

        private int TriangleY
        {
            get { return Center.Y - InnerRadius; }
        }
        private int TriangleHeight
        {
            get { return (InnerRadius * 3) / 2; }
        }
        private float TriangleWidth
        {
            get { return (TriangleHeight * 2) / (float)Math.Sqrt(3); }
        }

        private Rectangle HuePickerRect
        {
            get
            {
                int radius = 5;
                Point pt = GetPoint(InnerRadius + Thickness / 2, Center, (int)Hue);
                return new Rectangle(pt.X - radius, pt.Y - radius, radius * 2, radius * 2);
            }
        }
        private Rectangle SBPickerRect
        {
            get
            {
                float xPercent = Saturation / 100;
                float yPercent = Brightness / 100;
                float length = TriangleWidth * yPercent;
                int radius = 5;
                int x = (int)(Center.X - length / 2 + length * xPercent);
                int y = TriangleY + (int)(yPercent * TriangleHeight);
                Point pt = new Point(x, y);
                return new Rectangle(pt.X - radius, pt.Y - radius, radius * 2, radius * 2);
            }
        }

        public Color SelectedColor
        {
            get { return new ColorEx.HSB(Hue, Saturation, Brightness).ToColor(); }
            set
            {
                Red = value.R;
                Green = value.G;
                Blue = value.B;
            }
        }
        private float Hue
        {
            get { return (float)_numHue.Value; }
            set { _numHue.Value = (decimal)value; }
        }
        private float Saturation
        {
            get { return (float)_numSaturation.Value; }
            set { _numSaturation.Value = (decimal)Math.Min(Math.Max(value, 0), 100); }
        }
        private float Brightness
        {
            get { return (float)_numBrightness.Value; }
            set { _numBrightness.Value = (decimal)Math.Min(Math.Max(value, 0), 100); }
        }
        private int Red
        {
            get { return (int)_numRed.Value; }
            set { _numRed.Value = (decimal)value; }
        }
        private int Green
        {
            get { return (int)_numGreen.Value; }
            set { _numGreen.Value = (decimal)value; }
        }
        private int Blue
        {
            get { return (int)_numBlue.Value; }
            set { _numBlue.Value = (decimal)value; }
        }

        //#==================================================================== EVENTS
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawDonut(e.Graphics);
            DrawTriangle(e.Graphics);
            DrawColorSquare(e.Graphics);
            DrawHuePicker(e.Graphics);
            DrawSBPicker(e.Graphics);
            DrawLabels(e.Graphics);
            base.OnPaint(e);
        }
        private void DrawDonut(Graphics g)
        {
            g.DrawImage(_colorWheel, X, Y);
        }
        private void DrawTriangle(Graphics g)
        {
            // iterate top to bottom
            for (int y = TriangleY; y < TriangleY + TriangleHeight; y++)
            {
                float yPercent = (y - TriangleY) / (float)TriangleHeight;
                float length = TriangleWidth * yPercent;
                PointF pt1 = new PointF(Center.X - length / 2 - 1, y);
                PointF pt2 = new PointF(Center.X + length / 2 + 1, y);
                Color col1 = new ColorEx.HSB(Hue, 0, yPercent * 100).ToColor();
                Color col2 = new ColorEx.HSB(Hue, 100, yPercent * 100).ToColor();
                using (LinearGradientBrush brush = new LinearGradientBrush(pt1, pt2, col1, col2))
                using (Pen pen = new Pen(brush))
                    g.DrawLine(pen, Center.X - length / 2, y, Center.X + length / 2, y);
            }
        }
        private void DrawColorSquare(Graphics g)
        {
            int height = (_numBlue.Bottom - _numGreen.Top) / 2;
            using (SolidBrush brush = new SolidBrush(SelectedColor))
                g.FillRectangle(brush, _btnOK.Left, _numGreen.Top, _btnOK.Width, height);
            using (SolidBrush brush = new SolidBrush(_oldColor))
                g.FillRectangle(brush, _btnOK.Left, _numGreen.Top + height, _btnOK.Width, height);
        }
        private void DrawHuePicker(Graphics g)
        {
            using (Pen pen = new Pen(Color.Black, 2))
                g.DrawEllipse(pen, HuePickerRect);
        }
        private void DrawSBPicker(Graphics g)
        {
            int brightness = (Brightness > 50 ? 0 : 100);
            using (Pen pen = new Pen(new ColorEx.HSB(0, 0, brightness).ToColor(), 2))
                g.DrawEllipse(pen, SBPickerRect);
        }
        private void DrawLabels(Graphics g)
        {
            string[] labels = new string[] { "H", "S", "B", "R", "G", "B" };
            Rectangle rect = new Rectangle(X, _numHue.Top - 2, 1, _numHue.Height);
            for (int i = 0; i < 3; i++)
            {
                TextRenderer.DrawText(g, labels[i], this.Font, rect, SystemColors.WindowText, TextFormatFlags.VerticalCenter | TextFormatFlags.NoClipping);
                rect.X = _numHue.Right + X;
                TextRenderer.DrawText(g, labels[i + 3], this.Font, rect, SystemColors.WindowText, TextFormatFlags.VerticalCenter | TextFormatFlags.NoClipping);
                rect.X = X;
                rect.Y += _numHue.Height + 10;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (IsInsideDonut(e.Location))
            {
                _controlState = ControlState.Donut;
                Hue = GetHue(e.Location);
            }
            else if (IsInsideTriangle(e.Location))
            {
                _controlState = ControlState.Triangle;
                Saturation = GetSaturation(e.Location);
                Brightness = GetBrightness(e.Location);
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_controlState == ControlState.Donut)
                Hue = GetHue(e.Location);
            else if (_controlState == ControlState.Triangle)
            {
                Saturation = GetSaturation(e.Location);
                Brightness = GetBrightness(e.Location);
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _controlState = ControlState.None;
            base.OnMouseUp(e);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
            base.OnKeyPress(e);
        }

        private void numHSB_ValueChanged(object sender, EventArgs e)
        {
            if (_isManualChange == false)
            {
                _isManualChange = true;
                ColorEx.RGB col = new ColorEx.HSB(Hue, Saturation, Brightness).ToRGB();
                _numRed.Value = col.Red;
                _numGreen.Value = col.Green;
                _numBlue.Value = col.Blue;
                this.Invalidate(false);
                _isManualChange = false;
            }
        }
        private void numRGB_ValueChanged(object sender, EventArgs e)
        {
            if (_isManualChange == false)
            {
                _isManualChange = true;
                ColorEx.HSB col = new ColorEx.RGB(Red, Green, Blue).ToHSB();
                _numHue.Value = (decimal)col.Hue;
                _numSaturation.Value = (decimal)col.Saturation;
                _numBrightness.Value = (decimal)col.Brightness;
                this.Invalidate(false);
                _isManualChange = false;
            }
        }
    }
}
