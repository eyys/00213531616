using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace 屏保
{
    public partial class Form3 : Form
    {
        public  Form3()
        {
            InitializeComponent();
            Thread.Sleep(0);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string str = Form1.Strpath;
            this.BackgroundImage = Image.FromFile(str);
            Screen[] sc;
            sc = Screen.AllScreens;
            this.Top = sc[1].Bounds.Top;//位置信息
            this.Left = sc[1].Bounds.Left;
            this.Size = new System.Drawing.Size(sc[1].Bounds.Width, sc[1].Bounds.Height);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return true;
        }
    }
}
