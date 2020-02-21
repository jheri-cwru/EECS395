using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace Cryptographic_Mailer___Outlook_Desktop_Add_In
{
    public partial class CryptographicMailer
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(addPublicKey);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
        }

        private void addPublicKey(object OutgoingEmail, ref bool Cancel)
        {
            Outlook.MailItem Email = OutgoingEmail as Outlook.MailItem;

            string ps_internet_headers = "https://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/";
            
            Stack<string> custom_header_names = new Stack<string>();
            custom_header_names.Push("X-Public-Key");
            custom_header_names.Push("X-Message-Hash");

            Stack<string> custom_header_values = new Stack<string>();
            custom_header_values.Push("THISISATESTKEY");
            custom_header_values.Push("THISISATESTHASH");

            foreach(string entry in custom_header_values)
            {
                Email.PropertyAccessor.SetProperty((ps_internet_headers + custom_header_names.Pop()), custom_header_values.Pop());
            }

        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
