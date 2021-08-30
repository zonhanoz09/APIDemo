using Microsoft.EntityFrameworkCore;
using System;

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
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Datetime { get; set; }

        public int ArticleId { get; set; }
    }
}
