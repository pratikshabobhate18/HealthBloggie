using Microsoft.AspNetCore.Identity;

namespace HealthBloggie.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
