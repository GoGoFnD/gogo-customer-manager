using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using Microsoft.Web.WebView2.WinForms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto;

namespace GogoRCustomerManager
{
    public partial class Login : Form
    {
        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
            (
                "Server=cf.navers.co.kr ;Port= 3306; Database=goSafe; Uid=gosafe; Pwd=gogofnd0@; allow user variables=true;"
            );

        int login_Status;
        Main showmainform;
        public Login()
        {
            InitializeComponent();
            login_Status = 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)//회원가입 페이지 이동
        {
            CreateAccount showCreateAccountForm = new CreateAccount(null);
            showCreateAccountForm.ShowDialog();
        }

        private void PressEnter(object sender, KeyEventArgs e)//textBox 입력 중 enter로 로그인 함수 활성화
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login_button(sender, e);
            }
        }
        private void FocusPw(object sender, KeyEventArgs e)//textBox 입력 중 enter로 다음 textbox로 이동
        {
            if (e.KeyCode == Keys.Enter)
            {
                pw.Focus();     
            }
        }

        private void Login_button(object sender, EventArgs e)
        {
            string inputId = id.Text;
            string inputPw = pw.Text;

            connection.Open();

            ArrayList data = new ArrayList();

                
            string selectQuery = "select * from gogo_manager_account where id = '" + inputId + "'  and isDeleted = 0";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();

            while(userAccount.Read())
            {
                if (inputId == (string)userAccount["id"] && inputPw == (string)userAccount["pw"])
                {
                    login_Status = 1;
                    data.Add(userAccount["account_Num"].ToString());
                    //data.Add(userAccount["id"].ToString());
                    //data.Add(userAccount["AffiliatedAgency"].ToString());
                    //data.Add(userAccount["name"].ToString());
                    //data.Add(userAccount["powerLevel"].ToString());

                    //data.Add("");
                    data.Add("");
                    data.Add("");
                    data.Add("");
                    data.Add("");
                }
            }

            connection.Close();

            if (login_Status == 1)
            {
                if (Save_Login.Checked)
                {
                    Properties.Settings.Default.LoginIDSave = id.Text;
                    Properties.Settings.Default.LoginPWSave = pw.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.LoginIDSave = "";
                    Properties.Settings.Default.LoginPWSave = "";
                    Properties.Settings.Default.Save();
                }
                showmainform = new Main(data[0].ToString(), data[1].ToString(), data[2].ToString(), data[3].ToString(), data[4].ToString());
                showmainform.Show();

                this.Visible = false;
            }
            else
            {
                MessageBox.Show("아이디, 혹은 비밀번호를 다시 확인해 주세요.");
            }
            
        }
        private void Exit_Button(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        { 
            id.Text = Properties.Settings.Default.LoginIDSave;
            pw.Text = Properties.Settings.Default.LoginPWSave;
            if (id.Text.Length != 0 && pw.Text.Length != 0)
            {
                Save_Login.CheckState = CheckState.Checked;
            }
        }
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void None_Beep_Sound(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

    }
}
