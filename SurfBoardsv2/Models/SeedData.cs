using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using SurfBoardsv2.Models;
using SurfBoardsv2.Data;
using System.Text.Encodings.Web;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Identity;
using SurfBoardsv2.Core;
using MessagePack;
using SurfBoardsv2.Migrations;

namespace SurfBoardsv2.Models
{
    public static class SeedData
    {

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<SurfBoardsv2User>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(Constants.Roles.Manager))
                {
                    var managerRole = new IdentityRole(Constants.Roles.Manager);
                    await roleManager.CreateAsync(managerRole);
                }

                if (!await roleManager.RoleExistsAsync(Constants.Roles.Administrator))
                {
                    var adminRole = new IdentityRole(Constants.Roles.Administrator);
                    await roleManager.CreateAsync(adminRole);
                }

                //Check if there are any managers in the system
                var existingManagers = await userManager.GetUsersInRoleAsync(Constants.Roles.Manager);

                if (existingManagers.Count == 0)
                {
                    var defaultManager = await userManager.FindByEmailAsync("nikolajvolver@hotmail.com"); // Change to relevant email

                    if (defaultManager != null)
                    {
                        await userManager.AddToRoleAsync(defaultManager, Constants.Roles.Manager);
                    }
                }

                // If clearing the database is needed:

                context.Boards.RemoveRange(context.Boards);
                context.BoardImages.RemoveRange(context.BoardImages);
                context.Rents.RemoveRange(context.Rents);
                await context.SaveChangesAsync();



                if (await context.Boards.AnyAsync())
                {
                    return;   // DB has been seeded
                }

                await context.Boards.AddRangeAsync(
                   new Board
                   {
                       Name = "The Minilog",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       Volume = 38.8f,
                       Type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },

                   new Board
                   {
                       Name = "The Wide Glider",
                       Length = 7.1f,
                       Width = 21.75f,
                       Thickness = 2.75f,
                       Volume = 44.16f,
                       Type = "Funboard",
                       Price = 685,
                       Equipment = "",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },

                   new Board
                   {
                       Name = "The Golden Ratio",
                       Length = 6.3f,
                       Width = 21.85f,
                       Thickness = 2.9f,
                       Volume = 43.22f,
                       Type = "Funboard",
                       Price = 695,
                       Equipment = "",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },
                   new Board
                   {
                       Name = "Mahi Mahi",
                       Length = 5.4f,
                       Width = 20.75f,
                       Thickness = 2.3f,
                       Volume = 29.39f,
                       Type = "Fish",
                       Price = 645,
                       Equipment = "",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },
                   new Board
                   {
                       Name = "The Emerald Glider",
                       Length = 9.2f,
                       Width = 22.8f,
                       Thickness = 2.8f,
                       Volume = 65.4f,
                       Type = "Longboard",
                       Price = 895,
                       Equipment = "",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },
                   new Board
                   {
                       Name = "The Bomb",
                       Length = 5.5f,
                       Width = 21,
                       Thickness = 2.5f,
                       Volume = 33.7f,
                       Type = "Shortboard",
                       Price = 645,
                       Equipment = "",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },
                   new Board
                   {
                       Name = "Walden Magic",
                       Length = 9.6f,
                       Width = 19.4f,
                       Thickness = 3,
                       Volume = 80,
                       Type = "Longboard",
                       Price = 1025,
                       Equipment = "",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },
                   new Board
                   {
                       Name = "Naish One",
                       Length = 12.6f,
                       Width = 30,
                       Thickness = 6,
                       Volume = 301,
                       Type = "SUP",
                       Price = 854,
                       Equipment = "Paddle",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },
                   new Board
                   {
                       Name = "Six Tourer",
                       Length = 11.6f,
                       Width = 32,
                       Thickness = 6,
                       Volume = 270,
                       Type = "SUP",
                       Price = 611,
                       Equipment = "Fin, Paddle, Pump, Leash",
                       MainImageFileName = "",
                       IsAvailable = true,
                   },
                   new Board
                   {
                       Name = "Naish Maliko",
                       Length = 14,
                       Width = 25,
                       Thickness = 6,
                       Volume = 330,
                       Type = "SUP",
                       Price = 1304,
                       Equipment = "Fin, Paddle, Pump, Leash",
                       MainImageFileName = "",
                       IsAvailable = true,
                       
                   }
                    );

                await context.SaveChangesAsync();


                var boards = await context.Boards.ToListAsync();

                for (int i = 0; i < boards.Count(); i++)
                {
                    var board = boards[i];
                    var fileId = Guid.NewGuid();
                    // Process and save the uploaded image files
                    var fileName = board.Name + "-" + 1 + ".png".ToString(); // Generate a file name

                    // Set the directory where the images will be stored (adjust this path as per your application's requirements)
                    string imageDirectory = "wwwroot/Images/";

                    // Combine the directory and unique filename to create the full filepath
                    string filePath = Path.Combine(imageDirectory, fileName);

                    // Create a new BoardImage entity for each uploaded image file
                    var image = new BoardImage
                    {
                        FileName = fileName,
                        Extension = filePath,
                        BoardId = board.Id,
                        IsMainImage = true
                    };

                    await context.BoardImages.AddAsync(image);
                    await context.SaveChangesAsync();

                    board.MainImageId = fileId;
                    board.MainImageFileName = fileName;

                    context.Boards.Update(board);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}

