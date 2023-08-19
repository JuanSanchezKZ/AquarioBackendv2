using AquarioBackend.Models.Domain.DTO;
using AquarioBackend.Models;
using AquarioBackend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AquarioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository) { 

           this.userRepository = userRepository;
        
        }


        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var users = await userRepository.GetAllAsync();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var users = await userRepository.GetByIdAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }


        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddUserRequestDTO addUserRequestDTO)
        {
            var userDomainModel = new User
            {
                UserName = addUserRequestDTO.UserName,
                Email = addUserRequestDTO.Email,
                Password = addUserRequestDTO.Password,
            };


            userDomainModel = await userRepository.CreateAsync(userDomainModel);

            var userDto = new UserDTO
            {
                UserId = userDomainModel.UserId,
                UserName = userDomainModel.UserName,
                Email = userDomainModel.Email,
                Password = userDomainModel.Password,
                
            };


            return CreatedAtAction("GetById", new { id = userDomainModel.UserId }, userDto);
        }
    }
}
