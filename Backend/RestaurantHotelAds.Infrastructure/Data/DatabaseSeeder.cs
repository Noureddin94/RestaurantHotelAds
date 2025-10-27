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
        private static readonly DateTime SeedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Use FIXED Guids for consistent relationships
        private static readonly Guid AdminId = new Guid("11111111-1111-1111-1111-111111111111");
        private static readonly Guid HotelOwner1Id = new Guid("22222222-2222-2222-2222-222222222222");
        private static readonly Guid HotelOwner2Id = new Guid("33333333-3333-3333-3333-333333333333");
        private static readonly Guid RestaurantOwnerId = new Guid("44444444-4444-4444-4444-444444444444");
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

                // Seed Restaurants (Test Data)
                await SeedRestaurantsAsync(context, logger);

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

            // Create fixed GUIDs so relationships remain stable
            var adminId = Guid.NewGuid();
            var hotelOwner1Id = Guid.NewGuid();
            var hotelOwner2Id = Guid.NewGuid();
            var restaurantOwnerId = Guid.NewGuid();

            var users = new List<ApplicationUser>
            {
                // ADMIN USER (Fixed - Always ID 1)
                new ApplicationUser
                {
                    Id = adminId,
                    Email = "admin@restauranthotelads.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEBZp8qH9vZLq3F6xXKnZ8w==", // Password: Admin@123
                    Role = Core.Enums.UserRole.Admin,
                    FullName = "System Administrator",
                    CreatedAt = SeedDate,
                    IsActive = true
                },

                // HOTEL OWNER 1 (Fixed - Always ID 2)
                new ApplicationUser
                {
                    Id = hotelOwner1Id,
                    Email = "hotelowner@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEBZp8qH9vZLq3F6xXKnZ8w==", // Password: Hotel@123
                    Role = Core.Enums.UserRole.HotelOwner,
                    FullName = "John Hotel Owner",
                    CreatedAt = SeedDate,
                    IsActive = true
                },

                // HOTEL OWNER 2 (For testing multiple users)
                new ApplicationUser
                {
                    Id = hotelOwner2Id,
                    Email = "jane.hotel@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEBZp8qH9vZLq3F6xXKnZ8w==", // Password: Hotel@123
                    Role = Core.Enums.UserRole.HotelOwner,
                    FullName = "Jane Smith",
                    CreatedAt = SeedDate,
                    IsActive = true
                },

                // RESTAURANT OWNER (Fixed - Always ID 4)
                new ApplicationUser
                {
                    Id = restaurantOwnerId,
                    Email = "restaurant@test.com",
                    PasswordHash = "AQAAAAEAACcQAAAAEBZp8qH9vZLq3F6xXKnZ8w==", // Password: Restaurant@123
                    Role = Core.Enums.UserRole.Restaurant,
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

            // Match users using email since IDs are Guids
            var hotelOwner1 = await context.Users.FirstOrDefaultAsync(u => u.Email == "hotelowner@test.com");
            var hotelOwner2 = await context.Users.FirstOrDefaultAsync(u => u.Email == "jane.hotel@test.com");

            if (hotelOwner1 == null || hotelOwner2 == null)
            {
                logger.LogError("Hotel owners not found. Cannot seed hotels.");
                return;
            }

            var hotels = new List<Hotel>
            {
                new Hotel
                {
                    Id = Guid.NewGuid(), 
                    UserId = hotelOwner1.Id, // John Hotel Owner
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
                    Id = Guid.NewGuid(),
                    UserId = hotelOwner1.Id, // John Hotel Owner
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
                    Id = Guid.NewGuid(),
                    UserId = hotelOwner2.Id, // Jane Smith
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
        /// Seed Test Restaurants
        /// </summary>
        private static async Task SeedRestaurantsAsync(ApplicationDbContext context, ILogger logger)
        {
            if (await context.Restaurants.AnyAsync())
            {
                logger.LogInformation("Restaurants already exist. Skipping restaurant seeding.");
                return;
            }

            logger.LogInformation("Seeding restaurants...");

            var restaurantOwner = await context.Users.FirstOrDefaultAsync(u => u.Email == "restaurant@test.com");

            if (restaurantOwner == null)
            {
                logger.LogError("Restaurant owner not found. Cannot seed restaurants.");
                return;
            }

            var restaurants = new List<Restaurant>
    {
        new Restaurant
        {
            Id = Guid.NewGuid(),
            UserId = restaurantOwner.Id,
            Name = "La Piazza Italian Bistro",
            Description = "Authentic Italian cuisine with wood-fired pizzas and handmade pasta.",
            Address = "789 Sunset Boulevard, Los Angeles, CA 90028",
            ContactPhone = "+1-310-555-1000",
            ContactEmail = "info@lapiazza.com",
            IsActive = true,
            CreatedAt = SeedDate
        },
        new Restaurant
        {
            Id = Guid.NewGuid(),
            UserId = restaurantOwner.Id,
            Name = "Sushi Sakura",
            Description = "Modern Japanese restaurant offering traditional sushi and sashimi.",
            Address = "123 Cherry Blossom Way, San Francisco, CA 94109",
            ContactPhone = "+1-415-555-2000",
            ContactEmail = "contact@sushisakura.com",
            IsActive = true,
            CreatedAt = SeedDate
        }
    };

            context.Restaurants.AddRange(restaurants);
            await context.SaveChangesAsync();

            logger.LogInformation($"Seeded {restaurants.Count} restaurants");
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
            var hotels = await context.Hotels.ToListAsync();
            var rooms = new List<Room>();

            var grandPlaza = hotels.FirstOrDefault(h => h.Name.Contains("Grand Plaza"));
            var seasideResort = hotels.FirstOrDefault(h => h.Name.Contains("Seaside Resort"));
            var mountainView = hotels.FirstOrDefault(h => h.Name.Contains("Mountain View"));

            // Grand Plaza Hotel - Rooms (Hotel ID 1)
            if (grandPlaza != null)
            {
                for (int floor = 1; floor <= 5; floor++)
                {
                    for (int roomNum = 1; roomNum <= 10; roomNum++)
                    {
                        string roomNumber = $"{floor}{roomNum:D2}";
                        rooms.Add(new Room
                        {
                            Id = Guid.NewGuid(),
                            HotelId = grandPlaza.Id,
                            RoomNumber = roomNumber,
                            Floor = floor.ToString(),
                            RoomType = roomNum <= 3 ? "Suite" : roomNum <= 7 ? "Deluxe" : "Standard",
                            DisplayDeviceId = $"TV-GP-{roomNumber}",
                            IsActive = true,
                            CreatedAt = SeedDate
                        });
                    }
                }
            }
            

            // Seaside Resort - Sample Rooms (Hotel ID 2)
            if (seasideResort != null)
            {
                var seasideRoomTypes = new[] { "Ocean View", "Beach Front", "Garden View", "Pool View" };
                for (int i = 1; i <= 20; i++)
                {
                    rooms.Add(new Room
                    {
                        Id = Guid.NewGuid(),
                        HotelId = seasideResort.Id,
                        RoomNumber = $"A{i:D3}",
                        Floor = ((i - 1) / 10 + 1).ToString(),
                        RoomType = seasideRoomTypes[i % 4],
                        DisplayDeviceId = $"TV-SR-A{i:D3}",
                        IsActive = true,
                        CreatedAt = SeedDate
                    });
                }
            }

            // Mountain View Lodge - Sample Rooms (Hotel ID 3)
            if (mountainView != null)
            { 
                for (int i = 1; i <= 15; i++)
                {
                    rooms.Add(new Room
                    {
                        Id = Guid.NewGuid(),
                        HotelId = mountainView.Id,
                        RoomNumber = $"M{i:D2}",
                        Floor = ((i - 1) / 5 + 1).ToString(),
                        RoomType = i <= 5 ? "Cabin Suite" : "Standard Room",
                        DisplayDeviceId = $"TV-MV-M{i:D2}",
                        IsActive = true,
                        CreatedAt = SeedDate
                    });
                }
            }

            context.Rooms.AddRange(rooms);
            await context.SaveChangesAsync();

            logger.LogInformation($"Seeded {rooms.Count} rooms across all hotels");
        }
    }
}