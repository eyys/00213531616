using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using Timer = System.Windows.Forms.Timer;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace 屏保
{
    public partial class Form1 : Form
    {
        public static Form1 f1;
        TimeSpan ts = new TimeSpan();
        public Form1()
        {
            InitializeComponent();
            f1 = this;
        }
        #region 基本定义

        List<string> jpg = new List<string>();

        public static string path = Application.StartupPath + @"\图片";

        public static int[] a = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        public static int[] b = { 0, 1, 2, 3, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 56, 39, 41, 42, 53, 54 };
        public static string[] astr = new string[5];
        public static ComboBox[] combos = new ComboBox[4];
        public static string s = astr[0] + ":" + astr[1];
        public static int c = new int();
        private static TimeSpan span;
        public static TimeSpan Span
        {
            get { return span; }
            set { span = value; }
        }
        private static string strpath;
        public static string Strpath
        {
            get { return strpath; }
            set { strpath = value; }
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {

            noIcon.Visible = true;
            string h = Application.StartupPath + @"\ico\1.ico";
            noIcon.Icon = new Icon(h);
            noIcon.Text = "屏幕保护";
            //this.Hide();
            this.ShowInTaskbar = false;//不在任务栏展示

            combos[0] = comboBox1;
            combos[1] = comboBox2;
            combos[2] = comboBox3;
            combos[3] = comboBox4;
            //数组添加数据
            foreach (int i in a)
            {
                comboBox1.Items.Add(i);
                comboBox3.Items.Add(i);
            }
            foreach (int i in b)
            {
                comboBox2.Items.Add(i);
                comboBox4.Items.Add(i);
            }
            foreach (ComboBox c in combos)
            {
                c.SelectedIndex = 0;
            }
            comboBox4.SelectedIndex = 7;



            DirectoryInfo root = new DirectoryInfo(path);//实例化目录
            foreach (FileInfo f in root.GetFiles())
            {
                string s = path + @"\" + f.Name;
                if (IsRealImage(s))
                {
                    jpg.Add(f.Name);
                }
            }
            if (jpg.Count == 0)
            {
                MessageBox.Show("没有选择图片");
            }
            else
            {
                textBox1.Text = jpg.First();
                Strpath = path + @"\" + textBox1.Text;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Showfrm.show();
        }
        public static bool IsRealImage(string path)
        {
            try
            {
                Image img = Image.FromFile(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //初始显示的路径
                string stra = Application.StartupPath + @"\图片";//路径
                openFileDialog.InitialDirectory = stra;

                //初始显示的文件
                string strFilter = "JPG files (*.jpg)|*.jpg|PNG files(*.png)|*.png";
                //|All files (*.*)|*.*
                openFileDialog.Filter = strFilter;
                openFileDialog.FilterIndex = 1;//默认文件索引
                openFileDialog.RestoreDirectory = true;//是否还原当前选中目录
                openFileDialog.Title = "选择一张背景图片";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string c = openFileDialog.FileName;
                    int i = c.LastIndexOf(@"\");
                    textBox1.Text = c.Substring(i + 1);
                    Strpath = path + @"\" + textBox1.Text;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Start();
            if (Convert.ToInt32(astr[0]) < Convert.ToInt32(DateTime.Now.ToString("HH")))
            {

            }
            for (int i = 0; i < 4; i++)
            {
                astr[i] = combos[i].SelectedItem.ToString();
            }
        }

        //定时显示
        private void timer1_Tick(object sender, EventArgs e)
        {
            Showfrm.f2.TopMost = true;
            Showfrm.f3.TopMost = true;
            if (s == DateTime.Now.ToString("t"))
            {
                Showfrm.show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            c = Convert.ToInt32(comboBox4.Text);
            timer2.Interval = 100;
            Showfrm.show();
        }
        //间隔显示
        private void timer2_Tick(object sender, EventArgs e)
        {
            //Showfrm.f2.TopMost = true;
            //Showfrm.f3.TopMost = true;

            span = DateTime.Now.TimeOfDay;
            ts = span - Form2.Span;
            if (c == Convert.ToInt32(ts.Minutes.ToString()))
            {
                this.timer2.Stop();
                Showfrm.show();
            }

        }
        public void start()
        {
            this.timer2.Interval = 100;
            this.timer2.Start();
        }
        public void stop()
        {
            f1.timer2.Stop();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

        }

        private void noIcon_DoubleClick(object sender, EventArgs e)
        {
            this.TopMost = true;
            huanyuan();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            huanyuan();
        }
        public void huanyuan()
        {
            this.Show();
            if (WindowState == FormWindowState.Minimized)
            {

                this.WindowState = FormWindowState.Normal;//还原窗体显示 

                this.Activate();//激活窗体并给予它焦点

                this.ShowInTaskbar = true;//任务栏区显示图标 
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                Showfrm.show();
            }
            else
            {

            }
        }


        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);
        private void button5_Click(object sender, EventArgs e)
        {

            SetSuspendState(true, true, true);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            noIcon.Visible = true;
            e.Cancel = true;
            this.Hide();
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;//还原窗体显示 
                this.ShowInTaskbar = false;//任务栏区显示图标 
            }
            //DialogResult dr = MessageBox.Show("缩小到托盘区", "标题", MessageBoxButtons.YesNoCancel);
            //if(dr == DialogResult.OK)
            //{

            //}
            //else
            //{
            //    Application.ExitThread();
            //}
        }

        private void noIcon_Click(object sender, EventArgs e)
        {

        }

        private void noIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        public void comstart()
        {
            MessageBox.Show("设置开机自启动，需要修改注册表", "提示");
            string path = Application.ExecutablePath;
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rk2.SetValue("JcShutdown", path);
            rk2.Close();
            rk.Close();
        }
        private void comstop()
        {
            MessageBox.Show("取消开机自启动，需要修改注册表", "提示");
            string path = Application.ExecutablePath;
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rk2.DeleteValue("JcShutdown", false);
            rk2.Close();
            rk.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                comstart();
            }
            else
            {
                comstop();
            }
        }
    }
}