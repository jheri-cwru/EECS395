using Trail.Handlers;
using CredentialManagement;

namespace Trail.Helpers
{
    class UserAuthentication
    {
        public bool userAuthenticated()
        {
            Credential EchoCred = CredentialManager.getCredential("Trail - Master Password");

            if(!string.IsNullOrEmpty(EchoCred.Username) && !string.IsNullOrEmpty(EchoCred.Password))
            {
                CryptographyManager cryptographyHandler = (new CryptographyManager(EchoCred));
                string pubKey = cryptographyHandler.GetPublicKey();

                if (pubKey.Contains("PGP PUBLIC KEY BLOCK"))
                {
                    return true;
                }
            }
            return false;
        }

        public void createUser(string email, string password)
        {
            // Cache Credentials in secure windows credential manager.
            Credential newCred = CredentialManager.generateCredential(email, password, "Trail - Master Password");

            // Generate a key pair for the new user
            CryptographyManager cryptographyHandler = (new CryptographyManager(newCred));
            cryptographyHandler.GenerateKeyPair();

            // Check that credentials were properly saved & key-pair was generated:
            if (userAuthenticated())
            {
                // If user was properly created, sync user with cloud instance:
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

                RESTManager RESTClient = new RESTManager();
                RESTClient.processSignUp(email, passwordHash, cryptographyHandler.GetPublicKey());
                RESTClient.close();
            }
        }
    }
}
