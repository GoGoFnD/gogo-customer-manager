using System;
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
    public partial class cSenIDTab : Form
    {
        public BindingSource dataTable;
        public cSenIDTab()
        {
            InitializeComponent();
            
            cSenIDData.DataSource = dataTable;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            cSenIDData.DataSource = dataTable;

        }

        private void cSenIDTab_Activated(object sender, EventArgs e)
        {
            cSenIDData.DataSource = dataTable;
        }
    }
}
