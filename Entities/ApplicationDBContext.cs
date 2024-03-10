using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
      : base(options)
        {
        }

        public DbSet<BlogPost> Posts { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Menu> Menues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPost>().ToTable("Posts");

            //Configure Relationships
            modelBuilder.Entity<BlogPost>()
               .HasMany(c => c.Categories)
               .WithMany(c => c.Posts);
        }

    }
}
