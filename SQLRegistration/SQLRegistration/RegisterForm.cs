﻿using System;
using System.Windows.Forms;

namespace SQLRegistration
{
    public partial class RegisterForm : Form
    {
        //Allows form to be dragged
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            //Registers user
            if(Controller.Register(
                registrationUsernameTextBox.Text, 
                registrationPasswordTextBox.Text, 
                RegistrationEmailTextBox.Text, 
                RegistrationFirstNameTextBox.Text, 
                RegistrationLastNameTextBox.Text)
                )
            {
                //If registration succeeded, hide this form
                Hide();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //Drag and drop
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
