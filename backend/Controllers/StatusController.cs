using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DAL.Models;
using backend.DAL.Repository;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IGenericRepository<Status> _statusRepository;

        public StatusController(IGenericRepository<Status> statusRepository)
        {
            _statusRepository = statusRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            var statuses = await _statusRepository.GetAll();
            if (statuses == null)
            {
                return NotFound();
            }
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            var status = await _statusRepository.GetByID(id);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            await _statusRepository.Insert(status);
            await _statusRepository.Save();

            return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

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
