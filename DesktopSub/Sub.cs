using System;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopSub
{
    public partial class Sub : Form
    {
        public Sub()
        {
            InitializeComponent();
        }

        public Sub(string text, int size)
        {
            InitializeComponent();
            label1.Text = text;
            label1.Font = new Font("微软雅黑", size, FontStyle.Regular, GraphicsUnit.Point, 134);
        }

        public String SubText
        {
            get { return label1.Text; }
            set
            {
                label1.Text = value;
                Size = label1.Size;
            }
        }

        public int Y
        {
            set { Location = new Point(Location.X, value); }
            get { return Location.Y; }
        }

        public int X
        {
            get { return Location.X; }
            set { Location = new Point(value, Location.Y); }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Sub_Shown(object sender, EventArgs e)
        {
            Size = label1.Size;
        }
    }
}