using MessangerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MessangerApi
{
   public class MessangerDbContext : DbContext
   {
      public MessangerDbContext(DbContextOptions<MessangerDbContext> options) : base(options)
      {

      }

      public DbSet<User> Users { get; set; }
      public DbSet<Message> Messages { get; set; }
   }
}
