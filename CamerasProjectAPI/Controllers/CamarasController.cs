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
    public class CamarasController : ControllerBase
    {
        private readonly BodycamContext _context;

        public CamarasController(BodycamContext context)
        {
            _context = context;
        }

        // GET: api/Camaras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camara>>> GetCamaras()
        {
            return await _context.Camaras.Where(c => c.Estado == "activo").ToListAsync();
        }

        //// GET: api/Camaras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Camara>> GetCamara(int id)
        {
            var camara = await _context.Camaras.FindAsync(id);

            if (camara == null)
            {
                return NotFound();
            }

            return camara;
        }

        // PUT: api/Camaras/5
         //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamara(int id, Camara camara)
        {
            if (id != camara.Id)
            {
                return BadRequest();
            }

            _context.Entry(camara).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CamaraExists(id))
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

        [HttpPut("UpdateEstadoCamera")]
        public async Task<IActionResult> UpdateEstadoCamera(int id, string estado)
        {
            try
            {
                var camara = await _context.Camaras.FindAsync(id);

                camara.Estado = estado;
        
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Camaras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Camara>> PostCamara(Camara camara)
        {
            _context.Camaras.Add(camara);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCamara", new { id = camara.Id }, camara);
        }

        // DELETE: api/Camaras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamara(int id)
        {
            var camara = await _context.Camaras.FindAsync(id);
            if (camara == null)
            {
                return NotFound();
            }

            _context.Camaras.Remove(camara);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CamaraExists(int id)
        {
            return _context.Camaras.Any(e => e.Id == id);
        }
    }
}
