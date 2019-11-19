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

        /// <summary>
        /// Returns account with specific account ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
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
            string sql = @"SELECT * FROM `users` WHERE `ID`='" + ID.ToString() + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);

            if (Connection.reader != null && Connection.reader.IsClosed != true)
            {
                Connection.reader.Close();
            }

            //Executes the query
            Connection.reader = Connection.command.ExecuteReader(); 
            Connection.reader.Read();

            //Checks if the table has any rows (if there are any users with ID)
            if (Connection.reader.HasRows)
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

        /// <summary>
        /// Returns ID of any account with a specific username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static int GetAccountID(string username)
        {
            //Creates SQL-query, selectes user with specific username
            string sql = @"SELECT * FROM `users` WHERE `username`='" + username + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Executes the query
            Connection.reader.Read();

            //Get and return id of user
            int id = Int32.Parse(Connection.reader[0].ToString());
            Connection.reader.Close();

            return id;
        }

        /// <summary>
        /// Checks if the username follows some specific rule
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool IsValidUsername(string username)
        {
            //Checks if the username doesn't contain any special characters
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(username))
            {
                return false;
            }

            //Checks if the username is too long
            Func<string, int, bool> isTooLong = (text, length) => text.Length > length;

            return !isTooLong(username, 8);
        }

        /// <summary>
        /// Checks if the password follows some specific rule
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string password)
        {
            //Checks if the password doesn't contain any special characters
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(password))
            {
                return false;
            }

            //Checks if passowrd has at least one digit and one letter
            bool isLetter = false;
            bool isDigit = false;

            foreach (char c in password)
            {
                if (Controller.IsLetter(c) && isLetter == false)
                {
                    isLetter = true;
                }

                if (Controller.IsDigit(c) && isDigit == false)
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

        /// <summary>
        /// Checks if email is an actual email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
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

        /// <summary>
        /// Searches for a saved account in the local files and returns the account ID
        /// </summary>
        /// <returns></returns>
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
            //Didn't find a saved account

            //returns an impossible ID (-1)
            return -1;
        }

        /// <summary>
        /// Returns list of Ids of the friends of user with specific account ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<int> GetFriends(int userID)
        {
            //Creates SQL-query, selectes logged in user's id
            string sql = @"SELECT `frienduserIDsString` FROM `users` WHERE `ID`='" + userID + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);

            if (Connection.reader.IsClosed != true)
            {
                Connection.reader.Close();
            }

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

            //Returns list of friends' ids
            List<int> userFriendsIds = new List<int>();

            foreach (string operand in operands)
            {
                string id = operand.Replace(" ", "");

                int idString;
                Int32.TryParse(id, out idString);
                userFriendsIds.Add(idString);
            }

            Connection.reader.Close();

            return userFriendsIds;
        }
    }
}
