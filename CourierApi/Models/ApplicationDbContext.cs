using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourierApi.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /* a */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<TrackHistoryApi>()
                 .HasOne(c => c.User)
                 .WithMany(u => u.TrackHistory)
                 .HasForeignKey(c => c.UserId)
                 .IsRequired();
         } 
   
        public DbSet<TrackHistoryApi> TrackHistories { get; set; }
        public DbSet<UserApi> Userstr { get; set; }
        public DbSet<AdminLogin> AdminLogin { get; set; }

    }
}
