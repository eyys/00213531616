using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace 屏保
{
    public partial class Form2 : Form
    {
        static TimeSpan span;
        public static TimeSpan Span
        {
            get { return span; }
            set { span = value; }
        }
        Hook hk = new Hook();
        public Form2()
        {

            InitializeComponent();
            Label.CheckForIllegalCrossThreadCalls = false;
            Thread.Sleep(0);

            timer1.Interval = 100;
            timer1.Start();
        }
 
        private void Form2_Load(object sender, EventArgs e)
        {
            hk.Hook_Start();
            Screen[] sc;
            sc = Screen.AllScreens;
            this.Top = Screen.PrimaryScreen.Bounds.Top;//位置信息
            this.Left = Screen.PrimaryScreen.Bounds.Left;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            string str = Form1.Strpath;
            this.BackgroundImage = Image.FromFile(str);//图片的路径
            label2.Text = DateTime.Now.ToLongTimeString().ToString();
            label2.Left = Screen.PrimaryScreen.Bounds.Width / 2 - label2.Width;
            label2.Top = Screen.PrimaryScreen.Bounds.Top + label2.Height;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString().ToString();//这句话报异常
            this.TopMost = true;
            Showfrm.f3.TopMost = true;
        }

        private void Form2_DoubleClick(object sender, EventArgs e)
        {           
            Closefrm.close();
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x205)
            {
                //按下鼠标右键
            }
            base.WndProc(ref m);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            hk.Hook_Clear();
            span = DateTime.Now.TimeOfDay;
            Form1.f1.start();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}