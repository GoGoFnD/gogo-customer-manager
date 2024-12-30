using FontAwesome.Sharp;
using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GogoRCustomerManager
{
    public partial class CreateAccount : Form
    {
        MySqlConnection connection = new MySqlConnection
            (
                "Server= localhost;Port= 3307;Database= appsigncode;Uid= root;Pwd= gogovlfflq;"
            );
        bool isCreate;
        bool isdupSSN = false;
        bool isdupCo_Num = false;
        bool isdupid = false;

        string checkId;
        string member_num;
        string ssn_f;
        string ssn_b;
        string id_;
        string power_;
        string iswork_;

        int i = 0;

        Main main;
        
        public CreateAccount(Main main)
        {
            this.main = main;
            isCreate = true;
            InitializeComponent();
            initComboBox();
            isWork.Enabled = false;
            isWork.Text = "근무 전";
            this.Text = "회원가입";
            PW_Reset_Button.Enabled = false;
            Co_Leave.Enabled = false;
        }
        public CreateAccount(string manager_id, string member_num, Main main, int i)
        {
            this.main = main;
            isCreate = false;
            InitializeComponent();
            checkId = manager_id;
            this.member_num = member_num;
            Load_Account();
            initComboBox();
            id_Check.Enabled = false;
            SSN_Check.Enabled = false;
            this.Text = "회원정보 변경";

            PW.Enabled = false;
            PW_Visible.Enabled = false;
            this.i = i;
        }

        public CreateAccount(string manager_id, string member_num, Main main)
        {
            this.main = main;
            isCreate = false;
            InitializeComponent();
            checkId = manager_id;
            this.member_num = member_num;
            Load_Account();
            initComboBox();
            id_Check.Enabled = false;
            SSN_Check.Enabled = false;
            this.Text = "회원정보 변경";

            PW.Enabled = false;
            PW_Visible.Enabled = false;
        }

        private void Save_Button(object sender, EventArgs e)
        {
            if (isCreate)
            { 
                Save_New_Account_Data(sender, e); 
            }
            else
            {
                Update_information_Click(sender, e);
            }
        }

        private void initComboBox()
        {
            string[] power = { "관리자", "팀장", "사원" };
            string[] iswork = { "근무 전", "근무중", "휴가", "퇴사" };
            string[] agency = { "고고라이더스 본사", "고고라이더스 분점", "고고라이더스 체인점" };

            PowerLevel.Items.AddRange(power);
            isWork.Items.AddRange(iswork);
            AffiliatedAgency.Items.AddRange(agency);
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string Convert_isWork()
        {
            if (isWork.Text == "근무중")
            {
                return "Y";
            }
            else if (isWork.Text == "근무 전")
            {
                return "B";
            }
            else if (isWork.Text == "퇴사")
            {
                return "L";
            }
            else if (isWork.Text == "휴가")
            {
                return "T";
            }
            else
                return "N";
        }
        private string Convert_PowerLevel()
        {
            if (PowerLevel.Text == "관리자")
            {
                return "1";
            }
            else if (PowerLevel.Text == "팀장")
            {
                return "2";
            }
            else if (PowerLevel.Text == "사원")
            {
                return "3";
            }
            else
                return "N";
        }
        private string Convert_PowerLevelText()
        {
            if (power_ == "1")
            {
                return "관리자";
            }
            else if (power_ == "2")
            {
                return "팀장";
            }
            else if (power_ == "3")
            {
                return "사원";
            }
            else
                return "오류";
        }
        private string Convert_isWorkText()
        {
            if (iswork_ == "Y")
            {
                return "근무중";
            }
            else if (iswork_ == "B")
            {
                return "근무 전";
            }
            else if (iswork_ == "L")
            {
                return "퇴사";
            }
            else if (iswork_ == "T")
            {
                return "휴가";
            }
            else
                return "오류";
        }
        private void Save_New_Account_Data(object sender, EventArgs e)
        {
            if (isdupSSN && isdupCo_Num && isdupid)
            {
                string id_input = ID.Text;
                string pw_input = PW.Text;
                string name_input = MemberName.Text;
                string AffiliatedAgency_input = AffiliatedAgency.Text;
                string powerlevel_input = Convert_PowerLevel();
                string bank_name_input = Bank_Name.Text;
                string bank_account_holder_input = Bank_Account_Holder.Text;
                string co_join_input = Co_Join.Text;
                string memo_input = Memo.Text;
                string isWork_input = Convert_isWork();


                string co_member_number_input = Co_Member_Num.Text;
                string SSN_input = SSN.Text + "-" + SSN_Back.Text;
                string cellphone_Num_input = CellPhone_Num.Text;
                string bank_account_num_input = Bank_Account_Num.Text;

                int Phone_Num_Visible_input;
                int isCallExposure_input;

                if (Phone_Num_Visible.Checked)
                {
                    Phone_Num_Visible_input = 1;
                }
                else
                {
                    Phone_Num_Visible_input = 0;
                }
                if (isCallExposure.Checked)
                {
                    isCallExposure_input = 1;
                }
                else
                {
                    isCallExposure_input = 0;
                }


                if (Memo.Text.Length == 0)
                {
                    memo_input = null;
                }

                try
                {
                    connection.Open();
                    string selectQuery = "INSERT INTO userinformation (co_Member_Num, AffiliatedAgency, isWork, name, id, pw, powerLevel, SSN, " +
                                        "CellPhone_Num, Bank_name, Bank_Account_Num, Bank_Account_Holder, co_Join, memo, Phone_Num_Visible, isCallExposure, isDeleted)" +
                        "VALUES ('" + co_member_number_input +
                                "','" + AffiliatedAgency_input +
                                "','" + isWork_input +
                                "','" + name_input +
                                "','" + id_input +
                                "','" + pw_input +
                                "','" + powerlevel_input +
                                "','" + SSN_input +
                                "','" + cellphone_Num_input +
                                "','" + bank_name_input +
                                "','" + bank_account_num_input +
                                "','" + bank_account_holder_input +
                                "','" + co_join_input +
                                "','" + memo_input +
                                "','" + Phone_Num_Visible_input +
                                "','" + isCallExposure_input +
                                "','" + 0 + "');";
                    Console.WriteLine(selectQuery);
                    MySqlCommand insertData = new MySqlCommand(selectQuery, connection);
                    insertData.ExecuteNonQuery();
                    MessageBox.Show("성공적으로 저장되었습니다.");
                    
                }
                catch (Exception w)
                {

                    connection.Close();
                    Console.WriteLine(w);
                    MessageBox.Show("필수 정보를 입력하고 중복확인을 완료해주세요!");
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("필수 정보를 입력하고 중복확인을 완료해주세요");
                return;
            }
            if (main != null)
            {
                main.ResetMem_Click(null, null);
            }
        }
        

        private void Load_Account()
        {
            try
            {
                connection.Open();
                string selectQuery = "select * from userinformation where co_Member_Num = '" + member_num + "';";

                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                Co_Member_Num.Enabled = false;
                Co_Member_Num_Check.Enabled = false;
                ArrayList data = new ArrayList();

                while (userAccount.Read())
                {
                    data.Add(userAccount["co_Member_Num"]);
                    data.Add(userAccount["AffiliatedAgency"]);
                    data.Add(userAccount["name"]);
                    data.Add(userAccount["id"]);
                    data.Add(userAccount["pw"]);
                    data.Add(userAccount["powerLevel"]);
                    data.Add(userAccount["SSN"]);
                    data.Add(userAccount["CellPhone_Num"]);
                    data.Add(userAccount["Bank_name"]);
                    data.Add(userAccount["Bank_Account_Num"]);
                    data.Add(userAccount["Bank_Account_Holder"]);
                    data.Add(userAccount["co_Join"]);
                    data.Add(userAccount["co_leave"]);
                    data.Add(userAccount["isWork"]);
                    data.Add(userAccount["Memo"]);
                    data.Add(userAccount["Phone_Num_Visible"]);
                    data.Add(userAccount["isCallExposure"]);
                }
                Co_Member_Num.Text = data[0].ToString();
                AffiliatedAgency.Text = data[1].ToString();
                MemberName.Text = data[2].ToString();
                ID.Text = data[3].ToString();
                id_ = data[3].ToString();
                PW.Text = data[4].ToString();
                power_ = data[5].ToString();
                PowerLevel.Text = Convert_PowerLevelText();
                ssn_f = data[6].ToString().Substring(0, 6);
                SSN.Text = ssn_f;
                ssn_b = data[6].ToString().Substring(7, 7);
                SSN_Back.Text = ssn_b;
                CellPhone_Num.Text = data[7].ToString();
                Bank_Name.Text = data[8].ToString(); 
                Bank_Account_Num.Text = data[9].ToString();
                Bank_Account_Holder.Text = data[10].ToString();
                Co_Join.Text = data[11].ToString();
                if (data[12].ToString().Length == 0)
                    Co_Leave.Text = null;
                else
                    Co_Leave.Text = data[12].ToString().Substring(0, 10);

                iswork_ = data[13].ToString();
                isWork.Text = Convert_isWorkText();
             
                if (data[14] == null)
                    Memo.Text = null;
                else
                {
                    Memo.Text = data[14].ToString();
                }
                if (data[15].ToString() == "1")
                    Phone_Num_Visible.CheckState = CheckState.Checked;
                if (data[16].ToString() == "1")
                    isCallExposure.CheckState = CheckState.Checked;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();

                Co_Member_Num_Check.Enabled = false;

                isdupCo_Num = true;
                isdupSSN = true;
                isdupid = true;
            }
        }
        private void Update_information_Click(object sender, EventArgs e)
        {
            if (isdupSSN && isdupCo_Num && isdupid)
            {
                try
                {
                    string id_input = ",id= '" + ID.Text + "'";
                    string pw_input = ",pw= '" + PW.Text + "'";
                    string name_input = ",name= '" + MemberName.Text + "'";
                    string powerlevel_input = ",powerlevel= '" + Convert_PowerLevel() + "'";
                    string bank_name_input = ",Bank_name= '" + Bank_Name.Text + "'";
                    string bank_account_holder_input = ",Bank_Account_Holder = '" + Bank_Account_Holder.Text + "'";
                    string co_join_input = ",co_Join= '" + Co_Join.Text + "'";
                    string co_leave_input = ",co_leave= '" + Co_Leave.Text + "'";
                    if (Co_Leave.Text.Length == 0)
                        co_leave_input = null;
                    string memo_input = ",Memo= '" + Memo.Text + "'";
                    if (Memo.Text.Length == 0)
                        memo_input = null;
                    string AffiliatedAgency_input = "AffiliatedAgency = '" + AffiliatedAgency.Text + "'";

                    string SSN_input = ",SSN= '" + SSN.Text + "-" + SSN_Back.Text + "'";
                    string cellphone_Num_input = ",CellPhone_Num= '" + CellPhone_Num.Text + "'";
                    string bank_account_num_input = ",Bank_Account_Num = '" + Bank_Account_Num.Text + "'";
                    string iswork_input = ",isWork= '" + Convert_isWork() + "'";

                    string Phone_Num_Visible_input;
                    string isCallExposure_input;

                    if (Phone_Num_Visible.CheckState == CheckState.Checked)
                        Phone_Num_Visible_input = ", Phone_Num_Visible = " + 1;
                    else
                        Phone_Num_Visible_input = ", Phone_Num_Visible = " + 0;
                    if (isCallExposure.CheckState == CheckState.Checked)
                        isCallExposure_input = ", isCallExposure = " + 1;
                    else
                        isCallExposure_input = ", isCallExposure = " + 0;

                    connection.Open();
                    string selectQuery = "UPDATE userinformation SET " +
                                        AffiliatedAgency_input +
                                        name_input +
                                        id_input +
                                        pw_input +
                                        powerlevel_input +
                                        SSN_input +
                                        cellphone_Num_input +
                                        bank_name_input +
                                        bank_account_num_input +
                                        bank_account_holder_input +
                                        co_join_input +
                                        co_leave_input +
                                        iswork_input +
                                        memo_input +
                                        Phone_Num_Visible_input +
                                        isCallExposure_input +
                                        " WHERE co_Member_Num = '" + member_num + "';";
                    Console.WriteLine(selectQuery);
                    MySqlCommand insertData = new MySqlCommand(selectQuery, connection);
                    insertData.ExecuteNonQuery();
                    MessageBox.Show("성공적으로 저장되었습니다.");

                }
                catch (Exception a)
                {
                    connection.Close();
                    Console.WriteLine(a);
                }
                finally
                {
                    connection.Close();
                }
                if (i == 1)
                {

                }
                else
                {

                    main.ResetMem_Click(null, null);
                }
            }
        }
        
        private void dup_SSN(object sender, EventArgs e)
        {
            connection.Open();


            string selectQuery = "select ifnull(max(SSN), 0) SSN from userinformation where SSN = '" + SSN.Text + "-" + SSN_Back.Text + "' and isDeleted = 0;";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();

            int idstate = 0;
            if (SSN.Text != "" || SSN_Back.Text != "")
            {
                while (userAccount.Read())
                {
                    if ((string)userAccount["SSN"] == "0")
                    {
                        idstate = 1;
                    }
                }
            }
            connection.Close();
            if (idstate == 1)
            {
                isdupSSN = true;
                MessageBox.Show("유효한 주민등록번호 입니다.");
                SSN_Check.Enabled = false;
            }
            else
            {
                MessageBox.Show("유효하지 않은 주민등록번호 입니다.");
            }
            
        }
        private void Dup_Co_Num(object sender, EventArgs e)
        {
            connection.Open();


            string selectQuery = "select ifnull(max(co_Member_Num),0) co_Member_Num from userinformation where co_Member_Num = '" + Co_Member_Num.Text + "' and isDeleted = 0;";

            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();

            int idstate = 0;
            if (Co_Member_Num.Text != "")
            {
                while (userAccount.Read())
                {
                    if ((int)userAccount["co_Member_Num"] == 0)
                    {
                        idstate = 1;
                    }
                }
            }
            connection.Close();
            if (idstate == 1)
            {
                isdupCo_Num = true;
                MessageBox.Show("유효한 사원번호 입니다.");
                Co_Member_Num_Check.Enabled = false;
            }
            else
            {
                MessageBox.Show("유효하지 않은 사원번호 입니다.");
            }
        }
        private void Dup_id(object sender, EventArgs e)
        {
            connection.Open();
            
            string selectQuery = "select ifnull(max(id),0) id from userinformation where id = '" + ID.Text + "' and isDeleted = 0;";
            
            MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            MySqlDataReader userAccount = Selectcommand.ExecuteReader();

            int idstate = 0;
            if (ID.Text != "")
            {
                while (userAccount.Read())
                {

                    if ((string)userAccount["id"] == "0")
                    {
                        idstate = 1;
                    }

                }
            }
            connection.Close();
            if (idstate == 1)
            {
                isdupid = true;
                MessageBox.Show("유효한 아이디 입니다.");
                id_Check.Enabled = false;
            }
            else
            {
                MessageBox.Show("유효하지 않은 아이디 입니다.");
            }
        }

        private void isWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isWork.Text == "퇴사")
            {
                Co_Leave.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void Dup_TextChanged(object sender, EventArgs e)
        {
            if(sender == ID)
            {
                if (ID.Text == id_)
                {
                    isdupid = true;
                    id_Check.Enabled = false;
                }
                else
                {
                    isdupid = false;
                    id_Check.Enabled = true;
                }
            }
            if(sender == SSN)
            {
                if (SSN.Text == ssn_f && SSN_Back.Text == ssn_b)
                {
                    isdupSSN = true;
                    SSN_Check.Enabled = false;
                }
                else
                {
                    isdupSSN = false;
                    SSN_Check.Enabled = true;
                }
                if (SSN.TextLength >= 6)
                {
                    SSN_Back.Focus();
                }
            }
            
        }

        private void NumCheck(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Co_Member_Num_TextChanged(object sender, EventArgs e)
        {
            isdupCo_Num = false;
            Co_Member_Num_Check.Enabled = true;
        }
        private void PowerLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void PW_Visible_CheckedChanged(object sender, EventArgs e)
        {
            if(PW_Visible.Checked == true)
            {
                PW.PasswordChar = default(char);
            }
            else
            {
                PW.PasswordChar = '*';
            }
        }

        private void PW_Reset_Button_Click(object sender, EventArgs e)
        {
            PW.Text = "";
            PW.Enabled = true;
            PW_Visible.Enabled = true;
        }
    }
}
