using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace SQLRegistration
{
    public static class Controller
    {
        //Forms
        public static Form1 loginForm;
        public static RegisterForm registerForm;
        public static MainForm mainForm;

        public static bool Register(string usernameInput, string passwordInput, string emailInput, string firstNameInput, string lastNameInput)
        {
            //Registers user 

            //
            String sql = @"SELECT * FROM `users` WHERE `username`='"+ usernameInput + @"' OR `email`='"+ emailInput + @"';";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Execute query
            Connection.reader.Read();

            if (Connection.reader.HasRows)
            {
                if (Connection.reader[1].ToString() == usernameInput)
                {
                    MessageBox.Show("USERNAME IS ALREADY TAKEN");
                    Connection.reader.Close();
                    return false;
                }

                if (Connection.reader[3].ToString() == emailInput)
                {
                    MessageBox.Show("EMAIL IS ALREADY USED ONCE");
                    Connection.reader.Close();
                    return false;
                }
            }            

            Connection.reader.Close();

            if (!Account.IsValidUsername(usernameInput))
            {
                MessageBox.Show("USERNAME CAN'T USE SPECIAL CHARACTERS OR CAN'T BE LONGER THAN 8 CHARACTERS");
                return false;
            }

            if (!Account.IsValidEmail(emailInput))
            {
                MessageBox.Show("INVALID EMAIL ADRESS");
                return false;
            }

            if (!Account.IsValidPassword(passwordInput))
            {
                MessageBox.Show("PASSWORD HAS TO CONTAIN AT LEAST ONE CAPITAL LETTER AND ONE NUMBER AND CAN'T USE SPECIAL CHARACTERS");
                return false;
            }

            //Create a SQL-query that inserts user information
            sql = @"INSERT INTO users(username, password, email, firstname, lastname, frienduserIDsString) VALUES ('" + (usernameInput).ToLower() + "','" + HashPassword(passwordInput) + "','" + emailInput + "','" + firstNameInput + "','" + lastNameInput + "','" + "" + "')";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Execute query
            Connection.reader.Close();

            Login(usernameInput, HashPassword(passwordInput));

            return true;
        }

        public static string HashPassword(string password)
        {
            HashAlgorithm algorithm = SHA256.Create();
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        public static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool Login(string usernameInput, string passwordInput)
        {

            //If there's no connection to the server, return false
            if (!Connection.isConnectedToTheServer)
            {
                return false;
            }

            //Create SQL-query (gets the users with spcific username)
            String sql = @"SELECT * FROM `users` WHERE `username`='" + usernameInput + @"'";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.reader = Connection.command.ExecuteReader(); //Executes the query
            Connection.reader.Read();
            

            if (Connection.reader.HasRows) //Checks if the table has any rows (if there are any users created)
            {
                //Checking if username and password match a user in the table
                if (usernameInput == (Connection.reader[1].ToString()).ToLower() && passwordInput == Connection.reader[2].ToString())
                {
                    //Login Succeeded

                    int id = Int32.Parse(Connection.reader[0].ToString());

                    LoginSucceeded(id);
                    Connection.reader.Close();

                    return true;
                }
                else
                {
                    //Username or password didn't match
                    MessageBox.Show("Invalid Account! Wrong Username or Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //A user with that username wasn't found
                MessageBox.Show("Invalid Account! Wrong Username or Password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            Connection.reader.Close();

            return false;
        }

        public static void LoginSucceeded(int userID)
        {
            //Sets the userID variable to the ID of the logged in user
            Connection.loggedInUserID = userID;
            //Shows Main Form
            mainForm.Show();

            //If there is a saved account id, delete saved data
            if (!File.Exists(Application.LocalUserAppDataPath + @"\a.ca"))
            {
                //Create new data
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.LocalUserAppDataPath + @"\a.ca", FileMode.Create);
                bf.Serialize(fs, userID);
                fs.Close();
            }

            //Hides the other forms
            registerForm.Hide();
            loginForm.Hide();

            mainForm.UpdateUserInformation(Account.GetAccount(userID));
        }

        
    }
}
