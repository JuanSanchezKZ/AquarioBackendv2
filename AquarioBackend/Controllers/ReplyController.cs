using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;
using AquarioBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [Authorize(Roles = "Reader, Writer")]

        public async Task<IActionResult> GetAll()
        {
            var replies = await replyRepository.GetAllAsync();

            return Ok(replies);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Reader, Writer")]

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
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> Create([FromBody] AddReplyRequestDTO addReplyRequestDTO)
        {

            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var userName = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            var replyDomainModel = new Reply
            {
                Content = addReplyRequestDTO.Content,
                ForumThreadId = addReplyRequestDTO.ForumThreadId,
                UserId = userId,
                UserName = userName,
            };

            replyDomainModel = await replyRepository.CreateAsync(replyDomainModel);

            var replyDto = new ReplyDTO
            {
                Id = replyDomainModel.Id,
                Content = addReplyRequestDTO.Content,
                ForumThreadId = replyDomainModel.ForumThreadId,
                UserId = userId,
                UserName = userName,

            };


            return CreatedAtAction("GetById", new { id = replyDomainModel.Id }, replyDto);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Reader, Writer")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var reply = await replyRepository.DeleteAsync(id);

            if (reply == null)
            {
                return NotFound();
            }

            return Ok(reply);
        }


    }
}
