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
    public class HistoriesController : ControllerBase
    {
        private readonly IGenericRepository<History> _historyRepository;
        private readonly IMapper _mapper;

        public HistoriesController(IGenericRepository<History> historyRepository, IMapper mapper)
        {
            _historyRepository = historyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryDTO>>> GetHistories()
        {
            var histories = await _historyRepository.GetAll();
            if (histories == null || !histories.Any())
            {
                return NotFound();
            }
            var history_ = _mapper.Map<IEnumerable<HistoryDTO>>(histories);
            return Ok(history_);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryDTO>> GetHistory(int id)
        {
            var history = await _historyRepository.GetByID(id);
            if (history == null)
            {
                return NotFound();
            }
            var history_ = _mapper.Map<HistoryDTO>(history);
            return Ok(history_);
        }

        [HttpPost]
        public async Task<ActionResult<HistoryDTO>> PostHistory(HistoryDTO historyDTO)
        {
            var history = _mapper.Map<History>(historyDTO);
            await _historyRepository.Insert(history);
            await _historyRepository.Save();

            var result_ = _mapper.Map<HistoryDTO>(history);
            return CreatedAtAction(nameof(GetHistory), new { id = history.Id }, result_);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistory(int id, HistoryDTO historyDTO)
        {
            if (id != historyDTO.Id)
            {
                return BadRequest();
            }
            var history = _mapper.Map<History>(historyDTO);

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
