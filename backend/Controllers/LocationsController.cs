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
    public class LocationsController : ControllerBase
    {
        private readonly IGenericRepository<Location> _locationRepository;
        private readonly IMapper _mapper;

        public LocationsController(IGenericRepository<Location> locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            var locations = await _locationRepository.GetAll();
            if (locations == null || !locations.Any())
            {
                return NotFound();
            }
            var location_ = _mapper.Map<IEnumerable<LocationDTO>>(locations);
            return Ok(location_);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id)
        {
            var location = await _locationRepository.GetByID(id);
            if (location == null)
            {
                return NotFound();
            }
            var location_ = _mapper.Map<LocationDTO>(location);
            return Ok(location_);
        }

        [HttpPost]
        public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO locationDTO)
        {
            var location = _mapper.Map<Location>(locationDTO);
            await _locationRepository.Insert(location);
            await _locationRepository.Save();

            var result_ = _mapper.Map<LocationDTO>(location);
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, result_);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationDTO locationDTO)
        {
            if (id != locationDTO.Id)
            {
                return BadRequest();
            }
            var location = _mapper.Map<Location>(locationDTO);

            try
            {
                await _locationRepository.Update(location);
                await _locationRepository.Save();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _locationRepository.GetByID(id);
            if (location == null)
            {
                return NotFound();
            }
            await _locationRepository.Delete(id);
            await _locationRepository.Save();
            return NoContent();
        }
    }
}
