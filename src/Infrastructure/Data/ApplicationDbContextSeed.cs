using System;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager, ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                // add genders
                if (!context.Genders.Any())
                {
                    var gendersData = File.ReadAllText("../Infrastructure/Data/SeedData/genders.json");
                    var genders = JsonSerializer.Deserialize<List<Gender>>(gendersData);

                    foreach (var gender in genders!)
                    {
                        context.Genders.Add(gender);
                    }
                    await context.SaveChangesAsync();
                }
                // add statuses
                if (!context.Statuses.Any())
                {
                    var statusesData = File.ReadAllText("../Infrastructure/Data/SeedData/statuses.json");
                    var statuses = JsonSerializer.Deserialize<List<Status>>(statusesData);

                    foreach (var status in statuses!)
                    {
                        context.Statuses.Add(status);
                    }
                    await context.SaveChangesAsync();
                }
                // add regions
                if (!context.Regions.Any())
                {
                    var regionsData = File.ReadAllText("../Infrastructure/Data/SeedData/regions.json");
                    var regions = JsonSerializer.Deserialize<List<Region>>(regionsData);

                    foreach (var region in regions!)
                    {
                        context.Regions.Add(region);
                    }
                    await context.SaveChangesAsync();
                }
                // add cities
                if (!context.Cities.Any())
                {
                    var citiesData = File.ReadAllText("../Infrastructure/Data/SeedData/cities.json");
                    var cities = JsonSerializer.Deserialize<List<City>>(citiesData);

                    foreach (var city in cities!)
                    {
                        context.Cities.Add(city);
                    }
                    await context.SaveChangesAsync();
                }
                // add users
                if (!userManager.Users.Any())
                {
                    var usersData = File.ReadAllText("../Infrastructure/Data/SeedData/users.json");
                    var users = JsonSerializer.Deserialize<List<AppUser>>(usersData);

                    foreach (var user in users!)
                    {
                        await userManager.CreateAsync(user, "Pa$$w0rd");
                    }
                }
                // add photos
                if (!context.Photos.Any())
                {
                    var photosData = File.ReadAllText("../Infrastructure/Data/SeedData/photos.json");
                    var photos = JsonSerializer.Deserialize<List<Photo>>(photosData);

                    foreach (var photo in photos!)
                    {
                        context.Photos.Add(photo);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}

