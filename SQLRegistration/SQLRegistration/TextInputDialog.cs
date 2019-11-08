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
    public partial class TextInputDialog : Form
    {
        public string input = "";

        public TextInputDialog()
        {
            InitializeComponent();
        }

        public void SetDialogSettings(string Question, string ButtonText)
        {
            dialogLabel.Text = Question;
            DialogOKButton.Text = ButtonText;
        }

        private void addFriendButton_Click(object sender, EventArgs e)
        {
            //Saves the written username so that it's reachable in the MainForm class.
            this.input = dialogInput.Text;
            this.DialogResult = DialogResult.OK;
        }

    }
}
