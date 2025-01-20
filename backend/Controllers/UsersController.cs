using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DAL.Models;
using backend.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using backend.DTOs;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _userRepository.GetAll();
            if (users == null || !users.Any())
            {
                return NotFound();
            }
            var user_ = _mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(user_);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userRepository.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }
            var user_ = _mapper.Map<UserDTO>(user);
            return Ok(user_);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            await _userRepository.Insert(user);
            await _userRepository.Save();

            var result_ = _mapper.Map<UserDTO>(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, result_);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }
            var user = _mapper.Map<User>(userDTO);

            try
            {
                await _userRepository.Update(user);
                await _userRepository.Save();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByID(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userRepository.Delete(id);
            await _userRepository.Save();
            return NoContent();
        }
    }
}
