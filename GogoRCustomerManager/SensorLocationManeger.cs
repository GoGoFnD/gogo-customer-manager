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
using GMap.NET;
using GMap.NET.WindowsForms;
using MySql.Data.MySqlClient;

namespace GogoRCustomerManager
{
    public partial class SensorLocationManeger : Form
    {
        AESUtill aESUtill;
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

            gosafeMap.RemoveMarkers();
            // 날짜 범위를 출력
            string whereClause = "";
            aESUtill = new AESUtill();
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
            string selectQuery2 = "SELECT rPhoneNumer, rBikeNumber, rGpsLastLatitude, rGpsLastLongitude FROM tb_lte_rider_info " + whereClause + ";";
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
                        if (dataTable != null)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                Console.WriteLine(row["rGpsLastLatitude"]);
                                Console.WriteLine(row["rGpsLastLongitude"] != DBNull.Value);
                                if (row["rGpsLastLatitude"] != DBNull.Value && row["rGpsLastLongitude"] != DBNull.Value)
                                {
                                    var gpsLatitude = aESUtill.AESDecrypt128(row["rGpsLastLatitude"].ToString());
                                    row["rGpsLastLatitude"] = gpsLatitude;
                                    var gpsLongitude = aESUtill.AESDecrypt128(row["rGpsLastLongitude"].ToString());
                                    row["rGpsLastLongitude"] = gpsLongitude;
                                    Console.WriteLine(gpsLatitude);
                                    gosafeMap.AddMarker(Double.Parse(gpsLatitude), Double.Parse(gpsLongitude));
                                }
                            }
                            Console.WriteLine(gosafeMap.Position);

                        }
                        else
                        {
                            MessageBox.Show("오류: 너무 큰 범위를 검색하셨습니다.", "오류");
                        }
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
                SearchDate.Text = DateTime.Now.ToString();
            }

            return dataTable;
        }

        private void SensorDataSelected()
        {
            if (SensorDataGrid.SelectedRows[0].Cells[2].Value.ToString().Length != 0 && SensorDataGrid.SelectedRows[0].Cells[3].Value.ToString().Length != 0)
            {
                string lat = SensorDataGrid.SelectedRows[0].Cells[2].Value.ToString();
                string lng = SensorDataGrid.SelectedRows[0].Cells[3].Value.ToString();
                gMapControl1.Position = new PointLatLng(Double.Parse(lat), Double.Parse(lng));
                gosafeMap.AddSelectedMarker(Double.Parse(lat), Double.Parse(lng));
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            BindingSource bindingSource = new BindingSource();

            SensorDataGrid.DataSource = CSenIDFind();

            connection.Close();
        }

        private void SensorDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void SensorDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SensorDataSelected();
        }
    }
}
