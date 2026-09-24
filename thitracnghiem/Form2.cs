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
    public partial class Form2 : Form
    {
        private int cauHoiHienTai = 0;
        public Form2()
        {
            InitializeComponent();
        }
        private void HienThiCauHoi()
        {
            if (cauHoiHienTai >= 0 && cauHoiHienTai < baithi.Danhsach.Count)
            {
                cauhoi ch = baithi.Danhsach[cauHoiHienTai];
                label2.Text = ch.NoiDung;
                radioButton1.Text = ch.DapAnA;
                radioButton2.Text = ch.DapAnB;
                radioButton3.Text = ch.DapAnC;
                radioButton4.Text = ch.DapAnD;
                // Kiểm tra xem người dùng đã chọn đáp án nào chưa
                if (baithi.DapAn.ContainsKey(cauHoiHienTai))
                {
                    string dapAnDaChon = baithi.DapAn[cauHoiHienTai];
                    switch (dapAnDaChon)
                    {
                        case "A":
                            radioButton1.Checked = true;
                            break;
                        case "B":
                            radioButton2.Checked = true;
                            break;
                        case "C":
                            radioButton3.Checked = true;
                            break;
                        case "D":
                            radioButton4.Checked = true;
                            break;
                    }
                }
                else
                {
                    // Nếu chưa chọn đáp án, bỏ chọn tất cả
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                }
            }
        }
        private void LuuDapAn()
        {
            if (cauHoiHienTai >= 0 && cauHoiHienTai < baithi.Danhsach.Count)
            {
                string dapAnDaChon = null;
                if (radioButton1.Checked) dapAnDaChon = "A";
                else if (radioButton2.Checked) dapAnDaChon = "B";
                else if (radioButton3.Checked) dapAnDaChon = "C";
                else if (radioButton4.Checked) dapAnDaChon = "D";
                if (dapAnDaChon != null)
                {
                    baithi.DapAn[cauHoiHienTai] = dapAnDaChon;
                }
            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                baithi.Batdau();
                HienThiCauHoi();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải câu hỏi: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LuuDapAn();
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn nộp bài không?",
                "Xác nhận", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                baithi.nopBai();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát không? Bài thi sẽ không được nộp.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LuuDapAn();
            Form3 form3 = new Form3(this);
            form3.Show();
            this.Hide();    
        }
    }
}
