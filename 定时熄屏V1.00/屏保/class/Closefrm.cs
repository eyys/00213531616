using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 屏保
{
    //关闭窗体
    class Closefrm
    {
        public static void close()
        {
            Showfrm.f2.Close();
            Showfrm.f3.Close();
        }
    }
}
