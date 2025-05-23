﻿using HealthBloggie.Web.Models.Domain;

namespace HealthBloggie.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }


        public IEnumerable<Tag> Tags { get; set; }
    }
}
