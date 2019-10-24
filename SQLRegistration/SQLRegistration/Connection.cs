using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLRegistration
{
    static class Connection
    {
        public static MySqlConnection connection = new MySqlConnection(GetConnectionString);

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
