using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SurfBoardsv2.Models;

namespace SurfBoardsv2.Data
{
    public class ApplicationDbContext : IdentityDbContext<SurfBoardsv2User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SurfBoardsv2.Models.Board> Boards { get; set; }
        public DbSet<SurfBoardsv2.Models.Rent> Rents { get; set; }

 
    }

}