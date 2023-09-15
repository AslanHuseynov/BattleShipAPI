using BattleshipAPI.Models.Game;
using BattleshipAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Company.Persistence.DB
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GameResultEntity> GameResultEntities { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
