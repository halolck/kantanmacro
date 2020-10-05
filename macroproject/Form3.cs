using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace macroproject
{
    public partial class Form3 : Form
    {
        #region 定数
        private const int MOUSEEVENTF_LEFTDOWN = 0x2;
        private const int MOUSEEVENTF_LEFTUP = 0x4;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MBUTTONDOWN = 0x20;
        private const int MOUSEEVENTF_MBUTTONUP = 0x40;
        private const int MOUSEEVENTF_XBUTTON1DOWN = 0x80;
        private const int MOUSEEVENTF_XBUTTON1UP = 0x100;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        #endregion

        #region プロパティー
        public string SendData
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

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.End)
            {
                End();
            }
        }
        #endregion

        #region フィールド
        public Form1 f1;
        GlobalKeyboardHook gHook;
        private string sendData = "";
        private string sendData2 = "";
        private string macrodousa;
        private string timemoji;
        private string applicationname;
        private string[] mousejiku;
        private bool timecheck;
        private bool roop = false;
        private bool syoricheck = true;
        private bool endcheck = false;
        private bool mousecheck = true;
        private int count;
        private int commnadtype = 0;
        DateTime time;
        #endregion

        #region コンストラクタ
        public Form3()
        {
            InitializeComponent();
        }
        #endregion

        #region プライベート関数

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.End)
            {
                End();
            }
        }

        private void Macro(int macrocount, bool timecheck, string jikan, string jiku, string key, int taiki, int commandtype1, string commandname)
        {
            if (endcheck != true)
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
                            MessageBox.Show("エラーが発生しました。\r\nOKを押したら続きからスタートします。", "エラー");
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
                            MessageBox.Show("エラーが発生しました。\r\nOKを押したら続きからスタートします。", "エラー");
                        }
                        break;
                    case 5:
                        MessageBox.Show(commandname);
                        break;
                    case 6:
                        Process.Start("chrome.exe", commandname);
                        break;
                    case 7:
                        label1.Location = new Point(2, 2);
                        label1.Text = commandname;
                        break;

                }
                if (mousecheck == true)
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

                syoricheck = true;
            }

        }
        private void Taikimacro(int wait, int commandtype, string commanname)
        {
            if (endcheck != true)
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
                    case 6:
                        Process.Start("chrome.exe", commanname);
                        break;
                    case 7:
                        label1.Location = new Point(2, 2);
                        label1.Text = commanname;
                        break;

                }
                syoricheck = true;
            }
        }

        private void End()
        {
            label1.Text = "";
            endcheck = true;
            this.Close();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (f1 != null)
            {
                f1.ReceiveData2 = 0;
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook(); // Create a new GlobalKeyboardHook
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            gHook.hook();
        }
        #endregion

        #region async
        async Task Macrosetting()
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
                string[] macrowake = macrowake1[count].Split('\n');
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
                        case "パソコンを終了":
                            commnadtype = 3;
                            break;
                        case "パソコンを再起動":
                            commnadtype = 4;
                            break;
                        case "メッセージ1":
                            commnadtype = 5;
                            applicationname = applicationwake0[1];
                            break;
                        case "url":
                            commnadtype = 6;
                            applicationname = applicationwake0[1];
                            break;
                        case "メッセージ2":
                            commnadtype = 7;
                            applicationname = applicationwake0[1];
                            break;
                        default:
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
                        case "パソコンを終了":
                            commnadtype = 3;
                            break;
                        case "パソコンを再起動":
                            commnadtype = 4;
                            break;
                        case "メッセージ1":
                            commnadtype = 5;
                            applicationname = applicationwake0[1];

                            break;
                        case "url":
                            commnadtype = 6;
                            applicationname = applicationwake0[1];
                            break;
                        case "メッセージ2":
                            commnadtype = 7;
                            applicationname = applicationwake0[1];
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

                }

                syoricheck = false;

                count++;

            }
            if (roop == true)
            {
                Macrosetting();
            }
            else
            {
                End();
            }
        }
        #endregion

        #region dllimport
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        #endregion

        #region protected
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TOOLWINDOW;
                return cp;
            }
        }
        #endregion
 
    }
}
