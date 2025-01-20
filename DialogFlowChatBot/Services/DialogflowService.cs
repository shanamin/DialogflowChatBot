using Google.Cloud.Dialogflow.V2;
using Google.Apis.Auth.OAuth2;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DialogFlowChatBot
{
    public interface IDialogflowService
    {
        public Task<string> GetDialogflowResponse(string userMessage);
    }
    public class DialogflowRestService : IDialogflowService
    {
        private readonly string _credentialsPath = "./Secrets/dialogflow-credentials.json";
        private readonly HttpClient _httpClient;

        public DialogflowRestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetDialogflowResponse(string userMessage)
        {

            // Get the access token
            var accessToken = await GoogleAuthHelper.GetAccessTokenAsync(_credentialsPath);

            // Construct the session and URL
            var sessionId = Guid.NewGuid().ToString();
            var projectId = DialogFlowConfiguration.GetProjectId();
            var url = $"https://dialogflow.googleapis.com/v2/projects/{projectId}/agent/sessions/{sessionId}:detectIntent";

            // Prepare the request payload
            var payload = new
            {
                queryInput = new
                {
                    text = new
                    {
                        text = userMessage,
                        languageCode = "en"
                    }
                }
            };

            var jsonPayload = JsonSerializer.Serialize(payload);

            // Create the HTTP request
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };

            // Add the Authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            // Send the request
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonSerializer.Deserialize<DialogflowApiResponse>(responseContent);

                return jsonResponse?.queryResult?.fulfillmentText ?? "No response from Dialogflow.";
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Dialogflow API Error: {response.StatusCode}, {errorContent}");
            }
        }
    }

}
