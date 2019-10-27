using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLRegistration
{
    public static class Message<T>
    {
        public static void Send(T message)
        {
            if (message.GetType() == typeof(Image))
            {
                Connection.command = new MySqlCommand(@"INSERT INTO messages(conversationID, userID, messageimage) VALUES ('" + Conversation.activeConversationID + "','" + Connection.loggedInUserID + "','@messageimage');", Connection.connection);
                byte[] data = ImageToByte(message as Image);
                MySqlParameter blob = new MySqlParameter("@messageimage", MySqlDbType.Blob, data.Length);
                blob.Value = data;

                Connection.command.Parameters.Add(blob);

                Connection.command.ExecuteNonQuery();

            }
            else
            {
                MessageBox.Show(message.ToString());
            }
            
            
        }

        public static byte[] ImageToByte(Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }

    
}
