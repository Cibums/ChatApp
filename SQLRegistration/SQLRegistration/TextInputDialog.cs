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
        //Input is reachable from other classes
        public string input = "";

        public TextInputDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the question and the text on the button in the dialog window
        /// </summary>
        /// <param name="Question"></param>
        /// <param name="ButtonText"></param>
        public void SetDialogSettings(string Question, string ButtonText)
        {
            //Sets the dialog settings
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
