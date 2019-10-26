using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLRegistration
{
    public static class Message<T>
    {
        public static void Send(T message, int conversationID)
        {
            if (message.GetType() == typeof(Image))
            {
                try
                {
                    Process.Start((message as Image).imageUrl);
                }
                catch
                {
                    MessageBox.Show("Not a valid Image URL");
                }
                
            }
            else
            {
                MessageBox.Show(message.ToString());
            }
            
            
        }
    }
}
