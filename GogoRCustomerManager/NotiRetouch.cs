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

namespace GogoRCustomerManager
{
    public partial class NotiRetouch : Form
    {
        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
           (
               "Server= localhost ;Port= 3307; Database= appsigncode; Uid= root; Pwd= gogovlfflq;"
           );

        ArrayList data;

        bool isUpdate;
        public NotiRetouch()
        {
            InitializeComponent();
            ProgramNotiBtn_Click(null, null);
            label1.SendToBack();
        }

        private void ProgramNotiBtn_Click(object sender, EventArgs e)
        {
            TextBox.ReadOnly = true;
            TextBox.BackColor = SystemColors.Control;

            connection.Open();

            string selectQuery = "select * from textdatabase where data_id = '2'";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();
            data = new ArrayList();

            while (userAccount.Read())
            {
                data.Add(userAccount["text"]);
            }
            connection.Close();
            TextBox.Text = data[0].ToString();

            isUpdate = false;
        }

        private void UpadateBtn_Click(object sender, EventArgs e)
        {
            TextBox.ReadOnly = true;


            TextBox.BackColor = SystemColors.Control;

            connection.Open();

            string selectQuery = "select * from textdatabase where data_id = '3'";
            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();
            data = new ArrayList();

            while (userAccount.Read())
            {
                data.Add(userAccount["text"]);
            }
            connection.Close();
            TextBox.Text = data[0].ToString();

            isUpdate = true   ;
        }

        private void Retouch_Click(object sender, EventArgs e)
        {
            TextBox.ReadOnly = false;
            TextBox.BackColor = Color.White;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                connection.Open();
                string selectQuery = "UPDATE textdatabase SET text = '" + TextBox.Text +
                "' WHERE data_id = 3;";
                Console.WriteLine(selectQuery);
                MySqlCommand insertData = new MySqlCommand(selectQuery, connection);
                insertData.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("적용되었습니다.");
                TextBox.ReadOnly = true;
                TextBox.BackColor = SystemColors.Control;
            }
            else if (!isUpdate)
            {
                connection.Open();
                string selectQuery = "UPDATE textdatabase SET text = '" + TextBox.Text +
                "' WHERE data_id = 2;";
                Console.WriteLine(selectQuery);
                MySqlCommand insertData = new MySqlCommand(selectQuery, connection);
                insertData.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("적용되었습니다.");
                TextBox.ReadOnly = true;
                TextBox.BackColor = SystemColors.Control;
            }
            else
            {
                MessageBox.Show("적용오류!");
            }
        }

        private void NotiRetouch_Load(object sender, EventArgs e)
        {
            TextBox.ReadOnly = true;
            TextBox.BackColor = SystemColors.Control;

            connection.Open();

            string selectQuery = "select * from textdatabase where data_id = '2'";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();
            data = new ArrayList();

            while (userAccount.Read())
            {
                data.Add(userAccount["text"]);
            }
            connection.Close();
            TextBox.Text = data[0].ToString();

            isUpdate = false;
        }
    }
}
