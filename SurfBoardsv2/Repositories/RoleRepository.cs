using Microsoft.AspNetCore.Identity;
using SurfBoardsv2.Core.Repositories;
using SurfBoardsv2.Data;
using SurfBoardsv2.Models;

namespace SurfBoardsv2.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ICollection<IdentityRole> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
