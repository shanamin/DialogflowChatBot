using Google.Apis.Auth.OAuth2;
using System;

namespace DialogFlowChatBot
{
    public class GoogleAuthHelper
    {
        public static async Task<string> GetAccessTokenAsync(string credentialsPath)
        {
            var credential = GoogleCredential.FromFile(credentialsPath)
                                              .CreateScoped("https://www.googleapis.com/auth/cloud-platform");
            var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            return token;
        }
    }

}