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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IGenericRepository<Status> _statusRepository;
        private readonly IMapper _mapper;

        public StatusController(IGenericRepository<Status> statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDTO>>> GetStatuses()
        {
            var statuses = await _statusRepository.GetAll();
            if (statuses == null || !statuses.Any())
            {
                return NotFound();
            }
            var status_ = _mapper.Map<IEnumerable<StatusDTO>>(statuses);
            return Ok(status_);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDTO>> GetStatus(int id)
        {
            var status = await _statusRepository.GetByID(id);
            if (status == null)
            {
                return NotFound();
            }
            var status_ = _mapper.Map<StatusDTO>(status);
            return Ok(status_);
        }

        [HttpPost]
        public async Task<ActionResult<StatusDTO>> PostStatus(StatusDTO statusDTO)
        {
            var status = _mapper.Map<Status>(statusDTO);
            await _statusRepository.Insert(status);
            await _statusRepository.Save();

            var result_ = _mapper.Map<StatusDTO>(status);
            return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, result_);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, StatusDTO statusDTO)
        {
            if (id != statusDTO.Id)
            {
                return BadRequest();
            }
            var status = _mapper.Map<Status>(statusDTO);

            try
            {
                await _statusRepository.Update(status);
                await _statusRepository.Save();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _statusRepository.GetByID(id);
            if (status == null)
            {
                return NotFound();
            }
            await _statusRepository.Delete(id);
            await _statusRepository.Save();
            return NoContent();
        }
    }
}
