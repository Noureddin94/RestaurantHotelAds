using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantHotelAds.Core.Entities;


namespace RestaurantHotelAds.Infrastructure.Data
{
    /// <summary>
    /// Database Seeder - Creates initial data
    /// This runs automatically when the application starts
    /// </summary>
    public static class DatabaseSeeder
    {
        private static readonly DateTime SeedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static async Task SeedAsync(ApplicationDbContext context, ILogger logger)
        {
            try
            {
                // Ensure database is created
                await context.Database.EnsureCreatedAsync();

                // Seed Users (Admin & Test Users)
                await SeedUsersAsync(context, logger);

                // Seed Hotels (Test Data)
                await SeedHotelsAsync(context, logger);

                // Seed Rooms (Test Data)
                await SeedRoomsAsync(context, logger);

                // Save all changes
                await context.SaveChangesAsync();
                logger.LogInformation("Database seeding completed successfully!");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }

        }

        /// <summary>
        /// Seed Admin and Test Users
        /// </summary>
        private static async Task SeedUsersAsync(ApplicationDbContext context, ILogger logger)
        {
            if (await context.Users.AnyAsync())
            {
                logger.LogInformation("Users already exist. Skipping user seeding.");
                return;
            }

            logger.LogInformation("Seeding users...");

            var users = new List<User>
            {
                // ADMIN USER (Fixed - Always ID 1)
                new User
                {
                    Email = "admin@restauranthotelads.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEBZp8qH9vZLq3F6xXKnZ8w==", // Password: Admin@123
                    Role = "Admin",
                    FullName = "System Administrator",
                    CreatedAt = SeedDate,
                    IsActive = true
                },

                // HOTEL OWNER 1 (Fixed - Always ID 2)
                new User
                {
                    Email = "hotelowner@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEBZp8qH9vZLq3F6xXKnZ8w==", // Password: Hotel@123
                    Role = "HotelOwner",
                    FullName = "John Hotel Owner",
                    CreatedAt = SeedDate,
                    IsActive = true
                },

                // HOTEL OWNER 2 (For testing multiple users)
                new User
                {
                    Email = "jane.hotel@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEBZp8qH9vZLq3F6xXKnZ8w==", // Password: Hotel@123
                    Role = "HotelOwner",
                    FullName = "Jane Smith",
                    CreatedAt = SeedDate,
                    IsActive = true
                },

                // RESTAURANT OWNER (Fixed - Always ID 4)
                new User
                {
                    Email = "restaurant@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEBZp8qH9vZLq3F6xXKnZ8w==", // Password: Restaurant@123
                    Role = "Restaurant",
                    FullName = "Mike Restaurant Owner",
                    CreatedAt = SeedDate,
                    IsActive = true
                }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            logger.LogInformation($"Seeded {users.Count} users");
            logger.LogInformation("Admin Email: admin@restauranthotelads.com | Password: Admin@123");
            logger.LogInformation("Hotel Owner: hotelowner@test.com | Password: Hotel@123");
            logger.LogInformation("Restaurant: restaurant@test.com | Password: Restaurant@123");
        }

        /// <summary>
        /// Seed Test Hotels
        /// </summary>
        private static async Task SeedHotelsAsync(ApplicationDbContext context, ILogger logger)
        {
            if (await context.Hotels.AnyAsync())
            {
                logger.LogInformation("Hotels already exist. Skipping hotel seeding.");
                return;
            }

            logger.LogInformation("Seeding hotels...");

            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    UserId = 2, // John Hotel Owner
                    Name = "Grand Plaza Hotel",
                    Description = "Luxury 5-star hotel in the heart of downtown with stunning city views",
                    Address = "123 Main Street, New York, NY 10001",
                    ContactPhone = "+1-212-555-0100",
                    ContactEmail = "info@grandplaza.com",
                    TotalRooms = 150,
                    IsActive = true,
                    CreatedAt = SeedDate
                },

                new Hotel
                {
                    UserId = 2, // John Hotel Owner
                    Name = "Seaside Resort & Spa",
                    Description = "Beautiful beachfront resort with world-class spa facilities",
                    Address = "456 Ocean Drive, Miami Beach, FL 33139",
                    ContactPhone = "+1-305-555-0200",
                    ContactEmail = "contact@seasideresort.com",
                    TotalRooms = 200,
                    IsActive = true,
                    CreatedAt = SeedDate
                },

                new Hotel
                {
                    UserId = 3, // Jane Smith
                    Name = "Mountain View Lodge",
                    Description = "Cozy mountain retreat with breathtaking alpine views",
                    Address = "789 Mountain Road, Aspen, CO 81611",
                    ContactPhone = "+1-970-555-0300",
                    ContactEmail = "info@mountainview.com",
                    TotalRooms = 75,
                    IsActive = true,
                    CreatedAt = SeedDate
                }
            };

            context.Hotels.AddRange(hotels);
            await context.SaveChangesAsync();

            logger.LogInformation($"Seeded {hotels.Count} hotels");
        }

        /// <summary>
        /// Seed Test Rooms
        /// </summary>
        private static async Task SeedRoomsAsync(ApplicationDbContext context, ILogger logger)
        {
            if (await context.Rooms.AnyAsync())
            {
                logger.LogInformation("Rooms already exist. Skipping room seeding.");
                return;
            }

            logger.LogInformation("Seeding rooms...");

            var rooms = new List<Room>();

            // Grand Plaza Hotel - Rooms (Hotel ID 1)
            for (int floor = 1; floor <= 5; floor++)
            {
                for (int roomNum = 1; roomNum <= 10; roomNum++)
                {
                    string roomNumber = $"{floor}{roomNum:D2}";
                    rooms.Add(new Room
                    {
                        HotelId = 1,
                        RoomNumber = roomNumber,
                        Floor = floor.ToString(),
                        RoomType = roomNum <= 3 ? "Suite" : roomNum <= 7 ? "Deluxe" : "Standard",
                        DisplayDeviceId = $"TV-GP-{roomNumber}",
                        IsActive = true,
                        CreatedAt = SeedDate
                    });
                }
            }

            // Seaside Resort - Sample Rooms (Hotel ID 2)
            var seasideRoomTypes = new[] { "Ocean View", "Beach Front", "Garden View", "Pool View" };
            for (int i = 1; i <= 20; i++)
            {
                rooms.Add(new Room
                {
                    HotelId = 2,
                    RoomNumber = $"A{i:D3}",
                    Floor = ((i - 1) / 10 + 1).ToString(),
                    RoomType = seasideRoomTypes[i % 4],
                    DisplayDeviceId = $"TV-SR-A{i:D3}",
                    IsActive = true,
                    CreatedAt = SeedDate
                });
            }

            // Mountain View Lodge - Sample Rooms (Hotel ID 3)
            for (int i = 1; i <= 15; i++)
            {
                rooms.Add(new Room
                {
                    HotelId = 3,
                    RoomNumber = $"M{i:D2}",
                    Floor = ((i - 1) / 5 + 1).ToString(),
                    RoomType = i <= 5 ? "Cabin Suite" : "Standard Room",
                    DisplayDeviceId = $"TV-MV-M{i:D2}",
                    IsActive = true,
                    CreatedAt = SeedDate
                });
            }

            context.Rooms.AddRange(rooms);
            await context.SaveChangesAsync();

            logger.LogInformation($"Seeded {rooms.Count} rooms across all hotels");
        }
    }
}