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
    public static class Chat<T>
    {
        public static void Send(T message)
        {
            if (message.GetType() == typeof(Image))
            {

            }
            else if(message.GetType() == typeof(string))
            {
                string tagged = message.ToString().Split('@').Last().Split(' ').First();

                //Create a SQL-query that inserts user information
                String sql = @"INSERT INTO messages(conversationID, userID, messagetext) VALUES ('" + Conversation.activeConversationID + "','" + Connection.loggedInUserID + "','" + message + "');";
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Execute query
                Connection.reader.Close();

                try
                {
                    Chat<int>.Send(Account.GetAccountID(tagged));
                }
                catch { }

                
            }
            else if(message.GetType() == typeof(int))
            {
                //Create a SQL-query that inserts user information
                String sql = @"INSERT INTO messages(conversationID, userID, messageaccountID) VALUES ('" + Conversation.activeConversationID + "','" + Connection.loggedInUserID + "','" + message + "');";
                Connection.command = new MySqlCommand(sql, Connection.connection);
                Connection.reader = Connection.command.ExecuteReader(); //Execute query
                Connection.reader.Close();
            }
            else
            {

            }


        }

        public static byte[] ImageToByte(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}
