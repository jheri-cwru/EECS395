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
        PGPHandler pgpHandler;
        private void Execute_On_Startup(object sender, System.EventArgs e)
        {
            

            //
            using(var cred = new CredentialManagement.Credential())
            {
                cred.Username = "cwru.senior.project.395@outlook.com";
                cred.Password = "Changeme1!TEST";
                cred.Target = "CryptographyKey";
                cred.PersistanceType = CredentialManagement.PersistanceType.LocalComputer;
                cred.Type = CredentialManagement.CredentialType.Generic;
                cred.Save();
            }

            pgpHandler = new PGPHandler(new CredentialManagement.Credential { Target = "CryptographyKey" });
            pgpHandler.GenerateKeyPair();

            // Check that add-in is configured.
            

            // Register send handler to intercept & sign outgoing mail.
            Application.ItemSend += new Outlook.ApplicationEvents_11_ItemSendEventHandler(SignAndSend);
        }

        private void SignAndSend(object OutgoingEmail, ref bool Cancel)
        {
            Outlook.MailItem mailObject = OutgoingEmail as Outlook.MailItem;
            string signedMail = SignMail(mailObject);

            mailObject = GenerateHeader(mailObject, Properties.Settings.Default.HeaderIdentifier_PublicKey, pgpHandler.GetPublicKey());
            mailObject = GenerateHeader(mailObject, Properties.Settings.Default.HeaderIdentifier_SignedMessage, signedMail);
        }

        private string SignMail(Outlook.MailItem outgoingMail)
        {
            byte[] signedBytes = pgpHandler.SignContent(Encoding.UTF8.GetBytes(outgoingMail.HTMLBody));
            return Encoding.ASCII.GetString(signedBytes);

        }

        private Outlook.MailItem GenerateHeader(Outlook.MailItem outgoingMail, string headerName, string headerValue)
        {
            string PS_INTERNET_HEADERS = "http://schemas.microsoft.com/mapi/string/{00020386-0000-0000-C000-000000000046}/";
            outgoingMail.PropertyAccessor.SetProperty((PS_INTERNET_HEADERS + headerName), headerValue);

            return outgoingMail;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Execute_On_Startup);
        }
        
        #endregion
    }
}
