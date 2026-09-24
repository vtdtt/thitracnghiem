using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thitracnghiem
{
    public partial class Form3 : Form
    {
        private readonly Form2 truocdo;
        private readonly int cauHoiHienTai = 1;
        private System.Windows.Forms.Timer timerlambai;
        public Form3()
        {
            InitializeComponent();
            timerlambai = new System.Windows.Forms.Timer();
            timerlambai.Interval = 1000; // 1 second
            timerlambai.Tick += Timerlambai_Tick;
            this.FormClosed += (s, e) => timerlambai.Dispose(); // Stop timer when form is closed
        }
        public Form3(Form2 truocdo) : this()
        {
             this.truocdo = truocdo;
        }
        private void capnhatdongho()
        {
            TimeSpan conlai = baithi.thoigianketthuc - DateTime.Now;
            if (conlai <= TimeSpan.Zero)
            {
                timerlambai.Stop();
                luuDapAn();
                MessageBox.Show("Hết thời gian làm bài! Bài thi sẽ được nộp tự động.");
                baithi.nopBai();
                this.Close();
            }
            else
            {
                label1.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", conlai.Hours, conlai.Minutes, conlai.Seconds);
            }
        }
        private void Timerlambai_Tick(object sender, EventArgs e)
        {
            if (!this.Visible) return;
            if (DateTime.Now >= baithi.thoigianketthuc)
            {
                timerlambai.Stop();
                luuDapAn();
                MessageBox.Show("Hết thời gian làm bài! Bài thi sẽ được nộp tự động.");
                baithi.nopBai();
                this.Close();
            }
            else
            {
                capnhatdongho();
            }
        }
        private void Hienthicauhoi()
        {
            if(cauHoiHienTai >= baithi.Danhsach.Count)
            {
                MessageBox.Show("Khong co cau hoi!");
                return;
            }
            cauhoi ch = baithi.Danhsach[cauHoiHienTai];
            label2.Text = ch.NoiDung;
            radioButton1.Text = ch.DapAnA;
            radioButton2.Text = ch.DapAnB;
            radioButton3.Text = ch.DapAnC;
            radioButton4.Text = ch.DapAnD;
            string dapAn;
            baithi.DapAn.TryGetValue(cauHoiHienTai, out dapAn);
            radioButton1.Checked = dapAn == "A";
            radioButton2.Checked = dapAn == "B";
            radioButton3.Checked = dapAn == "C";
            radioButton4.Checked = dapAn == "D";

            }
        private void luuDapAn()
        {
            string dapAn = "";
            if(radioButton1.Checked) dapAn = "A";
            else if (radioButton2.Checked) dapAn = "B";
            else if (radioButton3.Checked) dapAn = "C";
            else if (radioButton4.Checked) dapAn = "D";
            if(dapAn != "")
            {
                baithi.DapAn[cauHoiHienTai] = dapAn;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            if(baithi.Danhsach.Count > cauHoiHienTai)
            {
                Hienthicauhoi();
            }
            else
            {
                MessageBox.Show("Khong co cau hoi!");
            }
            timerlambai.Start();
            capnhatdongho();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ban co chac chan muon thoat khong?",
                "Xac nhan", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                if(truocdo != null)
                {
                    truocdo.Close();
                }
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            luuDapAn();
            DialogResult result = MessageBox.Show("Ban co chac chan muon nop bai khong?",
                "Xac nhan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (baithi.danop)
                    return;
            }
            baithi.nopBai();
            if(truocdo != null)
            {
                truocdo.Close();
            }
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            luuDapAn();
            if(truocdo != null)
            {
                truocdo.Show();
            }
            this.Close();
        }
    }
}
