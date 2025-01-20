using System.Net.WebSockets;
using System.Text;

namespace DialogFlowChatBot
{
    public class WebSocketMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDialogflowService dialogflowService)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await HandleWebSocketCommunication(webSocket, dialogflowService);

            }
            else
            {
                await _next(context);
            }
        }

        private async Task HandleWebSocketCommunication(WebSocket webSocket, IDialogflowService dialogflowService)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open) 
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    var response = await dialogflowService.GetDialogflowResponse(message);

                    var responseBytes = Encoding.UTF8.GetBytes(response);

                    await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                        
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by Client", CancellationToken.None);
                }
            }
        
        }


    }
}
