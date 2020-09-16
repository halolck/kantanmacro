using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace macroproject
{
    public partial class Form1 : Form
    {
        #region プロパティ
        public string ReceiveData
        {
            set
            {
                receiveData = value;
                string[] wake = receiveData.Split('\a');
                textBox1.Text = wake[0];
                textBox2.Text = wake[1];
            }
            get
            {
                return receiveData;
            }
        }

        public string ReceiveData1
        {
            set
            {
                receiveData1 = value;
                f2 = new Form2();
                f2.f1 = this;

            }
            get
            {
                return receiveData1;
            }
        }

        public int ReceiveData2
        {
            set
            {
                reciveData2 = value;
                button5.Text = "Start";
                f3 = new Form3();
                f3.f1 = this;
            }
            get
            {
                return reciveData2;
            }
        }

        public int ReceiveData3
        {
            set
            {
                reciveData3 = value;

            }
            get
            {
                return reciveData3;
            }
        }
        #endregion

        #region フィールド
        Form2 f2;
        Form3 f3;
        private string receiveData = "";
        private string receiveData1 = "";
        private string key = "LButton";
        private string time;
        private string sendkey;
        private string commandsend;
        private int reciveData2;
        private int reciveData3;
        private int commandcombo = 0;
        private int rowscount;
        private int backcount;
        private int roopcheck = 0;
        private int count = 0;
        private bool roop = false;
        private bool keyinputjoutai = false;
        #endregion

        #region コンストラクタ
        public Form1()
        {
            InitializeComponent();
            f2 = new Form2();
            f2.f1 = this;
            f3 = new Form3();
            f3.f1 = this;
        }
        #endregion

        #region プライベート関数
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            IEnumerable<string> files =
            System.IO.Directory.EnumerateFiles(
                @"data", "*", System.IO.SearchOption.AllDirectories);
            foreach (string f in files)
            {
                string f2 = f.Remove(0, 5);
                string[] f3 = f2.Split('.');
                comboBox1.Items.Add(f3[0]);
            }

            DateTime dtn = DateTime.Now;
            label1.Text = dtn.ToString("HH:mm:ss");
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dtn = DateTime.Now;
            label1.Text = dtn.ToString("HH:mm:ss");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "作製")
            {
                count++;

                if (checkBox2.Checked == true)
                {
                    switch (commandcombo)
                    {
                        case 0:
                            commandsend = "無し";
                            break;
                        case 1:
                            commandsend = "アプリ起動:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 2:
                            commandsend = "アプリ終了:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 3:
                            commandsend = "パソコンを終了";
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 4:
                            commandsend = "パソコンを再起動";
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 5:
                            commandsend = "メッセージ1:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 6:
                            commandsend = "url:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 7:
                            commandsend = "メッセージ2:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;


                    }
                    dataGridView1.Rows.Add(count, "待機", "", "", textBox4.Text, commandsend);
                }
                else
                {
                    if (radioButton1.Checked == true)
                    {
                        time = "無し";
                    }
                    else
                    {
                        time = dateTimePicker1.Text;
                    }
                    string mouse;
                    string xjiku = textBox1.Text;
                    string yjiku = textBox2.Text;
                    if (checkBox3.Checked == true)
                    {
                        mouse = "無し";

                    }
                    else
                    {
                        mouse = xjiku + "," + yjiku;
                    }
                    if (radioButton3.Checked == true)
                    {
                        sendkey = "click:" + key;
                    }
                    if (radioButton4.Checked == true)
                    {
                        sendkey = "send:" + textBox3.Text;
                    }
                    if (radioButton5.Checked == true)
                    {
                        sendkey = "無し\t";
                    }
                    switch (commandcombo)
                    {
                        case 0:
                            commandsend = "無し";
                            break;
                        case 1:
                            commandsend = "アプリ起動:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 2:
                            commandsend = "アプリ終了:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 3:
                            commandsend = "パソコンを終了";
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 4:
                            commandsend = "パソコンを再起動";
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 5:
                            commandsend = "メッセージ1:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 6:
                            commandsend = "url:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                        case 7:
                            commandsend = "メッセージ2:" + textBox5.Text;
                            comboBox2.SelectedIndex = 0;
                            break;
                    }

                    dataGridView1.Rows.Add(count, time, mouse, sendkey, textBox4.Text, commandsend);

                }
                textBox5.ResetText();
            }
            else
            {
                Hensyuhozon();
                textBox5.ResetText();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f2.Show();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Sakujogyou();
        }
        private void Sakujogyou()
        {
            int senyoucount = 0;
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    dataGridView1.Rows.Remove(r);
                    count--;
                }
            }
            while (senyoucount != dataGridView1.Rows.Count)
            {
                dataGridView1.Rows[senyoucount].Cells[0].Value = (senyoucount + 1).ToString();

                senyoucount++;

            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;

            }
            else
            {
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                groupBox4.Enabled = true;


            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                roop = true;
            }
            else
            {
                roop = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "iniファイル(*.ini)|*.ini";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;
            string path = System.Environment.CurrentDirectory;
            sfd.InitialDirectory = path + @"\data";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                System.IO.Stream stream;
                stream = sfd.OpenFile();
                if (stream != null)
                {

                    System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            sw.Write(dataGridView1[j, i].Value + ";");
                        }
                        sw.WriteLine();
                    }

                    sw.Close();
                    stream.Close();
                }
                comboBox1.Items.Clear();
                IEnumerable<string> files = System.IO.Directory.EnumerateFiles(
                @"data", "*", System.IO.SearchOption.AllDirectories);
                foreach (string f in files)
                {
                    string f2 = f.Remove(0, 5);
                    string[] f3 = f2.Split('.');
                    comboBox1.Items.Add(f3[0]);
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("本当にすべて削除しますか？一度削除すると戻すことは出来ません。", "全削除",
             MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                comboBox1.Items.Clear();
                IEnumerable<string> files =
           System.IO.Directory.EnumerateFiles(
               @"data", "*", System.IO.SearchOption.AllDirectories);
                foreach (string f in files)
                {
                    string f2 = f.Remove(0, 5);
                    string[] f3 = f2.Split('.');
                    comboBox1.Items.Add(f3[0]);
                }
            }
            count = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\data";

            if (comboBox1.Text != "" && dataGridView1.Rows.Count != 0 && dataGridView1.Rows.Count == 0)
            {
                DialogResult result = MessageBox.Show("保存しますか?", "保存確認",
             MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "iniファイル(*.ini)|*.ini";
                    sfd.FilterIndex = 2;
                    sfd.RestoreDirectory = true;
                    sfd.InitialDirectory = path;
                    sfd.AddExtension = true;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {

                        System.IO.Stream stream;
                        stream = sfd.OpenFile();
                        if (stream != null)
                        {
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                {
                                    sw.Write(dataGridView1[j, i].Value + ";");
                                }
                                sw.WriteLine();
                            }

                            sw.Close();
                            stream.Close();
                        }
                        comboBox1.Items.Clear();
                        IEnumerable<string> files = System.IO.Directory.EnumerateFiles(
                        @"data", "*", System.IO.SearchOption.AllDirectories);
                        foreach (string f in files)
                        {
                            string f2 = f.Remove(0, 5);
                            string[] f3 = f2.Split('.');
                            comboBox1.Items.Add(f3[0]);
                        }
                    }
                }
            }


            string cellLine;

            StreamReader sr = new StreamReader(path + @"\" + comboBox1.Text + ".ini", Encoding.UTF8);

            dataGridView1.Rows.Clear();
            while (!sr.EndOfStream)
            {
                cellLine = sr.ReadLine();
                dataGridView1.Rows.Add(cellLine.Split(';'));


            }
            sr.Close();
            count = dataGridView1.Rows.Count;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (button5.Text == "Start")
                {
                    button5.Text = "Stop";
                    f3.Show();
                    rowscount = 1;
                    if (roop == true)
                    {
                        roopcheck = 1;
                    }
                    else
                    {
                        roopcheck = 0;
                    }

                    string applisyoki = dataGridView1.Rows[0].Cells[5].Value.ToString();
                    string applihenkan = applisyoki.Replace("起動:", "起動\f");
                    string applihenkan2 = applihenkan.Replace("終了:", "終了\f");
                    string applihenkanlast1 = applihenkan2.Replace("メッセージ1:", "メッセージ1\f");
                    string applihenkanlast2 = applihenkanlast1.Replace("url:", "url\f");
                    string applihenkanlast5 = applihenkanlast2.Replace("メッセージ2:", "メッセージ2\f");

                    string[] applinemahenkan = applihenkanlast5.Split('\f');

                    string macrookuri = roopcheck.ToString() + "\b" + dataGridView1.Rows.Count.ToString() + "\b" + dataGridView1.Rows[0].Cells[0].Value + "\n" + dataGridView1.Rows[0].Cells[1].Value + "\n" + dataGridView1.Rows[0].Cells[2].Value + "\n" + dataGridView1.Rows[0].Cells[3].Value + "\n" + dataGridView1.Rows[0].Cells[4].Value + "\n" + applihenkanlast5 + "\n" + applinemahenkan;
                    while (rowscount != dataGridView1.Rows.Count)
                    {
                        string applisyoki1 = dataGridView1.Rows[rowscount].Cells[5].Value.ToString();
                        string applihenkan3 = applisyoki1.Replace("起動:", "起動\f");
                        string applihenkan4 = applihenkan3.Replace("終了:", "終了\f");
                        string applihenkanlast3 = applihenkan4.Replace("メッセージ1:", "メッセージ1\f");
                        string applihenkanlast4 = applihenkanlast3.Replace("url:", "url\f");
                        string applihenkanlast6 = applihenkanlast4.Replace("メッセージ2:", "メッセージ2\f");

                        string[] applinemahenkan2 = applihenkanlast6.Split('\f');

                        macrookuri += "\r" + dataGridView1.Rows[rowscount].Cells[0].Value + "\n" + dataGridView1.Rows[rowscount].Cells[1].Value + "\n" + dataGridView1.Rows[rowscount].Cells[2].Value + "\n" + dataGridView1.Rows[rowscount].Cells[3].Value + "\n" + dataGridView1.Rows[rowscount].Cells[4].Value + "\n" + applihenkanlast6 + "\n" + applinemahenkan2;
                        rowscount++;

                    }
                    f3.SendData = macrookuri;
                }
                else
                {
                    f3.SendData2 = "";
                }
            }
            catch
            {
                MessageBox.Show("エラーが発生しました。\r\nデーターが入ってない可能性があります", "エラー");
                f3.SendData2 = "";
            }

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (comboBox2.Text)
            {
                case "無し":
                    textBox5.Enabled = false;
                    button8.Enabled = false;
                    commandcombo = 0;
                    break;
                case "アプリケーションの起動":
                    textBox5.Enabled = true;
                    button8.Enabled = true;
                    commandcombo = 1;
                    label7.Text = "アプリケーションの選択";

                    break;
                case "アプリケーションの終了":
                    textBox5.Enabled = true;
                    button8.Enabled = true;
                    label7.Text = "アプリケーションの選択";

                    commandcombo = 2;
                    break;
                case "パソコンの電源を切る":
                    textBox5.Enabled = false;
                    button8.Enabled = false;
                    commandcombo = 3;
                    break;
                case "パソコンを再起動":
                    textBox5.Enabled = false;
                    button8.Enabled = false;
                    commandcombo = 4;
                    break;
                case "メッセージ(マクロ一時停止)":
                    textBox5.Enabled = true;
                    button8.Enabled = false;
                    label7.Text = "メッセ―ジ内容";
                    commandcombo = 5;
                    break;
                case "urlを開く(chromeonly)":
                    textBox5.Enabled = true;
                    button8.Enabled = false;
                    label7.Text = "url";
                    commandcombo = 6;
                    break;
                case "メッセージを左上に表示":
                    textBox5.Enabled = true;
                    button8.Enabled = false;
                    label7.Text = "メッセージ内容";
                    commandcombo = 7;
                    break;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "exeファイル(*.exe)|*.exe;|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.Title = "アプリケーションを選択してください";
            ofd.RestoreDirectory = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = ofd.FileName;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\data";
            File.Delete(path + @"\" + comboBox1.Text + ".ini");
            comboBox1.Items.Remove(comboBox1.Text);
            dataGridView1.Rows.Clear();

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (keyinputjoutai == true)
            {
                foreach (Int32 i in Enum.GetValues(typeof(Keys)))
                {
                    if (GetAsyncKeyState(i) == -32767)
                    {
                        key = Enum.GetName(typeof(Keys), i);
                        if (key == "XButton1" || key == "XButton2")
                        {
                            key = "XButton";
                        }
                        textBox6.Text = key;
                        label8.Visible = false;
                        keyinputjoutai = false;
                        button3.Enabled = true;
                        timer2.Stop();
                    }
                }
            }
        }
        private void textBox6_Click(object sender, EventArgs e)
        {
            keyinputjoutai = true;
            textBox6.Text = "";
            button3.Enabled = false;
            label8.Visible = true;
            timer2.Start();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("このツールは室伏がどのようなツールを作ったことあるか、と聞かれたときに出すために作ったツールです。\r\n元々uwscから" +
                "プログラミング関係のものに興味を持ちました。そのためマクロスクリプトを作るだけでなくマクロを操作、作成するツールを自分で作ってみたいという重いからコノツールをつくりました。\r\n**今後の目標**\r\n安定化!!\r\n誰でも使えるわかりやすいものにする\r\n画像認識クリック(画像内ランダムクリックなど)の追加\r\n**はじめと比べて**\r\n・コマンドの追加(メッセージ表示)\r\n・コマンドを利用できない問題を訂正(指定文字列か確認の際文字列が普通に間違えていた)\r\n・マクロ中も止まらないように待機を変更(async利用)\r\n・時間指定,ループ追加", "このツールについて");
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (dataGridView1.Rows[0].Selected && dataGridView1.SelectedCells.Count > 0 && button3.Text == "作製")
            {
                ContextMenuStrip cntmenu = new ContextMenuStrip();
                {
                    ToolStripMenuItem newcontitem = new ToolStripMenuItem();
                    ToolStripMenuItem newcontitem2 = new ToolStripMenuItem();

                    newcontitem.Text = "選択している行を削除";
                    newcontitem2.Text = "選択している行を編集";

                    newcontitem.Click += delegate
                    {
                        Sakujogyou();
                    };
                    newcontitem2.Click += delegate
                    {
                        Hensyu();
                    };
                    cntmenu.Items.Add(newcontitem);
                    cntmenu.Items.Add(newcontitem2);

                }
                this.ContextMenuStrip = cntmenu;
            }

        }

        private void Hensyuhozon()
        {
            button5.Enabled = true;
            comboBox1.Enabled = true;
            button3.Text = "作製";
            checkBox1.Enabled = true;
            button6.Enabled = true;
            button2.Enabled = true;
            button9.Enabled = true;
            button4.Enabled = true;
            button7.Enabled = true;
            checkBox2.Checked = false;
            this.Text = "マクロ簡単作成";
            if (checkBox2.Checked == true)
            {
                switch (commandcombo)
                {
                    case 0:
                        commandsend = "無し";
                        break;
                    case 1:
                        commandsend = "アプリ起動:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 2:
                        commandsend = "アプリ終了:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 3:
                        commandsend = "パソコンを終了";
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 4:
                        commandsend = "パソコンを再起動";
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 5:
                        commandsend = "メッセージ1:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 6:
                        commandsend = "url:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 7:
                        commandsend = "メッセージ2:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;


                }
                dataGridView1[1, backcount].Value = "待機";
                dataGridView1[2, backcount].Value = "";
                dataGridView1[3, backcount].Value = "";
                dataGridView1[4, backcount].Value = textBox4.Text;
                dataGridView1[5, backcount].Value = commandsend;
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    time = "無し";
                }
                else
                {
                    time = dateTimePicker1.Text;
                }
                string mouse;
                string xjiku = textBox1.Text;
                string yjiku = textBox2.Text;
                if (checkBox3.Checked == true)
                {
                    mouse = "無し";

                }
                else
                {
                    mouse = xjiku + "," + yjiku;
                }
                if (radioButton3.Checked == true)
                {
                    sendkey = "click:" + key;
                }
                if (radioButton4.Checked == true)
                {
                    sendkey = "send:" + textBox3.Text;
                }
                if (radioButton5.Checked == true)
                {
                    sendkey = "無し\t";
                }
                switch (commandcombo)
                {
                    case 0:
                        commandsend = "無し";
                        break;
                    case 1:
                        commandsend = "アプリ起動:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 2:
                        commandsend = "アプリ終了:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 3:
                        commandsend = "パソコンを終了";
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 4:
                        commandsend = "パソコンを再起動";
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 5:
                        commandsend = "メッセージ1:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 6:
                        commandsend = "url:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                    case 7:
                        commandsend = "メッセージ2:" + textBox5.Text;
                        comboBox2.SelectedIndex = 0;
                        break;
                }
                dataGridView1[1, backcount].Value = time;
                dataGridView1[2, backcount].Value = mouse;
                dataGridView1[3, backcount].Value = sendkey;
                dataGridView1[4, backcount].Value = textBox4.Text;
                dataGridView1[5, backcount].Value = commandsend;

            }
        }

        private void Hensyu()
        {
            if (button3.Text == "編集終了")
            {
                DialogResult result = MessageBox.Show("現在編集中のデーターがあります。\r\n編集データーを保存してから新たに編集しますか？",
                    "編集中",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2);

                if (result == DialogResult.Yes)
                {
                    Hensyuhozon();
                    textBox5.ResetText();
                    Hensyustart();
                }

            }
            else
            {
                Hensyustart();
            }


        }
        private void Hensyustart()
        {

            if (dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("編集モードは複数選択時は利用できません。\r\n１行のみ選択してください。", "編集エラー"); ;
            }
            else
            {
                this.Text = "マクロ簡単作成[編集中]";
                button5.Enabled = false;
                comboBox1.Enabled = false;
                button3.Text = "編集終了";
                checkBox1.Enabled = false;
                button6.Enabled = false;
                button2.Enabled = false;
                button9.Enabled = false;
                button4.Enabled = false;
                button7.Enabled = false;
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        backcount = r.Index;
                        string time = (string)dataGridView1.Rows[r.Index].Cells[1].Value;

                        switch (time)
                        {
                            case "待機":
                                checkBox2.Checked = true;
                                break;
                            case "無し":
                                checkBox2.Checked = false;
                                radioButton1.Checked = true;
                                break;
                            default:
                                checkBox2.Checked = false;
                                radioButton2.Checked = true;
                                break;
                        }

                        string mouse = (string)dataGridView1.Rows[r.Index].Cells[2].Value;
                        if (mouse == "無し")
                        {
                            checkBox3.Checked = true;
                            textBox1.Text = "0";
                            textBox2.Text = "0";
                        }
                        else
                        {
                            try
                            {
                                checkBox3.Checked = false;
                                string[] jiku = mouse.Split(',');
                                textBox1.Text = jiku[0];
                                textBox2.Text = jiku[1];
                            }
                            catch
                            {
                                textBox1.Text = "0";
                                textBox2.Text = "0";
                            }

                        }
                        string key = (string)dataGridView1.Rows[r.Index].Cells[3].Value;
                        switch (key)
                        {
                            case "無し":
                                radioButton5.Checked = true;
                                textBox3.Enabled = false;
                                textBox3.Text = "";
                                break;
                            case "key:":
                                radioButton3.Checked = true;
                                textBox3.Enabled = false;
                                textBox3.Text = "";
                                string[] key2 = key.Split(':');
                                label8.Text = key2[2];
                                break;
                            case "send:":
                                radioButton4.Checked = true;
                                textBox3.Enabled = true;
                                string[] send2 = key.Split(':');
                                textBox3.Text = send2[1];
                                break;
                        }


                        string wait = (string)dataGridView1.Rows[r.Index].Cells[4].Value;
                        textBox4.Text = wait;
                        string command = (string)dataGridView1.Rows[r.Index].Cells[5].Value;
                        switch (command)
                        {
                            case string a when a.Contains("無し"):
                                comboBox2.SelectedIndex = 0;
                                command = command.Remove(0, 2);
                                break;
                            case string a when a.Contains("アプリ起動"):
                                comboBox2.SelectedIndex = 3;
                                command = command.Remove(0, 6);
                                break;
                            case string a when a.Contains("アプリ終了"):
                                comboBox2.SelectedIndex = 4;
                                command = command.Remove(0, 6);
                                break;
                            case string a when a.Contains("パソコンを終了"):
                                comboBox2.SelectedIndex = 5;
                                command = command.Remove(0, 7);
                                break;
                            case string a when a.Contains("パソコンを再起動"):
                                comboBox2.SelectedIndex = 6;
                                command = command.Remove(0, 8);
                                break;
                            case string a when a.Contains("メッセージ2"):
                                comboBox2.SelectedIndex = 2;
                                command = command.Remove(0, 7);
                                break;
                            case string a when a.Contains("メッセージ1"):
                                comboBox2.SelectedIndex = 1;
                                command = command.Remove(0, 7);
                                break;
                            case string a when a.Contains("url"):
                                comboBox2.SelectedIndex = 5;
                                command = command.Remove(0, 4);
                                break;
                        }

                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                button1.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
        }
        #endregion

        #region dllinport
        [DllImport("user32")] private static extern short GetAsyncKeyState(int vKey);
        #endregion

    }
}
