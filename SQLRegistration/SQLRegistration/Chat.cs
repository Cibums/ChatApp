using MySql.Data.MySqlClient;

namespace SQLRegistration
{
    public static class Chat
    {
        /// <summary>
        /// Inserts message in database
        /// </summary>
        /// <param name="message"></param>
        public static void Send(string message)
        {
            //Puts a message into the database
            string sql = @"INSERT INTO messages(conversationID, userID, messagetext) "+
                        "VALUES ('" + Conversation.activeConversationID + "','" +
                        Connection.loggedInUserID + "','" + message + "');";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            //Execute query
            Connection.reader = Connection.command.ExecuteReader();
            Connection.reader.Close();
        }
    }
}
