using HealthBloggie.Web.Models.Domain;
using HealthBloggie.Web.Models.ViewModels;
using HealthBloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthBloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostController : Controller
    {
        private readonly ITagRepositories tagrepositories;
        private readonly IBlogPostRepository blogPostRepository;

        public AdminBlogPostController(ITagRepositories tagrepositories, IBlogPostRepository blogPostRepository)
        {
            this.tagrepositories = tagrepositories;
            this.blogPostRepository = blogPostRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get tags from repository

            var tags = await tagrepositories.GetAllAsync();
            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName, Value = x.Id.ToString() })
            };
            return View(model);
        }

        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            //Map view model to domain model
            var blogpost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDesc = addBlogPostRequest.ShortDesc,
                FeaturedImgUrl = addBlogPostRequest.FeaturedImgUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishDate = addBlogPostRequest.PublishDate,
                Author = addBlogPostRequest.Author,
                visible = addBlogPostRequest.visible,

            };
            //map tags from selected tags
            var selectedTags = new List<Tag>();
            foreach (var selectedTagId in addBlogPostRequest.SelectedTag)
            {
                var selectedTagAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagrepositories.GetAsync(selectedTagAsGuid);
                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            blogpost.Tags = selectedTags;
            await blogPostRepository.AddAsync(blogpost);
            return RedirectToAction("Add");
        }

    

    [HttpGet]
        public async Task<IActionResult> List()
        {
         var blogPosts= await blogPostRepository.GetAllAsync();
         return View(blogPosts);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
          var blogpost= await  blogPostRepository.GetAsync(id);
            var tagsDomainModel = await tagrepositories.GetAllAsync();
            if(blogpost!=null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogpost.Id,
                    Heading = blogpost.Heading,
                    PageTitle = blogpost.PageTitle,
                    Content = blogpost.Content,
                    Author = blogpost.Author,
                    FeaturedImgUrl = blogpost.FeaturedImgUrl,
                    UrlHandle = blogpost.UrlHandle,
                    ShortDesc = blogpost.ShortDesc,
                    PublishDate = blogpost.PublishDate,
                    visible = blogpost.visible,
                    Tags = tagsDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTag = blogpost.Tags.Select(x => x.Id.ToString()).ToArray()

                };
                return View(model);
            }
           
            return View(null);

        }
        [HttpPost]
        public async Task<IActionResult>Edit(EditBlogPostRequest editBlogPostRequest)
        {
            // map view model to domain model
            var blogpostDomainmodel = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Author = editBlogPostRequest.Author,
                ShortDesc = editBlogPostRequest.ShortDesc,
                FeaturedImgUrl = editBlogPostRequest.FeaturedImgUrl,
                PublishDate = editBlogPostRequest.PublishDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                visible = editBlogPostRequest.visible
            };
            //map tags into domain model
            var selectedTags = new List<Tag>();
            foreach(var selectedTag in editBlogPostRequest.SelectedTag)
            {
                if (Guid.TryParse(selectedTag,out var tag))
                {
                    var foundTag = await tagrepositories.GetAsync(tag);
                    if(foundTag!=null)
                    {
                        selectedTags.Add(foundTag);

                    }
                }
            }
            blogpostDomainmodel.Tags = selectedTags;
            //submit information to a repository to update
           var updatedBlog= await blogPostRepository.UpdateAsync(blogpostDomainmodel);
            if(updatedBlog!=null)
            {
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            //task to repository to delete blogpost
            var deletedBlogPost = await blogPostRepository.DeleteAsync(editBlogPostRequest.Id);
            if(deletedBlogPost!=null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id= editBlogPostRequest.Id });
        }

    }
    
}
