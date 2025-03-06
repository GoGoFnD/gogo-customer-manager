using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace GogoRCustomerManager
{
    public partial class Main : Form
    {
        private TabPage tabPage = null;
        RControl rControl;
        GosafeDataMapview gosafeDataMapview;
        SensorLocationManeger sensorLocationManeger;
        private DataGridView DBData = new DataGridView();
        private DataGridViewTextBoxColumn rPhoneNumber = new DataGridViewTextBoxColumn();
        private DataGridViewTextBoxColumn rBikeNumber = new DataGridViewTextBoxColumn();

        private Label label6 = new Label();
        private Label label7 = new Label();
        private ComboBox Condition = new ComboBox();
        private TextBox SearchText = new TextBox();
        private TextBox ConSearch = new TextBox();
        private FontAwesome.Sharp.IconButton MemSearchBtn = new FontAwesome.Sharp.IconButton();
        private FontAwesome.Sharp.IconButton ResetMem = new FontAwesome.Sharp.IconButton();


        private Login login = new Login();
        private ArrayList data;

        private RichTextBox programNotiText = new RichTextBox();
        private RichTextBox programUpdateText = new RichTextBox();

        bool isProgramPopupOpen = false;
        bool bikePageOpen = false;
        bool notiPageOpen = false;
        bool RControl1 = false;
        public bool islogin = false;



        int addControll = 0;
        int notiPage_Num;
        int bikePage_Num;

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

        //MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
        //   (
        //       "Server= localhost ;Port= 3307; Database= appsigncode; Uid= root; Pwd= gogovlfflq;"
        //   );
        MySqlConnection connection2 = new MySqlConnection//데이터 베이스 연결
           (
               "Server=cf.navers.co.kr ;Port= 3306; Database=goSafe; Uid=gosafe; Pwd=gogofnd0@; allow user variables=true;charset=utf8mb4;"
           );


        public Main(string memNum, string id, string team, string name, string powerLevel)
        {
            InitializeComponent();
            //account_id = id;
            //this.powerLevel = powerLevel;
            //mainTitle = "관리자용PC (" + version + ") [ " + team + ": " + name + " ]";
            //this.Text = mainTitle;
            this.MemberNum = memNum;
            //AccountName = name;
            //Agency = team;

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += TabControl1_DrawItem;


            OffPopup();
            //InitializeWebView();
        }
        /*private async void InitializeWebView()
        {
            webView2 = new Microsoft.Web.WebView2.WinForms.WebView2
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(webView2);

            await webView2.EnsureCoreWebView2Async(null);
            webView2.Source = new Uri("https://google.com");
        }*/
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
            //programUpdateText.Click += new EventHandler(NotiPage_Click);
            //programNotiText.Click += new EventHandler(NotiPage_Click);
            SearchText.KeyDown += new KeyEventHandler(this.PressEnter);
            initComboBox();
        }

        private void initComboBox()
        {
            string[] agency = { "전체", "고고라이더스 본사", "고고라이더스 분점", "고고라이더스 체인점" };
            string[] iswork = { "전체", "근무중", "근무 전", "퇴사", "휴가" };
            string[] con = { "차량번호", "센서번호" };

            Condition.Items.AddRange(con);
        }

        private void CreateTabPage(string page_Name)
        {
            if (!notiPageOpen && page_Name == "Noti")
            {
               /* NotiTab notiTab = new NotiTab(page_Name, tabPage, this, notiPageOpen, notiPage_Num);
                notiTab.createTab();
                notiPageOpen = true;*/
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
                programNotiText.Size = new Size(tabPage.Width, tabPage.Height/2);
                programNotiText.TabIndex = 0;
                programNotiText.ReadOnly = true;
                programNotiText.BackColor = Color.White;
                programNotiText.Font = new Font("나눔고딕", 8.95F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                tabPage.Controls.Add(programNotiText);

                programUpdateText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                programUpdateText.BackColor = Color.White;
                programUpdateText.BorderStyle = BorderStyle.FixedSingle;
                programUpdateText.Font = new Font("나눔고딕", 8.95F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
                programUpdateText.Location = new Point(-2, tabPage.Height / 2);
                programUpdateText.Name = "programUpdateText";
                programUpdateText.ReadOnly = true;
                programUpdateText.Size = new Size(tabPage.Width, tabPage.Height/2);
                programUpdateText.TabIndex = 0;
                tabPage.Controls.Add(programUpdateText);

                data = new ArrayList();
                for (int i = 2; i <= 3; i++)
                {
                    connection2.Open();

                    string selectQuery = "select * from gogo_manager_alertdata where data_id = " + i + ";";

                    MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection2);
                    MySqlDataReader userAccount = Selectcommand.ExecuteReader();



                    while (userAccount.Read())
                    {
                        data.Add(userAccount["text"]);
                    }
                    connection2.Close();
                }





                programNotiText.Text = data[0].ToString();
                programUpdateText.Text = data[1].ToString();
                notiPageOpen = true;
                this.AutoScaleMode = AutoScaleMode.Dpi;
            }

            if (!bikePageOpen && page_Name == "BikePage")
            {
                addControll++;

                tabPage = new TabPage(page_Name);
                tabPage.Name = "BikePageTab";
                tabPage.Padding = new Padding(20);
                tabPage.Size = new Size(1147, 476);
                tabPage.TabIndex = 0;
                tabPage.Text = "";
                tabPage.Font = new Font("나눔고딕", 8F, FontStyle.Regular, GraphicsUnit.Point, (byte)(129));
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
                DBData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                DBData.Columns.AddRange(new DataGridViewColumn[] {
                rBikeNumber,
                rPhoneNumber,});
                DBData.Location = new Point(0, 54);
                DBData.Name = "DBData";
                DBData.RowHeadersVisible = false;
                DBData.RowTemplate.Height = 23;
                DBData.Size = new Size(tabPage.Size.Width, tabPage.Size.Height - 55);
                DBData.TabIndex = 50;
                DBData.ScrollBars = ScrollBars.Both;

                //
                //rBikeNumber 
                //
                rBikeNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                rBikeNumber.DataPropertyName = "rBikeNumber";
                rBikeNumber.HeaderText = "    차량번호";
                rBikeNumber.Name = "rBikeNumber";
                rBikeNumber.ReadOnly = true;
                rBikeNumber.Width = 200;
                // 
                // rPhoneNumer
                // 
                rPhoneNumber.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                rPhoneNumber.DataPropertyName = "rPhoneNumer";
                rPhoneNumber.HeaderText = "    센서번호";
                rPhoneNumber.Name = "rPhoneNumer";
                rPhoneNumber.ReadOnly = true;
                rPhoneNumber.MinimumWidth = 100;
                rPhoneNumber.Width = 200;
                // 
                // label6
                // 
                label6.BorderStyle = BorderStyle.FixedSingle;
                label6.Location = new Point(2, 4);
                label6.Name = "label6";
                //label6.Size = new Size(69, 22);
                label6.TabIndex = 1;
                label6.Text = "조건검색";
                label6.TextAlign = ContentAlignment.MiddleCenter;
                // 
                // label7
                // 
                label7.BorderStyle = BorderStyle.FixedSingle;
                label7.Location = new Point(2, 29);
                label7.Name = "label7";
                //label7.Size = new Size(69, 22);
                label7.TabIndex = 1;
                label7.Text = "전체검색";
                label7.TextAlign = ContentAlignment.MiddleCenter;
                // 
                // Condition
                // 
                Condition.FormattingEnabled = true;
                Condition.Location = new Point(label6.Size.Width + 10, 4);
                Condition.Name = "Condition";
                Condition.Size = new Size(102, 22);
                Condition.TabIndex = 34;
                //Condition.Enabled = false;
                // 
                // SearchText
                // 
                SearchText.Location = new Point(label6.Size.Width + 10, 29);
                SearchText.Name = "SearchText";
                SearchText.Size = new Size(232, 22);
                SearchText.TabIndex = 36;
                //SearchText.ReadOnly = true;
                // 
                // ConSearch
                // 
                ConSearch.Location = new Point(label6.Size.Width + Condition.Size.Width + 15, 4);
                ConSearch.Name = "ConSearch";
                ConSearch.Size = new Size(125, 22);
                ConSearch.TabIndex = 35;
                //ConSearch.ReadOnly = true;
                // 
                // MemSearchBtn
                // 
                MemSearchBtn.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
                MemSearchBtn.IconColor = Color.Black;
                MemSearchBtn.IconFont = FontAwesome.Sharp.IconFont.Auto;
                MemSearchBtn.IconSize = 24;
                MemSearchBtn.ImageAlign = ContentAlignment.MiddleLeft;
                MemSearchBtn.Location = new Point(label6.Size.Width + SearchText.Size.Width + 15, 3);
                MemSearchBtn.Name = "MemSearchBtn";
                MemSearchBtn.Size = new Size(120, 48);
                //MemSearchBtn.Height = 48;
                MemSearchBtn.TabIndex = 37;
                MemSearchBtn.Text = "     차량조회";
                MemSearchBtn.UseVisualStyleBackColor = true;
                // 
                // ResetMem
                // 
                ResetMem.IconChar = FontAwesome.Sharp.IconChar.ClipboardUser;
                ResetMem.IconColor = Color.Black;
                ResetMem.IconFont = FontAwesome.Sharp.IconFont.Auto;
                ResetMem.IconSize = 24;
                ResetMem.ImageAlign = ContentAlignment.MiddleLeft;
                ResetMem.Location = new Point(label6.Size.Width + SearchText.Size.Width + MemSearchBtn.Size.Width + 20, 3);
                ResetMem.Name = "ResetMem";
                ResetMem.Rotation = 180D;
                ResetMem.Size = new Size(120, 48);
                //ResetMem.Height = 48;
                ResetMem.TabIndex = 38;
                ResetMem.Text = "      초기화";
                ResetMem.UseVisualStyleBackColor = true;


                tabPage.Controls.Add(ResetMem);
                //tabPage.Controls.Add(MemAddBtn);
                tabPage.Controls.Add(MemSearchBtn);
                tabPage.Controls.Add(ConSearch);
                tabPage.Controls.Add(SearchText);
                tabPage.Controls.Add(Condition);
                tabPage.Controls.Add(label7);
                tabPage.Controls.Add(label6);

                tabPage.Controls.Add(DBData);


                
                MemSearchBtn_Click(null, null);

                bikePageOpen = true;
                DBData.DoubleBuffered(true);
                DBData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                tabControl1.SelectedTab = tabPage;

                bikePage_Num = tabControl1.SelectedIndex;

                if (addControll == 1)
                    AddFuntion();
                SetDB();
                //if (tabPage.Controls.Contains(DBData))
                //{
                //    int padding = 20;
                //    int bottomPadding = 60; // Adjust according to your layout

                //    // Adjust DBData size according to the tabPage size
                //    DBData.Size = new Size(tabPage.Width - padding, tabPage.Height - bottomPadding);

                //    // Adjust font size based on the new size
                //    float baseFontSize = 9.75F; // Initial font size
                //    float scaleFactor = (float)DBData.Width / (tabPage.Width - padding);
                //    DBData.Font = new Font("나눔고딕", baseFontSize * scaleFactor, FontStyle.Regular, GraphicsUnit.Point);
                //}
                AutoScaleMode = AutoScaleMode.Dpi;
                PerformAutoScale();
            }

            Console.WriteLine(ProgramBtn.Font.Size);
            Console.WriteLine(ResetMem.Font.Size);
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
                bshBank = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Indigo,
                                                                           Color.Indigo,
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

            if (tabControl1.TabPages[e.Index].Name == "BikePageTab")
                tabName = "차량";
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
                    if (TabP.Name.Equals("BikePageTab"))
                    {
                        bikePageOpen = false;
                    }
                    if (TabP.Name.Equals("NotiTab"))
                    {
                        notiPageOpen = false;
                    }
                }
            }
        }
        private void SetDB()
        { // 첫 번째 열로 설정
            DBData.Columns["rBikeNumber"].DisplayIndex = 1; // 두 번째 열로 설정
            DBData.Columns["rPhoneNumer"].DisplayIndex = 2;
            DBData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DBData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

                    int ProgramBtnSize_Y = ProgramBtn.Height;
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

            sensorLocationManeger = new SensorLocationManeger();
            sensorLocationManeger.Show();
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
            if (bikePageOpen)
            {
                tabControl1.SelectedIndex = bikePage_Num;
            }
            else
            {
                CreateTabPage("BikePage");
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
            gosafeDataMapview = new GosafeDataMapview();
            gosafeDataMapview.Show();
        }
        private void NotiPage_Click(object sender, EventArgs e)
        {
            OffPopup();
        }

        private void RControlBtn1_Click(object sender, EventArgs e)
        {
            OffPopup();
            rControl = new RControl();
            rControl.Show();

        }

        private void RControlBtn2_Click(object sender, EventArgs e)
        {
            OffPopup();
            //rControl = new RControl();
            //rControl.Show();
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
            //Console.WriteLine(selectQuery);
            //try
            //{
            //    connection.Open();
            //    MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection);
            //    MySqlDataReader userAccount = Selectcommand.ExecuteReader();
            //    data = new ArrayList();
            //    while (userAccount.Read())
            //    {
            //        data.Add(userAccount["AgencySavedmoney"]);
            //        data.Add(userAccount["EventCount_Hold"]);
            //        data.Add(userAccount["EventCount_Reservation"]);
            //        data.Add(userAccount["EventCount_Receipt"]);
            //        data.Add(userAccount["EventCount_Posible"]);
            //        data.Add(userAccount["EventCount_Doing"]);
            //        data.Add(userAccount["EventCount_Pickup"]);
            //        data.Add(userAccount["EventCount_Succece"]);
            //        data.Add(userAccount["EventCount_Cancel"]);
            //        data.Add(userAccount["EventCount_Accident"]);
            //        data.Add(userAccount["EventCount_Inquire"]);
            //        data.Add(userAccount["AgencyName"]);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    connection.Close();
            //    i++;
            //}
            //finally
            //{
                
            //    connection.Close();
            //}
            //Console.WriteLine(data.Count);
            //if (i == 1 || data == null || data.Count == 0)
            //{
            //    ManagerUi_Savedmoney.Text = "적립금: -";
            //    ManagerUi_All.Text = "전체: -";
            //    ManagerUi_Reservation.Text = "예약: -";
            //    ManagerUi_Receipt.Text = "접수: -";
            //    ManagerUi_Posible.Text = "가배차: -";
            //    ManagerUi_Doing.Text = "배차: -";
            //    ManagerUi_Pickup.Text = "픽업: -";
            //    ManagerUi_Succece.Text = "완료: -";
            //    ManagerUi_Cancel.Text = "취소: -";
            //    ManagerUi_Accident.Text = "사고: -";
            //    ManagerUi_Inquire.Text = "문의: -";

            //    ManagerUi_Agency.Text = "대리점:" + Agency;
            //    ManagerUi_Account.Text = "계정:" + AccountName;
            //}
            //else
            //{
            //    ManagerUi_Savedmoney.Text = "적립금:" + string.Format("{0:#,###}", data[0].ToString());
            //    ManagerUi_All.Text = "전체:" + ((int)data[2] + (int)data[3] + (int)data[4] + (int)data[5] + (int)data[6] +
            //                                            (int)data[7] + (int)data[8] + (int)data[9] + (int)data[10]);
            //    ManagerUi_Reservation.Text = "예약:" + data[2].ToString();
            //    ManagerUi_Receipt.Text = "접수:" + data[3].ToString();
            //    ManagerUi_Posible.Text = "가배차:" + data[4].ToString();
            //    ManagerUi_Doing.Text = "배차:" + data[5].ToString();
            //    ManagerUi_Pickup.Text = "픽업:" + data[6].ToString();
            //    ManagerUi_Succece.Text = "완료:" + data[7].ToString();
            //    ManagerUi_Cancel.Text = "취소:" + data[8].ToString();
            //    ManagerUi_Accident.Text = "사고:" + data[9].ToString();
            //    ManagerUi_Inquire.Text = "문의:" + data[10].ToString();

            //    ManagerUi_Agency.Text = "대리점:" + data[11].ToString();
            //    ManagerUi_Account.Text = "계정:" + AccountName;
            //}
            
           


            //ManagerUi_P1.Location = new Point(ManagerUi_Connection.Location.X + ManagerUi_Connection.Width + 4, 3);
            //ManagerUi_Agency.Location = new Point(ManagerUi_P1.Location.X + 6, 3);
            //ManagerUi_P2.Location = new Point(ManagerUi_Agency.Location.X + ManagerUi_Agency.Width + 4, 3);
            //ManagerUi_Savedmoney.Location = new Point(ManagerUi_P2.Location.X + 6, 3);
            //ManagerUi_P3.Location = new Point(ManagerUi_Savedmoney.Location.X + ManagerUi_Savedmoney.Width + 4, 3);
            //ManagerUi_Account.Location = new Point(ManagerUi_P3.Location.X + 6, 3);


            //ManagerUi_Inquire.Location = new Point(ManagerUi_P14.Location.X - ManagerUi_Inquire.Width - 4, 3);
            //ManagerUi_P13.Location = new Point(ManagerUi_Inquire.Location.X - 6, 3);
            //ManagerUi_Accident.Location = new Point(ManagerUi_P13.Location.X - ManagerUi_Accident.Width - 4, 3);
            //ManagerUi_P12.Location = new Point(ManagerUi_Accident.Location.X - 6, 3);
            //ManagerUi_Cancel.Location = new Point(ManagerUi_P12.Location.X - ManagerUi_Cancel.Width - 4, 3);
            //ManagerUi_P11.Location = new Point(ManagerUi_Cancel.Location.X - 6, 3);
            //ManagerUi_Succece.Location = new Point(ManagerUi_P11.Location.X - ManagerUi_Succece.Width - 4, 3);
            //ManagerUi_P10.Location = new Point(ManagerUi_Succece.Location.X - 6, 3);
            //ManagerUi_Pickup.Location = new Point(ManagerUi_P10.Location.X - ManagerUi_Pickup.Width - 4, 3);
            //ManagerUi_P9.Location = new Point(ManagerUi_Pickup.Location.X - 6, 3);
            //ManagerUi_Doing.Location = new Point(ManagerUi_P9.Location.X - ManagerUi_Doing.Width - 4, 3);
            //ManagerUi_P8.Location = new Point(ManagerUi_Doing.Location.X - 6, 3);
            //ManagerUi_Posible.Location = new Point(ManagerUi_P8.Location.X - ManagerUi_Posible.Width - 4, 3);
            //ManagerUi_P7.Location = new Point(ManagerUi_Posible.Location.X - 6, 3);
            //ManagerUi_Receipt.Location = new Point(ManagerUi_P7.Location.X - ManagerUi_Receipt.Width - 4, 3);
            //ManagerUi_P6.Location = new Point(ManagerUi_Receipt.Location.X - 6, 3);
            //ManagerUi_Reservation.Location = new Point(ManagerUi_P6.Location.X - ManagerUi_Reservation.Width - 4, 3);
            //ManagerUi_P5.Location = new Point(ManagerUi_Reservation.Location.X - 6, 3);
            //ManagerUi_Hold.Location = new Point(ManagerUi_P5.Location.X - ManagerUi_Hold.Width - 4, 3);
            //ManagerUi_P4.Location = new Point(ManagerUi_Hold.Location.X - 6, 3);
            //ManagerUi_All.Location = new Point(ManagerUi_P4.Location.X - ManagerUi_All.Width - 4, 3);
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
            // Prepare variables for the query construction
            string conSearch = string.IsNullOrEmpty(ConSearch.Text) ? null : "'%" + ConSearch.Text + "%'";
            //string conAgency = string.IsNullOrEmpty(ConAgency.Text) || ConAgency.Text == "전체" ? null : "rBikeNumber = '" + ConAgency.Text + "'";
            //string conIsWork = string.IsNullOrEmpty(ConisWork.Text) || ConisWork.Text == "전체" ? null : "isWork = " + Convert_isWork();
            string searchCondition = null;
            string conjunction = " AND";

            // Validate user input
            if (string.IsNullOrEmpty(ConSearch.Text) && !string.IsNullOrEmpty(Condition.Text))
            {
                MessageBox.Show("검색 조건을 기입해주세요!");
                ConSearch.Focus();
                return;
            }

            // Construct search condition based on input
            if (!string.IsNullOrEmpty(Condition.Text))
            {
                switch (Condition.Text)
                {
                    case "차량번호":
                        searchCondition = "rBikeNumber LIKE " + conSearch+"";
                        break;
                    case "센서번호":
                        searchCondition = "rPhoneNumer LIKE " + conSearch+"";
                        break;
                }
            }

            string searchText = null;
            if (!string.IsNullOrEmpty(SearchText.Text))
            {
                    searchText = "(rBikeNumber LIKE '%" + SearchText.Text +"%"+
                                "' OR rPhoneNumer LIKE '%" + SearchText.Text + "%')";
            }

            // Construct final query
            string whereClause = "";
            if (conSearch != null)
            {
                whereClause = "WHERE";
            }
            if (searchText != null)
            {
                whereClause = "WHERE";
            }
            //if (conAgency != null) whereClause += conjunction + " " + conAgency;
            //if (conIsWork != null) whereClause += conjunction + " " + conIsWork;
            if (searchCondition != null) whereClause += " " + searchCondition;
            if (searchText != null) whereClause +=  " " + searchText+"";

            string selectQuery = "SELECT *, CASE WHEN isWork='Y' THEN '근무' WHEN isWork='B' THEN '근무 전' WHEN isWork='T' THEN '휴가' WHEN isWork='L' THEN '퇴사' END as isWorkConvert, CASE WHEN powerLevel='1' THEN '관리자' WHEN powerLevel='2' THEN '팀장' WHEN powerLevel='3' THEN '사원' WHEN powerLevel='N' THEN '오류' END as rPhoneNumber FROM userinformation " + whereClause + ";";
            string selectQuery2 = "SELECT rPhoneNumer, rBikeNumber FROM tb_lte_rider_info "+ whereClause+";";
            Console.WriteLine(selectQuery2);

            // Execute query and handle results
            try
            {
                connection2.Open();
                MySqlCommand command = new MySqlCommand(selectQuery2, connection2);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                for(int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Console.WriteLine(dataTable.Rows[i]["rBikeNumber"]);
                }
                dataTable.Columns.Add("순번", typeof(int));

                //Assign row numbers
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable.Rows[i]["순번"] = i + 1;
                }


                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = dataTable;


                DBData.DataSource = bindingSource;
                DBData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                SetDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("결과가 없습니다!\n" + ex.Message);
            }
            finally
            {
                connection2.Close();
                OffPopup();
            }
        }
        public void ResetMem_Click(object sender, EventArgs e)
        {
            ConSearch.Text = null;
            Condition.Text = null;
            SearchText.Text = null;
            MemSearchBtn_Click(null, null);
            
            OffPopup();
        }
        private void PageLoad()
        {
            string main_biketab = "null";
            string main_notitab = "null";

            string selectQuery = null;
            ipSearch();

            try
            {
                connection2.Open();
                selectQuery = "select * from gogo_manager_tabstate where account_Num = " + MemberNum +
                                                        " and ip = '" + localIP +
                                                        "' ORDER BY time DESC LIMIT 1;";
                MySqlCommand Selectcommand = new MySqlCommand(selectQuery, connection2);
                MySqlDataReader userAccount = Selectcommand.ExecuteReader();
                ArrayList load = new ArrayList();
                while (userAccount.Read())
                {
                    load.Add(userAccount["main_notitab"]);
                    load.Add(userAccount["main_biketab"]);
                }

                main_notitab = load[0].ToString();
                main_biketab = load[1].ToString();
            }
            catch (Exception a)
            {
                connection2.Close();
                Console.WriteLine("qiwjeqow");
                //CreateTabPage("Noti");
                Console.WriteLine(a);
                return;
            }
            finally
            {
                connection2.Close();
            }
            if (main_notitab == "True")
            {
                CreateTabPage("Noti");
            }

            if (main_biketab == "True")
            {
                CreateTabPage("BikePage");
            }
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

            //ManagerCheck showCreateAccountForm = new ManagerCheck(MemChangeBtn.Name, this);
            //showCreateAccountForm.ShowDialog();
        }
        private void Delete_Column_Click(object sender, EventArgs e)
        {
            //ManagerCheck showmainform = new ManagerCheck(DeleteMemBtn.Name, this);
            //showmainform.ShowDialog();
            //DataGridViewRow dgvr = DBData.CurrentRow;
            //DataRow row = (dgvr.DataBoundItem as DataRowView).Row;


            //input_Name = row["name"].ToString();
            //input_Num = row["co_Member_Num"].ToString();
            //if (islogin)
            //{
            //    if (MessageBox.Show("정말 회원정보를 삭제하시겠습니까?", "주의", MessageBoxButtons.YesNo) == DialogResult.Yes && input_Num != null)
            //    {

            //        try
            //        {
            //            connection.Open();
            //            string selectQuery = "UPDATE userinformation SET isDeleted = 1 where co_Member_Num = '" + input_Num + "';";
            //            MySqlCommand insertData = new MySqlCommand(selectQuery, connection);
            //            insertData.ExecuteNonQuery();
            //            MessageBox.Show(input_Name + "님의 회원정보가 삭제되었습니다");
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex);
            //            MessageBox.Show("실패");
            //            connection.Close();

            //        }
            //        finally
            //        {
            //            connection.Close();
            //            islogin = false;
            //            ResetMem_Click(sender, e);
            //        }
            //    }
            //    input_Num = null;
            //    input_Name = null;
            //}
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

            if (bikePageOpen)
                mem = 1;
            else
                mem = 0;
            if (notiPageOpen)
                noti = 1;
            else
                noti = 0;

            ipSearch();

            DateTime date = DateTime.Now;
            date.ToString("yyyy-MM-dd H:mm:ss");

            try
            {
                connection2.Open();
                string selectQuery = "INSERT INTO gogo_manager_tabstate (account_Num, time, ip, " +
                                                               "main_biketab, main_notitab) " +
                    "VALUES ('" + MemberNum +
                            "','" + date.ToString("yyyy-MM-dd H:mm:ss") +
                            "','" + localIP +
                            "','" + mem +
                            "','" + noti + "');";

                Console.WriteLine(selectQuery);
                MySqlCommand insertData = new MySqlCommand(selectQuery, connection2);
                insertData.ExecuteNonQuery();
                MessageBox.Show("현재 화면이 저장되었습니다.");
            }
            catch (Exception w)
            {

                connection2.Close();
                Console.WriteLine(w);
            }
            finally
            {
                connection2.Close();
            }
        }
        

        private void webView2_Click(object sender, EventArgs e)
        {

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
