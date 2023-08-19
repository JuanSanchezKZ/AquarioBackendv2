using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;
using AquarioBackend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AquarioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumThreadController : ControllerBase
    {
        private readonly IForumThreadRepository threadsRepository;

        public ForumThreadController(IForumThreadRepository threadsRepository)
        {
            this.threadsRepository = threadsRepository;
        }


        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var threads = await threadsRepository.GetAllAsync();

            return Ok(threads);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var threads = await threadsRepository.GetByIdAsync(id);

            if (threads == null)
            {
                return NotFound();
            } 

            return Ok(threads);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] addForumThreadRequestDTO addForumThreadRequestDTO)
        {
            var forumThreadDomainModel = new ForumThread
            {
                Content = addForumThreadRequestDTO.Content,
                Title = addForumThreadRequestDTO.Title,
                UserId = addForumThreadRequestDTO.UserId,
                
            };

            forumThreadDomainModel = await threadsRepository.CreateAsync(forumThreadDomainModel);

            var forumThreadDto = new ForumThreadDTO
            {
                ThreadId = forumThreadDomainModel.ThreadId,
                Content = forumThreadDomainModel.Content,
                Title = forumThreadDomainModel.Title, 
                UserId = forumThreadDomainModel.UserId,
            };

            return CreatedAtAction("GetById", new { id = forumThreadDomainModel.ThreadId}, forumThreadDto);
        }
    }
}
