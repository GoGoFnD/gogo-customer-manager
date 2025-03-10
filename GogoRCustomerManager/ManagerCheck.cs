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

namespace GogoRCustomerManager
{
    public partial class ManagerCheck : Form
    {
        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
            (
                "Server= localhost ;Port= 3307; Database= appsigncode; Uid= root; Pwd= gogovlfflq;"
            );
        int login_Status;
        int isDeleted;

        string sended_id;
        Main main;
        public ManagerCheck(string sender, Main main)
        {
            InitializeComponent();
            this.main = main;
            if (sender == "MemChangeBtn")
            {
                Text = "회원 정보 수정";
                isDeleted = 1;
                this.LoginButton.Click += new System.EventHandler(this.UPD_Login_Button);
            }
            else if (sender == "DeleteMemBtn")
            {
                Text = "회원 삭제";
                isDeleted = 2;
                this.LoginButton.Click += new System.EventHandler(this.Delete_Click);
            }
        }
        public ManagerCheck(Main main, string id)
        {

            InitializeComponent();
            this.main = main;
            this.sended_id = id;
            Text = "계정정보 수정";
            Title.Text = "본인 인증";
            isDeleted = 3;
            this.LoginButton.Click += new System.EventHandler(this.Self_UPD_Account_Button);
        }
        private void Login_button()
        {
            if (isDeleted == 1)
            {
                UPD_Login_Button(null, null);
            }
            else if (isDeleted == 2)
            {
                Delete_Click(null, null);
            }
            else if (isDeleted == 3)
            {
                Self_UPD_Account_Button(null, null);
            }
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            string inputId = id.Text;
            string inputPw = pw.Text;
            string powerLevel = "1";
            
            connection.Open();

            string selectQuery = "select * from userinformation where id = '" + inputId + "' and isDeleted = 0";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();

            while (userAccount.Read())
            {
                if (inputId == (string)userAccount["id"] && inputPw == (string)userAccount["pw"] && powerLevel == (string)userAccount["powerLevel"])
                {
                    login_Status = 1;
                }
            }

            connection.Close();
            if (login_Status == 1)
            {

                main.SetisLogin(true);
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("아이디, 혹은 비밀번호를 다시 확인해 주세요.");
            }
        }
        private void UPD_Login_Button(object sender, EventArgs e)
        {
            string inputId = id.Text;
            string inputPw = pw.Text;
            string powerLevel = "1";
            string member_num = main.input_Num;
            connection.Open();


            string selectQuery = "select * from userinformation where id = '" + inputId + "' and isDeleted = 0";
            Console.WriteLine(selectQuery);
            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();

            while (userAccount.Read())
            {
                if (inputId == (string)userAccount["id"] && inputPw == (string)userAccount["pw"]&& powerLevel == (string)userAccount["powerLevel"])
                {
                    login_Status = 1;
                }
            }   

            connection.Close();
            if (login_Status == 1)
            {
                this.Visible = false;
                CreateAccount showmainform = new CreateAccount(inputId, member_num, main);
                this.Close();
                showmainform.ShowDialog();
                
            }
            else
            {
                MessageBox.Show("아이디, 혹은 비밀번호를 다시 확인해 주세요.");
            }
        }
        private void Self_UPD_Account_Button(object sender, EventArgs e)
        {
            string inputId = id.Text;
            string inputPw = pw.Text;
            string member_num = main.input_Num;
            connection.Open();


            string selectQuery = "select * from userinformation where id = '" + inputId + "' and isDeleted = 0";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();
            ArrayList data = new ArrayList();
            while (userAccount.Read())
            {
                if (sended_id == (string)userAccount["id"] && inputPw == (string)userAccount["pw"])
                {
                    login_Status = 1;
                }
                data.Add(userAccount["co_Member_Num"]);
            }

            connection.Close();
            if (login_Status == 1)
            {
                member_num = data[0].ToString();
                this.Visible = false;
                CreateAccount showmainform = new CreateAccount(inputId, member_num, main, 1);
                this.Close();
                showmainform.ShowDialog();

            }
            else
            {
                MessageBox.Show("아이디, 혹은 비밀번호를 다시 확인해 주세요.");
            }
        }
        private void PressEnter(object sender, KeyEventArgs e)//textBox 입력 중 enter로 로그인 함수 활성화
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_button();
            }
        }
        private void FocusPw(object sender, KeyEventArgs e)//textBox 입력 중 enter로 다음 textbox로 이동
        {
            if (e.KeyCode == Keys.Enter)
            {
                pw.Focus();
            }
        }
    }
}
    