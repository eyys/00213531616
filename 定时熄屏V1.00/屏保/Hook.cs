using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace 屏保
{
    class Hook
    {
        static int hHook = 0;

        public const int WH_KEYBOARD_LL = 13;//全局键盘获取输入

        public delegate int HookProc(int nCode, int wParam, IntPtr lParam);//委托

        HookProc KeyBoardHookProcedure;//事件

        #region 结构函数      
        //键盘Hook结构函数
        [StructLayout(LayoutKind.Sequential)]
        public class KeyBoardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        //设置钩子
        [DllImport("user32.dll")]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //抽掉钩子
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll")]
        //调用下一个钩子
        public static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);
        #endregion

        public void Hook_Start()
        {
            // 安装键盘钩子
            if (hHook == 0)
            {
                KeyBoardHookProcedure = new HookProc(KeyBoardHookProc);

                hHook = SetWindowsHookEx//hook是返回值 则返回值就是该挂钩处理过程的句柄
                    (
                        WH_KEYBOARD_LL,         //钩子类型
                        KeyBoardHookProcedure,  //回调函数地址
                        GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName),//实例句柄
                        0 //线程ID
                    );

                //如果设置钩子失败.
                if (hHook == 0)
                {
                    Hook_Clear();
                    //throw new Exception("设置Hook失败!");
                }
            }
        }

        //取消钩子事件
        public void Hook_Clear()
        {
            bool retKeyboard = true;//
            if (hHook != 0)//钩子存在 = 1
            {
                retKeyboard = UnhookWindowsHookEx(hHook);
                hHook = 0;
            }
            //如果去掉钩子失败.
            if (!retKeyboard) throw new Exception("UnhookWindowsHookEx failed.");
        }
        //钩子子程序
        //这里可以添加自己想要的信息处理
        public static int KeyBoardHookProc(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode >= 0)//键盘被按下
            {
                KeyBoardHookStruct kbh = (KeyBoardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyBoardHookStruct));
                if (kbh.vkCode == 91)  // 截获左win(开始菜单键)
                {
                    return 1;
                }
                if (kbh.vkCode == 92)// 截获右winKD
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Control) //截获Ctrl+Esc
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.F4 && (int)Control.ModifierKeys == (int)Keys.Alt)  //截获alt+f4
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Tab && (int)Control.ModifierKeys == (int)Keys.Alt) //截获alt+tab
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Escape && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Shift) //截获Ctrl+Shift+Esc
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Space && (int)Control.ModifierKeys == (int)Keys.Alt)  //截获alt+空格
                {
                    return 1;
                }
                if (kbh.vkCode == 241)                  //截获F1
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Delete && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)      //截获Ctrl+Alt+Delete
                {
                    return 1;
                }
                //截获Ctrl+Alt+Delete 
                if ((int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt + (int)Keys.Delete)
                {
                    return 1;
                }
                if (kbh.vkCode == (int)Keys.Delete && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)
                {
                    return 1;
                }
                if (kbh.vkCode == 110 && (int)Control.ModifierKeys == (int)Keys.Control + (int)Keys.Alt)
                {
                    return 1;
                }
                if (kbh.vkCode == 122)  //截取F11
                {
                    return 1;
                }
            }
            return CallNextHookEx(hHook, nCode, wParam, lParam);
        }
    }
}