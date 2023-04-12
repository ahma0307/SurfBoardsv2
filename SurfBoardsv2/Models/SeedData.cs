﻿using Microsoft.EntityFrameworkCore;
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
                // Look for any movies.
                if (context.Board.Any())
                {
                    return;   // DB has been seeded
                }
                context.Board.AddRange(
                   new Board
                   {
                       Name = "The Minilog",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                       

                   },

                   new Board
                   {
                       Name = "The Wide Glider",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                   },

                   new Board
                   {
                       Name = "The Golden Ratio",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                   },
                   new Board
                   {
                       Name = "Mahi Mahi",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                   },
                   new Board
                   {
                       Name = "The Emerald Glider",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                   },
                   new Board
                   {
                       Name = "The Bomb",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                   },
                   new Board
                   {
                       Name = "Walden Magic",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                   },
                   new Board
                   {
                       Name = "Naish One",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                   },
                   new Board
                   {
                       Name = "Six Tourer",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       
                   },
                   new Board
                   {
                       Name = "Naish Maliko",
                       Length = 6,
                       Width = 21,
                       Thickness = 2.75f,
                       volume = 2.88f,
                       type = "Shortboard",
                       Price = 565,
                       Equipment = "",
                       ImageFileName = "",
                       
                       

                   }
                    );
                context.SaveChanges();

            }
        }
    }
}
