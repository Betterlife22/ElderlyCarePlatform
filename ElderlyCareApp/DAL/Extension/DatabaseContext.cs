using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Extension
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Receipt> Receipts => Set<Receipt>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<HealthRecord> HealthRecords => Set<HealthRecord>();
        public DbSet<ElderlyProfile> ElderlyProfiles => Set<ElderlyProfile>();
        public DbSet<Caregiver> Caregivers => Set<Caregiver>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Caregiver)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CaregiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Service)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Booking)
                .WithMany(b => b.Ratings)
                .HasForeignKey(r => r.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.Caregiver)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.CaregiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
