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
    public class RolesPermisoesController : ControllerBase
    {
        private readonly BodycamContext _context;

        public RolesPermisoesController(BodycamContext context)
        {
            _context = context;
        }

        // GET: api/RolesPermisoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesPermiso>>> GetRolesPermisos()
        {
            return await _context.RolesPermisos.ToListAsync();
        }

        // GET: api/RolesPermisoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolesPermiso>> GetRolesPermiso(int id)
        {
            var rolesPermiso = await _context.RolesPermisos.FindAsync(id);

            if (rolesPermiso == null)
            {
                return NotFound();
            }

            return rolesPermiso;
        }
        [HttpGet("GetPermisoByRol")]
        public async Task<ActionResult<IEnumerable<RolesPermiso>>> GetPermisoByRol(int idRol)
        {
            var rolesPermiso = await _context.RolesPermisos.Where(r=>r.IdRoles==idRol).ToListAsync();

            if (rolesPermiso == null)
            {
                return NotFound();
            }

            return rolesPermiso;
        }

        // PUT: api/RolesPermisoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolesPermiso(int id, RolesPermiso rolesPermiso)
        {
            if (id != rolesPermiso.Id)
            {
                return BadRequest();
            }

            _context.Entry(rolesPermiso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesPermisoExists(id))
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

        // POST: api/RolesPermisoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RolesPermiso>> PostRolesPermiso(RolesPermiso rolesPermiso)
        {
            _context.RolesPermisos.Add(rolesPermiso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRolesPermiso", new { id = rolesPermiso.Id }, rolesPermiso);
        }

        // DELETE: api/RolesPermisoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolesPermiso(int id)
        {
            var rolesPermiso = await _context.RolesPermisos.FindAsync(id);
            if (rolesPermiso == null)
            {
                return NotFound();
            }

            _context.RolesPermisos.Remove(rolesPermiso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RolesPermisoExists(int id)
        {
            return _context.RolesPermisos.Any(e => e.Id == id);
        }
    }
}
