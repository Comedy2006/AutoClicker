using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

// AutoClicker by github.com/Comedy2006

namespace AutoClicker
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int LEFTUP_BTT = 0x0004;
        private const int LEFTDOWN_BTT = 0x0002;

        public int intervalms;
        public bool isPressed = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread AutoClicker = new Thread(clicker);
            AutoClicker.Start();
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            backgroundKeys.RunWorkerAsync();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            isPressed = true;
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;
        }



        private void buttonStop_Click(object sender, EventArgs e)
        {
            isPressed = false;
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
        }

        private void clicker()
        {
            while (true)
            {
                if (isPressed == true)
                {
                    mouse_event(dwFlags: LEFTUP_BTT, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(1);
                    mouse_event(dwFlags: LEFTDOWN_BTT, dx: 0, dy: 0, cButtons: 0, dwExtraInfo: 0);
                    Thread.Sleep(intervalms);
                }
                Thread.Sleep(3);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            intervalms = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
        }

        private void backgroundKeys_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (buttonStart.Enabled == true)
                {
                    if (GetAsyncKeyState(Keys.F7) < 0)
                    {
                        buttonStart.PerformClick();
                        Thread.Sleep(250);
                    }
                }
                else if (buttonStop.Enabled == true)
                {
                    if (GetAsyncKeyState(Keys.F7) < 0)
                    {
                        buttonStop.PerformClick();
                        Thread.Sleep(250);
                    }

                }
                Thread.Sleep(1);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
