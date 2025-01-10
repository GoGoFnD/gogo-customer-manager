using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml.Linq;

namespace GogoRCustomerManager
{
    public partial class GosafeDataMapview : Form
    {
        cSenIDTab cSenIDTab;
        GosafeMap gosafeMap;
        AESUtill aESUtill;
        string phoneNumPatten = @"^\+82-?(12|2|[3-9]\d)-?\d{3,4}-?\d{4}$";
        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
           (
               "Server=cf.navers.co.kr ;Port= 3306; Database=goSafe; Uid=gosafe; Pwd=gogofnd0@; allow user variables=true;"
           );
        public GosafeDataMapview()
        {
            InitializeComponent();
            DatePickerSet();
            GMapSetting();
        }
        private void GMapSetting()
        {
            gosafeMap = new GosafeMap(gMapControl);
            gosafeMap.Position = new PointLatLng(37.497872, 127.0275142);
            Console.WriteLine(gosafeMap.Position);
           
        }
        private void DatePickerSet()
        {
            startTime.Format = DateTimePickerFormat.Custom;
            startTime.CustomFormat = "yyyy-MM-dd";
            endTime.Format = DateTimePickerFormat.Custom;
            endTime.CustomFormat = "yyyy-MM-dd";

            startTime.MinDate = new DateTime(2024, 1, 22);
            startTime.MaxDate = DateTime.Today;
            endTime.MinDate = new DateTime(2024, 1, 22);
            endTime.MaxDate = DateTime.Today;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GosafeDataMapview_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void CSenIDFindButton_Click(object sender, EventArgs e)
        {
            cSenIDTab = new cSenIDTab(cSenIDTextBox);
            BindingSource bindingSource = new BindingSource();

            bindingSource.DataSource = CSenIDFind(startTime.Value, endTime.Value);
            cSenIDTab.dataTable = bindingSource;

            connection.Close();
            cSenIDTab.ShowDialog();
        }
        private DataTable CSenIDFind(DateTime startDate, DateTime endDate)
        {
            
            // 날짜 범위를 출력
            Console.WriteLine($"Start Date: {startDate:yyyy-MM-dd}");
            Console.WriteLine($"End Date: {endDate:yyyy-MM-dd}");

            // 동적 SQL을 생성
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.AppendLine("SET SESSION group_concat_max_len = 1000000;");
            sqlBuilder.AppendLine("WITH RECURSIVE DateRange AS (");
            sqlBuilder.AppendLine("  SELECT @StartDate AS DateValue");
            sqlBuilder.AppendLine("  UNION ALL");
            sqlBuilder.AppendLine("  SELECT DATE_ADD(DateValue, INTERVAL 1 DAY)");
            sqlBuilder.AppendLine("  FROM DateRange");
            sqlBuilder.AppendLine("  WHERE DateValue < @EndDate");
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("SELECT GROUP_CONCAT(CONCAT('SELECT DISTINCT cSenID FROM tb_sensordata_', DATE_FORMAT(DateValue, '%Y%m%d')) SEPARATOR ' UNION ALL ') INTO @sql");
            sqlBuilder.AppendLine("FROM DateRange;");
            sqlBuilder.AppendLine("PREPARE stmt FROM @sql;");
            sqlBuilder.AppendLine("EXECUTE stmt;");
            sqlBuilder.AppendLine("DEALLOCATE PREPARE stmt;");
            Console.WriteLine(sqlBuilder.ToString());
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(sqlBuilder.ToString(), connection))
                {
                    // 파라미터 설정
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

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
        private DataTable BikeLogFind(DateTime startDate, DateTime endDate)
        {
            string ID = cSenIDTextBox.Text.ToString();
            // 동적 SQL을 생성
            StringBuilder sqlBuilder = new StringBuilder();

            sqlBuilder.AppendLine("WITH RECURSIVE DateRange AS (");
            sqlBuilder.AppendLine("  SELECT @StartDate AS DateValue");
            sqlBuilder.AppendLine("  UNION ALL");
            sqlBuilder.AppendLine("  SELECT DATE_ADD(DateValue, INTERVAL 1 DAY)");
            sqlBuilder.AppendLine("  FROM DateRange");
            sqlBuilder.AppendLine("  WHERE DateValue < @EndDate");
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("SELECT GROUP_CONCAT(CONCAT('SELECT cSenID, cSenDate, cSenTime, gpsLatitude, gpsLongitude, gpsSpeed FROM tb_sensordata_', DATE_FORMAT(DateValue, '%Y%m%d'),");
            sqlBuilder.AppendLine($"         ' WHERE cSenID = \"{ID}\"') ");
            sqlBuilder.AppendLine(" SEPARATOR ' UNION ALL ') INTO @sql");
            sqlBuilder.AppendLine("FROM DateRange;");
            sqlBuilder.AppendLine("PREPARE stmt FROM @sql;");
            sqlBuilder.AppendLine("EXECUTE stmt;");
            sqlBuilder.AppendLine("DEALLOCATE PREPARE stmt;");
            Console.WriteLine(sqlBuilder.ToString());
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(sqlBuilder.ToString(), connection))
                {
                    // 파라미터 설정
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        Console.WriteLine(adapter);
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
            SensorDataGrid.DataSource = null;
            aESUtill = new AESUtill();
            if (Regex.IsMatch(cSenIDTextBox.Text.ToString(), phoneNumPatten))
            {
                DateTime startTimeD = DateTime.Now;

                // dataTable 객체로 받고
                DataTable result = BikeLogFind(startTime.Value.Date, endTime.Value.Date);


                DateTime endTimeD = DateTime.Now;

                TimeSpan ts = endTimeD - startTimeD;
                Console.WriteLine($"Elapsed Time: {ts.TotalMilliseconds} ms");
                startTimeD = DateTime.Now;
                // foreach, for 사용하여 위도/경도 복호화 후 setting
                //foreach (DataRow row in result.Rows)
                //{
                //    if (row["gpsLatitude"] != DBNull.Value && row["gpsLongitude"] != DBNull.Value)
                //    {
                //        // 병렬로 AES 복호화 처리
                //        //var decryptTaskLatitude = Task.Run(() => aESUtill.AESDecrypt128(row["gpsLatitude"].ToString()));
                //        //var decryptTaskLongitude = Task.Run(() => aESUtill.AESDecrypt128(row["gpsLongitude"].ToString()));

                //        // 결과 대기
                //        //var decryptedLatitude = await decryptTaskLatitude;
                //        //var decryptedLongitude = await decryptTaskLongitude;

                //        row["gpsLatitude"] = aESUtill.AESDecrypt128(row["gpsLatitude"].ToString());
                //        row["gpsLongitude"] = aESUtill.AESDecrypt128(row["gpsLongitude"].ToString());
                //    }

                //}
                int i = 0;
                Parallel.ForEach(result.AsEnumerable(), row =>
                {
                    Console.WriteLine(row["gpsLongitude"]);
                    Console.WriteLine(i++);
                    if (row["gpsLatitude"] != DBNull.Value && row["gpsLongitude"] != DBNull.Value)
                    {
                        row["gpsLatitude"] = aESUtill.AESDecrypt128(row["gpsLatitude"].ToString());
                        row["gpsLongitude"] = aESUtill.AESDecrypt128(row["gpsLongitude"].ToString());
                    }
                });
                // setting한 객체를 SensorDataGrid.DataSource로 할당
                SensorDataGrid.DataSource = result;
                //gosafeMap.SetPositionByKeywords("대한민국, 서울특별시 강남구 코엑스");
                gosafeMap.Position = new PointLatLng(37.497872, 127.0275142);
                Console.WriteLine(gosafeMap.Position);
                endTimeD = DateTime.Now;

                ts = endTimeD - startTimeD;
                Console.WriteLine($"Elapsed Time: {ts.TotalMilliseconds} ms");
            }
            else
            {
                MessageBox.Show("센서 ID를 입력해주세요.");
            }
        }

        private void startTime_ValueChanged(object sender, EventArgs e)
        {
            endTime.MinDate = startTime.Value;
        }

        private void endTime_ValueChanged(object sender, EventArgs e)
        {
            startTime.MaxDate = endTime.Value;
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
