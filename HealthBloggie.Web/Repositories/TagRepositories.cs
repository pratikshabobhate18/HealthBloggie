using HealthBloggie.Web.Data;
using HealthBloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HealthBloggie.Web.Repositories
{
    public class TagRepositories : ITagRepositories
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepositories(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                bloggieDbContext.Tags.Remove(existingTag);
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }


        public async Task<IEnumerable<Tag>> GetAllAsync(string ? searchQuery)
        {
            var query = bloggieDbContext.Tags.AsQueryable();

            //filtering
            if(string.IsNullOrWhiteSpace(searchQuery)== false)
            {
                query = query.Where(x => x.Name.Contains(searchQuery) || 
                                         x.DisplayName.Contains(searchQuery)) ;
            }
            //sorting
            //pagination

            return await query.ToListAsync();
            //return await bloggieDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                await bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
