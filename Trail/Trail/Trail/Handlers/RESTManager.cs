using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Trail.Handlers
{
    class RESTManager
    {
        HttpClient client = new HttpClient();

        public RESTManager() {}

        public void processSignUp(string email, string passwordHash, string pubKey)
        {
            client.DefaultRequestHeaders.Clear();

            var signupRequest = new Dictionary<string,string>
            {
                { "Email", email },
                { "Hash", passwordHash },
                { "UUID", "" }, // Optional
                { "PubKey", pubKey }
            };

            string jsonRequest = JsonConvert.SerializeObject(signupRequest);

            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonRequest);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            System.Diagnostics.Debug.WriteLine(jsonRequest);
            HttpResponseMessage r = (client.PostAsync("http://127.0.0.1" + ":8080" + "/signup", byteContent)).Result;
            System.Diagnostics.Debug.WriteLine(r.Headers);
        }

        public void processSignIn()
        {

        }

        public void close()
        {
            client.Dispose();
        }
    }
}
