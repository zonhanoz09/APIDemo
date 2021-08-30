using API.Article.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly CommentDbContext _context;

        public CommentController(CommentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CommentVm>>> GetComments()
        {
            return await _context.Comments
                .Select(x => new CommentVm { Id = x.Id, Content = x.Content, ArticleId = x.ArticleId })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CommentVm>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            var commentVm = new CommentVm
            {
                Id = comment.Id,
                Content = comment.Content,
                ArticleId = comment.ArticleId
            };

            return commentVm;
        }

        [HttpGet("/api/comment/article/:id")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CommentVm>>> GetCommentByArticleId(int id)
        {
            var comments = await _context.Comments.Where(x => x.ArticleId == id)
                .Select(x => new CommentVm { Id = x.Id, Content = x.Content, ArticleId = x.ArticleId, DateTime = x.Datetime })
                .ToListAsync();

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        [HttpPost]
        public async Task<ActionResult<CommentVm>> PostComment(CommentCreateRequest commentCreateRequest)
        {
            var comment = new Comment
            {
                Content = commentCreateRequest.Content,
                Datetime = System.DateTime.Now,
                ArticleId = commentCreateRequest.ArticleId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, new CommentVm { Id = comment.Id, Content = comment.Content, ArticleId = comment.ArticleId, DateTime = comment.Datetime });
        }
    }
}
