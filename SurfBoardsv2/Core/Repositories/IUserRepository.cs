using SurfBoardsv2.Models;

namespace SurfBoardsv2.Core.Repositories
{
        public interface IUserRepository
        {
            ICollection<SurfBoardsv2User> GetUsers();
        SurfBoardsv2User GetUser(string id);

        SurfBoardsv2User UpdateUser(SurfBoardsv2User user);

        }
}

