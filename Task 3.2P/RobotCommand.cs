namespace robot_controller_api;

public class RobotCommand
{
    // Add public properties for RobotCommand class
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsMoveCommand { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    // Create constructor to initialize property on command creation
    public RobotCommand(
        int id, string name, bool isMoveCommand, DateTime createdDate, DateTime modifiedDate, string? description = null)
    {
        // Initialize every property with parameters
        Id = id;
        Name = name;
        IsMoveCommand = isMoveCommand;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
        Description = description;
    }
}


