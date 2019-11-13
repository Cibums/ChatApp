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
            foreach (string friend in friendListBox.SelectedItems)
            {
                int id = Account.GetAccountID(friend);

                Conversation.AddUser(id, Conversation.activeConversationID);
            }
        }

        private void SelectFriendsDialog_Load(object sender, EventArgs e)
        {
            allFriends = Account.GetFriends(Connection.loggedInUserID);

            if (allFriends == null)
            {
                MessageBox.Show("You don't have nay friends yet", "No Friends Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            friendListBox.Items.Clear();

            foreach (int i in allFriends)
            {
                Account friend = Account.GetAccount(i);

                friendListBox.Items.Add(friend.username);
            }
        }
    }
}
