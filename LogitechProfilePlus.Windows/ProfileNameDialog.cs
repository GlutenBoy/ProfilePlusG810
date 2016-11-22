using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogitechProfilePlus.Windows
{
    public partial class ProfileNameDialog : Form
    {
        public string ProfileName { get; set; }

        public ProfileNameDialog()
        {
            InitializeComponent();
        }

        private void ProfileNameDialog_Load(object sender, EventArgs e)
        {
            txtProfileName.Text = ProfileName;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            ProfileName = txtProfileName.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ProfileNameDialog_Activated(object sender, EventArgs e)
        {
           if (txtProfileName.Text != "")
            {
                txtProfileName.SelectAll();
                txtProfileName.Focus();
            }
        }
    }
}
