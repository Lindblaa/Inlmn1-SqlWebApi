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
    public class IssuesController : ControllerBase
    {
        private readonly DataContext _context;

        public IssuesController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Create(IssueRequest req)
        {
            try
            {
                var datetime = DateTime.Now;
                var issueEntity = new IssueEntity
                {
                    Title = req.Title,
                    Description = req.Description,
                    UserId = req.UserId,
                    Created = datetime,
                    Updated = datetime,
                    StatusId = 1
                };
                _context.Add(issueEntity);
                await _context.SaveChangesAsync();

                return new OkObjectResult(issueEntity);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var issues = new List<IssueResponse>();
                var comments = new List<CommentResponse>();
                foreach (var issueEntity in await _context.Issues.Include(x => x.Status).Include(x => x.User).ToListAsync())
                    issues.Add(new IssueResponse
                    {
                        Id = issueEntity.Id,
                        Title = issueEntity.Title,
                        Description = issueEntity.Description,
                        Created = issueEntity.Created,
                        Updated = issueEntity.Updated,
                        Status = new StatusResponse
                        {
                            Id = issueEntity.Status.Id,
                            Status = issueEntity.Status.Status
                        },
                        User = new UserResponse
                        {
                            Id = issueEntity.User.Id,
                            FirstName = issueEntity.User.FirstName,
                            LastName = issueEntity.User.LastName,
                            Email = issueEntity.User.Email
                        }
                    });

                return new OkObjectResult(issues);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var issueEntity = await _context.Issues.Include(x => x.Status).Include(x => x.User).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
                var comments = new List<CommentResponse>();

                if (issueEntity != null)
                {
                    foreach (var comment in issueEntity.Comments)
                        comments.Add(new CommentResponse
                        {
                            Id = comment.Id,
                            Message = comment.Message,
                            Created = comment.Created,
                            UserId = comment.UserId
                        });

                    return new OkObjectResult(new IssueResponse
                    {
                        Id = issueEntity.Id,
                        Title = issueEntity.Title,
                        Description = issueEntity.Description,
                        Created = issueEntity.Created,
                        Updated = issueEntity.Updated,
                        Status = new StatusResponse
                        {
                            Id = issueEntity.Status.Id,
                            Status = issueEntity.Status.Status
                        },
                        User = new UserResponse
                        {
                            Id = issueEntity.User.Id,
                            FirstName = issueEntity.User.FirstName,
                            LastName = issueEntity.User.LastName,
                            Email = issueEntity.User.Email
                        },
                        Comments = comments
                    });
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IssueUpdateRequest issueUpdateRequest)
        {
            try
            {
                var _issueEntity = await _context.Issues.FirstOrDefaultAsync(x => x.Id == id);
                if (_issueEntity != null)
                {
                    _issueEntity.StatusId = issueUpdateRequest.StatusId;
                    _issueEntity.Updated = DateTime.Now;

                    _context.Update(_issueEntity);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _issueEntity = await _context.Issues.FirstOrDefaultAsync(x => x.Id == id);
                if (_issueEntity != null)
                {
                    _context.Remove(_issueEntity);
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
