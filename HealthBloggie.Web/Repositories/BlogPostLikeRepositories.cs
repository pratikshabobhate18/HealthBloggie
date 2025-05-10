using HealthBloggie.Web.Data;
using HealthBloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HealthBloggie.Web.Repositories
{
    public class BlogPostLikeRepositories : IBlogPostLikeRepositories
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepositories(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await bloggieDbContext.BlogPostLike.AddAsync(blogPostLike);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<IEnumerable<BlogPostLike>> GetLikesForBlog(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike.Where(x => x.BlogPostId == blogPostId)
               .ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike
               .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
