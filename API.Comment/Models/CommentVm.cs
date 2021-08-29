using System;

namespace API.Article.Models
{
    public class CommentVm
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int ArticleId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
