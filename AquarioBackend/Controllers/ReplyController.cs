using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;
using AquarioBackend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AquarioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplyController : ControllerBase
    {

        private readonly IReplyRepository replyRepository;

        public ReplyController(IReplyRepository replyRepository)
        {
            this.replyRepository = replyRepository;
        }


        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var replies = await replyRepository.GetAllAsync();

            return Ok(replies);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var replies = await replyRepository.GetByIdAsync(id);

            if (replies == null)
            {
                return NotFound();
            }

            return Ok(replies);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddReplyRequestDTO addReplyRequestDTO)
        {
            var replyDomainModel = new Reply
            {
                Content = addReplyRequestDTO.Content,
                ForumThreadId = addReplyRequestDTO.ForumThreadId,
                UserId = addReplyRequestDTO.UserId,
            };

            replyDomainModel = await replyRepository.CreateAsync(replyDomainModel);

            var replyDto = new ReplyDTO
            {
                Id = replyDomainModel.Id,
                Content = addReplyRequestDTO.Content,
                ForumThreadId = replyDomainModel.ForumThreadId,
                UserId = replyDomainModel.UserId,

            };


            return CreatedAtAction("GetById", new { id = replyDomainModel.Id }, replyDto);
        }
    }
}
