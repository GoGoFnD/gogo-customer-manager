using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BetterTabs;
using GMap.NET.WindowsForms;
using MySql.Data.MySqlClient;

namespace GogoRCustomerManager
{
    public partial class SensorLocationManeger : Form
    {
        GosafeMap gosafeMap;
        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
           (
               "Server=cf.navers.co.kr ;Port= 3306; Database=goSafe; Uid=gosafe; Pwd=gogofnd0@; allow user variables=true;charset=utf8mb4;"
           );
        public SensorLocationManeger()
        {
            InitializeComponent();
            //SearchTextBox.p
            GMapSetting();

        }

        private void GMapSetting()
        {
            gosafeMap = new GosafeMap(gMapControl1);
            gMapControl1.Visible = true;

            gMapControl1.CenterPen.Color = Color.DeepSkyBlue;
            gMapControl1.Zoom = 14;
            gMapControl1.ShowCenter = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchText_Click(object sender, EventArgs e)
        {

        }
        private void CSenIDFindButton_Click(object sender, EventArgs e)
        {
            
        }
        private DataTable CSenIDFind()
        {

            // 날짜 범위를 출력
            string whereClause = "";
            if (SensorNumTextBox.Text.Length != 0 || BikeNumTextbox.Text.Length != 0)
            {
                whereClause = "WHERE";
            }
            //if (conAgency != null) whereClause += conjunction + " " + conAgency;
            //if (conIsWork != null) whereClause += conjunction + " " + conIsWork;
            Console.WriteLine(SensorNumTextBox != null);
            Console.WriteLine(BikeNumTextbox);

            if (SensorNumTextBox.Text.Length != 0) whereClause += " rPhoneNumer LIKE '%" + SensorNumTextBox.Text + "%'";
            if (BikeNumTextbox.Text.Length != 0)
            {
                if(BikeNumTextbox.Text.Length != 0 && SensorNumTextBox.Text.Length != 0)
                {
                    whereClause += " and rBikeNumber LIKE '%" + BikeNumTextbox.Text + "%'";
                }
                else
                {
                    whereClause += " rBikeNumber LIKE '%" + BikeNumTextbox.Text + "%'";
                }
            }
            // 동적 SQL을 생성
            StringBuilder sqlBuilder = new StringBuilder();
            string selectQuery2 = "SELECT rPhoneNumer, rBikeNumber FROM tb_lte_rider_info " + whereClause + ";";
            Console.WriteLine(selectQuery2);

            sqlBuilder.AppendLine("SELECT rBikeNumber, rPhoneNumer");
            sqlBuilder.AppendLine("FROM tb_lte_rider_info");
            sqlBuilder.AppendLine("WHERE ;");
            //Console.WriteLine(sqlBuilder.ToString());
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(selectQuery2, connection))
                {
                    // 파라미터 설정
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                // 커넥션 닫기
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return dataTable;
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            BindingSource bindingSource = new BindingSource();

            dataGridView1.DataSource = CSenIDFind();

            connection.Close();
        }
    }
}
