using CredentialManagement;

namespace Trail.Handlers
{
    static partial class CredentialManager
    {
        public static Credential generateCredential(string username, string password, string target, PersistanceType persistance = PersistanceType.LocalComputer, CredentialType type = CredentialType.Generic)
        {
            Credential newCred = new Credential();
            newCred.Username = username;
            newCred.Password = password;
            newCred.Target = target;
            newCred.PersistanceType = persistance;
            newCred.Type = type;
            newCred.Save();

            return newCred;
        }
        
        public static Credential getCredential(string target)
        {
            return new Credential { Target = target };
        }  
    }
}
