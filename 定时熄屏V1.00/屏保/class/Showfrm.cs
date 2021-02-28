using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace 屏保
{

    public class Showfrm
    {
        public static Form2 f2 = new Form2();
        public static Form3 f3 = new Form3();

        public static void show()
        {
            Form1.f1.Hide();
            Screen[] sc;
            sc = Screen.AllScreens;
            if (sc.Length > 1)
            {
                F3();
            }
            F2();
        }
        public static void F2()
        {
            if (f2 == null || f2.IsDisposed)
            {
                f2 = new Form2();
            }
            f2.Show();
        }
        public static void F3()
        {
            if (f3 == null || f3.IsDisposed)
            {
                f3 = new Form3();
            }
            f3.Show();
        }
    }
}
