using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Trail.Handlers;
using Trail.Forms;
using Trail.Helpers;

namespace Trail
{
    public partial class Trail
    {
        private CryptographyManager cryptoContext;
        private UserAuthentication authFramework = new UserAuthentication();

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            // Check that user is authenticated..
            if (!authFramework.userAuthenticated())
            {
                SignupWindow window = new SignupWindow();
                window.Show();
            }

            // Once authenticated, generate cryptography context to run in.
            cryptoContext = new CryptographyManager(CredentialManager.getCredential("Trail - Master Password"));

            // Register send handler to intercept & sign outgoing mail.
            Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(SignAndSend);
        }
        private void SignAndSend(object OutgoingEmail, ref bool Cancel)
        {
            Outlook.MailItem mailObject = OutgoingEmail as Outlook.MailItem;
            string signedMail = SignMail(mailObject);

            mailObject = GenerateHeader(mailObject, Properties.Settings.Default.HeaderIdentifier_PublicKey, cryptoContext.GetPublicKey());
            mailObject = GenerateHeader(mailObject, Properties.Settings.Default.HeaderIdentifier_SignedMessage, signedMail);
        }

        private string SignMail(Outlook.MailItem outgoingMail)
        {
            byte[] signedBytes = cryptoContext.SignContent(Encoding.UTF8.GetBytes(outgoingMail.HTMLBody));
            return Encoding.ASCII.GetString(signedBytes);

        }

        private Outlook.MailItem GenerateHeader(Outlook.MailItem outgoingMail, string headerName, string headerValue)
        {
            string PS_INTERNET_HEADERS = "http://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/";
            outgoingMail.PropertyAccessor.SetProperty((PS_INTERNET_HEADERS + headerName), headerValue);

            return outgoingMail;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see https://go.microsoft.com/fwlink/?LinkId=506785
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
