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
    public class StatusController : ControllerBase
    {
        private readonly DataContext _context;

        public StatusController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(StatusRequest req)
        {
            try
            {
                if (!await _context.Statuses.AnyAsync(x => x.Status == req.Status))
                {
                    var statusEntity = new StatusEntity { Status = req.Status };
                    _context.Add(statusEntity);
                    await _context.SaveChangesAsync();

                    return new OkObjectResult(statusEntity);
                }

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var statues = new List<StatusResponse>();
                foreach (var status in await _context.Statuses.ToListAsync())
                    statues.Add(new StatusResponse { Id = status.Id, Status = status.Status });

                return new OkObjectResult(statues);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new BadRequestResult();
        }
    }
}
