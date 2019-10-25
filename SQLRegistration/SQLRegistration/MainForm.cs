using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace SQLRegistration
{
    public partial class MainForm : Form
    {
        //Variables

        public int userID; //Logged in user ID
        private int activeConversationID = -1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Updates the UI to the correct user information
            UpdateUserInformation(Account.accounts.GetAccount(userID));

            List<int> friends = Account.accounts.GetFriends(userID);

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
            foreach (Conversation conv in Conversation.conversations.GetConversations(userID))
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
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                Connection.reader.Read();

                if (Connection.reader.HasRows) //Checks if the table has any rows (if there are any users with that username)
                {
                    

                    //Updates the friend list
                    String updateFriendSql = @"UPDATE `users` SET `frienduserIDsString`='" + Connection.reader[6] + " " + Connection.reader[0].ToString() + "' WHERE `username`='" + Account.accounts.GetAccount(userID).username + "';";
                    Connection.reader.Close();
                    Connection.command = new MySqlCommand(updateFriendSql, Connection.connection);
                    Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                }

                Connection.reader.Close();
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

                activeConversationID = Conversation.conversations.GetConversationID(myList.SelectedItems[0].Text);
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
