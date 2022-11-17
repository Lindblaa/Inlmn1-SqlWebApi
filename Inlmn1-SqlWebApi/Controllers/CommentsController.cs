using Inlmn1_SqlWebApi.Data;
using Inlmn1_SqlWebApi.Models.Entities;
using Inlmn1_SqlWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Inlmn1_SqlWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentsController(DataContext context)
        {
            _context = context;
        }



        [HttpPost]
        public async Task<IActionResult> Create(CommentRequest req)
        {
            try
            {
                var datetime = DateTime.Now;
                var commentEntity = new CommentEntity
                {
                    Message = req.Message,
                    Created = datetime,
                    IssueId = req.IssueId,
                    UserId = req.UserId
                };
                _context.Add(commentEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(commentEntity);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = new List<CommentResponse>();
                foreach (var comment in await _context.Comments.ToListAsync())
                    comments.Add(new CommentResponse { Id = comment.Id, Message = comment.Message, Created = comment.Created, UserId = comment.UserId });

                return new OkObjectResult(comments);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            try
            {
                var comments = new List<CommentResponse>();
                foreach (var comment in await _context.Comments.ToListAsync())
                    if (comment.IssueId == id)
                    {
                        comments.Add(new CommentResponse
                        {
                            Id = comment.Id,
                            Message = comment.Message,
                            Created = comment.Created,
                        });
                    }
                return new OkObjectResult(comments);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new NotFoundResult();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CommentUpdateRequest req)
        {
            try
            {
                var _comment = await _context.Comments.FindAsync(id);
                _comment.Message = req.Message;

                _context.Entry(_comment).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
                return new OkObjectResult(new CommentResponse
                {
                    Id = comment.Id,
                    Created = comment.Created,
                    Message = comment.Message,

                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new NotFoundResult();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _commentEntity = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
                if (_commentEntity != null)
                {
                    _context.Remove(_commentEntity);
                    await _context.SaveChangesAsync();
                    return new OkResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
    }
}
