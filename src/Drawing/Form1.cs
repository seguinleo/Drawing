namespace Drawing_App
{
    public partial class Form1 : Form
    {
        public Point end = new Point();
        public Point start = new Point();
        public Pen p;
        bool draw = false;
        string color;
        Graphics graphics;
        Bitmap bmp;

#pragma warning disable CS8618
        public Form1()
#pragma warning restore CS8618

        {
            InitializeComponent();
            GenerateVaribles();
        }

        void GenerateVaribles()
        {
            graphics = pb_canvas.CreateGraphics();
            comboBox1.Text = "10";
            color = "#000000";
            txt_color.Text = color;
            CreateCanvas();
        }

        void CreateCanvas()
        {
            bmp = new Bitmap(pb_canvas.Width, pb_canvas.Height);
            graphics = Graphics.FromImage(bmp);
            pb_canvas.BackgroundImage = bmp;
            pb_canvas.BackgroundImageLayout = ImageLayout.None;
        }

        private void pb_canvas_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
            start = e.Location;
            int size;

            if (comboBox1.Text == "")
            {
                size = 10;
            }
            else
            {
                size = Convert.ToInt32(comboBox1.Text);
            }

            Color newColor = ColorTranslator.FromHtml(color);
            p = new Pen(newColor, size);
            p.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
        }

        private void pb_canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                if (e.Button == MouseButtons.Left)
                {
                    end = e.Location;
#pragma warning disable CA1416
                    graphics.DrawLine(p, start, end);
#pragma warning restore CA1416
                    start = end;
                    pb_canvas.Invalidate();
                }
            }
        }

        private void pb_canvas_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                color = "#" + (cd.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                txt_color.Text = color;
            }
        }

        private void save()
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "PNG | .png";
            if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(s.FileName))
                {
                    File.Delete(s.FileName);
                }
                if (s.FileName.Contains(".png"))
                {
                    bmp.Save(s.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateCanvas();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Drawing app built with C# via Visual Studio 2022\n" +
                "Developped by PouletEnSlip © 2022 - All Rights Reserved\n" +
                "GitHub : https://github.com/PouletEnSlip/Drawing", "About");
        }
    }
}
