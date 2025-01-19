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
    public class BenchesController : ControllerBase
    {
        private readonly IGenericRepository<Bench> _benchRepository;

        public BenchesController(IGenericRepository<Bench> benchRepository)
        {
            _benchRepository = benchRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bench>>> GetBenches()
        {
            var benches = await _benchRepository.GetAll();
            if (benches == null)
            {
                return NotFound();
            }
            return Ok(benches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Bench>> GetBench(int id)
        {
            var bench = await _benchRepository.GetByID(id);
            if (bench == null)
            {
                return NotFound();
            }
            return Ok(bench);
        }

        [HttpPost]
        public async Task<ActionResult<Bench>> PostBench(Bench bench)
        {
            await _benchRepository.Insert(bench);
            await _benchRepository.Save();

            return CreatedAtAction(nameof(GetBench), new { id = bench.Id }, bench);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBench(int id, Bench bench)
        {
            if (id != bench.Id)
            {
                return BadRequest();
            }

            try
            {
                await _benchRepository.Update(bench);
                await _benchRepository.Save();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBench(int id)
        {
            var bench = await _benchRepository.GetByID(id);
            if (bench == null)
            {
                return NotFound();
            }

            await _benchRepository.Delete(id);
            await _benchRepository.Save();

            return NoContent();
        }
    }
}
