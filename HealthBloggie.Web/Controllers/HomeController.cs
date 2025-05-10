using System.Diagnostics;
using HealthBloggie.Web.Models;
using HealthBloggie.Web.Models.ViewModels;
using HealthBloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthBloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ITagRepositories tagRepositories;

        public HomeController(ILogger<HomeController> logger , IBlogPostRepository blogPostRepository,ITagRepositories tagRepositories)
        {
            _logger = logger;
                this.blogPostRepository = blogPostRepository;
            this.tagRepositories = tagRepositories;
        }

        public async Task<IActionResult> Index()
        {
           var blogPosts= await blogPostRepository.GetAllAsync();

           var tags= await tagRepositories.GetAllAsync();
            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tags = tags,
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
