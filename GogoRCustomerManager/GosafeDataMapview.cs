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
        int timeUnit;
        bool SensorDataNotEmpty = false;
        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
           (
               "Server=cf.navers.co.kr ;Port= 3306; Database=goSafe; Uid=gosafe; Pwd=gogofnd0@; allow user variables=true;"
           );
        public GosafeDataMapview()
        {
            InitializeComponent();
            DatePickerSet();
            GMapSetting();
            timeunitSetting();

            AutoScaleMode = AutoScaleMode.Dpi;
        }
        private void timeunitSetting()
        {
            timeunitCombo.Text = "1분 단위";
            string[] unit = { "시간 단위", "10분 단위", "1분 단위", "10초 단위", "1초 단위" };

            timeunitCombo.Items.AddRange(unit);
            timeUnit = 5;
        }
        private void GMapSetting()
        {
            gosafeMap = new GosafeMap(gMapControl);
            gMapControl.Visible = false;

            gMapControl.CenterPen.Color = Color.DeepSkyBlue;
        }
        private void DatePickerSet()
        {
            startTime.Format = DateTimePickerFormat.Custom;
            startTime.CustomFormat = "yyyy-MM-dd";
            startTime.Value = DateTime.Today.AddDays(-1);
            endTime.Format = DateTimePickerFormat.Custom;
            endTime.CustomFormat = "yyyy-MM-dd";
            endTime.Value = DateTime.Today.AddTicks(-1);

            startTime.MinDate = new DateTime(2024, 1, 22);
            startTime.MaxDate = DateTime.Today;
            endTime.MinDate = new DateTime(2024, 1, 22);
            endTime.MaxDate = DateTime.Today;
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
            //Console.WriteLine(sqlBuilder.ToString());
            DataTable dataTable = new DataTable();

            try
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(sqlBuilder.ToString(), connection))
                {
                    // 파라미터 설정
                    command.Parameters.AddWithValue("@StartDate", startDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndDate", endDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@StartTime", startDate.ToString("HH:mm:ss"));
                    command.Parameters.AddWithValue("@EndTime", endDate.ToString("HH:mm:ss"));
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
            
            // 동적 SQL을 생성
            StringBuilder sqlBuilder = new StringBuilder();
            string ID = cSenIDTextBox.Text.ToString();

            sqlBuilder.AppendLine("WITH RECURSIVE DateRange AS (");
            sqlBuilder.AppendLine("  SELECT @StartDate AS DateValue");
            sqlBuilder.AppendLine("  UNION ALL");
            sqlBuilder.AppendLine("  SELECT DATE_ADD(DateValue, INTERVAL 1 DAY)");
            sqlBuilder.AppendLine("  FROM DateRange");
            sqlBuilder.AppendLine("  WHERE DateValue < @EndDate");
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("SELECT GROUP_CONCAT(CONCAT('SELECT cSenID, cSenDate, cSenTime, gpsLatitude, gpsLongitude, gpsSpeed FROM tb_sensordata_', DATE_FORMAT(DateValue, '%Y%m%d'),");
            sqlBuilder.AppendLine($"         ' WHERE cSenID = \"{ID}\" and gpsLatitude > 0 group by LEFT(cSenTime, \"{timeUnit}\")') ");
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
                    command.Parameters.AddWithValue("@ID", cSenIDTextBox.Text.ToString());
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
            SensorDataNotEmpty = false;
            SensorDataGrid.DataSource = null;
            gosafeMap.RemoveMarkers();
            aESUtill = new AESUtill();
            if (Regex.IsMatch(cSenIDTextBox.Text.ToString(), phoneNumPatten))
            {
                DateTime startTimeD = DateTime.Now;

                // dataTable 객체로 받고
                Console.WriteLine(BikeLogFind(startTime.Value.Date, endTime.Value.Date));
                DataTable result = BikeLogFind(startTime.Value.Date, endTime.Value.Date);

                DateTime endTimeD = DateTime.Now;

                TimeSpan ts = endTimeD - startTimeD;
                Console.WriteLine($"Elapsed Time: {ts.TotalMilliseconds} ms");
                startTimeD = DateTime.Now;

                // foreach, for 사용하여 위도/경도 복호화 후 setting
                bool isFirst = true;
                int i = 0;
                if(result != null)
                {
                    foreach (DataRow row in result.Rows)
                    {
                        if (row["gpsLatitude"] != DBNull.Value && row["gpsLongitude"] != DBNull.Value)
                        {
                            var gpsLatitude = aESUtill.AESDecrypt128(row["gpsLatitude"].ToString());
                            row["gpsLatitude"] = gpsLatitude;
                            var gpsLongitude = aESUtill.AESDecrypt128(row["gpsLongitude"].ToString());
                            row["gpsLongitude"] = gpsLongitude;
                            //Console.WriteLine(i++);
                            gosafeMap.AddMarker(Double.Parse(gpsLatitude), Double.Parse(gpsLongitude));
                            if (isFirst && Double.Parse(gpsLatitude) > 1)
                            {
                                gMapControl.Position = new PointLatLng(Double.Parse(gpsLatitude), Double.Parse(gpsLongitude));
                                gMapControl.Zoom = 19;
                                isFirst = false;
                            }
                        }
                    }
                    SensorDataGrid.DataSource = result;
                    SensorDataNotEmpty = true;
                    Console.WriteLine(gosafeMap.Position);
                    endTimeD = DateTime.Now;

                    gMapControl.Visible = true;
                    ts = endTimeD - startTimeD;
                    Console.WriteLine($"Elapsed Time: {ts.TotalMilliseconds} ms");
                }
                else
                {
                    MessageBox.Show("오류: 너무 큰 범위를 검색하셨습니다.", "오류");
                }
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private int timeUnitConvert(string text)
        {
            switch (text)
            {
                case "시간 단위": return 2;
                case "10분 단위": return 4;
                case "1분 단위": return 5;
                case "10초 단위": return 7;
                case "1초 단위": return 8;
            }
            return 5;
        }
        private void timeunitCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(timeunitCombo.Text);
            //1초단위 선택시 경고
            if (timeunitCombo.Text == "1초 단위")
            {
                if(MessageBox.Show("주의: 1초 단위로 검색할 경우, 검색 시간이 길어질 수 있습니다.", "경고", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    timeUnit = timeUnitConvert(timeunitCombo.Text);
                }
                else
                {
                    timeunitCombo.Text = "1분 단위";
                    timeUnit = timeUnitConvert(timeunitCombo.Text);
                }
            }
            else
            {
                //선택한 단위 저장
                timeUnit = timeUnitConvert(timeunitCombo.Text);
            }
        }

        private void SensorDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SensorDataSelected();
        }
        private void SensorDataSelected()
        {

            string lat = SensorDataGrid.SelectedRows[0].Cells[3].Value.ToString();
            string lng = SensorDataGrid.SelectedRows[0].Cells[4].Value.ToString();
            gMapControl.Position = new PointLatLng(Double.Parse(lat), Double.Parse(lng));
            gosafeMap.AddSelectedMarker(Double.Parse(lat), Double.Parse(lng));
        }

        private void SensorDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine(SensorDataGrid.CurrentCell);
            if (SensorDataGrid.CurrentCell != null && SensorDataGrid.CurrentCell.RowIndex != 0 && SensorDataNotEmpty)
            {
                SensorDataSelected();
            }
        }
    }
}
