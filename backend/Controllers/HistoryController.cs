using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DAL.Models;
using backend.DAL.Repository;

namespace EventsBotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IGenericRepository<History> _historyRepository;

        public HistoryController(IGenericRepository<History> historyRepository)
        {
            _historyRepository = historyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetHistories()
        {
            var histories = await _historyRepository.GetAll();
            if (histories == null)
            {
                return NotFound();
            }
            return Ok(histories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<History>> GetHistory(int id)
        {
            var history = await _historyRepository.GetByID(id);
            if (history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<History>> PostHistory(History history)
        {
            await _historyRepository.Insert(history);
            await _historyRepository.Save();

            return CreatedAtAction(nameof(GetHistory), new { id = history.Id }, history);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory(int id, History history)
        {
            if (id != history.Id)
            {
                return BadRequest();
            }

            try
            {
                await _historyRepository.Update(history);
                await _historyRepository.Save();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(int id)
        {
            var history = await _historyRepository.GetByID(id);
            if (history == null)
            {
                return NotFound();
            }

            await _historyRepository.Delete(id);
            await _historyRepository.Save();

            return NoContent();
        }
    }
}
