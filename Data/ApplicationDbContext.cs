using FiwFriends.Models;
using Microsoft.EntityFrameworkCore;

namespace FiwFriends.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
