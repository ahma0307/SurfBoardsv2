using SurfBoardsv2.Core.Repositories;
using SurfBoardsv2.Data;
using SurfBoardsv2.Models;

namespace SurfBoardsv2.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public SurfBoardsv2User GetUser(string id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<SurfBoardsv2User> GetUsers()
        {
            return _context.Users.ToList();
        }

        SurfBoardsv2User IUserRepository.UpdateUser(SurfBoardsv2User user)
        {
            _context.Update(user);
            _context.SaveChanges();
            return user;
        }
    }
}
    
  
