using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SQLRegistration
{
    public class Conversation
    {
        public string conversationName;
        public List<int> usersInConversation;

        public static int activeConversationID = -1;

        /// <summary>
        /// Gets all conversation connected to a user with specific account ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<Conversation> GetConversations(int userID)
        {
            //Gets all conversation data
            string sql = @"SELECT * FROM `conversations`;";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Executes the query

            //Creates list of Conversation instances 
            List<Conversation> conversations = new List<Conversation>();

            //Reads all conversations
            while (Connection.reader.Read())
            {
                Conversation conv = new Conversation();
                conv.conversationName = Connection.reader[1].ToString();

                List<int> usersIDs = new List<int>();

                //Splits the users' ids into int list
                string[] operands = Regex.Split(Connection.reader[2].ToString(), @"\s+");

                List<int> users = new List<int>();

                foreach (string operand in operands)
                {
                    string index = operand.Replace(" ", "");
                    int indexInt;
                    Int32.TryParse(index, out indexInt);
                    users.Add(indexInt);
                }

                conv.usersInConversation = users;

                //Check if id list contains the specific user's id, then add it to the list of Conversation instances
                foreach (int id in conv.usersInConversation)
                {
                    if (id == userID)
                    {
                        conversations.Add(conv);
                    }
                }
            }

            Connection.reader.Close();
            return conversations;
        }

        /// <summary>
        /// Gets ID of conversation with specific name
        /// </summary>
        /// <param name="conversationName"></param>
        /// <returns></returns>
        public static int GetConversationID(string conversationName)
        {
            //Creates SQL-query, selectes conversation with name
            string sql = @"SELECT * FROM `conversations` WHERE `name`='" + conversationName + "';";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            //Executes the query
            Connection.reader = Connection.command.ExecuteReader();
            Connection.reader.Read();

            int id = Int32.Parse(Connection.reader[0].ToString());

            Connection.reader.Close();

            return id;
        }

        /// <summary>
        /// Adds user to conversation
        /// </summary>
        /// <param name="useriD"></param>
        /// <param name="conversationID"></param>
        public static void AddUser(int useriD, int conversationID)
        {
            //Selects data from conversation with ID of the active conversation
            string sql = @"SELECT * FROM `conversations` WHERE `ID`='" + activeConversationID + @"';";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Execute query
            Connection.reader.Read();

            //Updates the users in conversation
            //(Gets the string of users in the conversations, add the new user and put the new value back in the database)
            string usersString = "";

            if (Connection.reader.HasRows)
            {
                usersString = Connection.reader[2].ToString();
            }

            Connection.reader.Close();

            usersString += " "+useriD.ToString();

            sql = @"UPDATE `conversations` SET `userIDsString`='" + usersString +"' WHERE `ID`= " + activeConversationID + ";";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            //Execute query
            Connection.reader = Connection.command.ExecuteReader(); 
            Connection.reader.Close();
        }

        /// <summary>
        /// Selects conversation with specific ID
        /// </summary>
        /// <param name="conversationID"></param>
        /// <returns></returns>
        public static List<int> GetConversationUsers(int conversationID)
        {
            //Creates sql-statement, selects conversation from ID
            string sql = @"SELECT * FROM `conversations` WHERE `ID`='" + conversationID + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);

            if (Connection.reader.IsClosed != true)
            {
                Connection.reader.Close();
            }

            Connection.reader = Connection.command.ExecuteReader(); //Executes the query
            Connection.reader.Read();

            //Checks if the user has no friends
            if (Connection.reader[2].ToString() == "")
            {
                Connection.reader.Close();
                return null;
            }

            //Splits the user's friends' ids into int list
            string[] operands = Regex.Split(Connection.reader[2].ToString(), @"\s+");

            //Gets and returns a list of the IDs of all users in this specific conversation
            List<int> userFriendsIds = new List<int>();

            foreach (string operand in operands)
            {
                string id = operand.Replace(" ", "");
                int idInt;
                Int32.TryParse(id, out idInt);
                userFriendsIds.Add(idInt);
            }

            Connection.reader.Close();

            return userFriendsIds;
        }
    }
}
