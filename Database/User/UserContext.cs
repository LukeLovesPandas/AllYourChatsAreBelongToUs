using Microsoft.EntityFrameworkCore;
namespace AllYourChatsAreBelongToUs.Database.User {

    public class UserContext : DbContext {
        public UserContext(DbContextOptions<UserContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<SlackIntegrationEntity>().HasBaseType<ChatIntegrationEntity>();            
        }

        public DbSet<UserEntity> Users {get; set;}        
    }
}