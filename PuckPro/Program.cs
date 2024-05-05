using PuckPro;
using PuckPro.Models;

var builder = WebApplication.CreateBuilder();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<PuckDBContext>();
    startup.Configure(app, db);
}

app.Run();