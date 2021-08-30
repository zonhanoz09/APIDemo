using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace API.Article
{
    public class ArticleDbContext : IdentityDbContext<User>
    {
        public DbSet<Article> Articles { get; set; }



        public ArticleDbContext(DbContextOptions<ArticleDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("AspNetUsers");
        }
    }

    public class Article
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }


    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public User(string userName) : base(userName)
        {
        }
    }
}
