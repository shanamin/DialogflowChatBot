using DialogFlowChatBot;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<IDialogflowService,DialogflowRestService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    
}



app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseWebSockets();


app.MapControllers();
app.UseMiddleware<WebSocketMiddleware>();


app.Run();
