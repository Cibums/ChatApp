using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace SQLRegistration
{
    public class Controller
    {
        //Variables
        MySqlConnection con = new MySqlConnection();
        MySqlCommand command = new MySqlCommand();
        MySqlDataReader reader;
        Connection connection = new Connection();
        public bool isConnectedToServer = true;

        public static Controller controller;

        //Forms
        public Form1 loginForm;
        public RegisterForm registerForm;
        public MainForm mainForm;

        public void Start()
        {
            if (controller == null)
            {
                controller = this;
            }

            con = new MySqlConnection(connection.GetConnectionString);

            //Trying to open the connection
            try
            {
                con.Open();
            }
            catch
            {
                isConnectedToServer = false;

                MessageBox.Show("Not connected to the server! Follow readme.txt", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                

            }

        }

        public bool Register(string usernameInput, string passwordInput, string emailInput, string firstNameInput, string lastNameInput)
        {
            try
            {
                reader.Close();
            }
            catch { }

            //Registers user



            //
            String sql = @"SELECT * FROM `users` WHERE `username`='"+ usernameInput + @"' OR `email`='"+ emailInput + @"';";
            command = new MySqlCommand(sql, con);
            reader = command.ExecuteReader(); //Execute query
            reader.Read();

            if (reader.HasRows)
            {
                if (reader[1].ToString() == usernameInput)
                {
                    MessageBox.Show("USERNAME IS ALREADY TAKEN");
                    return false;
                }

                if (reader[3].ToString() == emailInput)
                {
                    MessageBox.Show("EMAIL IS ALREADY USED ONCE");
                    return false;
                }
            }            

            reader.Close();

            if (!IsValidUsername(usernameInput))
            {
                MessageBox.Show("USERNAME CAN'T USE SPECIAL CHARACTERS OR CAN'T BE LONGER THAN 8 CHARACTERS");
                return false;
            }

            if (!IsValidEmail(emailInput))
            {
                MessageBox.Show("INVALID EMAIL ADRESS");
                return false;
            }

            if (!IsValidPassword(passwordInput))
            {
                MessageBox.Show("PASSWORD HAS TO CONTAIN AT LEAST ONE CAPITAL LETTER AND ONE NUMBER AND CAN'T USE SPECIAL CHARACTERS");
                return false;
            }

            //Create a SQL-query that inserts user information
            sql = @"INSERT INTO users(username, password, email, firstname, lastname, frienduserIDsString) VALUES ('" + (usernameInput).ToLower() + "','" + HashPassword(passwordInput) + "','" + emailInput + "','" + firstNameInput + "','" + lastNameInput + "','" + "" + "')";
            command = new MySqlCommand(sql, con);
            reader = command.ExecuteReader(); //Execute query
            reader.Close();

            Login(usernameInput, HashPassword(passwordInput));

            return true;
        }

        public string HashPassword(string password)
        {
            HashAlgorithm algorithm = SHA256.Create();
            byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public bool IsValidUsername(string username)
        {
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(username))
            {
                return false;
            }
            Func<string, int, bool> isTooLong = (text, length) => text.Length > length;

            return !isTooLong(username, 8);
        }

        public bool IsValidPassword(string password)
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
                if (IsLetter(c))
                {
                    isLetter = true;
                }

                if (IsDigit(c))
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

        bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public bool Login(string usernameInput, string passwordInput)
        {
            //If there's no connection to the server, return false
            if (!isConnectedToServer)
            {
                return false;
            }

            //Create SQL-query (gets the users with spcific username)
            String sql = @"SELECT * FROM `users` WHERE `username`='" + usernameInput + @"'";
            command = new MySqlCommand(sql, con);
            reader = command.ExecuteReader(); //Executes the query
            reader.Read();
            

            if (reader.HasRows) //Checks if the table has any rows (if there are any users created)
            {
                //Checking if username and password match a user in the table
                if (usernameInput == (reader[1].ToString()).ToLower() && passwordInput == reader[2].ToString())
                {
                    //Login Succeeded
                    LoginSucceeded(Int32.Parse(reader[0].ToString()));
                    reader.Close();

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

            reader.Close();

            return false;
        }

        public bool IsValidEmail(string email)
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

        public int GetSavedAccountID()
        {
            //Checks if there is a saved account
            if (File.Exists(Application.LocalUserAppDataPath + @"\a.ca"))
            {
                //Returns the saved account ID
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Application.LocalUserAppDataPath + @"\a.ca", FileMode.Open);
                int acc = (int)bf.Deserialize(fs);
                return acc;
            }

            //Didn't find a saved account
            //returns an impossible ID (-1)
            return -1;
        }

        public List<Conversation> GetConversations(int userID)
        {
            //Creates SQL-query, selectes logged in user's id
            String sql = @"SELECT * FROM `conversations`;";
            command = new MySqlCommand(sql, con);
            reader = command.ExecuteReader(); //Executes the query

            List<Conversation> conversations = new List<Conversation>();

            while (reader.Read())
            {
                Conversation conv = new Conversation();
                conv.conversationName = reader[1].ToString();

                List<int> usersIDs = new List<int>();

                //Splits the users' ids into int list
                string[] operands = Regex.Split(reader[2].ToString(), @"\s+");

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

            reader.Close();
            return conversations;


        }

        public int GetConversationID(string conversationName)
        {
            //Creates SQL-query, selectes conversation with name
            String sql = @"SELECT * FROM `conversations` WHERE `name`='"+conversationName+"';";
            command = new MySqlCommand(sql, con);
            reader = command.ExecuteReader(); //Executes the query
            reader.Read();

            int id = Int32.Parse(reader[0].ToString());

            reader.Close();

            return id;
        }

        public void LoginSucceeded(int userID)
        {
            //Sets the userID variable to the ID of the logged in user
            mainForm.userID = userID;
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
        }

        public List<int> GetFriends(int userID)
        {
            //Creates SQL-query, selectes logged in user's id
            String sql = @"SELECT `frienduserIDsString` FROM `users` WHERE `ID`='" + userID + @"'";
            command = new MySqlCommand(sql, con);
            reader = command.ExecuteReader(); //Executes the query
            reader.Read();

            //Checks if the user has no friends
            if (reader[0].ToString() == "")
            {
                reader.Close();
                return null;
            }

            //Splits the user's friends' ids into int list
            string[] operands = Regex.Split(reader[0].ToString(), @"\s+");

            List<int> returnValue = new List<int>();

            foreach (string operand in operands)
            {
                string index = operand.Replace(" ","");
                try
                {
                    returnValue.Add(Int32.Parse(index));
                }
                catch { }
                
            }


            reader.Close();

            return returnValue;
        }

        public Account GetAccount(int ID)
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
            command = new MySqlCommand(sql, con);

            try
            {
                reader.Close();
            }
            catch { }

            reader = command.ExecuteReader(); //Executes the query
            reader.Read();

            if (reader.HasRows) //Checks if the table has any rows (if there are any users with ID)
            {
                //Setting variables
                username = reader[1].ToString();
                password = reader[2].ToString();
                email = reader[3].ToString();
                firstname = reader[4].ToString();
                lastname = reader[5].ToString();
                friendListString = reader[6].ToString();

                //Creating account instance
                Account acc = new Account();
                acc.username = username;
                acc.password = password;
                acc.email = email;
                acc.firstname = firstname;
                acc.lastname = lastname;
                acc.friendsUserIDs = friendListString;

                reader.Close();

                //Returning account
                return acc;
            }

            reader.Close();

            //Didn't find an account
            return null;


        }
    }
}
