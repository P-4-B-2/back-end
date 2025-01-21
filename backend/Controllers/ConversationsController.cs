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
    public class ConversationsController : ControllerBase
    {
        private readonly IGenericRepository<Conversation> _conversationRepository;
        private readonly IMapper _mapper;

        public ConversationsController(IGenericRepository<Conversation> conversationRepository, IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConversationDTO>>> GetConversations()
        {
            var conversations = await _conversationRepository.GetAll();
            if (conversations == null || !conversations.Any())
            {
                return NotFound();
            }
            var conversation_ = _mapper.Map<IEnumerable<ConversationDTO>>(conversations);
            return Ok(conversation_);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConversationDTO>> GetConversation(int id)
        {
            var conversation = await _conversationRepository.GetByID(id);
            if (conversation == null)
            {
                return NotFound();
            }
            var conversation_ = _mapper.Map<ConversationDTO>(conversation);
            return Ok(conversation_);
        }

        [HttpPost]
        public async Task<ActionResult<ConversationDTO>> PostConversation(ConversationDTO conversationDTO)
        {
            var conversation = _mapper.Map<Conversation>(conversationDTO);
            await _conversationRepository.Insert(conversation);
            await _conversationRepository.Save();

            var result_ = _mapper.Map<ConversationDTO>(conversation);
            return CreatedAtAction(nameof(GetConversation), new { id = conversation.Id }, result_);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConversation(int id, ConversationDTO conversationDTO)
        {
            if (id != conversationDTO.Id)
            {
                return BadRequest();
            }
            var conversation = _mapper.Map<Conversation>(conversationDTO);

            try
            {
                await _conversationRepository.Update(conversation);
                await _conversationRepository.Save();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConversation(int id)
        {
            var conversation = await _conversationRepository.GetByID(id);
            if (conversation == null)
            {
                return NotFound();
            }
            await _conversationRepository.Delete(id);
            await _conversationRepository.Save();
            return NoContent();
        }
    }
}
