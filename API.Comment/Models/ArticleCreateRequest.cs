using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Article.Models
{
    public class ArticleCreateRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
