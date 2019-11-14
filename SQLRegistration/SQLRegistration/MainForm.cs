using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace SQLRegistration
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void UpdateUserInformation(Account account)
        {
            //If there's already a user specific button: delete it before creating new
            if (menuStrip.Items.Count >= 4)
            {
                menuStrip.Items.RemoveAt(0);
            }

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

            UpdateConversations();
            
        }

        public void UpdateConversations()
        {
            conversationList.Items.Clear();

            foreach (Conversation conv in Conversation.GetConversations(Connection.loggedInUserID))
            {
                conversationList.Items.Add(conv.conversationName);
            }
        }

        private void addFriendButton_Click(object sender, EventArgs e)
        {
            
        }

        private void addFriendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.Write(Connection.loggedInUserID);

            if (File.Exists(Application.LocalUserAppDataPath + @"\a.ca"))
            {
                File.Delete(Application.LocalUserAppDataPath + @"\a.ca");
            }
 
            Connection.loggedInUserID = -1;

            Controller.loginForm.Show();

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

                Conversation.activeConversationID = Conversation.GetConversationID(myList.SelectedItems[0].Text);
            }

            conversationList.Visible = false;
            conversationPanel.Visible = true;

            conversationList.SelectedItems.Clear();

            UpdateChat();
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            Chat.Send(messageTextBox.Text);

            messageTextBox.Text = "";

            UpdateChat();
        }

        public void GoHome()
        {
            conversationPanel.Visible = false;
            conversationList.Visible = true;

            UpdateConversations();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoHome();
        }

        public void UpdateChat()
        {
            messagesList.Items.Clear();

            //Creates SQL-query
            String sql = @"SELECT * FROM `messages` WHERE `conversationID`='" + Conversation.activeConversationID.ToString() + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);

            try
            {
                Connection.reader.Close();
            }
            catch { }

            Connection.reader = Connection.command.ExecuteReader(); //Executes the query

            List<int> messageIDs = new List<int>();

            while (Connection.reader.Read())
            {
                string message = Connection.reader[3].ToString();

                messagesList.Items.Add(message);
                messageIDs.Add(Int32.Parse(Connection.reader[2].ToString()));
                
            }

            int index = 0;
            List<string> finalStrings = new List<string>();

            foreach (string item in messagesList.Items)
            {
                string s = "";

                try
                {
                    s = Account.GetAccount(messageIDs[index]).firstname + ": " + item;
                }catch{}

                finalStrings.Add(s);
                index++;
            }

            for (int i = 0; i < messagesList.Items.Count; i++)
            {
                messagesList.Items[i] = finalStrings[i];
            }

            index = 0;
            finalStrings.Clear();
            messageIDs.Clear();

            Connection.reader.Close();

            messagesList.SelectedIndex = messagesList.Items.Count - 1;
            messagesList.SelectedIndex = -1;

        }

        private void messagesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Clears Selection if user tries to click
            if (messagesList.SelectedItems.Count > 0)
            {
                messagesList.ClearSelected();
            }
        }

        private void friendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Opens the dialog window
            TextInputDialog aff = new TextInputDialog();
            aff.SetDialogSettings("USERNAME:", "Add Friend");
            aff.ShowDialog();

            //Checks if the user clicked OK
            if (aff.DialogResult == DialogResult.OK)
            {
                String sql = @"SELECT * FROM `users` WHERE `ID`='" + Connection.loggedInUserID + @"';";
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                Connection.reader.Read();

                string frienduserIDsString = "";

                if (!Connection.reader.HasRows)
                {
                    MessageBox.Show("Can't find logged in user");
                    Connection.reader.Close();
                    return;
                }
                else
                {
                    frienduserIDsString = Connection.reader[6].ToString();
                }

                Connection.reader.Close();

                sql = @"SELECT * FROM `users` WHERE `username`='" + aff.input + @"'";
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                Connection.reader.Read();

                if (Connection.reader.HasRows) //Checks if the table has any rows (if there are any users with that username)
                {
                    //Updates the friend list
                    String updateFriendSql = @"UPDATE `users` SET `frienduserIDsString`='" + frienduserIDsString + " " + Connection.reader[0] + "' WHERE `ID`='" + Connection.loggedInUserID + "';";
                    Connection.reader.Close();
                    Connection.command = new MySqlCommand(updateFriendSql, Connection.connection);
                    Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                }
                else
                {
                    MessageBox.Show("Can't find the user " + aff.input, "Something went wrong", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                Connection.reader.Close();
            }
        }

        private void conversationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateConversartion();
        }

        void CreateConversartion()
        {
            TextInputDialog dialog = new TextInputDialog();
            dialog.SetDialogSettings("CONVERSATION NAME:", "Add Conversation");
            dialog.ShowDialog();

            if (dialog.DialogResult == DialogResult.OK)
            {
                if (dialog.input == "")
                {
                    MessageBox.Show("You have to name the conversation soemthing", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Creates a conversation in the database
                string sql = @"INSERT INTO conversations(name, userIDsString) VALUES ('" + dialog.input + "'," + Connection.loggedInUserID + ")";
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Execute query
                Connection.reader.Close();

                UpdateConversations();
            }
        }

        private void AddFriendsButton_Click(object sender, EventArgs e)
        {
            SelectFriendsDialog sfd = new SelectFriendsDialog();
            sfd.ShowDialog();
        }
    }
}
