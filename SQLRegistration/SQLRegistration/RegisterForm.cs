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
    public partial class RegisterForm : Form
    {

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            //Registers user
            if(Controller.controller.Register(registrationUsernameTextBox.Text, registrationPasswordTextBox.Text, RegistrationEmailTextBox.Text, RegistrationFirstNameTextBox.Text, RegistrationLastNameTextBox.Text))
            {
                //If registration succeeded, hide this form
                Hide();
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
