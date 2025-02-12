using WebApi.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Register(builder.Configuration);

builder.Host.Register(builder.Configuration);

var app = builder.Build();

app.Register();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
