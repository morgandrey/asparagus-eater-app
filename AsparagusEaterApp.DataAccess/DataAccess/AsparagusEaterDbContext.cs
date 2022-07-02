using AsparagusEaterApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace AsparagusEaterApp.DataAccess.DataAccess
{
    public partial class AsparagusEaterDbContext : DbContext
    {
        public AsparagusEaterDbContext(DbContextOptions<AsparagusEaterDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.UserId, "User_user_id_uindex")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserEatLastDate)
                    .HasColumnType("datetime")
                    .HasColumnName("user_eat_last_date");

                entity.Property(e => e.UserEmail).HasColumnName("user_email");

                entity.Property(e => e.UserName).HasColumnName("user_name");

                entity.Property(e => e.UserTimesEat).HasColumnName("user_times_eat");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
