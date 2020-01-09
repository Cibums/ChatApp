using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace SQLRegistration
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the information displayed in the main form to the information of a specific account
        /// </summary>
        /// <param name="account"></param>
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Quits the whole application (including other hidden forms)
            Application.Exit();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateConversations();
        }

        /// <summary>
        /// Updates the list of conversations
        /// </summary>
        public void UpdateConversations()
        {
            //Removes currently showing conversation in the list of conversations
            conversationList.Items.Clear();

            //For each conversation that the logged in user has access to
            foreach (Conversation conv in Conversation.GetConversations(Connection.loggedInUserID))
            {
                //Add conversation to the list of conversations
                conversationList.Items.Add(conv.conversationName);
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //If the saved account data exists: delete it
            if (File.Exists(Application.LocalUserAppDataPath + @"\a.ca"))
            {
                File.Delete(Application.LocalUserAppDataPath + @"\a.ca");
            }

            //Log out the user
            Connection.loggedInUserID = -1;

            Controller.loginForm.Show();

            Hide();
        }

        private void conversationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Changes activeConversationID to clicked conversation
            if(sender.GetType() == typeof(ListView))
            {
                ListView myList = (ListView)sender;

                if (myList.SelectedItems.Count <= 0)
                {
                    return;
                }

                Conversation.activeConversationID = 
                    Conversation.GetConversationID(myList.SelectedItems[0].Text);
            }

            //Shows chat panel
            conversationList.Visible = false;
            conversationPanel.Visible = true;

            conversationList.SelectedItems.Clear();

            UpdateChat();
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {
            //Sends message
            Chat.Send(messageTextBox.Text);

            messageTextBox.Text = "";

            //Updates the chat
            UpdateChat();
        }

        /// <summary>
        /// Shows the list of conversations
        /// </summary>
        public void GoHome()
        {
            //Goes to conversation list
            conversationPanel.Visible = false;
            conversationList.Visible = true;

            UpdateConversations();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoHome();
        }

        /// <summary>
        /// Updates the messages shown in the chat
        /// </summary>
        public void UpdateChat()
        {
            //Removes all messages currently shown
            messagesList.Items.Clear();

            //Gets all messages in active conversation
            string sql = @"SELECT * FROM `messages` WHERE `conversationID`='" 
                        + Conversation.activeConversationID.ToString() + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);

            if (Connection.reader.IsClosed != true)
            {
                Connection.reader.Close();
            }

            //Executes the query
            Connection.reader = Connection.command.ExecuteReader();

            List<int> messageIDs = new List<int>();

            while (Connection.reader.Read())
            {
                string message = Connection.reader[3].ToString();

                //Adds message to list of messages
                messagesList.Items.Add(message);
                //Adds the ID of the message to a list
                messageIDs.Add(int.Parse(Connection.reader[2].ToString()));
            }

            //Show all messages
            int index = 0;
            List<string> finalStrings = new List<string>();

            //Adds the first names of the senders of all the messages ro the messages
            foreach (string item in messagesList.Items)
            {
                string s = "";

                if (Account.GetAccount(messageIDs[index]).firstname + ": " + item != null)
                {
                    s = Account.GetAccount(messageIDs[index]).firstname + ": " + item;
                }

                finalStrings.Add(s);
                index++;
            }

            //Update the messages to show the first names of the senders
            for (int i = 0; i < messagesList.Items.Count; i++)
            {
                messagesList.Items[i] = finalStrings[i];
            }

            index = 0;
            finalStrings.Clear();
            messageIDs.Clear();

            Connection.reader.Close();

            //Deselects selected alterantives in check box list
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
                //Gets all data from the user with the specific ID
                String sql = @"SELECT * FROM `users` WHERE `ID`='" 
                            + Connection.loggedInUserID + @"';";
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                Connection.reader.Read();

                string frienduserIDsString = "";

                //Stop if user can't be found
                if (!Connection.reader.HasRows)
                {
                    MessageBox.Show("Can't find logged in user");
                    Connection.reader.Close();
                    return;
                }
                else
                {
                    //Gets the user's current friends
                    frienduserIDsString = Connection.reader[6].ToString();
                }

                Connection.reader.Close();

                //Selects the user that has the same username as the input
                sql = @"SELECT * FROM `users` WHERE `username`='" + aff.input + @"'";
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                Connection.reader.Read();

                if (Connection.reader.HasRows) //Checks if the table has any rows (if there are any users with that username)
                {
                    //Updates the friend list
                    String updateFriendSql = @"UPDATE `users` SET `frienduserIDsString`='" 
                                            + frienduserIDsString + " " + Connection.reader[0] 
                                            + "' WHERE `ID`='" + Connection.loggedInUserID + "';";
                    Connection.reader.Close();
                    Connection.command = new MySqlCommand(updateFriendSql, Connection.connection);
                    Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                }
                else
                {
                    //There's no user with the same username as the input
                    MessageBox.Show(
                        "Can't find the user " + aff.input, 
                        "Something went wrong", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Exclamation
                        );
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
            //Opens dialog form window
            TextInputDialog dialog = new TextInputDialog();
            dialog.SetDialogSettings("CONVERSATION NAME:", "Add Conversation");
            dialog.ShowDialog();

            //Checks if clicked OK
            if (dialog.DialogResult == DialogResult.OK)
            {
                if (dialog.input == "")
                {
                    //The user didn't write anything as an input
                    MessageBox.Show(
                        "You have to name the conversation soemthing", 
                        "Failed", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Warning
                        );
                    return;
                }

                //Creates a conversation in the database
                string sql = @"INSERT INTO conversations(name, userIDsString) VALUES ('" 
                            + dialog.input + "'," + Connection.loggedInUserID + ")";
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Execute query
                Connection.reader.Close();

                //Updates the conversation list
                UpdateConversations();
            }
        }

        private void AddFriendsButton_Click(object sender, EventArgs e)
        {
            //Opens the select friend dialog
            SelectFriendsDialog sfd = new SelectFriendsDialog();
            sfd.ShowDialog();
        }
    }
}
