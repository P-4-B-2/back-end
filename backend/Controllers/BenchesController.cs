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
    public class BenchesController : ControllerBase
    {
        private readonly IGenericRepository<Bench> _benchRepository;
        private readonly IMapper _mapper;

        public BenchesController(IGenericRepository<Bench> benchRepository, IMapper mapper)
        {
            _benchRepository = benchRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenchDTO>>> GetBenches()
        {
            var benches = await _benchRepository.GetAll();
            if (benches == null || !benches.Any())
            {
                return NotFound();
            }
            var bench_ = _mapper.Map<IEnumerable<BenchDTO>>(benches);
            return Ok(bench_);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BenchDTO>> GetBench(int id)
        {
            var bench = await _benchRepository.GetByID(id);
            if (bench == null)
            {
                return NotFound();
            }
            var bench_ = _mapper.Map<BenchDTO>(bench);
            return Ok(bench_);
        }

        [HttpPost]
        public async Task<ActionResult<BenchDTO>> PostBench(BenchDTO benchDTO)
        {
            var bench = _mapper.Map<Bench>(benchDTO);
            await _benchRepository.Insert(bench);
            await _benchRepository.Save();

            var result_ = _mapper.Map<BenchDTO>(bench);
            return CreatedAtAction(nameof(GetBench), new { id = bench.Id }, result_);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBench(int id, BenchDTO benchDTO)
        {
            if (id != benchDTO.Id)
            {
                return BadRequest();
            }
            var bench = _mapper.Map<Bench>(benchDTO);

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
