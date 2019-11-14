using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLRegistration
{
    public static class Chat
    {
        public static void Send(string message)
        {
            //Create a SQL-query that inserts user information
            String sql = @"INSERT INTO messages(conversationID, userID, messagetext) VALUES ('" + Conversation.activeConversationID + "','" + Connection.loggedInUserID + "','" + message + "');";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Execute query
            Connection.reader.Close();
        }
    }
}
