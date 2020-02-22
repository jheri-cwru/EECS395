using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptographic_Mailer___Outlook_Desktop_Add_In
{
    public class PGPHandler
    {
        PgpCore.PGP pgpHandler;
        string cryptographyPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Properties.Settings.Default.DataPath + "\\Crypto";
        string username;
        string password;

        public PGPHandler(CredentialManagement.Credential credential)
        {
            pgpHandler = new PgpCore.PGP();
            username = credential.Username;
            password = credential.Password;
        }

        public string GetPublicKey()
        {
            return System.IO.File.ReadAllText(cryptographyPath + "\\PublicKey.pkr");
        }

        public void GenerateKeyPair()
        {
            pgpHandler.GenerateKey(cryptographyPath + "\\PublicKey.pkr", cryptographyPath + "\\PrivateKey.skr", username, password);
        }

        public byte[] SignContent(byte[] rawContent)
        {
            System.IO.MemoryStream signedStream = new System.IO.MemoryStream();

            pgpHandler.SignStream(new System.IO.MemoryStream(rawContent), signedStream, new System.IO.FileStream(cryptographyPath + "\\PrivateKey.skr", System.IO.FileMode.Open), password);

            return signedStream.ToArray();
        }
    }
}
