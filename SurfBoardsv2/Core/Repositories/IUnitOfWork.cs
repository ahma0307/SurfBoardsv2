using SurfBoardsv2.Models;

namespace SurfBoardsv2.Core.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }

        IRoleRepository Role { get; }

        Task IsInRoleAsync(SurfBoardsv2User currentUser, string v);
    }
}
