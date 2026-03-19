using Microsoft.AspNetCore.Mvc;

namespace robot_controller_api.Controllers;

[ApiController]
[Route("api/robot-commands")]
public class RobotCommandsController : ControllerBase
{
    private static readonly List<RobotCommand> _commands = new List<RobotCommand>
    {
        // commands here
        new RobotCommand(1, "LEFT", false, DateTime.Now, DateTime.Now, "Turn left"),
        new RobotCommand(2, "RIGHT", false, DateTime.Now, DateTime.Now, "Turn right"),
        new RobotCommand(3, "MOVE", true, DateTime.Now, DateTime.Now, "Move forward"),
        new RobotCommand(4, "PLACE", false, DateTime.Now, DateTime.Now, "Place robot"),
        new RobotCommand(5, "REPORT", false, DateTime.Now, DateTime.Now, "Report location"),
    };

    // Robot commands endpoints here
    [HttpGet()]
    public IEnumerable<RobotCommand> GetAllRobotCommands() => _commands;

}
