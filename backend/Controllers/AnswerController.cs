using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DAL.Models;
using backend.DAL.Repository;

namespace EventsBotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IGenericRepository<Answer> _answerRepository;

        public AnswerController(IGenericRepository<Answer> answerRepository)
        {
            _answerRepository = answerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            var answers = await _answerRepository.GetAll();
            if (answers == null)
            {
                return NotFound();
            }
            return Ok(answers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(int id)
        {
            var answer = await _answerRepository.GetByID(id);
            if (answer == null)
            {
                return NotFound();
            }
            return Ok(answer);
        }

        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            await _answerRepository.Insert(answer);
            await _answerRepository.Save();

            return CreatedAtAction(nameof(GetAnswer), new { id = answer.Id }, answer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(int id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

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
