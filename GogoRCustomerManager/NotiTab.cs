using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;

namespace GogoRCustomerManager
{
    public class NotiTab
    {

        private System.Windows.Forms.TabControl tabControl = new System.Windows.Forms.TabControl();
        private System.Windows.Forms.RichTextBox programNotiText = new System.Windows.Forms.RichTextBox();
        private System.Windows.Forms.RichTextBox programUpdateText = new System.Windows.Forms.RichTextBox();
        private string page_Name = null;
        private TabPage tabPage = null;
        private bool notiPageOpen;
        private Form Main = null;
        private int notiPage_Num;

        MySqlConnection connection = new MySqlConnection//데이터 베이스 연결
           (
               "Server= localhost ;Port= 3307; Database= appsigncode; Uid= root; Pwd= gogovlfflq;"
           );
        private ArrayList data;
        public NotiTab(string page_Name, TabPage tabPage, Form Main, bool notiPageOpen, int notiPage_Num)
        {
            this.page_Name = page_Name;
            this.tabPage = tabPage;
            this.Main = Main;
            this.notiPageOpen = notiPageOpen;
            this.notiPage_Num = notiPage_Num;



            
        }

        public void createTab()
        {
            Console.WriteLine("커넥션 완료");
            Main.Controls.Add(tabControl);
            tabPage = new TabPage(page_Name);
            tabPage.Name = "NotiTab";
            tabPage.Padding = new Padding(3);
            tabPage.Size = new Size(1147, 476);
            tabPage.TabIndex = 0;
            tabPage.Text = "";
            tabPage.UseVisualStyleBackColor = true;
            tabControl.TabPages.Add(tabPage);
            //tabPage.Click += new EventHandler(tabPage1_Click);
            tabPage.Font = new Font("나눔고딕", 22F, FontStyle.Bold, GraphicsUnit.Point, (byte)(129));
            tabControl.SelectedTab = tabPage;
            notiPage_Num = tabControl.SelectedIndex;
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
                Console.WriteLine("커넥션 완료");
            }





            programNotiText.Text = data[0].ToString();
            programUpdateText.Text = data[1].ToString();
        }
    }
    }

