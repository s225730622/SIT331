// All the services for the web application are configured between lines 1 and 3
using Robot.Api.Dtos;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
        
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

// Browser Test, "Hello, Robot!"   >>   GET /
app.MapGet("/", () => "Hello, Robot!");

// Retrieve all robot commands   >>   GET /robot-commands
app.MapGet("/robot-commands", () => Results.Ok(commands));

// Retrieve all commands that move the robot   >>   GET /robot-commands/move
app.MapGet("/robot-commands/move", () =>
{
    var command = commands.Where(command => command.Command == "MOVE");
    return Results.Ok(command);
});

// Retrieve a robot command by ID   >>   GET /robot-commands/{id}
app.MapGet("/robot-commands/{id}", (int id) => 
{
    var command = commands.Find(command => command.CommandId == id);
    // Return 404 not found if ID does not exist
    if (command == null)
        return Results.NotFound();
    // Return 200 OK if no errors and request succeeds
    return Results.Ok(command);
})
.WithName("GetCommand");

// Add a new command   >>   POST /robot-commands
app.MapPost("/robot-commands", (CreateCommandDto newCommand) =>
{
    CommandDto command = new(
        commands.Count + 1,
        newCommand.Command
    );

    commands.Add(command);
    // Return 201 Created Status Code if new command is successfully created
    return Results.CreatedAtRoute("GetCommand", new {id = command.CommandId}, command);
});

// Update an existing robot command   >>   PUT /robot-commands/{id}
app.MapPut("/robot-commands/{id}", (int id, UpdateCommandDto updatedCommand) =>
{
    var index = commands.FindIndex(command => command.CommandId == id);
    
    // Return 404 Not Found if index does not exist
    if (index == -1)
        return Results.NotFound();

    commands[index] = new CommandDto(
        id,
        updatedCommand.Command
    );
    // Return 204 No Content Status Code if no errors and robot command updates successfully
    return Results.Ok($"Command with ID #{index} updated to {updatedCommand.Command}");
});

// Retrieve a robot map   >>   GET /robot-map
app.MapGet("/robot-map", () => Results.Ok(maps));    

// Retrieve a coordinate in the form of x-y (e.g. 0-0, 3-5), return True if coordinate belongs to map and false otherwise   >>   GET /robot-map/{coordinate}
app.MapGet("/robot-map/{coordinate}", (string coordinate) => 
{
    bool doesBelong = maps.Any(map => map.Coordinate == coordinate);
    return Results.Ok(doesBelong);
});

// Update an existing map with a new size   >>   PUT /robot-map
app.MapPut("/robot-map", (UpdateMapDto updatedMap) => 
{
    var current = maps[0];
    // Include constraint - If new map size is less than 2 or greater than 100, display error
    if (updatedMap.MapSize < 2 || updatedMap.MapSize > 100)
        return Results.BadRequest("Map size invalid! Size must be between 2 and 100.");

    maps[0] = new MapDto(
        current.MapId,
        updatedMap.MapSize,
        current.XVal,
        current.YVal
    );
    // Return 200 OK Status Code if no errors and map size updates successfully
    return Results.Ok($"Map size updated to {updatedMap.MapSize}x{updatedMap.MapSize}");
});

app.Run();
