using Trail.Handlers;
using Trail.Helpers;
using CredentialManagement;

namespace Trail.Helpers
{
    class UserAuthentication
    {
        public void createUser(string email, string password)
        {
            // Cache Credentials in secure windows credential manager.
            CredentialManager.generateCredential(email, password, "Trail - Master Password");

            // Check that credentials were properly saved:
            Credential EchoCred = CredentialManager.getCredential("Trail - Master Password");
            if ((EchoCred.Username == email) && (EchoCred.Password == password))
            {
                // If credentials were properly saved, proceed and generate a key pair for the new user:
                CryptographyManager cryptographyHandler = (new CryptographyManager(CredentialManager.getCredential("Trail - Master Password")));
                cryptographyHandler.GenerateKeyPair();

                // Check that the key-pair properly generated:
                string pubKey = cryptographyHandler.GetPublicKey();
                if(pubKey.Contains("PGP PUBLIC KEY BLOCK"))
                {
                    // If key-pair was generated, sync user with cloud instance:
                    string passwordHash = "";

                    RESTManager RESTClient = new RESTManager();
                    RESTClient.processSignUp(email, passwordHash, pubKey);
                    RESTClient.close();
                }
            }
        }
    }
}
