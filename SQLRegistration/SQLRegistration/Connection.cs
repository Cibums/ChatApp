using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace SQLRegistration
{
    static class Connection
    {
        public static MySqlConnection connection = new MySqlConnection(GetConnectionString);
        //Assumes that the connection won't work
        public static bool isConnectedToTheServer = false;
        public static MySqlCommand command;
        public static MySqlDataReader reader;
        public static int loggedInUserID = -1;

        public static string GetConnectionString
        {
            get
            {
                //Reads config.txt
                string dbusername = "";
                string dbpassword = "";

                foreach (string line in File.ReadAllLines(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName + @"\config.txt"))
                {
                    if (line.Contains("username: "))
                    {
                        string username = line;
                        username = username.Replace("username: ", "");
                        username = username.Replace("\"", "");

                        dbusername = username;
                    }
                    else if (line.Contains("password: "))
                    {
                        string password = line;
                        password = password.Replace("password: ", "");
                        password = password.Replace("\"", "");

                        dbpassword = password;
                    }
                }

                //Returns Server Information
                string con = @"server=localhost; user id="+dbusername+";password="+dbpassword+"; database=chatapp";
                return con;
            }
        }
    }
}
