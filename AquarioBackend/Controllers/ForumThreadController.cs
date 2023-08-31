using AquarioBackend.Models;
using AquarioBackend.Models.Domain.DTO;
using AquarioBackend.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AquarioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ForumThreadController : ControllerBase
    {
        private readonly IForumThreadRepository threadsRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ForumThreadController(IForumThreadRepository threadsRepository, UserManager<IdentityUser> userManager)
        {
            this.threadsRepository = threadsRepository;
            this.userManager = userManager;
        }


        [HttpGet]
        [Authorize (Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()

        {

            var threads = await threadsRepository.GetAllAsync();

            return Ok(threads);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Reader, Writer")]

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
        [Authorize(Roles = "Reader, Writer")]

        public async Task<IActionResult> Create([FromBody] addForumThreadRequestDTO addForumThreadRequestDTO)
        {

            var userId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var userName = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;


            var forumThreadDomainModel = new ForumThread
            {
                Content = addForumThreadRequestDTO.Content,
                Title = addForumThreadRequestDTO.Title,
                UserId = userId,
                UserName = userName,
                Tag = addForumThreadRequestDTO.Tag,
            };


            forumThreadDomainModel = await threadsRepository.CreateAsync(forumThreadDomainModel);

            var forumThreadDto = new ForumThreadDTO
            {
                ThreadId = forumThreadDomainModel.ThreadId,
                Content = forumThreadDomainModel.Content,
                Title = forumThreadDomainModel.Title,
                UserId = userId,
                UserName = userName,
                Tag = forumThreadDomainModel.Tag,  
            };

            return CreatedAtAction("GetById", new { id = forumThreadDomainModel.ThreadId}, forumThreadDto);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Reader, Writer")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var thread = await threadsRepository.DeleteAsync(id);

            if (thread == null)
            {
                return NotFound();
            }

            return Ok(thread);
        }


    }
}
