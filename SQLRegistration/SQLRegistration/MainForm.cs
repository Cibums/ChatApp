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

        private void MainForm_Load(object sender, EventArgs e)
        {

            List<int> friends = Account.GetFriends(Connection.loggedInUserID);

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
            foreach (Conversation conv in Conversation.GetConversations(Connection.loggedInUserID))
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
                    String updateFriendSql = @"UPDATE `users` SET `frienduserIDsString`='" + Connection.reader[6] + " " + Connection.reader[0].ToString() + "' WHERE `username`='" + Account.GetAccount(Connection.loggedInUserID).username + "';";
                    Connection.reader.Close();
                    Connection.command = new MySqlCommand(updateFriendSql, Connection.connection);
                    Connection.reader = Connection.command.ExecuteReader(); //Executes the query
                }

                Connection.reader.Close();
            }
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
        }

        private void sendMessageButton_Click(object sender, EventArgs e)
        {

        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conversationPanel.Visible = false;
            conversationList.Visible = true;

        }

        private void SelectImageButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    Process.Start(open.FileName);
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }
        }
    }
}
