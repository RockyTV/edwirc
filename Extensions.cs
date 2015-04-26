using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace edwirc.Extensions
{
    public static class ControlExtensions
    {
        public static void Invoke(this Control control, Action action)
        {
            if (control.InvokeRequired) control.Invoke(new MethodInvoker(action), null);
            else action.Invoke();
        }
    }
    public static class StringExtensions
    {
        public static int NthIndexOf(this string target, string value, int n)
        {
            Match m = Regex.Match(target, "((" + Regex.Escape(value) + ").*?){" + n + "}");

            if (m.Success)
                return m.Groups[2].Captures[n - 1].Index;
            else
                return -1;
        }
    }

    public static class TextBoxExtensions
    {
        public static void SafeAppendText(this TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke((MethodInvoker)delegate
                {
                    textBox.AppendText(text + "\r\n");
                });
                return;
            }
            textBox.AppendText(text + "\r\n");
        }
    }
    public static class RichTextBoxExtensions
    {
        public static void SafeAppendText(this RichTextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke((MethodInvoker)delegate
                {
                    textBox.AppendText(text + "\r\n");
                });
                return;
            }
            textBox.AppendText(text + "\r\n");
        }
    }
    public static class TabControlExtensions
    {
        public static void SelectTab(this TabControl tabControl, string tabPageName)
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.SelectTab(tabPageName);
                });
                return;
            }
            tabControl.SelectTab(tabPageName);
        }
        public static void SelectTab(this TabControl tabControl, int index)
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.SelectTab(index);
                });
                return;
            }
            tabControl.SelectTab(index);
        }
        public static void SelectTab(this TabControl tabControl, TabPage tabPage)
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.SelectTab(tabPage);
                });
                return;
            }
            tabControl.SelectTab(tabPage);
        }

        public static void AddPage(this TabControl tabControl, string text)
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.TabPages.Add(text);
                });
                return;
            }
            tabControl.TabPages.Add(text);
        }
        public static void AddPage(this TabControl tabControl, TabPage tabPage)
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.TabPages.Add(tabPage);
                });
                return;
            }
            tabControl.TabPages.Add(tabPage);
        }
        public static void AddPage(this TabControl tabControl, string key, string text)
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.TabPages.Add(key, text);
                });
                return;
            }
            tabControl.TabPages.Add(key, text);
        }
        public static void AddPage(this TabControl tabControl, string key, string text, int imageIndex)
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.TabPages.Add(key, text, imageIndex);
                });
                return;
            }
            tabControl.TabPages.Add(key, text, imageIndex);
        }
        public static void AddPage(this TabControl tabControl, string key, string text, string imageKey)
        {
            if (tabControl.InvokeRequired)
            {
                tabControl.Invoke((MethodInvoker)delegate
                {
                    tabControl.TabPages.Add(key, text, imageKey);
                });
                return;
            }
            tabControl.TabPages.Add(key, text, imageKey);
        }
    }
}
