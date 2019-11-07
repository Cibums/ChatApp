using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SQLRegistration
{
    public class Account
    {
        //Account information variables

        public string username;
        public string password;
        public string email;
        public string firstname;
        public string lastname;
        public string friendsUserIDs;

        public static Account GetAccount(int ID)
        {
            //Varibales
            string username;
            string password;
            string email;
            string firstname;
            string lastname;
            string friendListString;

            //Creates SQL-query, selectes user with specific ID
            String sql = @"SELECT * FROM `users` WHERE `ID`='" + ID.ToString() + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);

            try
            {
                Connection.reader.Close();
            }
            catch { }

            Connection.reader = Connection.command.ExecuteReader(); //Executes the query
            Connection.reader.Read();

            if (Connection.reader.HasRows) //Checks if the table has any rows (if there are any users with ID)
            {
                //Setting variables
                username = Connection.reader[1].ToString();
                password = Connection.reader[2].ToString();
                email = Connection.reader[3].ToString();
                firstname = Connection.reader[4].ToString();
                lastname = Connection.reader[5].ToString();
                friendListString = Connection.reader[6].ToString();

                //Creating account instance
                Account acc = new Account();
                acc.username = username;
                acc.password = password;
                acc.email = email;
                acc.firstname = firstname;
                acc.lastname = lastname;
                acc.friendsUserIDs = friendListString;

                Connection.reader.Close();

                //Returning account
                return acc;
            }

            Connection.reader.Close();

            //Didn't find an account
            return null;


        }

        public static int GetAccountID(string username)
        {
            //Creates SQL-query, selectes user with specific ID
            String sql = @"SELECT * FROM `users` WHERE `username`='" + username + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Executes the query
            Connection.reader.Read();

            int id = Int32.Parse(Connection.reader[0].ToString());
            Connection.reader.Close();

            return id;
        }

        public static bool IsValidUsername(string username)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(username))
            {
                return false;
            }
            Func<string, int, bool> isTooLong = (text, length) => text.Length > length;

            return !isTooLong(username, 8);
        }

        public static bool IsValidPassword(string password)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(password))
            {
                return false;
            }

            bool isLetter = false;
            bool isDigit = false;

            foreach (char c in password)
            {
                if (Controller.IsLetter(c))
                {
                    isLetter = true;
                }

                if (Controller.IsDigit(c))
                {
                    isDigit = true;
                }
            }

            if (isDigit && isLetter)
            {
                return true;
            }

            return false;
        }

        public static bool IsValidEmail(string email)
        {
            //Checks if email is an actual email
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static int GetSavedAccountID()
        {
            //Checks if there is a saved account
            if (File.Exists(Application.LocalUserAppDataPath + @"\a.ca"))
            {
                //Returns the saved account ID
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.LocalUserAppDataPath + @"\a.ca", FileMode.Open);
                int acc = (int)bf.Deserialize(fs);
                fs.Close();
                return acc;
            }

            

            //Didn't find a saved accoun

            //returns an impossible ID (-1)
            return -1;
        }

        public static List<int> GetFriends(int userID)
        {
            //Creates SQL-query, selectes logged in user's id
            String sql = @"SELECT `frienduserIDsString` FROM `users` WHERE `ID`='" + userID + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);

            try
            {
                Connection.reader.Close();
            }
            catch { }

            Connection.reader = Connection.command.ExecuteReader(); //Executes the query
            Connection.reader.Read();

            //Checks if the user has no friends
            if (Connection.reader[0].ToString() == "")
            {
                Connection.reader.Close();
                return null;
            }

            //Splits the user's friends' ids into int list
            string[] operands = Regex.Split(Connection.reader[0].ToString(), @"\s+");

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
