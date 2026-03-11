using Microsoft.EntityFrameworkCore;

class RobotDb : DbContext
{
    public RobotDb(DbContextOptions<RobotDb> options)
        : base(options) { }
    
    public DbSet<Robot> Robots => Set<Robot>();
}
