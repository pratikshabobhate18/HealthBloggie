using System.ComponentModel.DataAnnotations;
using Azure.Core;
using HealthBloggie.Web.Data;
using HealthBloggie.Web.Models.Domain;
using HealthBloggie.Web.Models.ViewModels;
using HealthBloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthBloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagController : Controller
    {
        private BloggieDbContext _bloggieDbContext;
        private readonly ITagRepositories tagRepositories;

        public AdminTagController(ITagRepositories tagRepositories) 
        {
            this.tagRepositories = tagRepositories;
        }
       
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            ValidateAddTagRequest(addTagRequest);
            if(ModelState.IsValid==false)
            {
                return View();
            }
            //mapping addtagrequest to tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepositories.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List(string? searchQuery)
        {
            ViewBag.searchQuery = searchQuery;
            //use dbcontext to read

           var tags= await tagRepositories.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid Id)
        {
            var tag = await tagRepositories.GetAsync(Id);
            if(tag!=null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
            
        }

        [HttpPost]
      public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name=editTagRequest.Name,
                DisplayName=editTagRequest.DisplayName
            };
        var updatedTag=   await tagRepositories.UpdateAsync(tag);
           if(updatedTag!=null)
            {
                //return RedirectToAction("Edit", new { id = editTagRequest.Id });

            }
            else
            {
              //  return RedirectToAction("Edit", new { id = editTagRequest.Id });

            }
            // show Error notification

            return RedirectToAction("Edit", new { id = editTagRequest.Id });


        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
           var deleteTag =await tagRepositories.DeleteAsync(editTagRequest.Id);
            if(deleteTag!=null)
            {
                return RedirectToAction("List");
            }
            else
            {

            }
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

       private void ValidateAddTagRequest(AddTagRequest request)
        {
            if (request.Name is not null && request.DisplayName is not null)
            {
                if (request.Name == request.DisplayName)
                {
                    ModelState.AddModelError("DisplayName", "Name cannot be the same as DisplayName");
                }
            }
        }

    }
}