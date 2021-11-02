namespace SpaceShips.Data
{
    using Microsoft.EntityFrameworkCore;
    using SpaceShips.Data.Models;

    public class SpaceShipsDbContext : DbContext
    {
        public DbSet<User> Users { get; init; }

        public DbSet<SpaceShip> SpaceShips { get; init; }
        
        public DbSet<SpaceTransferFee> SpaceTransferFees { get; init; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                     .UseSqlServer(@"Server=DESKTOP-67I1RCM\DEV;Database=SpaceCorporation;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<QuestionUser>().HasKey(qs => new { qs.UserId, qs.QuestionId });          

        }
    }
}
