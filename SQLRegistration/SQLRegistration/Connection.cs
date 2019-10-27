using MySql.Data.MySqlClient;
using System;

namespace SQLRegistration
{
    static class Connection
    {
        public static MySqlConnection connection = new MySqlConnection(GetConnectionString);
        public static bool isConnectedToTheServer = false; //Assumes that the connection won't work
        public static MySqlCommand command;
        public static MySqlDataReader reader;
        public static int loggedInUserID = -1;

        public static string GetConnectionString
        {
            get{
                //Server Information
                String con = @"server=localhost; user id=root;password=; database=chatapp";
                return con;
            }
        }
    }
}
