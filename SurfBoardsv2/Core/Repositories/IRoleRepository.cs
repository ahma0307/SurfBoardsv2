using Microsoft.AspNetCore.Identity;

namespace SurfBoardsv2.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
