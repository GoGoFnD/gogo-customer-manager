using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GogoRCustomerManager
{
    public partial class cSenIDTab : Form
    {
        public BindingSource dataTable;
        public string selectedID;
        TextBox cSenIDTextBox;
        public cSenIDTab()
        {
            InitializeComponent();
            
            cSenIDData.DataSource = dataTable;
        }
        public cSenIDTab(TextBox cSenIDTextBox)
        {
            InitializeComponent();
            this.cSenIDTextBox = cSenIDTextBox;
            cSenIDData.DataSource = dataTable;

        }


        private void button1_Click(object sender, EventArgs e)//확인 버튼
        {
            cSenIDTextBox.Text = selectedID;
            Close();
        }

        private void cSenIDTab_Activated(object sender, EventArgs e)
        {
            cSenIDData.DataSource = dataTable;
        }

        private void button2_Click(object sender, EventArgs e)//취소 버튼
        {
            Close();
        }

        private void cSenIDData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedID = cSenIDData.CurrentCell.Value.ToString();
        }

        private void cSenIDData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cSenIDTextBox.Text = selectedID;
            Close();
        }
    }
}
