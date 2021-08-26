using Microsoft.EntityFrameworkCore;
using Planning_Poker.Models;

namespace Planning_Poker.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Letters> Letters { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<Votes> Votes { get; set; }
    }
}

