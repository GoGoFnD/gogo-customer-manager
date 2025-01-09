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
        string phoneNumPatten = @"^\+82-?(12|2|[3-9]\d)-?\d{3,4}-?\d{4}$";
        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
           (
               "Server=cf.navers.co.kr ;Port= 3306; Database=goSafe; Uid=gosafe; Pwd=gogofnd0@; allow user variables=true;"
           );
        public GosafeDataMapview()
        {
            InitializeComponent();
            DatePickerSet();
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

            bindingSource.DataSource = FetchDataFromSchemas(startTime.Value, endTime.Value);
            cSenIDTab.dataTable = bindingSource;

            connection.Close();
            cSenIDTab.ShowDialog();
        }
        private DataTable FetchDataFromSchemas(DateTime startDate, DateTime endDate)
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

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(cSenIDTextBox.Text.ToString(), phoneNumPatten)){
                SensorDataGrid.DataSource = FetchDataFromSchemas(startTime.Value.Date, endTime.Value.Date);
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
    }
}
