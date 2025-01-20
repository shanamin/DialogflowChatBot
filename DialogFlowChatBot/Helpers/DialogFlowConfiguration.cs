using System.Text.Json;

namespace DialogFlowChatBot
{
    public static  class DialogFlowConfiguration
    {
        private static readonly string _credentialsPath = "./Secrets/dialogflow-credentials.json";
        public static string GetProjectId()
        {
            try
            {
                // Read the JSON file
                var json = File.ReadAllText(_credentialsPath);

                // Deserialize the JSON into a dynamic object
                var credentials = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                // Return the project_id
                if (credentials != null && credentials.TryGetValue("project_id", out var projectId))
                {
                    return projectId;
                }

                throw new Exception("project_id not found in the credentials file.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading project ID: {ex.Message}", ex);
            }
        }
    }
}
