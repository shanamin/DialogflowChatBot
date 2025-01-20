namespace DialogFlowChatBot
{
    public class DialogflowApiResponse
    {
        public queryResult queryResult { get; set; }
    }

    public class queryResult
    {
        public string queryText { get; set; }
        public string fulfillmentText { get; set; }
    }
}
