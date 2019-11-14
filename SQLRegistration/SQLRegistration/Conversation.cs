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

        //public static Conversation conversations;
        public static int activeConversationID = -1;

        public static List<Conversation> GetConversations(int userID)
        {
            //Gets all conversation data
            String sql = @"SELECT * FROM `conversations`;";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Executes the query

            //Creates list of Conversation instances 

            List<Conversation> conversations = new List<Conversation>();

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
                    try
                    {
                        users.Add(Int32.Parse(index));
                    }
                    catch { }

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

        public static int GetConversationID(string conversationName)
        {
            //Creates SQL-query, selectes conversation with name
            String sql = @"SELECT * FROM `conversations` WHERE `name`='" + conversationName + "';";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Executes the query
            Connection.reader.Read();

            int id = Int32.Parse(Connection.reader[0].ToString());

            Connection.reader.Close();

            return id;
        }

        public static void AddUser(int useriD, int conversationID)
        {
            //Adds user to conversation

            //Selects data from conversation with ID of the active conversation
            String sql = @"SELECT * FROM `conversations` WHERE `ID`='" + activeConversationID + @"';";
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
            Connection.reader = Connection.command.ExecuteReader(); //Execute query
            Connection.reader.Close();
        }

        public static List<int> GetConversationUsers(int conversationID)
        {
            //Selects conversation from ID
            String sql = @"SELECT * FROM `conversations` WHERE `ID`='" + conversationID + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);

            try
            {
                Connection.reader.Close();
            }
            catch { }

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

            List<int> returnValue = new List<int>();

            foreach (string operand in operands)
            {
                string index = operand.Replace(" ", "");
                try
                {
                    returnValue.Add(Int32.Parse(index));
                }
                catch { }

            }


            Connection.reader.Close();

            return returnValue;
        }
    }
}
