namespace robot_controller_api;

public class RobotCommand
{
    /// Implement <see cref="RobotCommand"> here following the task sheet requirements
    public int Id { get; set; }
    string Name { get; set; }
    string? Description { get; }
    bool IsMoveCommand { get; set; }
    DateTime CreatedDate { get; set; }
    DateTime ModifiedDate { get; set; }

    public RobotCommand(
        int id, string name, bool isMoveCommand, DateTime createdDate, DateTime modifiedDate, string? description = null)
        {
            Id = id;
            Name = name;
            IsMoveCommand = isMoveCommand;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
            Description = description;
        }
}
