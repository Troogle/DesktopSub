using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DesktopSub
{
    public partial class Form1 : Form
    {
        private static int _currentScreenCount;
        private static int _fontSize;
        private static int _windowHeight;
        private static int _speed;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "DesktopSub.ini"));
            _currentScreenCount = Convert.ToInt32(ini.IniReadValue("Screen", "Count"));
            _fontSize = Convert.ToInt32(ini.IniReadValue("Font", "Size"));
            _windowHeight = Convert.ToInt32(ini.IniReadValue("Window", "Height"));
            _speed = Convert.ToInt32(ini.IniReadValue("Sub", "Speed"));
        }

        private readonly Screen CurrentScreen = Screen.AllScreens[_currentScreenCount];
        private readonly List<Sub> pool = new List<Sub>();

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Sub sub in pool.Where(sub => sub.SubText == ""))
            {
                sub.SubText = textBox1.Text;
                sub.X = CurrentScreen.Bounds.Right;
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Sub sub in pool.Where(sub => (sub.X + sub.Width > CurrentScreen.Bounds.Left) && sub.SubText != ""))
            {
                sub.X -= _speed;
                if (sub.X + sub.Width <= CurrentScreen.Bounds.Left)
                {
                    sub.SubText = "";
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            for (int i = 0; i < CurrentScreen.Bounds.Height / _windowHeight; i++)
            {
                var tmp = new Sub("", _fontSize)
                {
                    X = CurrentScreen.Bounds.Right,
                    Y = _windowHeight * i
                };
                tmp.Show();
                pool.Add(tmp);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1.PerformClick();
                textBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        /*        public class sub
        {
            public string text;
            public int x;
        }
        public List<sub> subpool = new List<sub>();
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (sub t in subpool.Where(t => t.text == ""))
            {
                t.text = textBox1.Text;
                t.x = 0;
                return;
            }
            var sub = new sub
                {
                    text = textBox1.Text,
                    x = 0
                };
            subpool.Add(sub);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var sub in subpool.Where(sub => sub.text != ""))
            {
                sub.x += 20;
                if (sub.x >= CurrentScreen.Bounds.Width) sub.text = "";
            }
        }
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);
        private Font globalFont = new Font("微软雅黑", 20F, FontStyle.Regular, GraphicsUnit.Point, 134);
        private void Draw()
        {

            while (true)
            {
                var desktopPtr = CreateDC(null, CurrentScreen.DeviceName, null, IntPtr.Zero);
                var g = Graphics.FromHdc(desktopPtr);
                //g.Clear(Color.Transparent);
                for (int i = 0; i < subpool.Count; i++)
                {

                    if (subpool[i].text != "")
                    {
                        g.DrawString(subpool[i].text, globalFont,
                            Brushes.White,
                            CurrentScreen.WorkingArea.Right - subpool[i].x,
                            i * globalFont.Height);
                    }
                }
                g.Dispose();
                ReleaseDC(IntPtr.Zero, desktopPtr);
            }

        }
        private Thread _drawingThread;
        private void Form1_Shown(object sender, EventArgs e)
        {
            _drawingThread = new Thread(Draw);
            _drawingThread.Start();
            //BackColor = Color.LightGreen;
            //TransparencyKey = Color.LightGreen; 
        }*/
    }
}