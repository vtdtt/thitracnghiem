using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace thitracnghiem
{
    public class cauhoi
    {
        public string Id { get; set; }
        public string NoiDung { get; set; }
        public string DapAnA { get; set; }
        public string DapAnB { get; set; }
        public string DapAnC { get; set; }
        public string DapAnD { get; set; }
        public string DapAnDung { get; set; }
    }
    public class baithi
    {
        private const string connectionString = "Data Source=LAPTOP-FCIR975G;Initial Catalog=thitracnghiem;Integrated Security=True";
        public static List<cauhoi> Danhsach = new List<cauhoi>(); 
        public static Dictionary<int, string> DapAn = new Dictionary<int, string>();
        public static readonly TimeSpan thoigianlambai = TimeSpan.FromSeconds(5);
        public static DateTime thoigianketthuc;
        public static bool danop = false;

        public static void Batdau()
        {
            Danhsach.Clear();
            DapAn.Clear();
            foreach (string bang in new[] { "dbo.CauHoi", "dbo.CauHoi2" })
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT * FROM {bang}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cauhoi ch = new cauhoi
                                {
                                    Id = reader["Id"].ToString(),
                                    NoiDung = reader["NoiDung"].ToString(),
                                    DapAnA = reader["DapAnA"].ToString(),
                                    DapAnB = reader["DapAnB"].ToString(),
                                    DapAnC = reader["DapAnC"].ToString(),
                                    DapAnD = reader["DapAnD"].ToString(),
                                    DapAnDung = reader["DapAnDung"].ToString()
                                };
                                Danhsach.Add(ch);
                            }
                        }
                    }
                }
            }
            danop = false;
            thoigianketthuc = DateTime.Now + thoigianlambai;
        }
        public static void nopBai()
        {
            if (danop) return;
            danop = true;
            int tong = Danhsach.Count;
            int dung = 0;
            int chualam = 0;
            for (int i = 0; i < tong; i++)
            {
                if (DapAn.ContainsKey(i))
                {
                    if (DapAn[i] == Danhsach[i].DapAnDung)
                    {
                        dung++;
                    }
                }
                else
                {
                    chualam++;
                }
            }
                int sai = tong - dung - chualam;
                double diem =tong > 0 ? (double)dung / tong * 10 : 0;
                MessageBox.Show(
                    $"Bai thi da nop\n\nTong so cau: {tong}\n" +
                    $"So cau dung: {dung}\n" +
                    $"So cau sai: {sai}\n" +
                    $"So cau chua lam: {chualam}\n" +
                    $"Diem: {diem:F2}",
                    "Ket qua thi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }

