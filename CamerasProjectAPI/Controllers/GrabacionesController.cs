using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CamerasProjectAPI.Models;

namespace CamerasProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrabacionesController : ControllerBase
    {
        private readonly BodycamContext _context;

        public GrabacionesController(BodycamContext context)
        {
            _context = context;
        }

        // GET: api/Grabaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grabacione>>> GetGrabaciones()
        {
            return await _context.Grabaciones.ToListAsync();
        }

        // GET: api/Grabaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grabacione>> GetGrabacione(int id)
        {
            var grabacione = await _context.Grabaciones.FindAsync(id);

            if (grabacione == null)
            {
                return NotFound();
            }

            return grabacione;
        }

        // PUT: api/Grabaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrabacione(int id, Grabacione grabacione)
        {
            if (id != grabacione.Id)
            {
                return BadRequest();
            }

            _context.Entry(grabacione).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GrabacioneExists(id))
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

        // POST: api/Grabaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Grabacione>> PostGrabacione(Grabacione grabacione)
        {
            _context.Grabaciones.Add(grabacione);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrabacione", new { id = grabacione.Id }, grabacione);
        }

        // DELETE: api/Grabaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrabacione(int id)
        {
            var grabacione = await _context.Grabaciones.FindAsync(id);
            if (grabacione == null)
            {
                return NotFound();
            }

            _context.Grabaciones.Remove(grabacione);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GrabacioneExists(int id)
        {
            return _context.Grabaciones.Any(e => e.Id == id);
        }
    }
}
