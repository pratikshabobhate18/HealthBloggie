using HealthBloggie.Web.Models.Domain;

namespace HealthBloggie.Web.Repositories
{
    public interface IBlogPostLikeRepositories
    {
        Task<int> GetTotalLikes(Guid blogPostId);

        Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId);

        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
    }
}
