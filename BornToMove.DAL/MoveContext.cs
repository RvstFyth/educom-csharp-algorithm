using Microsoft.EntityFrameworkCore;

namespace BornToMove.DAL;

public class MoveContext : DbContext
{
    public DbSet<Move> Moves { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=localhost,1433;Database=BornToMove;User ID=SA;Password=yourStrong(Password;TrustServerCertificate=true");
        base.OnConfiguring(builder);
    }
}