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
    public partial class SelectFriendsDialog : Form
    {
        List<int> allFriends = new List<int>();

        public SelectFriendsDialog()
        {
            InitializeComponent();
        }

        private void addFriendsButton_Click(object sender, EventArgs e)
        {
            string friendsAdded = "";

            List<int> conversationUsersIDs = 
                Conversation.GetConversationUsers(Conversation.activeConversationID);

            bool first = true;

            //Tries to add all checked friends to active conversation
            foreach (string friend in friendListBox.CheckedItems)
            {
                int id = Account.GetAccountID(friend);

                if (conversationUsersIDs.Contains(id))
                {
                    //Don't add friend if user already is in this conversation
                    Close();
                    return;
                }

                //Ads friend to active conversation
                Conversation.AddUser(id, Conversation.activeConversationID);

                //Creates string with all friends added to the conversation
                if (!first)
                {
                    friendsAdded += ", ";
                }

                friendsAdded += Account.GetAccount(id).firstname;

                first = false;
            }

            //Tell the user which users were added
            MessageBox.Show(
                "Added " + friendsAdded + " to this conversation", 
                "Added", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information
                );

            Close();
        }

        private void SelectFriendsDialog_Load(object sender, EventArgs e)
        {
            //Gets logged in user's friends
            allFriends = Account.GetFriends(Connection.loggedInUserID);

            //If the user doesn't have any friends: Tell the user and continue
            if (allFriends == null)
            {
                MessageBox.Show(
                    "You don't have nay friends yet", 
                    "No Friends Added", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information
                    );
                this.Close();
                return;
            }

            //Updates friend list
            friendListBox.Items.Clear();

            foreach (int i in allFriends)
            {
                Account friend = Account.GetAccount(i);

                friendListBox.Items.Add(friend.username);
            }
        }
    }
}
