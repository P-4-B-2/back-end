﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DAL.Models;
using backend.DAL.Repository;
using Microsoft.AspNetCore.Authorization;
using backend.DTOs;
using AutoMapper;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IGenericRepository<Question> _questionRepository;
        private readonly IMapper _mapper;

        public QuestionsController(IGenericRepository<Question> questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestions()
        {
            var questions = await _questionRepository.GetAll();
            if (questions == null)
            {
                return NotFound();
            }
            var questions_ = _mapper.Map<IEnumerable<QuestionDTO>>(questions);
            return Ok(questions_);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetActiveQuestions()
        {
            var activeQuestions = await _questionRepository.GetByCondition(q => q.IsActive);
            if (activeQuestions == null || !activeQuestions.Any())
            {
                return NotFound();
            }
            var activeQuestions_ = _mapper.Map<IEnumerable<QuestionDTO>>(activeQuestions);
            return Ok(activeQuestions_);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> GetQuestion(int id)
        {
            var question = await _questionRepository.GetByID(id);
            if (question == null)
            {
                return NotFound();
            }
            var question_ = _mapper.Map<QuestionDTO>(question);
            return Ok(question_);
        }

        [HttpPost]
        public async Task<ActionResult<QuestionDTO>> PostQuestion(QuestionDTO questionDTO)
        {
            var question = _mapper.Map<Question>(questionDTO);
            question.IsActive = true;

            await _questionRepository.Insert(question);
            await _questionRepository.Save();

            var result = _mapper.Map<QuestionDTO>(question);
            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, QuestionDTO questionDTO)
        {
            if (id != questionDTO.Id)
            {
                return BadRequest();
            }

            var question = _mapper.Map<Question>(questionDTO);

            try
            {
                await _questionRepository.Update(question);
                await _questionRepository.Save();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _questionRepository.GetByID(id);
            if (question == null)
            {
                return NotFound();
            }

            question.IsActive = false;

            await _questionRepository.Update(question);
            await _questionRepository.Save();

            return NoContent();
        }
    }
}
