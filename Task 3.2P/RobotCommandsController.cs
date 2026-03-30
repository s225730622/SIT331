using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace robot_controller_api.Controllers;

[ApiController]
[Route("api/robot-commands")]
public class RobotCommandsController : ControllerBase
{
    private static readonly List<RobotCommand> _commands = new List<RobotCommand>
    {
        // Add RobotCommand for every command that exists in the legacy system: LEFT, RIGHT, MOVE, PLACE, REPORT
        new RobotCommand(1,"LEFT", true, DateTime.Now, DateTime.Now, "Turn robot left 90°."),
        new RobotCommand(2,"RIGHT", true, DateTime.Now, DateTime.Now, "Turn robot right 90°."),
        new RobotCommand(3,"MOVE", true, DateTime.Now, DateTime.Now, "Move robot one step toward the direction it is facing."),
        new RobotCommand(4,"PLACE", false, DateTime.Now, DateTime.Now, "Place robot on the map at (x,y)."),
        new RobotCommand(5,"REPORT", false, DateTime.Now, DateTime.Now, "Report robots current location and direction it is facing.")
    };

    //    --- ENDPOINTS ---    
    // GET : Returns the entire contents of _commands list
    [HttpGet()]
    public IEnumerable<RobotCommand> GetAllRobotCommands() => _commands;

    // GET : Returns only the move robot commands in the list
    [HttpGet("move")]
    public IEnumerable<RobotCommand> GetMoveCommandsOnly()
    {
        // Return filtered _commands (only move commands)
        return _commands.Where(command => command.IsMoveCommand);
    }

    // GET : Returns a robot command by Id
    [HttpGet("{id}", Name = "GetRobotCommand")]   // Route utilizes 'id' & has a unique command name
    public IActionResult GetRobotCommandById(int id)
    {
        var command = _commands.Find(c => c.Id == id);
        // Check whether you have a command with a specified Id 
        if (command == null)
            // Return NotFound() ActionResult if no id found 
            return NotFound();
        else
            // If the command exists, return ActionResult Ok() and pass the command object in it as a parameter
            return Ok(command);
    }

    // POST : Add a new command
    [HttpPost()]
    public IActionResult AddRobotCommand(RobotCommand newCommand)
    {
        // Return BadRequest() if a newCommand is null
        if (newCommand == null)
            return BadRequest();
        // Return Conflict() if a newCommand's name already exists in the commands collection
        else if (_commands.Any(c => c.Name == newCommand.Name.ToUpper()))
            return Conflict();
        
        // Otherwise, continue on to let the user create a new command from newCommand
        int newId = _commands.Count + 1;
        var command = new RobotCommand(newId, newCommand.Name, newCommand.IsMoveCommand, DateTime.Now, DateTime.Now, newCommand.Description);

        // Add to _commands collection 
        _commands.Add(command);
        // Return a GET endpoint resource URI - GetRobotCommand here is a Name attribute set for the HTTP GET GetRobotCommandById method.
        return CreatedAtRoute("GetRobotCommand", new { id = command.Id }, command);
    }

    // PUT : Update an existing robot command
    [HttpPut("{id}")]
    public IActionResult UpdateRobotCommand(int id, RobotCommand updatedCommand)
    {
        // Find a command by Id
        var command = _commands.Find(c => c.Id == id);
        
        // Return NotFound() if Id doesn't exist
        if (command == null)
            return NotFound();
        // Try to modify an existing command with fields from updatedCommand
        else
        {
            // Return BadRequest() if modification fails
            if (updatedCommand == null)
                return BadRequest("Command cannot be null!");

            // Update fields by drawing from updatedCommand
            command.Name = updatedCommand.Name.ToUpper();
            command.IsMoveCommand = updatedCommand.IsMoveCommand;
            command.Description = updatedCommand.Description;
            // Set ModifiedDate of an existing command to DateTime.Now if modification is successful
            command.ModifiedDate = DateTime.Now;

            // Return with a successful HTTP status code (204)
            return NoContent();
        }
    }

    // DELETE : Delete a robot command by Id
    [HttpDelete("{id}")]
    public IActionResult DeleteRobotCommand(int id)
    {

        // Find a command by Id, return NotFound() if it doesn't exist
        var command = _commands.Find(c => c.Id == id);
        
        // Return NotFound() if Id doesn't exist
        if (command == null)
            return NotFound();
        // Otherwise, remove the command from _commands which is associated with the found Id
        else
            _commands.Remove(command);

        return NoContent();
    }
}




