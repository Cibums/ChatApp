using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLRegistration
{
    public partial class AddFriendForm : Form
    {
        public string usernameInput = "";

        public AddFriendForm()
        {
            InitializeComponent();
        }

        private void addFriendButton_Click(object sender, EventArgs e)
        {
            //Saves the written username so that it's reachable in the MainForm class.
            this.usernameInput = addFriendUsernameTextBox.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
