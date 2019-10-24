using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLRegistration
{
    class Connection
    {
        public string GetConnectionString
        {
            get{
                //Server Information
                String con = @"server=localhost; user id=root;password=; database=chatapp";
                return con;
            }
        }
    }
}
