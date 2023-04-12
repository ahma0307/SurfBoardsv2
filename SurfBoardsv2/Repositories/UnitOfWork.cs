using SurfBoardsv2.Core.Repositories;
using SurfBoardsv2.Models;

namespace SurfBoardsv2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get;}

        public IRoleRepository Role { get;}

        public UnitOfWork(IUserRepository user, IRoleRepository role)
        {
            User = user;
            Role = role;
        }

        public Task IsInRoleAsync(SurfBoardsv2User currentUser, string v)
        {
            throw new NotImplementedException();
        }
    } 
}
