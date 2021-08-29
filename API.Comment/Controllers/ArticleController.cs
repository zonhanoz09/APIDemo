using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using System.IO;
using API.Article.Models;

namespace API.Article.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleDbContext _context;

        public ArticleController(ArticleDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ArticlesVm>> GetArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            var articlesVm = new ArticlesVm
            {
                Id = article.Id,
                Name = article.Name
            };

            return articlesVm;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ArticlesVm>>> GetArticles()
        {
            var articles = await _context.Articles.Select(x =>
                new
                {
                    x.Id,
                    x.Name
                }).ToListAsync();

            var articleVms = articles.Select(x =>
                new ArticlesVm
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            return articleVms;
        }

        [HttpPost]
        public async Task<IActionResult> PostArticle([FromForm] ArticleCreateRequest articleCreateRequest)
        {
            var article = new Article
            {
                Name = articleCreateRequest.Name
            };

            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArticle), new { id = article.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, ArticleCreateRequest articleCreateRequest)
        {
            var article = await _context.Articles.FindAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            article.Name = articleCreateRequest.Name ?? article.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
