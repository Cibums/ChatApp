using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;

namespace SQLRegistration
{
    public static class Controller
    {
        //Forms
        public static LoginForm loginForm;
        public static RegisterForm registerForm;
        public static MainForm mainForm;

        /// <summary>
        /// Registers user
        /// </summary>
        /// <param name="usernameInput"></param>
        /// <param name="passwordInput"></param>
        /// <param name="emailInput"></param>
        /// <param name="firstNameInput"></param>
        /// <param name="lastNameInput"></param>
        /// <returns></returns>
        public static bool Register(
            string usernameInput, 
            string passwordInput, 
            string emailInput, 
            string firstNameInput, 
            string lastNameInput
            )
        {
            //Tries to select data from user with the same username or email
            string sql = @"SELECT * FROM `users` WHERE `username`='" 
                        + usernameInput + @"' OR `email`='"+ emailInput + @"';";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            //Execute query
            Connection.reader = Connection.command.ExecuteReader(); 
            Connection.reader.Read();

            //If user already exists in database
            if (Connection.reader.HasRows)
            {
                //If user with the same username already exists: tell the user and return
                if (Connection.reader[1].ToString() == usernameInput)
                {
                    MessageBox.Show("USERNAME IS ALREADY TAKEN");
                    Connection.reader.Close();
                    return false;
                }

                //If user with the same email already exists: tell the user adn return
                if (Connection.reader[3].ToString() == emailInput)
                {
                    MessageBox.Show("EMAIL IS ALREADY USED ONCE");
                    Connection.reader.Close();
                    return false;
                }
            }            

            Connection.reader.Close();

            //Checks if all inputs are valid
            if (!Account.IsValidUsername(usernameInput))
            {
                MessageBox.Show(
                    "USERNAME CAN'T USE SPECIAL CHARACTERS OR CAN'T BE LONGER THAN 8 CHARACTERS"
                    );
                return false;
            }

            if (!Account.IsValidEmail(emailInput))
            {
                MessageBox.Show("INVALID EMAIL ADRESS");
                return false;
            }

            if (!Account.IsValidPassword(passwordInput))
            {
                MessageBox.Show(
                    "PASSWORD HAS TO CONTAIN AT LEAST ONE CAPITAL LETTER "
                   +"AND ONE NUMBER AND CAN'T USE SPECIAL CHARACTERS"
                   );
                return false;
            }

            //Inserts user into database
            sql = @"INSERT INTO users(
                    username, password, email, firstname, lastname, frienduserIDsString)
                    VALUES ('" + (usernameInput).ToLower() + "','" + HashPassword(passwordInput)
                    + "','" + emailInput + "','" + firstNameInput + "','" 
                    + lastNameInput + "','" + "" + "')";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            //Execute query
            Connection.reader = Connection.command.ExecuteReader(); 
            Connection.reader.Close();

            //Tries to log in to the newly created account
            Login(usernameInput, HashPassword(passwordInput));

            return true;
        }

        /// <summary>
        /// Returns an encrypted version of the password input
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password)
        {
            HashAlgorithm algorithm = SHA256.Create();
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        /// <summary>
        /// Returns if character is a letter
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsLetter(char c)
        {
            //If character is a letter: return true
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        /// <summary>
        /// Returns if character is a digit
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsDigit(char c)
        {
            //If character is digit: return true
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// Tries to log in the user
        /// </summary>
        /// <param name="usernameInput"></param>
        /// <param name="passwordInput"></param>
        /// <returns></returns>
        public static bool Login(string usernameInput, string passwordInput)
        {
            //If there's no connection to the server, return false
            if (!Connection.isConnectedToTheServer)
            {
                return false;
            }

            string usernameInputLower = usernameInput.ToLower();

            //Create SQL-query (gets the users with spcific username)
            string sql = @"SELECT * FROM `users` WHERE `username`= '" + usernameInputLower + "';";
            Connection.command = new MySqlCommand(sql, Connection.connection);
            Connection.command.Parameters.Add(new MySqlParameter(usernameInput, MySqlDbType.String));
            MessageBox.Show(Connection.command.CommandText);

            //Executes the query
            Connection.reader = Connection.command.ExecuteReader(); 
            Connection.reader.Read();

            if (Connection.reader.HasRows) //Checks if the table has any rows (if there are any users found)
            {
                //Checking if username and password match a user in the table
                if (usernameInputLower == (Connection.reader[1].ToString())
                    .ToLower() && passwordInput == Connection.reader[2].ToString())
                {
                    int id = int.Parse(Connection.reader[0].ToString());

                    LoginSucceeded(id);
                    Connection.reader.Close();

                    return true;
                }
                else
                {
                    //Username or password didn't match
                    MessageBox.Show(
                        "Invalid Account! Wrong Username or Password", "Message",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //A user with that username wasn't found
                MessageBox.Show(
                    "Invalid Account! Wrong Username or Password", "Message",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Connection.reader.Close();

            return false;
        }

        /// <summary>
        /// Changes information to the information of the logged in user and saves the logged in user ID to local files
        /// </summary>
        /// <param name="userID"></param>
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
                FileStream fs = new FileStream(Application.LocalUserAppDataPath 
                                                + @"\a.ca", FileMode.Create);
                bf.Serialize(fs, userID);
                fs.Close();
            }

            //Hides the other forms
            registerForm.Hide();
            loginForm.Hide();

            mainForm.UpdateUserInformation(Account.GetAccount(userID));
            mainForm.GoHome();
        }
    }
}
