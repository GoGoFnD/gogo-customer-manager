using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Xml.Linq;

namespace GogoRCustomerManager
{
    public partial class GosafeDataMapview : Form
    {
        cSenIDTab cSenIDTab;
        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
           (
               "Server=cf.navers.co.kr ;Port= 3306; Database=goSafe;; Uid=gosafe; Pwd=gogofnd0@;"
           );
        public GosafeDataMapview()
        {
            InitializeComponent();
            startTime.Format = DateTimePickerFormat.Custom;
            startTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            endTime.Format = DateTimePickerFormat.Custom;
            endTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
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

        private void SelectButton_Click(object sender, EventArgs e)
        {
            cSenIDTab = new cSenIDTab();
            ArrayList data = new ArrayList();
            connection.Open();

            string selectQuery = "select distinct cSenID from tb_sensordata_20250104;";
                                //+"UNION "
                                // +"select distinct cSenID from tb_sensordata_20250105; ; ";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.SelectCommand = Selectcommand;
            DataTable dbDataset = new DataTable();
            mySqlDataAdapter.Fill(dbDataset);
            BindingSource bindingSource = new BindingSource();

            bindingSource.DataSource = dbDataset;
            SensorDataGrid.DataSource = bindingSource;
            cSenIDTab.dataTable = bindingSource;

            connection.Close();
            cSenIDTab.Show();
        }
    }
}
