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

        public static Conversation conversations;

        public List<Conversation> GetConversations(int userID)
        {
            //Creates SQL-query, selectes logged in user's id
            String sql = @"SELECT * FROM `conversations`;";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Executes the query

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

        public int GetConversationID(string conversationName)
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
    }
}
