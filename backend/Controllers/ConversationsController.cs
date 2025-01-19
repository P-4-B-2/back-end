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
    public class ConversationsController : ControllerBase
    {
        private readonly IGenericRepository<Conversation> _conversationRepository;

        public ConversationsController(IGenericRepository<Conversation> conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conversation>>> GetConversations()
        {
            var conversations = await _conversationRepository.GetAll();
            if (conversations == null)
            {
                return NotFound();
            }
            return Ok(conversations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Conversation>> GetConversation(int id)
        {
            var conversation = await _conversationRepository.GetByID(id);
            if (conversation == null)
            {
                return NotFound();
            }
            return Ok(conversation);
        }

        [HttpPost]
        public async Task<ActionResult<Conversation>> PostConversation(Conversation conversation)
        {
            await _conversationRepository.Insert(conversation);
            await _conversationRepository.Save();

            return CreatedAtAction(nameof(GetConversation), new { id = conversation.Id }, conversation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConversation(int id, Conversation conversation)
        {
            if (id != conversation.Id)
            {
                return BadRequest();
            }

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
