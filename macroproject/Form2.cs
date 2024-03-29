﻿using System;
using System.Windows.Forms;

namespace macroproject
{
    public partial class Form2 : Form
    {
        #region フィールド
        public Form1 f1;
        private const int WS_EX_TOOLWINDOW = 0x00000080;
        private int x = 0;
        private int y = 0;
        private int h = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        private int w = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        private int egg = 0;
        #endregion

        #region コンストラクタ
        public Form2()
        {
            InitializeComponent();
        }
        #endregion

        #region プライベート関数
        private void Form2_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            x = System.Windows.Forms.Cursor.Position.X;
            y = System.Windows.Forms.Cursor.Position.Y;
            label1.Text = "X:" + x;
            label2.Text = "Y:" + y;
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            x = System.Windows.Forms.Cursor.Position.X;
            y = System.Windows.Forms.Cursor.Position.Y;
            label1.Text = "X:" + x;
            label2.Text = "Y:" + y;
            if (x < w / 2)
            {
                panel1.Left = x + 5;
            }
            else
            {
                panel1.Left = x - 176;
            }

            if (y < h / 2)
            {
                panel1.Top = y + 20;
            }
            else
            {
                panel1.Top = y - 116;
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F)
            {
                panel1.Visible = false;
                MessageBox.Show("マウス位置を保存しました。\r\nX:" + x + "\r\nY:" + y, "マウス設定");
                if (f1 != null)
                {
                    f1.ReceiveData = x.ToString() + "\a" + y.ToString();
                }
                this.Close();
            }
            if (e.KeyData == Keys.Escape)
            {
                this.Close();

            }

            #region easteregg
            if (e.KeyCode == Keys.M && egg == 0)
            {
                egg = 1;
                MessageBox.Show("1");
            }


            if (e.KeyCode == Keys.Y && egg == 1)
            {
                egg = 2;
                MessageBox.Show("2");

            }


            if (e.KeyCode == Keys.S && egg == 2)
            {
                MessageBox.Show("3");

                MessageBox.Show("暇人なあなたへ\r\nよくこのメッセージを見つけたね!!コード見たのかな？\r\nそんな君には特別にランダムなマウス位置を保存しておくね!!", "春卵1");
                Random rx = new System.Random();
                Random ry = new System.Random();
                int randomx = rx.Next(0, w);
                int randomy = ry.Next(0, h);
                MessageBox.Show("マウス位置を保存しました。\r\nX:" + randomx + "\r\nY:" + randomy, "マウス設定");
                if (f1 != null)
                {
                    f1.ReceiveData = randomx.ToString() + "\a" + randomy.ToString();
                }
                this.Close();
            }

            #endregion





        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (f1 != null)
            {
                f1.ReceiveData1 = "kakunin";
            }
        }
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
