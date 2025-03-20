using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfilTest.Data;
using ProfilTest.Models;

namespace Profiltest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Emails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emails>>> GetEmail()
        {
            return await _context.Emails.ToListAsync();
        }

        // GET: api/Emails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emails>> GetEmails(int id)
        {
            var emails = await _context.Emails.FindAsync(id);

            if (emails == null)
            {
                return NotFound();
            }

            return emails;
        }

        // PUT: api/Emails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmails(int id, Emails emails)
        {
            if (id != emails.Id)
            {
                return BadRequest();
            }

            _context.Entry(emails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Emails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emails>> PostEmails(Emails emails)
        {
            _context.Emails.Add(emails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmails", new { id = emails.Id }, emails);
        }

        // DELETE: api/Emails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmails(int id)
        {
            var emails = await _context.Emails.FindAsync(id);
            if (emails == null)
            {
                return NotFound();
            }

            _context.Emails.Remove(emails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmailsExists(int id)
        {
            return _context.Emails.Any(e => e.Id == id);
        }
    }
}