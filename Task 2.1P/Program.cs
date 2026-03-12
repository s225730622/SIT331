// All the services for the web application are configured between lines 1 and 3
using Robot.Api.Dtos;

var builder = WebApplication.CreateBuilder(args); 

var app = builder.Build();
        
// Create a list of robot commands
List<CommandDto> commands = [
    new (1, "LEFT"), 
    new (2, "RIGHT"),
    new (3, "PLACE"),
    new (4, "MOVE"),
];

// Create a robot map
List<MapDto> maps = [
    new (1, 25, 5, 8)
];

// Browser Test, "Hello, World!"   >>   GET /
app.MapGet("/", () => "Hello World!");

// Retrieve all robot commands   >>   GET /robot-commands
app.MapGet("/robot-commands", () => commands);

// Retrieve all commands that move the robot   >>   GET /robot-commands/move
app.MapGet("/robot-commands/move", () => commands.Where(command => command.Command == "MOVE"));

// Retrieve a robot command by ID   >>   GET /robot-commands/{id}
app.MapGet("/robot-commands/{id}", (int id) => commands.Find(command => command.CommandId == id))
    .WithName("GetCommand");

// Add a new command   >>   POST /robot-commands
app.MapPost("/robot-commands", (CreateCommandDto newCommand) =>
{
    CommandDto command = new(
        commands.Count + 1,
        newCommand.Command
    );
    commands.Add(command);

    return Results.CreatedAtRoute("GetCommand", new {id = command.CommandId}, command);
});

// Update an existing robot command   >>   PUT /robot-commands/{id}
app.MapPut("/robot-commands/{id}", (int id, UpdateCommandDto updatedCommand) =>
{
    var index = commands.FindIndex(command => command.CommandId == id);

    commands[index] = new CommandDto(
        id,
        updatedCommand.Command
    );

    return Results.NoContent();
});

// Retrieve a robot map   >>   GET /robot-map
app.MapGet("/robot-map", () => maps);    


// Retrieve a coordinate in the form of x-y (e.g. 0-0, 3-5), return True if coordinate belongs to map and false otherwise   >>   GET /robot-map/{coordinate}
app.MapGet("/robot-map/{coordinate}", (string coordinate) => maps.Any(map => map.Coordinate == coordinate));

// Update an existing map with a new size   >>   PUT /robot-map
app.MapPut("/robot-map", (UpdateMapDto updatedMap) => 
{
    var current = maps[0];

    maps[0] = new MapDto(
        current.MapId,
        updatedMap.MapSize,
        current.XVal,
        current.YVal
    );

    return Results.NoContent();
});

app.Run();
