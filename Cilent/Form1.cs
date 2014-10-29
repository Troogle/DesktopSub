using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cilent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static int _currentScreenCount;
        private static int _fontSize;
        private static int _windowHeight;
        private static int _speed;
        private string _url;

        private void Form1_Load(object sender, EventArgs e)
        {
            var ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "DesktopSub.ini"));
            _currentScreenCount = Convert.ToInt32(ini.IniReadValue("Screen", "Count"));
            _fontSize = Convert.ToInt32(ini.IniReadValue("Font", "Size"));
            _windowHeight = Convert.ToInt32(ini.IniReadValue("Window", "Height"));
            _speed = Convert.ToInt32(ini.IniReadValue("Sub", "Speed"));
            _url = ini.IniReadValue("Web", "GetUrl");
        }
        private readonly Screen CurrentScreen = Screen.AllScreens[_currentScreenCount];
        private readonly List<Sub> pool = new List<Sub>();
        private void timer1_Tick(object sender, EventArgs e)
        {
            string ret = string.Empty;
            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(_url);
                webReq.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (!string.IsNullOrWhiteSpace(ret))
            {
                foreach (Sub sub in pool.Where(sub => sub.SubText == ""))
                {
                    sub.SubText = ret;
                    sub.X = CurrentScreen.Bounds.Right;
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
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

        private void timer2_Tick(object sender, EventArgs e)
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
    }
}
