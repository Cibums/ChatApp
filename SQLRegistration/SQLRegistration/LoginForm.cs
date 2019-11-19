using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;
using System.IO;

namespace SQLRegistration
{
    public partial class LoginForm : Form
    {
        //Allows form to be dragged
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            //Tries to Log In
            Controller.Login(usernameTextBox.Text, Controller.HashPassword(passwordTextBox.Text));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Opens the Registration Form
            RegisterForm rf = new RegisterForm();
            rf.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Trying to open the connection
            try
            {
                Connection.connection.Open();
                Connection.isConnectedToTheServer = true;
            }
            catch
            {
                //If the connection to the server isn't there, tell the user
                Connection.isConnectedToTheServer = false;

                MessageBox.Show("Not connected to the server! Follow readme.txt", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //Creates other forms and making them reachable from the controller class
            Controller.loginForm = this;

            RegisterForm rf = new RegisterForm();
            Controller.registerForm = rf;

            MainForm mainForm = new MainForm();
            Controller.mainForm = mainForm;

            //If there is no connection to the server, close the whole application (all forms)
            if (!Connection.isConnectedToTheServer)
            {
                Application.Exit();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            //Close The Whole Application
            Application.Exit();
        }

        /// <summary>
        /// Tries to log in to account saved in local files
        /// </summary>
        public static void LoginToSavedAccount()
        {
            //Gets the saved account ID
            int savedID = Account.GetSavedAccountID();

            //If there was an account saved, try to log in to the saved account
            if (savedID != -1)
            {
                Account acc = Account.GetAccount(savedID);

                try
                {
                    Controller.Login(acc.username, acc.password);
                }
                catch
                {
                    MessageBox.Show("Couldn't Log In With Saved ID");
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoginToSavedAccount();
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
    }
}
