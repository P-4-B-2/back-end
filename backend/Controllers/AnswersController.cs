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
    public class AnswersController : ControllerBase
    {
        private readonly IGenericRepository<Answer> _answerRepository;
        private readonly IMapper _mapper;

        public AnswersController(IGenericRepository<Answer> answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerDTO>>> GetAnswers()
        {
            var answers = await _answerRepository.GetAll();
            if (answers == null || !answers.Any())
            {
                return NotFound();
            }
            var answer_ = _mapper.Map<IEnumerable<AnswerDTO>>(answers);
            return Ok(answer_);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDTO>> GetAnswer(int id)
        {
            var answer = await _answerRepository.GetByID(id);
            if (answer == null)
            {
                return NotFound();
            }
            var answer_ = _mapper.Map<AnswerDTO>(answer);
            return Ok(answer_);
        }

        [HttpPost]
        public async Task<ActionResult<AnswerDTO>> PostAnswer(AnswerDTO answerDTO)
        {
            var answer = _mapper.Map<Answer>(answerDTO);
            await _answerRepository.Insert(answer);
            await _answerRepository.Save();

            var result = _mapper.Map<AnswerDTO>(answer);
            return CreatedAtAction(nameof(GetAnswer), new { id = answer.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(int id, AnswerDTO answerDTO)
        {
            if (id != answerDTO.Id)
            {
                return BadRequest();
            }
            var answer = _mapper.Map<Answer>(answerDTO);

            try
            {
                await _answerRepository.Update(answer);
                await _answerRepository.Save();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await _answerRepository.GetByID(id);
            if (answer == null)
            {
                return NotFound();
            }
            await _answerRepository.Delete(id);
            await _answerRepository.Save();
            return NoContent();
        }
    }
}
