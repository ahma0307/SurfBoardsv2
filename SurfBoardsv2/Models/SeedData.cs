using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using SurfBoardsv2.Models;
using SurfBoardsv2.Data;
using System.Text.Encodings.Web;
using static System.Net.WebRequestMethods;

namespace SurfBoardsv2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Boards.Any())
                {
                    return;   // DB has been seeded
                }
                context.Boards.AddRange(
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
                       ImageFileName = "",
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
                       ImageFileName = "",
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
                       ImageFileName = "",
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
                       ImageFileName = "",
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
                       ImageFileName = "",
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
                       ImageFileName = "",
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
                       ImageFileName = "",
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
                       ImageFileName = "",
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
                       ImageFileName = "",
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
                       ImageFileName = "",
                       IsAvailable = true,
                   }
                    );
                context.SaveChanges();

            }
        }
    }
}

