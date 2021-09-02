using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Comment
{
    public class CommentDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }


        public CommentDbContext(DbContextOptions<CommentDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

    public class Comment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Datetime { get; set; }

        [Required]
        public int ArticleId { get; set; }
    }
}
