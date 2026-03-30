namespace robot_controller_api;

public class Map
{
    // Add public properties for RobotCommand class
    public int Id { get; set; }
    public int Columns { get; set; }
    public int Rows { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

    // Create constructor to initialize property on command creation
    public Map(
        int id, int columns, int rows, string name, string? description, DateTime createdDate, DateTime modifiedDate)
    {
        Id = id;
        Columns = columns;
        Rows = rows;
        Name = name;
        Description = description;
        CreatedDate = createdDate;
        ModifiedDate = modifiedDate;
    }
}


