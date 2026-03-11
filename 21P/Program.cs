using NSwag.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RobotDb>(opt => opt.UseInMemoryDatabase("RobotCommands"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "RobotAPI";
    config.Title = "RobotAPI v1";
    config.Version = "v1";
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "RobotAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.UseHttpsRedirection();

var commands = new[]
{
    "LEFT", "RIGHT", "PLACE", "MOVE"
};

app.MapGet("/", () => "Hello, Robot!");

app.MapGet("/robot-commands", () =>
{
    return commands;
});

app.MapGet("/robot-commands/move", () =>
{
    if (commands.Equals("MOVE"))
        return "commands";
});


app.Run();
