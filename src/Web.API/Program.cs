using Core.Interfaces;
using NLog;
using Web.API.Extensions;
using Web.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

var config = builder.Configuration;
builder.Services.ConfigureApplicationServices(config);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
var env = app.Environment;
app.ConfigureExceptionHandler(logger, env);

app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();
app.MapControllers();
await app.MigrateDatabaseAsync();
app.Run();

