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
    public class ProfilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProfilesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ProfilDto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfilDto>>> GetProfiles()
        {
            var profiles = await _context.Profiles.ToListAsync();
            return Ok(_mapper.Map<List<ProfilDto>>(profiles));
        }

        // GET: api/ProfilDto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProfilDto>> GetProfiles(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ProfilDto>>(profile));
        }

        // PUT: api/ProfilDto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfiles(int id, [FromBody] ProfilDto profilDto)
        {
            if (id != profilDto.Id)
            {
                return BadRequest("Portfolio-ID matcher ikke.");
            }

            //_context.Entry(profiles).State = EntityState.Modified;

            var profile = await _context.Profiles.FindAsync(id);
            if (profile == null)
            {
                return NotFound("Profil ikke fundet");
            }

            _mapper.Map(profilDto, profile);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(_mapper.Map<List<ProfilDto>>(profile));
        }

        // POST: api/ProfilDto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profiles>> PostProfiles([FromBody] ProfilDto profilDto)
        {
            if (profilDto == null)
            {
                return BadRequest("Ugyldig profildata");

            }
            var profiles = _mapper.Map<Profiles>(profilDto);

            _context.Profiles.Add(profiles);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfiles), new { id = profiles.Id }, _mapper.Map<ProfilDto>(profiles));
        }

        // DELETE: api/ProfilDto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfiles(int id)
        {
            var profiles = await _context.Profiles.FindAsync(id);
            if (profiles == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profiles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfilesExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
