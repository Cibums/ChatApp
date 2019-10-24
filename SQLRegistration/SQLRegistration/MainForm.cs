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
using System.IO;

namespace SQLRegistration
{
    public partial class MainForm : Form
    {
        //Variables

        public int userID; //Logged in user ID

        MySqlConnection con = new MySqlConnection();
        MySqlCommand command = new MySqlCommand();
        MySqlDataReader reader;
        Connection connection = new Connection();
        private bool isConnectedToServer = true; //Supposes that the connection will work
        private int activeConversationID = -1;

        public MainForm()
        {
            InitializeComponent();

            //Create a connection to the database
            con = new MySqlConnection(connection.GetConnectionString);

            //Trying to open the connection
            try
            {
                con.Open();
            }
            catch
            {
                isConnectedToServer = false;
                MessageBox.Show("Not connected to the server!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Updates the UI to the correct user information
            UpdateUserInformation(Controller.controller.GetAccount(userID));

            List<int> friends = Controller.controller.GetFriends(userID);

            if (friends != null)
            {
                foreach (int i in friends)
                {
                    MessageBox.Show(i.ToString());
                }
            }

            
        }

        public void GenerateConversations()
        {

        }

        void UpdateUserInformation(Account account)
        {
            //Adds button in menustrip that reads the logged in users full name
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = account.firstname + " " + account.lastname;
            menuStrip.Items.Insert(0, tsmi);
        }

        //When form is closing
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Quites the whole application (including other hidden forms)
            Application.Exit();
        }

        //When form is shown
        private void MainForm_Shown(object sender, EventArgs e)
        {
            foreach (Conversation conv in Controller.controller.GetConversations(userID))
            {
                conversationList.Items.Add(conv.conversationName);
            }

            
        }

        private void addFriendButton_Click(object sender, EventArgs e)
        {
            
        }

        private void conversationsList_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void addFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Opens the dialog window
            AddFriendForm aff = new AddFriendForm();
            aff.ShowDialog();

            //Checks if the user clicked OK
            if (aff.DialogResult == DialogResult.OK)
            {
                MessageBox.Show(aff.usernameInput);

                //Creates SQL-query
                String sql = @"SELECT * FROM `users` WHERE `username`='" + aff.usernameInput + @"'";
                command = new MySqlCommand(sql, con);
                reader = command.ExecuteReader(); //Executes the query
                reader.Read();

                if (reader.HasRows) //Checks if the table has any rows (if there are any users with that username)
                {
                    

                    //Updates the friend list
                    String updateFriendSql = @"UPDATE `users` SET `frienduserIDsString`='" + reader[6] + " " + reader[0].ToString() + "' WHERE `username`='" + Controller.controller.GetAccount(userID).username + "';";
                    reader.Close();
                    command = new MySqlCommand(updateFriendSql, con);
                    reader = command.ExecuteReader(); //Executes the query
                }

                reader.Close();
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.LocalUserAppDataPath + @"\a.ca"))
            {
                try
                {
                    File.Delete(Application.LocalUserAppDataPath + @"\a.ca");
                }
                catch { }
            }

            Controller.controller.loginForm.Show();
            userID = -1;
            Hide();
        }

        private void conversationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(sender.GetType() == typeof(ListView))
            {
                ListView myList = (ListView)sender;

                if (myList.SelectedItems.Count <= 0)
                {
                    return;
                }

                activeConversationID = Controller.controller.GetConversationID(myList.SelectedItems[0].Text);
            }

            conversationList.Visible = false;
            conversationPanel.Visible = true;

            conversationList.SelectedItems.Clear();
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conversationPanel.Visible = false;
            conversationList.Visible = true;

        }
    }
}
