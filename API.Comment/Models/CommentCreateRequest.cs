using System.ComponentModel.DataAnnotations;

namespace API.Article.Models
{
    public class CommentCreateRequest
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int ArticleId { get; set; }
    }
}
