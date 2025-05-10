using Microsoft.AspNetCore.Mvc.Rendering;

namespace HealthBloggie.Web.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDesc { get; set; }
        public string FeaturedImgUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool visible { get; set; }
        public IEnumerable<SelectListItem> Tags { get; set; }
        //collect tg
        public string[] SelectedTag { get; set; } = Array.Empty<string>();
    }
}
