using System.Net;
using System.Reflection.Metadata.Ecma335;
using HealthBloggie.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthBloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpGet]
        //public IActionResult Index()
        //{
        //    return Ok("response");
        //}
        [HttpPost]
        public async Task<IActionResult> UploadAync(IFormFile file)
        {
          var imageUrl=  await imageRepository.UploadAsync(file);
            if (imageUrl == null)
            {
                return Problem("Something Went Wrong", null , (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new {link = imageUrl});
        }
       

    }
}
