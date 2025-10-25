using Microsoft.EntityFrameworkCore;
using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets - represent tables in the database
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<AdRequest> AdRequests { get; set; }
        public DbSet<RoomAdvertisement> RoomAdvertisements { get; set; }


        /// <summary>
        /// OnModelCreating - Configure entity relationships and constraints
        /// This is where you define database schema using Fluent API
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User Entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure Hotel Entity
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.Id);

                // One User can have Many Hotels
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Don't delete hotels when user is deleted

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.HasIndex(e => e.UserId); // Index for faster queries
            });

            // Configure Room Entity
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);

                // One Hotel can have Many Rooms
                entity.HasOne(e => e.Hotel)
                    .WithMany(h => h.Rooms)
                    .HasForeignKey(e => e.HotelId)
                    .OnDelete(DeleteBehavior.Cascade); // Delete rooms when hotel is deleted

                // Unique constraint: Room number must be unique per hotel
                entity.HasIndex(e => new { e.HotelId, e.RoomNumber }).IsUnique();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // Configure RoomAdvertisement Entity
            modelBuilder.Entity<RoomAdvertisement>(entity =>
            {
                entity.HasKey(e => e.Id);

                // One AdRequest can have many RoomAdvertisements
                entity.HasOne(e => e.AdRequest)
                    .WithMany(a => a.RoomAdvertisements)
                    .HasForeignKey(e => e.AdRequestId)
                    .OnDelete(DeleteBehavior.Cascade); // keep this cascade — delete ads when AdRequest is deleted

                // One Room can have many RoomAdvertisements
                entity.HasOne(e => e.Room)
                    .WithMany(r => r.RoomAdvertisements)
                    .HasForeignKey(e => e.RoomId)
                    .OnDelete(DeleteBehavior.NoAction); // prevent cascade cycle here ✅

                entity.Property(e => e.AssignedAt).HasDefaultValueSql("GETUTCDATE()");
            });


            // Seed initial data for testing
            //SeedData(modelBuilder);
        }

        /// <summary>
        /// Seed initial data - runs once when database is created
        /// </summary>
        //private void SeedData(ModelBuilder modelBuilder)
        //{
        //    // Create a test hotel owner
        //    modelBuilder.Entity<User>().HasData(
        //        new User
        //        {
        //            Id = 1,
        //            Email = "hotelowner@test.com",
        //            PasswordHash = "AQAAAAEAACcQAAAAEDummyHashForTesting", // In production, use proper hashing
        //            Role = "HotelOwner",
        //            FullName = "John Doe",
        //            CreatedAt = DateTime.UtcNow,
        //            IsActive = true
        //        }
        //    );
        //}
    }
}