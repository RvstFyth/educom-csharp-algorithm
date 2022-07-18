using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BornToMove.DAL;

public class MoveContext : DbContext
{
    public DbSet<Move> Moves { get; set; }
    
    public DbSet<MoveRating> MoveRatings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        //        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=bornTomove_randy;Trusted_Connection=True");
        builder.UseSqlServer("Server=localhost,1433;Database=BornToMove;User ID=SA;Password=yourStrong(Password;TrustServerCertificate=true");
        builder.LogTo(Console.WriteLine, LogLevel.Information);
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Move>().HasData(
            new Move() { Id = 1, SweatRate = 3, Name = "Push up", Description = "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes"},    
            new Move() { Id = 2, SweatRate = 3, Name = "Planking", Description = "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast"},   
            new Move() { Id = 3, SweatRate = 5, Name = "Squat", Description = "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes"}    
        );
    }
}