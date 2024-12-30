
using K4os.Compression.LZ4.Internal;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GogoRCustomerManager
{
    public partial class Main : Form
    {
        private TabPage tabPage = null;

        private DataGridView DBData = new DataGridView();
        private DataGridViewTextBoxColumn co_Member_Num = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn isWorkConvert = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn AffiliatedAgency = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn id = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn pw = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn powerLevelConvert = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn SSN = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn CellPhone_Num = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn Bank_name = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn Bank_Account_Num = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn Bank_Account_Holder = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn co_Join = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn co_leave = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn memo = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn Phone_Num_Visible = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn isCallExposure = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn isDeleted = new DataGridViewTextBoxColumn();

        private Label label4 = new Label();
        private ComboBox ConAgency = new ComboBox();
        private Label label5 = new Label();
        private ComboBox ConisWork = new ComboBox();
        private CheckBox checkBox1 = new CheckBox();
        private Label label6 = new Label();
        private Label label7 = new Label();
        private ComboBox Condition = new ComboBox();
        private TextBox SearchText = new TextBox();
        private TextBox ConSearch = new TextBox();
        private FontAwesome.Sharp.IconButton MemSearchBtn = new FontAwesome.Sharp.IconButton();
        private FontAwesome.Sharp.IconButton ResetMem = new FontAwesome.Sharp.IconButton();
        //private FontAwesome.Sharp.IconButton MemAddBtn = new FontAwesome.Sharp.IconButton();
        private FontAwesome.Sharp.IconButton MemChangeBtn = new FontAwesome.Sharp.IconButton();
        private FontAwesome.Sharp.IconButton DeleteMemBtn = new FontAwesome.Sharp.IconButton();
        private RichTextBox programNotiText = new RichTextBox();
        private RichTextBox programUpdateText = new RichTextBox();

        private Login login = new Login();
        private ArrayList data;

        bool isProgramPopupOpen = false;
        bool memberPageOpen = false;
        bool notiPageOpen = false;
        public bool islogin = false;



        int addControll = 0;
        int notiPage_Num;
        int memberPage_Num;

        private Point _imageLocation = new Point(15, 5);
        private Point _imgHitArea = new Point(13, 2);

        string powerLevel;
        public string input_Num;
        string input_Name;
        string MemberNum;
        string mainTitle;
        string AccountName;
        string Agency;
        string account_id;
        string QueryAgency = null;
        string QueryisWork = null;
        string QueryCon = null;
        string QueryConSearch = null;
        string QuerySearch = null;
        string localIP;
        readonly string version = "1.0.0";

        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
           (
               "Server= localhost ;Port= 3307; Database= appsigncode; Uid= root; Pwd= gogovlfflq;"
           );


        public Main(string memNum, string id, string team, string name, string powerLevel)
        {
            InitializeComponent();
            account_id = id;
            this.powerLevel = powerLevel;
            mainTitle = "관리자용PC (" + version + ") [ " + team + ": " + name + " ]";
            this.Text = mainTitle;
            this.MemberNum = memNum;
            AccountName = name;
            Agency = team;

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += TabControl1_DrawItem;

            OffPopup();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            SetManagerUi();
            PageLoad();
        }

        private void AddFuntion()
        {
            tabPage.Click += new EventHandler(tabPage1_Click);
            DBData.Click += new EventHandler(tabPage1_Click);
            DBData.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DBData_CellFormatting);
            tabPage.Resize += new EventHandler(this.tabPage1_Resize);
            MemSearchBtn.Click += new EventHandler(this.MemSearchBtn_Click);
            ResetMem.Click += new EventHandler(this.ResetMem_Click);
            MemChangeBtn.Click += new EventHandler(ChangeAccount_Click);
            programUpdateText.Click += new EventHandler(NotiPage_Click);
            programNotiText.Click += new EventHandler(NotiPage_Click);
            DeleteMemBtn.Click += new EventHandler(Delete_Column_Click);
            SearchText.KeyDown += new KeyEventHandler(this.PressEnter);
            initComboBox();
        }

        private void powerLevelOpinion()
        {
            if (powerLevel == "3")
            {
                DeleteMemBtn.Enabled = false;
            }
        }
        private string Convert_isWork()
        {
            if (ConisWork.Text == "근무중")
                return "'Y'";
            else if (ConisWork.Text == "근무 전")
                return "'B'";
            else if (ConisWork.Text == "퇴사")
                return "'L'";
            else if (ConisWork.Text == "휴가")
                return "'T'";
            else
                return "'N'";
        }
        private string Convert_isWork(string sql)
        {
            if (sql == "근무중")
                return "'Y'";
            else if (sql == "근무 전")
                return "'B'";
            else if (sql == "퇴사")
                return "'L'";
            else if (sql == "휴가")
                return "'T'";
            else
                return "'N'";
        }
        private void initComboBox()
        {
            string[] agency = { "전체", "고고라이더스 본사", "고고라이더스 분점", "고고라이더스 체인점" };
            string[] iswork = { "전체", "근무중", "근무 전", "퇴사", "휴가" };
            string[] con = { "사원번호", "이름", "권한 등급", "아이디", "비밀번호", "주거래 은행", "예금주" };

            ConAgency.Items.AddRange(agency);
            ConisWork.Items.AddRange(iswork);
            Condition.Items.AddRange(con);
            ConAgency.Text = "전체";
            ConisWork.Text = "전체";
        }

        private void CreateTabPage(string page_Name)
        {

            if (!notiPageOpen && page_Name == "Noti")
            {
                tabPage = new TabPage(page_Name);
                tabPage.Name = "NotiTab";
                tabPage.Padding = new Padding(3);
                tabPage.Size = new Size(1147, 476);
                tabPage.TabIndex = 0;
                tabPage.Text = "";
                tabPage.UseVisualStyleBackColor = true;
                tabControl1.TabPages.Add(tabPage);
                tabPage.Click += new EventHandler(tabPage1_Click);
                tabPage.Font = new Font("나눔고딕", 22F, FontStyle.Bold, GraphicsUnit.Point, (byte)(129));
                tabControl1.SelectedTab = tabPage;
                notiPage_Num = tabControl1.SelectedIndex;
                // 
                // programNotiText
                // 
                programNotiText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                programNotiText.BorderStyle = BorderStyle.FixedSingle;
                programNotiText.Location = new Point(-2, -1);
                programNotiText.Name = "programNotiText";
                programNotiText.Size = new Size(1194, 167);
                programNotiText.TabIndex = 0;
                programNotiText.ReadOnly = true;
                programNotiText.BackColor = Color.White;
                programNotiText.Font = new Font("나눔고딕", 8.95F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                tabPage.Controls.Add(programNotiText);

                programUpdateText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                programUpdateText.BackColor = Color.White;
                programUpdateText.BorderStyle = BorderStyle.FixedSingle;
                programUpdateText.Font = new Font("나눔고딕", 8.95F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                programUpdateText.Location = new Point(-5, 164);
                programUpdateText.Name = "programUpdateText";
                programUpdateText.ReadOnly = true;
                programUpdateText.Size = new Size(1197, 378);
                programUpdateText.TabIndex = 0;
                tabPage.Controls.Add(programUpdateText);

                data = new ArrayList();
                for (int i = 2; i <= 3; i++)
                {
                    connection.Open();

                    string selectQuery = "select * from textdatabase where data_id = " + i + ";";

                    MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
                    MySqlDataReader userAccount = Selectcommand.ExecuteReader();



                    while (userAccount.Read())
                    {
                        data.Add(userAccount["text"]);
                    }
                    connection.Close();
                }





                programNotiText.Text = data[0].ToString();
                programUpdateText.Text = data[1].ToString();
                notiPageOpen = true;
            }

            if (!memberPageOpen && page_Name == "MemberPage")
            {
                addControll++;

                tabPage = new TabPage(page_Name);
                tabPage.Name = "MemberPageTab";
                tabPage.Padding = new Padding(20);
                tabPage.Size = new Size(1147, 476);
                tabPage.TabIndex = 0;
                tabPage.Text = "";
                tabPage.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                tabPage.UseVisualStyleBackColor = true;
                tabControl1.TabPages.Add(tabPage);
                tabPage.BackColor = SystemColors.Control;

                DBData = new DataGridView();
                DBData.AllowUserToAddRows = false;
                DBData.AllowUserToDeleteRows = false;
                DBData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
                DBData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                DBData.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                DBData.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, (byte)(129));
                DBData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                DBData.Columns.AddRange(new DataGridViewColumn[] {
                co_Member_Num,
                AffiliatedAgency,
                powerLevelConvert,
                isWorkConvert,
                name,
                id,
                pw,
                SSN,
                CellPhone_Num,
                Bank_name,
                Bank_Account_Num,
                Bank_Account_Holder,
                co_Join,
                co_leave,
                memo,
                Phone_Num_Visible});
                DBData.Location = new Point(0, 54);
                DBData.Name = "DBData";
                DBData.RowHeadersVisible = false;
                DBData.RowTemplate.Height = 23;
                DBData.Size = new Size(tabPage.Size.Width, tabPage.Size.Height - 55);
                DBData.TabIndex = 50;
                DBData.ScrollBars = ScrollBars.Both;

                co_Member_Num.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                co_Member_Num.DataPropertyName = "co_Member_Num";
                co_Member_Num.HeaderText = "    사원번호    ";
                co_Member_Num.Name = "co_Member_Num";
                co_Member_Num.ReadOnly = true;
                //
                //AffiliatedAgency 
                //
                AffiliatedAgency.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                AffiliatedAgency.DataPropertyName = "AffiliatedAgency";
                AffiliatedAgency.HeaderText = "  소속 대리점";
                AffiliatedAgency.Name = "AffiliatedAgency";
                AffiliatedAgency.ReadOnly = true;
                AffiliatedAgency.Width = 200;
                // 
                // powelLevel
                // 
                powerLevelConvert.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                powerLevelConvert.DataPropertyName = "powerLevelConvert";
                powerLevelConvert.HeaderText = "    권한레벨";
                powerLevelConvert.Name = "powerLevelConvert";
                powerLevelConvert.ReadOnly = true;
                powerLevelConvert.MinimumWidth = 100;
                powerLevelConvert.Width = 100;
                // 
                // isWork
                // 
                isWorkConvert.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                isWorkConvert.DataPropertyName = "isWorkConvert";
                isWorkConvert.HeaderText = "    근무상태";
                isWorkConvert.Name = "isWorkConvert";
                isWorkConvert.ReadOnly = true;
                isWorkConvert.MinimumWidth = 100;
                isWorkConvert.Width = 100;
                // 
                // name
                // 
                name.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                name.DataPropertyName = "name";
                name.HeaderText = "    이름";
                name.Name = "name";
                name.ReadOnly = true;
                name.MinimumWidth = 100;
                name.Width = 100;
                // 
                // id
                // 
                id.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                id.DataPropertyName = "id";
                id.HeaderText = "    아이디";
                id.Name = "id";
                id.ReadOnly = true;
                id.MinimumWidth = 100;
                id.Width = 100;
                // 
                // pw
                //  
                pw.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                pw.DataPropertyName = "pw";
                pw.HeaderText = "    비밀번호";
                pw.Name = "pw";
                pw.ReadOnly = true;
                pw.MinimumWidth = 100;
                pw.Width = 100;
                // 
                // SSN
                // 
                SSN.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                SSN.DataPropertyName = "SSN";
                SSN.HeaderText = "       주민등록번호";
                SSN.Name = "SSN";
                SSN.ReadOnly = true;
                SSN.MinimumWidth = 200;
                // 
                // CellPhone_Num
                // 
                CellPhone_Num.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                CellPhone_Num.DataPropertyName = "CellPhone_Num";
                CellPhone_Num.HeaderText = "    전화번호";
                CellPhone_Num.Name = "CellPhone_Num";
                CellPhone_Num.ReadOnly = true;
                CellPhone_Num.MinimumWidth = 100;
                CellPhone_Num.Width = 150;
                // 
                // Bank_name
                // 
                Bank_name.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Bank_name.DataPropertyName = "Bank_name";
                Bank_name.HeaderText = "    주거래 은행";
                Bank_name.Name = "Bank_name";
                Bank_name.ReadOnly = true;
                Bank_name.MinimumWidth = 100;
                Bank_name.Width = 120;
                // 
                // Bank_Account_Num
                // 
                Bank_Account_Num.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Bank_Account_Num.DataPropertyName = "Bank_Account_Num";
                Bank_Account_Num.HeaderText = "    계좌번호";
                Bank_Account_Num.Name = "Bank_Account_Num";
                Bank_Account_Num.ReadOnly = true;
                Bank_Account_Num.MinimumWidth = 100;
                Bank_Account_Num.Width = 160;
                // 
                // Bank_Account_Holder
                // 
                Bank_Account_Holder.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Bank_Account_Holder.DataPropertyName = "Bank_Account_Holder";
                Bank_Account_Holder.HeaderText = "    예금주";
                Bank_Account_Holder.Name = "Bank_Account_Holder";
                Bank_Account_Holder.ReadOnly = true;
                Bank_Account_Holder.MinimumWidth = 100;
                Bank_Account_Holder.Width = 100;
                // 
                // co_Join
                // 
                co_Join.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                co_Join.DataPropertyName = "co_Join";
                co_Join.HeaderText = "   입사일";
                co_Join.Name = "co_Join";
                co_Join.ReadOnly = true;
                co_Join.MinimumWidth = 100;
                co_Join.Width = 100;
                // 
                // co_leave
                // 
                co_leave.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                co_leave.DataPropertyName = "co_leave";
                co_leave.HeaderText = "   퇴사일";
                co_leave.Name = "co_leave";
                co_leave.ReadOnly = true;
                co_leave.MinimumWidth = 100;
                co_leave.Width = 100;
                // 
                // memo
                // 
                memo.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                memo.DataPropertyName = "Memo";
                memo.HeaderText = "메모";
                memo.MaxInputLength = 30000;
                memo.Name = "memo";
                memo.ReadOnly = true;
                memo.MinimumWidth = 100;
                memo.Width = 54;
                // 
                // Phone_Num_Visible
                // 
                Phone_Num_Visible.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                Phone_Num_Visible.DataPropertyName = "Phone_Num_Visible";
                Phone_Num_Visible.HeaderText = "라이더앱 연락처 노출";
                Phone_Num_Visible.MaxInputLength = 30000;
                Phone_Num_Visible.Name = "Phone_Num_Visible";
                Phone_Num_Visible.ReadOnly = true;
                Phone_Num_Visible.MinimumWidth = 100;
                Phone_Num_Visible.Width = 54;
                // 
                // isCallExposure
                // 
                isCallExposure.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                isCallExposure.DataPropertyName = "isCallExposure";
                isCallExposure.HeaderText = "보류콜 노출";
                isCallExposure.MaxInputLength = 30000;
                isCallExposure.Name = "isCallExposure";
                isCallExposure.ReadOnly = true;
                isCallExposure.MinimumWidth = 100;
                isCallExposure.Width = 54;
                // 
                // isDeleted
                // 
                isDeleted.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                isDeleted.DataPropertyName = "isDeleted";
                isDeleted.HeaderText = "삭제됨";
                isDeleted.MaxInputLength = 30000;
                isDeleted.Name = "isDeleted";
                isDeleted.ReadOnly = true;
                isDeleted.MinimumWidth = 100;
                isDeleted.Width = 54;
                // 
                // MemSearchBtn
                // 
                MemSearchBtn.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                MemSearchBtn.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
                MemSearchBtn.IconColor = Color.Black;
                MemSearchBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
                MemSearchBtn.IconSize = 24;
                MemSearchBtn.ImageAlign = ContentAlignment.MiddleLeft;
                MemSearchBtn.Location = new Point(721, 3);
                MemSearchBtn.Name = "MemSearchBtn";
                MemSearchBtn.Size = new Size(99, 48);
                MemSearchBtn.TabIndex = 37;
                MemSearchBtn.Text = "     직원조회";
                MemSearchBtn.UseVisualStyleBackColor = true;
                // 
                // ResetMem
                // 
                ResetMem.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                ResetMem.IconChar = FontAwesome.Sharp.IconChar.ClipboardUser;
                ResetMem.IconColor = Color.Black;
                ResetMem.IconFont = FontAwesome.Sharp.IconFont.Auto;
                ResetMem.IconSize = 24;
                ResetMem.ImageAlign = ContentAlignment.MiddleLeft;
                ResetMem.Location = new Point(824, 3);
                ResetMem.Name = "ResetMem";
                ResetMem.Rotation = 180D;
                ResetMem.Size = new Size(99, 48);
                ResetMem.TabIndex = 38;
                ResetMem.Text = "      직원검색 \r\n    초기화";
                ResetMem.UseVisualStyleBackColor = true;
                // 
                // MemAddBtn
                // 
                /*MemAddBtn.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                MemAddBtn.IconChar = FontAwesome.Sharp.IconChar.PlusSquare;
                MemAddBtn.IconColor = Color.Black;
                MemAddBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
                MemAddBtn.IconSize = 24;
                MemAddBtn.ImageAlign = ContentAlignment.MiddleLeft;
                MemAddBtn.Location = new Point(927, 3);
                MemAddBtn.Name = "MemAddBtn";
                MemAddBtn.Rotation = 180D;
                MemAddBtn.Size = new Size(99, 48);
                MemAddBtn.TabIndex = 5;
                MemAddBtn.Text = "      신규직원 \r\n    등록";
                MemAddBtn.UseVisualStyleBackColor = true;
                MemAddBtn.Click += new EventHandler(CreateAccount_Click);*/
                // 
                // MemChangeBtn
                // 
                MemChangeBtn.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                MemChangeBtn.IconChar = FontAwesome.Sharp.IconChar.SheetPlastic;
                MemChangeBtn.IconColor = Color.Black;
                MemChangeBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
                MemChangeBtn.IconSize = 24;
                MemChangeBtn.ImageAlign = ContentAlignment.MiddleLeft;
                MemChangeBtn.Location = new Point(927, 3);
                MemChangeBtn.Name = "MemChangeBtn";
                MemChangeBtn.Rotation = 180D;
                MemChangeBtn.Size = new Size(99, 48);
                MemChangeBtn.TabIndex = 39;
                MemChangeBtn.Text = "     직원정보 \r\n    수정";
                MemChangeBtn.UseVisualStyleBackColor = true;
                // 
                // DeleteMemBtn
                // 
                DeleteMemBtn.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                DeleteMemBtn.IconChar = FontAwesome.Sharp.IconChar.X;
                DeleteMemBtn.IconColor = Color.Black;
                DeleteMemBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
                DeleteMemBtn.IconSize = 21;
                DeleteMemBtn.ImageAlign = ContentAlignment.MiddleLeft;
                DeleteMemBtn.Location = new Point(1030, 3);
                DeleteMemBtn.Name = "DeleteMemBtn";
                DeleteMemBtn.Rotation = 180D;
                DeleteMemBtn.Size = new Size(99, 48);
                DeleteMemBtn.TabIndex = 40;
                DeleteMemBtn.Text = "     직원삭제";
                DeleteMemBtn.UseVisualStyleBackColor = true;
                //
                //label4
                //
                label4.BorderStyle = BorderStyle.FixedSingle;
                label4.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                label4.Location = new Point(5, 4);
                label4.Name = "label4";
                label4.Size = new Size(69, 47);
                label4.Text = "대리점선택";
                label4.TextAlign = ContentAlignment.MiddleCenter;
                // 
                // ConAgency
                // 
                ConAgency.FormattingEnabled = true;
                ConAgency.Location = new Point(77, 4);
                ConAgency.Name = "ConAgency";
                ConAgency.Size = new Size(135, 22);
                ConAgency.TabIndex = 31;
                // 
                // label5
                // 
                label5.BorderStyle = BorderStyle.FixedSingle;
                label5.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                label5.Location = new Point(216, 4);
                label5.Name = "label5";
                label5.Size = new Size(69, 22);
                label5.Text = "근무상태";
                label5.TextAlign = ContentAlignment.MiddleCenter;
                // 
                // ConisWork
                // 
                ConisWork.FormattingEnabled = true;
                ConisWork.Location = new Point(288, 4);
                ConisWork.Name = "ConisWork";
                ConisWork.Size = new Size(121, 22);
                ConisWork.TabIndex = 33;
                // 
                // checkBox1
                // 
                checkBox1.AutoSize = true;
                checkBox1.Location = new Point(77, 32);
                checkBox1.Name = "checkBox1";
                checkBox1.Size = new Size(109, 18);
                checkBox1.TabIndex = 32;
                checkBox1.Text = "하위 대리점 포함";
                checkBox1.UseVisualStyleBackColor = true;
                // 
                // label6
                // 
                label6.BorderStyle = BorderStyle.FixedSingle;
                label6.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                label6.Location = new Point(412, 4);
                label6.Name = "label6";
                label6.Size = new Size(69, 22);
                label6.TabIndex = 1;
                label6.Text = "조건검색";
                label6.TextAlign = ContentAlignment.MiddleCenter;
                // 
                // label7
                // 
                label7.BorderStyle = BorderStyle.FixedSingle;
                label7.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                label7.Location = new Point(412, 29);
                label7.Name = "label7";
                label7.Size = new Size(69, 22);
                label7.TabIndex = 1;
                label7.Text = "전체검색";
                label7.TextAlign = ContentAlignment.MiddleCenter;
                // 
                // Condition
                // 
                Condition.FormattingEnabled = true;
                Condition.Location = new Point(484, 4);
                Condition.Name = "Condition";
                Condition.Size = new Size(103, 22);
                Condition.TabIndex = 34;
                // 
                // SearchText
                // 
                SearchText.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                SearchText.Location = new Point(484, 29);
                SearchText.Name = "SearchText";
                SearchText.Size = new Size(232, 22);
                SearchText.TabIndex = 36;
                // 
                // ConSearch
                // 
                ConSearch.Font = new Font("나눔고딕", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                ConSearch.Location = new Point(590, 4);
                ConSearch.Name = "ConSearch";
                ConSearch.Size = new Size(126, 22);
                ConSearch.TabIndex = 35;


                tabPage.Controls.Add(DeleteMemBtn);
                tabPage.Controls.Add(ResetMem);
                tabPage.Controls.Add(MemChangeBtn);
                //tabPage.Controls.Add(MemAddBtn);
                tabPage.Controls.Add(MemSearchBtn);
                tabPage.Controls.Add(ConSearch);
                tabPage.Controls.Add(SearchText);
                tabPage.Controls.Add(checkBox1);
                tabPage.Controls.Add(Condition);
                tabPage.Controls.Add(ConisWork);
                tabPage.Controls.Add(ConAgency);
                tabPage.Controls.Add(label7);
                tabPage.Controls.Add(label6);
                tabPage.Controls.Add(label5);
                tabPage.Controls.Add(label4);

                tabPage.Controls.Add(DBData);


                
                MemSearchBtn_Click(null, null);

                memberPageOpen = true;
                DBData.DoubleBuffered(true);
                DBData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                tabControl1.SelectedTab = tabPage;

                memberPage_Num = tabControl1.SelectedIndex;

                if (addControll == 1)
                    AddFuntion();
                SetDB();

            }
        }
        private void DBData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            for (int i = 0; i < DBData.Rows.Count; i++)
            {
                if (i % 2 != 0)
                {
                    DBData.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {

                    DBData.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                }
            }
        }
        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font fntTab;
            Brush bshBank;
            Brush bshFore;
            PointF point;
            string tabName;
            if (e.Index == this.tabControl1.SelectedIndex)
            {
                fntTab = new Font(e.Font, FontStyle.Regular);
                bshBank = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, panel2.BackColor,
                                                                           panel2.BackColor,
                                                                           System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                bshFore = Brushes.White;

                point = new PointF(e.Bounds.X + 10, e.Bounds.Y + 4);
            }
            else
            {
                fntTab = e.Font;
                bshBank = new SolidBrush(Color.White);
                bshFore = new SolidBrush(Color.Black);

                point = new PointF(e.Bounds.X + 6, e.Bounds.Y + 2);
            }

            if (tabControl1.TabPages[e.Index].Name == "MemberPageTab")
                tabName = "직원 등록현황";
            else if (tabControl1.TabPages[e.Index].Name == "NotiTab")
                tabName = "공지";
            else
                tabName = "오류";


            StringFormat sftTab = new StringFormat(StringFormatFlags.NoClip);

            sftTab.Alignment = StringAlignment.Near;

            sftTab.LineAlignment = StringAlignment.Center;

            e.Graphics.FillRectangle(bshBank, e.Bounds);


            Rectangle recTab = e.Bounds;

            e.Graphics.DrawString(tabName, fntTab, bshFore, point);

#if true    // <--- 여기를 true 로 변경하면 텍스트의 좌우를 뒤집지 않음


#else
                        recTab = new Rectangle(0, 0, recTab.Width, recTab.Height);
                        Bitmap bitmap = new Bitmap(e.Bounds.Width, e.Bounds.Height);
                        Graphics g = Graphics.FromImage(bitmap);
                        g.Clear(BackColor);        // <--- 여기에 원하는 배경색상을 지정
                        g.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        e.Graphics.DrawImage(bitmap, e.Bounds.X + 1, e.Bounds.Y);   // +1 안해주면 왼쪽에서 짤림

                        g.Dispose();

                        bitmap.Dispose();

#endif
            try
            {
                Image img = Properties.Resources.Close_BWhite;

                Rectangle r = e.Bounds;
                r = tabControl1.GetTabRect(e.Index);
                r.Offset(5, 5);

                Brush TitleBrush = new SolidBrush(Color.Black);
                Font f = Font;

                string title = tabControl1.TabPages[e.Index].Text;

                e.Graphics.DrawString(title, f, TitleBrush, new PointF(r.X, r.Y));
                e.Graphics.DrawImage(img, new Point(r.X + (tabControl1.GetTabRect(e.Index).Width - _imageLocation.X - 15), _imageLocation.Y - 4));
            }
            catch (Exception) { }

        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            Point p = e.Location;
            int _tabWidth;
            _tabWidth = tabControl1.GetTabRect(tc.SelectedIndex).Width - (_imgHitArea.X) - 15;
            Rectangle r = tabControl1.GetTabRect(tc.SelectedIndex);
            r.Offset(_tabWidth, _imgHitArea.Y - 4);
            r.Width = 20;
            r.Height = 20;
            if (r.Contains(p))
            {
                if (MessageBox.Show("정말 탭을 종료하시겠습니까?", "탭 종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    TabPage TabP = (TabPage)tc.TabPages[tc.SelectedIndex];
                    tc.TabPages.Remove(TabP);
                    if (TabP.Name.Equals("MemberPageTab"))
                    {
                        memberPageOpen = false;
                    }
                    if (TabP.Name.Equals("NotiTab"))
                    {
                        notiPageOpen = false;
                    }
                }
            }
        }
        private void SetDB()
        {
            DBData.Columns["isWork"].Visible = false;
            DBData.Columns["powerLevel"].Visible = false;
            DBData.Columns["Memo"].Visible = false;
            DBData.Columns["Phone_Num_Visible"].Visible = false;
            DBData.Columns["isCallExposure"].Visible = false;
            DBData.Columns["isDeleted"].Visible = false;
            DBData.Columns["co_Member_Num"].DisplayIndex = 0;
            DBData.Columns["AffiliatedAgency"].DisplayIndex = 1;
            DBData.Columns["powerLevelConvert"].DisplayIndex = 2;
            DBData.Columns["isWorkConvert"].DisplayIndex = 3;
            DBData.Columns["name"].DisplayIndex = 4;
            DBData.Columns["id"].DisplayIndex = 5;
            DBData.Columns["SSN"].DisplayIndex = 6;
            DBData.Columns["CellPhone_Num"].DisplayIndex = 7;
            DBData.Columns["Bank_name"].DisplayIndex = 8;
            DBData.Columns["Bank_Account_Num"].DisplayIndex = 9;
            DBData.Columns["Bank_Account_Holder"].DisplayIndex = 10;
            DBData.Columns["co_Join"].DisplayIndex = 11;
            DBData.Columns["co_leave"].DisplayIndex = 12;
            DBData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DBData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void PressEnter(object sender, KeyEventArgs e)//textBox 입력 중 enter로 로그인 함수 활성화
        {
            if (e.KeyCode == Keys.Enter)
            {
                MemSearchBtn_Click(sender, e);
            }
        }
        public void LogOut()
        {
            //OutMain();
            login.Visible = true;
            Visible = false;

        }
        private void tabPage1_Resize(object sender, EventArgs e)
        {
            //DBData.Size = new Size(tabPage.Size.Width, tabPage.Size.Height - 57);
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //OutMain();

            Application.Exit();
        }
        private void OffPopup()
        {
            ProgramPanel.Visible = false;
            isProgramPopupOpen = false;
        }
        private string Convert_PowerLevel()
        {
            if (ConSearch.Text == "관리자")
            {
                return "1";
            }
            else if (ConSearch.Text == "팀장")
            {
                return "2";
            }
            else if (ConSearch.Text == "사원")
            {
                return "3";
            }
            else
                return "N";
        }
        private void ProgramBtn_Click(object sender, EventArgs e)
        {


            if (ProgramPanel.Visible == true)
            {
                OffPopup();

            }
            else if (ProgramPanel.Visible == false)
            {
                int isVisible = 1;
                if (isVisible == 1)
                {
                    isProgramPopupOpen = true;

                    int ProgramBtnSize_Y = 53;
                    ProgramPanel.Location = new Point(ProgramBtn.Location.X, ProgramBtn.Location.Y + ProgramBtnSize_Y);
                    ProgramPanel.BringToFront();
                    isVisible++;
                }
                if (!isProgramPopupOpen || isVisible > 1)
                {
                    ProgramPanel.Visible = true;
                    isProgramPopupOpen = true;
                }
                ProgramPanel.Focus();
            }
            
        }
        private void Main_Click_1(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void OrderBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void RiderBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void AgencyBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void FranchiseeBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void MemberBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
            if (memberPageOpen)
            {
                tabControl1.SelectedIndex = memberPage_Num;
            }
            else
            {
                CreateTabPage("MemberPage");
            }
        }

        private void NotiBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
            if (notiPageOpen)
            {
                tabControl1.SelectedIndex = notiPage_Num;
            }
            else
            {
                CreateTabPage("Noti");
            }
        }

        private void ManageBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }
        private void NotiPage_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void RControlBtn1_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void RControlBtn2_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void RControlBtn3_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void RMessege_Click(object sender, EventArgs e)
        {
            OffPopup();

        }

        private void AAddchargeBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void OSituationBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void ImpossibleAreaBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void NApplyBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
            NotiRetouch showmainform = new NotiRetouch();
            showmainform.Show();
        }

        private void FMessegeBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void ProgramEndBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("정말 종료하시겠습니까?", "시스템 종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void SetManagerUi()
        {     
            int i = 0;
            string selectQuery = "select * from managingdatabase where AgencyName = '" + Agency + "';";
            Console.WriteLine(selectQuery);
            try
            {
                connection.Open();
                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                data = new ArrayList();
                while (userAccount.Read())
                {
                    data.Add(userAccount["AgencySavedmoney"]);
                    data.Add(userAccount["EventCount_Hold"]);
                    data.Add(userAccount["EventCount_Reservation"]);
                    data.Add(userAccount["EventCount_Receipt"]);
                    data.Add(userAccount["EventCount_Posible"]);
                    data.Add(userAccount["EventCount_Doing"]);
                    data.Add(userAccount["EventCount_Pickup"]);
                    data.Add(userAccount["EventCount_Succece"]);
                    data.Add(userAccount["EventCount_Cancel"]);
                    data.Add(userAccount["EventCount_Accident"]);
                    data.Add(userAccount["EventCount_Inquire"]);
                    data.Add(userAccount["AgencyName"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                connection.Close();
                i++;
            }
            finally
            {
                
                connection.Close();
            }
            Console.WriteLine(data.Count);
            if (i == 1 || data == null || data.Count == 0)
            {
                ManagerUi_Savedmoney.Text = "적립금: -";
                ManagerUi_All.Text = "전체: -";
                ManagerUi_Reservation.Text = "예약: -";
                ManagerUi_Receipt.Text = "접수: -";
                ManagerUi_Posible.Text = "가배차: -";
                ManagerUi_Doing.Text = "배차: -";
                ManagerUi_Pickup.Text = "픽업: -";
                ManagerUi_Succece.Text = "완료: -";
                ManagerUi_Cancel.Text = "취소: -";
                ManagerUi_Accident.Text = "사고: -";
                ManagerUi_Inquire.Text = "문의: -";

                ManagerUi_Agency.Text = "대리점:" + Agency;
                ManagerUi_Account.Text = "계정:" + AccountName;
            }
            else
            {
                ManagerUi_Savedmoney.Text = "적립금:" + string.Format("{0:#,###}", data[0].ToString());
                ManagerUi_All.Text = "전체:" + ((int)data[2] + (int)data[3] + (int)data[4] + (int)data[5] + (int)data[6] +
                                                        (int)data[7] + (int)data[8] + (int)data[9] + (int)data[10]);
                ManagerUi_Reservation.Text = "예약:" + data[2].ToString();
                ManagerUi_Receipt.Text = "접수:" + data[3].ToString();
                ManagerUi_Posible.Text = "가배차:" + data[4].ToString();
                ManagerUi_Doing.Text = "배차:" + data[5].ToString();
                ManagerUi_Pickup.Text = "픽업:" + data[6].ToString();
                ManagerUi_Succece.Text = "완료:" + data[7].ToString();
                ManagerUi_Cancel.Text = "취소:" + data[8].ToString();
                ManagerUi_Accident.Text = "사고:" + data[9].ToString();
                ManagerUi_Inquire.Text = "문의:" + data[10].ToString();

                ManagerUi_Agency.Text = "대리점:" + data[11].ToString();
                ManagerUi_Account.Text = "계정:" + AccountName;
            }
            
           


            ManagerUi_P1.Location = new Point(ManagerUi_Connection.Location.X + ManagerUi_Connection.Width + 4, 3);
            ManagerUi_Agency.Location = new Point(ManagerUi_P1.Location.X + 6, 3);
            ManagerUi_P2.Location = new Point(ManagerUi_Agency.Location.X + ManagerUi_Agency.Width + 4, 3);
            ManagerUi_Savedmoney.Location = new Point(ManagerUi_P2.Location.X + 6, 3);
            ManagerUi_P3.Location = new Point(ManagerUi_Savedmoney.Location.X + ManagerUi_Savedmoney.Width + 4, 3);
            ManagerUi_Account.Location = new Point(ManagerUi_P3.Location.X + 6, 3);


            ManagerUi_Inquire.Location = new Point(ManagerUi_P14.Location.X - ManagerUi_Inquire.Width - 4, 3);
            ManagerUi_P13.Location = new Point(ManagerUi_Inquire.Location.X - 6, 3);
            ManagerUi_Accident.Location = new Point(ManagerUi_P13.Location.X - ManagerUi_Accident.Width - 4, 3);
            ManagerUi_P12.Location = new Point(ManagerUi_Accident.Location.X - 6, 3);
            ManagerUi_Cancel.Location = new Point(ManagerUi_P12.Location.X - ManagerUi_Cancel.Width - 4, 3);
            ManagerUi_P11.Location = new Point(ManagerUi_Cancel.Location.X - 6, 3);
            ManagerUi_Succece.Location = new Point(ManagerUi_P11.Location.X - ManagerUi_Succece.Width - 4, 3);
            ManagerUi_P10.Location = new Point(ManagerUi_Succece.Location.X - 6, 3);
            ManagerUi_Pickup.Location = new Point(ManagerUi_P10.Location.X - ManagerUi_Pickup.Width - 4, 3);
            ManagerUi_P9.Location = new Point(ManagerUi_Pickup.Location.X - 6, 3);
            ManagerUi_Doing.Location = new Point(ManagerUi_P9.Location.X - ManagerUi_Doing.Width - 4, 3);
            ManagerUi_P8.Location = new Point(ManagerUi_Doing.Location.X - 6, 3);
            ManagerUi_Posible.Location = new Point(ManagerUi_P8.Location.X - ManagerUi_Posible.Width - 4, 3);
            ManagerUi_P7.Location = new Point(ManagerUi_Posible.Location.X - 6, 3);
            ManagerUi_Receipt.Location = new Point(ManagerUi_P7.Location.X - ManagerUi_Receipt.Width - 4, 3);
            ManagerUi_P6.Location = new Point(ManagerUi_Receipt.Location.X - 6, 3);
            ManagerUi_Reservation.Location = new Point(ManagerUi_P6.Location.X - ManagerUi_Reservation.Width - 4, 3);
            ManagerUi_P5.Location = new Point(ManagerUi_Reservation.Location.X - 6, 3);
            ManagerUi_Hold.Location = new Point(ManagerUi_P5.Location.X - ManagerUi_Hold.Width - 4, 3);
            ManagerUi_P4.Location = new Point(ManagerUi_Hold.Location.X - 6, 3);
            ManagerUi_All.Location = new Point(ManagerUi_P4.Location.X - ManagerUi_All.Width - 4, 3);
        }
        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            LogOut();
        }

        private void ProgramNotiBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            OffPopup();
            DBData.Size = new Size(tabPage.Size.Width, tabPage.Size.Height - 57);

        }
        private void ChangeAccountBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
            ManagerCheck showmainform = new ManagerCheck(this, account_id);
            showmainform.ShowDialog();
        }
        private void MemSearchBtn_Click(object sender, EventArgs e)
        {
            string consearch = "'" + ConSearch.Text + "'";
            string conAgency = " AffiliatedAgency = '" + ConAgency.Text + "' ";
            string con = null;
            string conisWork = " isWork = " + Convert_isWork();
            string searchText;
            string WAnd = " AND";
            string SAnd = " AND";
            string CAnd = " AND";
            string DAnd = " AND";
            string Cwhwre = "WHERE";

            if ((ConSearch.Text == null || ConSearch.Text.Length == 0) && Condition.Text.Length != 0)
            {
                MessageBox.Show("검색 조건을 기입해주세요!");
                ConSearch.Focus();
                return;
            }

            QueryAgency = ConAgency.Text;
            QueryisWork = ConisWork.Text;
            QueryCon = Condition.Text;
            QueryConSearch = ConSearch.Text;
            QuerySearch = SearchText.Text;

            if (ConSearch.Text.Length == 0)
            {
                consearch = null;
            }
            if (ConSearch.Text.Length == 0 && (ConisWork.Text.Length == 0 || ConisWork.Text == "전체"))
            {
                WAnd = null;
                if (ConAgency.Text.Length == 0 || ConAgency.Text == "전체")
                {
                    SAnd = null;
                    if (SearchText.Text.Length == 0)
                    {
                        //Cwhwre = null;
                        DAnd = null;
                    }
                }
            }
            if (ConisWork.Text.Length == 0 || ConisWork.Text == "전체")
            {
                conisWork = null;

                WAnd = null;
            }
            if (ConAgency.Text.Length == 0 || ConAgency.Text == "전체")
            {
                conAgency = null;
                WAnd = null;
                if (ConisWork.Text.Length == 0 || ConisWork.Text == "전체")
                {
                    CAnd = null;
                }
            }
            if (Condition.Text == "사원번호")
            {
                con = CAnd + " co_Member_Num =" + consearch + "";
            }
            if (Condition.Text == "권한 등급")
            {
                consearch = Convert_PowerLevel();
                con = CAnd + " powerLevel =" + consearch + "";
            }
            if (Condition.Text == "이름")
            {
                con = CAnd + " name =" + consearch + "";
            }
            if (Condition.Text == "아이디")
            {
                con = CAnd + " id =" + consearch + "";
            }
            if (Condition.Text == "비밀번호")
            {
                con = CAnd + " pw=" + consearch + "";
            }
            else if (Condition.Text == "주거래 은행")
            {
                con = CAnd + " Bank_name =" + consearch + "";
            }
            else if (Condition.Text == "예금주")
            {
                con = CAnd + " Bank_Account_Holder =" + consearch + "";
            }
            if (SearchText.Text.Length == 0)
            {
                SAnd = null;
                searchText = null;
            }
            else
            {
                if (SearchText.Text == "관리자" || SearchText.Text == "팀장" || SearchText.Text == "사원")
                {
                    if (SearchText.Text == "관리자")
                    {
                        searchText = " " + SAnd + " (powerLevel = 1 ";
                    }
                    else if (ConSearch.Text == "팀장")
                    {
                        searchText = " " + SAnd + " (powerLevel = 2 ";
                    }
                    else if (ConSearch.Text == "사원")
                    {
                        searchText = " " + SAnd + " (powerLevel = 3 ";
                    }
                }
                searchText = " " + SAnd +
                    " (name = '" + SearchText.Text +
                    "' or id = '" + SearchText.Text +
                    "' or id = '" + SearchText.Text +
                    "' or co_Member_Num = '" + SearchText.Text +
                    "' or pw = '" + SearchText.Text +
                    "' or SSN = '" + SearchText.Text +
                    "' or Bank_name = '" + SearchText.Text +
                    "' or Bank_Account_Num = '" + SearchText.Text +
                    "' or Bank_Account_Holder = '" + SearchText.Text +
                    "' or co_Join = '" + SearchText.Text +
                    "' or co_Leave = '" + SearchText.Text + "')";
            }
            if (ConAgency.Text == "" && ConisWork.Text == "" && Condition.Text == "" && ConSearch.Text == "" && SearchText.Text == "")
            {
                string selectQuery = "SELECT *, CASE " +
                    "WHEN isWork='Y' THEN '근무' " +
                    "WHEN isWork='B' THEN '근무 전' " +
                    "WHEN isWork='T' THEN '휴가' " +
                    "WHEN isWork='L' THEN '퇴사' " +
                    "END as isWorkConvert , " +
                    "CASE  " +
                    "WHEN powerLevel='1' THEN '관리자' " +
                    "WHEN powerLevel='2' THEN '팀장' " +
                    "WHEN powerLevel='3' THEN '사원' " +
                    "WHEN powerLevel='N' THEN '오류' " +
                    "END as powerLevelConvert " +
                    "FROM userinformation" +
                    " WHERE isDeleted = 0;";
                Console.WriteLine(selectQuery);
                connection.Open();
                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);

                try
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = Selectcommand;
                    DataTable dbDataset = new DataTable();
                    mySqlDataAdapter.Fill(dbDataset);
                    BindingSource bindingSource = new BindingSource();

                    bindingSource.DataSource = dbDataset;
                    DBData.DataSource = bindingSource;
                    mySqlDataAdapter.Update(dbDataset);
                    SetDB();
                }
                catch (Exception ex)
                {
                    connection.Close();
                    Console.WriteLine(ex);
                }
                finally
                {
                    connection.Close();

                    DBData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            else
            {
                string selectQuery = "SELECT *, CASE " +
                                        "WHEN isWork='Y' THEN '근무' " +
                                        "WHEN isWork='B' THEN '근무 전' " +
                                        "WHEN isWork='T' THEN '휴가' " +
                                        "WHEN isWork='L' THEN '퇴사' " +
                                        "END as isWorkConvert , " +
                                        "CASE  " +
                                        "WHEN powerLevel='1' THEN '관리자' " +
                                        "WHEN powerLevel='2' THEN '팀장' " +
                                        "WHEN powerLevel='3' THEN '사원' " +
                                        "WHEN powerLevel='N' THEN '오류' " +
                                        "END as powerLevelConvert " +
                                        "FROM userinformation " +
                                        Cwhwre + conAgency +
                                        WAnd + conisWork +
                                        "" + con + "" +
                                        "" + searchText + "" +
                                        DAnd + " isDeleted = 0;";
                Console.WriteLine(selectQuery);



                connection.Open();
                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);

                try
                {
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = Selectcommand;
                    DataTable dbDataset = new DataTable();
                    mySqlDataAdapter.Fill(dbDataset);
                    BindingSource bindingSource = new BindingSource();

                    bindingSource.DataSource = dbDataset;
                    DBData.DataSource = bindingSource;
                    mySqlDataAdapter.Update(dbDataset);

                    DBData.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DBData_CellFormatting);
                    SetDB();
                }
                catch
                {
                    connection.Close();
                    MessageBox.Show("결과가 없습니다!");

                }
                finally
                {
                    connection.Close();

                    DBData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    SetDB();

                }
                OffPopup();
            }
        }

        public void ResetMem_Click(object sender, EventArgs e)
        {
            ConAgency.Text = "전체";
            ConisWork.Text = "전체";
            ConSearch.Text = null;
            Condition.Text = null;
            SearchText.Text = null;
            MemSearchBtn_Click(null, null);
            
            OffPopup();
        }
        private void CreateAccount_Click(object sender, EventArgs e)
        {
            CreateAccount showCreateAccountForm = new CreateAccount(this);
            showCreateAccountForm.ShowDialog();
        }
        private void ChangeAccount_Click(object sender, EventArgs e)
        {
            DataGridViewRow dgvr = DBData.CurrentRow;
            DataRow row = (dgvr.DataBoundItem as DataRowView).Row;

            input_Num = row["co_Member_Num"].ToString();

            ManagerCheck showCreateAccountForm = new ManagerCheck(MemChangeBtn.Name, this);
            showCreateAccountForm.ShowDialog();
        }
        private void Delete_Column_Click(object sender, EventArgs e)
        {
            ManagerCheck showmainform = new ManagerCheck(DeleteMemBtn.Name, this);
            showmainform.ShowDialog();
            DataGridViewRow dgvr = DBData.CurrentRow;
            DataRow row = (dgvr.DataBoundItem as DataRowView).Row;


            input_Name = row["name"].ToString();
            input_Num = row["co_Member_Num"].ToString();
            if (islogin)
            {
                if (MessageBox.Show("정말 회원정보를 삭제하시겠습니까?", "주의", MessageBoxButtons.YesNo) == DialogResult.Yes && input_Num != null)
                {

                    try
                    {
                        connection.Open();
                        string selectQuery = "UPDATE userinformation SET isDeleted = 1 where co_Member_Num = '" + input_Num + "';";
                        MySqlCommand insertData = new MySqlCommand(selectQuery, connection);
                        insertData.ExecuteNonQuery();
                        MessageBox.Show(input_Name + "님의 회원정보가 삭제되었습니다");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        MessageBox.Show("실패");
                        connection.Close();

                    }
                    finally
                    {
                        connection.Close();
                        islogin = false;
                        ResetMem_Click(sender, e);
                    }
                }
                input_Num = null;
                input_Name = null;
            }
        }
        public void SetisLogin(bool b)
        {
            islogin = b;
        }
        private void ipSearch()
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
        }
        private void StateSaveBtn_Click(object sender, EventArgs e)
        {
            OffPopup();
            int mem;
            int noti;

            if (memberPageOpen)
                mem = 1;
            else
                mem = 0;
            if (notiPageOpen)
                noti = 1;
            else
                noti = 0;

            string _queryAgency;
            string _queryisWork;
            string _queryCon;
            string _queryConSearch;
            string _querySearch;

            ipSearch();

            DateTime date = DateTime.Now;
            date.ToString("yyyy-MM-dd H:mm:ss");

            if (QueryAgency == null)
                _queryAgency = "','" + "null";
            else if (QueryAgency.Length == 0)
                _queryAgency = "','" + "null";
            else if (QueryAgency == "전체")
                _queryAgency = "','" + "null";
            else
                _queryAgency = "','" + QueryAgency;

            if (QueryisWork == null)
                _queryisWork = "','" + "null";
            else if (QueryisWork.Length == 0)
                _queryisWork = "','" + "null";
            else if (QueryisWork == "전체")
                _queryisWork = "','" + "null";
            else
                _queryisWork = "','" + QueryisWork;

            if (QueryCon == null)
                _queryCon = "','" + "null";
            else if (QueryCon.Length == 0)
                _queryCon = "','" + "null";
            else
                _queryCon = "','" + QueryCon;



            if (QueryConSearch == null)
                _queryConSearch = "','" + "null";
            else if (QueryConSearch.Length == 0)
                _queryConSearch = "','" + "null";
            else
                _queryConSearch = "','" + QueryConSearch;

            if (QuerySearch == null)
                _querySearch = "','" + "null";
            else if (QuerySearch.Length == 0)
                _querySearch = "','" + "null";
            else
                _querySearch = "','" + QuerySearch;

            try
            {
                connection.Open();
                string selectQuery = "INSERT INTO usertabstate (co_Member_Num, insertedtime, ip, " +
                                                               "maintab_member, maintab_Noti, " +
                                                               "maintab_member_query_Agency, " +
                                                               "maintab_member_query_isWork, " +
                                                               "maintab_member_query_Condition," +
                                                               "maintab_member_query_ConSearch, " +
                                                               "maintab_member_query_SearchText)" +
                    "VALUES ('" + MemberNum +
                            "','" + date.ToString("yyyy-MM-dd H:mm:ss") +
                            "','" + localIP +
                            "','" + mem +
                            "','" + noti +
                            _queryAgency +
                            _queryisWork +
                            _queryCon +
                            _queryConSearch +
                            _querySearch + "');";
                MySqlCommand insertData = new MySqlCommand(selectQuery, connection);
                insertData.ExecuteNonQuery();
                MessageBox.Show("현재 화면이 저장되었습니다.");
            }
            catch (Exception w)
            {

                connection.Close();
                Console.WriteLine(w);
            }
            finally
            {
                connection.Close();
            }
        }
        private void PageLoad()
        {
            string maintab_member = "null";
            string maintab_Noti = "null";
            string maintab_member_query_Agency = "null";
            string maintab_member_query_isWork = "null";
            string maintab_member_query_Condition = "null";
            string maintab_member_query_ConSearch = "null";
            string maintab_member_query_SearchText = "null";

            string selectQuery = null;

            ipSearch();

            try
            {
                connection.Open();
                selectQuery = "select * from usertabstate where co_Member_Num = " + MemberNum +
                                                        " and ip = '" + localIP +
                                                        "' ORDER BY insertedtime DESC LIMIT 1;";
                Console.WriteLine(selectQuery);
                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                ArrayList load = new ArrayList();


                while (userAccount.Read())
                {
                    load.Add(userAccount["maintab_Noti"]);
                    load.Add(userAccount["maintab_member"]);
                    load.Add(userAccount["maintab_member_query_Agency"]);
                    load.Add(userAccount["maintab_member_query_isWork"]);
                    load.Add(userAccount["maintab_member_query_Condition"]);
                    load.Add(userAccount["maintab_member_query_ConSearch"]);
                    load.Add(userAccount["maintab_member_query_SearchText"]);
                }

                maintab_Noti = load[0].ToString();
                maintab_member = load[1].ToString();
                maintab_member_query_Agency = load[2].ToString();
                maintab_member_query_isWork = load[3].ToString();
                maintab_member_query_Condition = load[4].ToString();
                maintab_member_query_ConSearch = load[5].ToString();
                maintab_member_query_SearchText = load[6].ToString();
            }
            catch (Exception a)
            {
                connection.Close();
                CreateTabPage("Noti");
                Console.WriteLine(a);
                return;
            }
            finally
            {
                connection.Close();
            }
            if (maintab_Noti == "True")
            {
                CreateTabPage("Noti");
            }

            if (maintab_member == "True")
            {
                CreateTabPage("MemberPage");

                if (maintab_member_query_Agency.Length == 0)
                {
                    ConAgency.Text = "전체";
                }
                else if (maintab_member_query_Agency == "null")
                    ConAgency.Text = "전체";
                else
                    ConAgency.Text = maintab_member_query_Agency;

                if (maintab_member_query_isWork.Length == 0)
                {
                    ConisWork.Text = "전체";
                }
                else if (maintab_member_query_isWork == "null")
                    ConisWork.Text = "전체";
                else
                    ConisWork.Text = maintab_member_query_isWork;

                if (maintab_member_query_Condition.Length == 0)
                {
                    Condition.Text = "";
                }
                else if (maintab_member_query_Condition == "null")
                    Condition.Text = "";
                else
                    Condition.Text = maintab_member_query_Condition;

                if (maintab_member_query_ConSearch.Length == 0)
                {
                    ConSearch.Text = "";
                }
                else if (maintab_member_query_ConSearch == "null")
                    ConSearch.Text = "";
                else
                    ConSearch.Text = maintab_member_query_ConSearch;

                if (maintab_member_query_SearchText.Length == 0)
                {
                    SearchText.Text = "";
                }
                else if (maintab_member_query_SearchText == "null")
                    SearchText.Text = "";
                else
                    SearchText.Text = maintab_member_query_SearchText;
                MemSearchBtn_Click(null, null);
                
            }
        }
    }
    public static class ExtensionMethods
    { 
        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }
    }
}
