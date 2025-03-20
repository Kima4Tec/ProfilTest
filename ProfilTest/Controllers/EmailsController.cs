using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfilTest.Data;
using ProfilTest.Models;
using ProfilTest.DTOs;
using AutoMapper;

namespace ProfilTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmailController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Email
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailDto>>> GetEmails()
        {
            var emails = await _context.Emails.ToListAsync();
            return Ok(_mapper.Map<List<EmailDto>>(emails));
        }

        // GET: api/Email/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailDto>> GetEmail(int id)
        {
            var email = await _context.Emails.FindAsync(id);

            if (email == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmailDto>(email));
        }

        // PUT: api/Email/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmail(int id, [FromBody] EmailDto emailDto)
        {
            if (id != emailDto.Id)
            {
                return BadRequest("Email-ID matcher ikke.");
            }

            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound("Email ikke fundet");
            }

            _mapper.Map(emailDto, email);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_mapper.Map<EmailDto>(email));
        }

        // POST: api/Email
        [HttpPost]
        public async Task<ActionResult<Emails>> PostEmail([FromBody] EmailDto emailDto)
        {
            if (emailDto == null)
            {
                return BadRequest("Ugyldig emaildata");
            }

            var email = _mapper.Map<Emails>(emailDto);

            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmail), new { id = email.Id }, _mapper.Map<EmailDto>(email));
        }

        // DELETE: api/Email/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }

            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmailExists(int id)
        {
            return _context.Emails.Any(e => e.Id == id);
        }
    }
}
