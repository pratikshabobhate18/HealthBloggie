using HealthBloggie.Web.Models.Domain;

namespace HealthBloggie.Web.Repositories
{
    public interface ITagRepositories
    {
        Task<IEnumerable<Tag>>  GetAllAsync(string ? searchQuery=null);
        Task<Tag?> GetAsync(Guid id);

        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);


    }
}
