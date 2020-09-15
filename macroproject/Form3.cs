using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace macroproject
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public Form1 f1;
        private string sendData = "";
        private string sendData2 = "";
        private const int MOUSEEVENTF_LEFTDOWN = 0x2;
        private const int MOUSEEVENTF_LEFTUP = 0x4;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MBUTTONDOWN = 0x20;
        private const int MOUSEEVENTF_MBUTTONUP = 0x40;
        private const int MOUSEEVENTF_XBUTTON1DOWN = 0x80;
        private const int MOUSEEVENTF_XBUTTON1UP = 0x100;

        const int WS_EX_TOOLWINDOW = 0x00000080;

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            if (e.KeyData == Keys.End)
            {
                End();
            }
        }
        string macrodousa;
        DateTime time;
        bool timecheck;
        string timemoji;
        bool roop = false;
        int count;
        int commnadtype = 0;
        string applicationname;
        public string SendData//ここは早めにしっかりとした送り方に直したい。問題点は複数あるので何度も送りなおす必要がありそうなところ(今は１度で一気に送っているが不安定)
        {
            set
            {
                sendData = value;
                macrodousa = sendData;
                Macrosetting();
            }
            get
            {
                return sendData;
            }
        }
        bool nextcheck = true;

        async void Macrosetting()
        {
            count = 0;
            string[] gyousu = macrodousa.Split('\b');
            string[] macrowake1 = gyousu[2].Split('\r');

            int roopcheck = Int32.Parse(gyousu[0]);
            if (roopcheck == 0)
            {
                roop = false;
            }
            else
            {
                roop = true;
            }
            while (count < Int32.Parse(gyousu[1]))
            {

                string[] macrowake = macrowake1[count].Split('\n');//その行の内容
                int waittime = Int32.Parse(macrowake[4]);
                if (waittime != 0)
                {
                    await Task.Delay(waittime);
                }
                if (macrowake[1] == "待機")
                {

                    string[] applicationwake0 = macrowake[5].Split('\f');
                    switch (applicationwake0[0])
                    {
                        case "無し":
                            commnadtype = 0;
                            break;
                        case "アプリ起動":
                            commnadtype = 1;
                            applicationname = applicationwake0[1];
                            break;
                        case "アプリ終了":
                            commnadtype = 2;
                            applicationname = applicationwake0[1];
                            break;
                        case "パソコン終了":
                            commnadtype = 3;
                            break;
                        case "パソコン再起動":
                            commnadtype = 4;
                            break;
                        case "メッセージ":
                            commnadtype = 5;
                            applicationname = applicationwake0[1];
                            break;
                    }

                    Taikimacro(waittime, commnadtype, applicationname);
                }
                else
                {
                    int counter = Int32.Parse(macrowake[0]);
                    if (macrowake[1] == "無し")
                    {
                        timecheck = false;
                    }
                    else
                    {
                        timecheck = true;
                    }
                    timemoji = macrowake[1];

                    string jiku = macrowake[2];
                    string key = macrowake[3];
                    waittime = Int32.Parse(macrowake[4]);


                    string[] applicationwake0 = macrowake[5].Split('\f');
                    switch (applicationwake0[0])
                    {
                        case "無し":
                            commnadtype = 0;
                            break;
                        case "アプリ起動":
                            commnadtype = 1;
                            applicationname = applicationwake0[1];
                            break;
                        case "アプリ終了":
                            commnadtype = 2;
                            applicationname = applicationwake0[1];
                            break;
                        case "パソコン終了":
                            commnadtype = 3;
                            break;
                        case "パソコン再起動":
                            commnadtype = 4;
                            break;
                        case "メッセージ":
                            commnadtype = 5;
                            applicationname = applicationwake0[1];

                            break;
                        default:
                            MessageBox.Show(applicationwake0[0]);
                            break;
                    }
                    if (timecheck == true)
                    {
                        string timenow = DateTime.Now.ToString("HH:mm:ss");
                        while (timemoji != timenow)
                        {
                            timenow = DateTime.Now.ToString("HH:mm:ss");
                            await Task.Delay(200);
                        }
                        Macro(counter, true, timemoji, jiku, key, waittime, commnadtype, applicationname);
                    }
                    else
                    {
                        Macro(counter, false, timemoji, jiku, key, waittime, commnadtype, applicationname);
                    }

                    nextcheck = false;
                }



                count++;

            }

            End();
        }
        bool firstcheck = false;
        bool mousecheck = true;
        string[] mousejiku;
        private void Macro(int macrocount, bool timecheck, string jikan, string jiku, string key, int taiki, int commandtype1, string commandname)
        {
            if (jiku == "無し")
            {
                mousecheck = false;
            }
            else
            {
                mousecheck = true;
                mousejiku = jiku.Split(',');
            }


            switch (commandtype1)
            {
                case 0:
                    break;
                case 1:
                    System.Diagnostics.Process p = System.Diagnostics.Process.Start(commandname);
                    break;
                case 2:
                    System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(commandname);
                    foreach (System.Diagnostics.Process pc in ps)
                    {
                        pc.Kill();
                    }
                    break;
                case 3:
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "shutdown.exe";
                        psi.Arguments = "-s -t 0";

                        psi.CreateNoWindow = true;
                        Process psd = Process.Start(psi);
                    }
                    catch
                    {
                        MessageBox.Show("エラーが発生しました", "エラー");
                    }
                    break;
                case 4:
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "shutdown.exe";
                        psi.Arguments = "-r -t 0";
                        psi.CreateNoWindow = true;
                        Process prb = Process.Start(psi);
                    }
                    catch
                    {
                        MessageBox.Show("エラーが発生しました", "エラー");
                    }
                    break;
                case 5:
                    MessageBox.Show(commandname);
                    break;
            }
            if(mousecheck == true)
            {
                int xjiku = Int32.Parse(mousejiku[0]);
                int yjiku = Int32.Parse(mousejiku[1]);
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(xjiku, yjiku);

            }



            string[] keytype = key.Split(':');
            if (keytype[0] == "click")
            {
                switch (keytype[1])
                {
                    case "LButton":
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                        break;
                    case "RButton":
                        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                        break;
                    case "MButton":
                        mouse_event(MOUSEEVENTF_MBUTTONDOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_MBUTTONUP, 0, 0, 0, 0);

                        break;
                    case "XButton":
                        mouse_event(MOUSEEVENTF_XBUTTON1DOWN, 0, 0, 0, 0);
                        mouse_event(MOUSEEVENTF_XBUTTON1UP, 0, 0, 0, 0);
                        break;

                    default:
                        SendKeys.Send("{" + keytype[1] + "}");
                        break;
                }



            }
            if (keytype[0] == "send")
            {
                SendKeys.Send(keytype[1]);
            }

            nextcheck = true;
            //f1.ReceiveData2 =macrocount;

        }
        private void Taikimacro(int wait, int commandtype, string commanname)
        {

            switch (commandtype)
            {
                case 0:
                    break;
                case 1:
                    System.Diagnostics.Process p = System.Diagnostics.Process.Start(commanname);
                    break;
                case 2:
                    System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName(commanname);
                    foreach (System.Diagnostics.Process pc in ps)
                    {
                        pc.Kill();
                    }
                    break;
                case 3:
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "shutdown.exe";
                        psi.Arguments = "-s -t 0";

                        psi.CreateNoWindow = true;
                        Process psd = Process.Start(psi);
                    }
                    catch
                    {
                        MessageBox.Show("エラーが発生しました", "エラー");
                    }
                    break;
                case 4:
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "shutdown.exe";
                        psi.Arguments = "-r -t 0";
                        psi.CreateNoWindow = true;
                        Process prb = Process.Start(psi);
                    }
                    catch
                    {
                        MessageBox.Show("エラーが発生しました", "エラー");
                    }
                    break;
                case 5:
                    MessageBox.Show(commanname);
                    break;
            }
            nextcheck = true;
        }
        
        public string SendData2
        {
            set
            {
                sendData2 = value;
                End();
            }
            get
            {
                return sendData2;
            }
        }
        private void End()
        {
            while(firstcheck == true)
            {

            }
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (f1 != null)
            {
                f1.ReceiveData2 = 0;
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
