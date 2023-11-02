using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApiMediaDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoReportesController : ControllerBase
    {
        private readonly WebApiMediaDbContex _context;

        public TipoReportesController(WebApiMediaDbContex context)
        {
            _context = context;
        }

        // GET: api/TipoReportes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoReporte>>> GetTiposReporte()
        {
            return await _context.TiposReporte.ToListAsync();
        }

        // GET: api/TipoReportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoReporte>> GetTipoReporte(int id)
        {
            var tipoReporte = await _context.TiposReporte.FindAsync(id);

            if (tipoReporte == null)
            {
                return NotFound();
            }

            return tipoReporte;
        }

        // PUT: api/TipoReportes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoReporte(int id, TipoReporte tipoReporte)
        {
            if (id != tipoReporte.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoReporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoReporteExists(id))
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

        // POST: api/TipoReportes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoReporte>> PostTipoReporte(TipoReporte tipoReporte)
        {
            _context.TiposReporte.Add(tipoReporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoReporte", new { id = tipoReporte.Id }, tipoReporte);
        }

        // DELETE: api/TipoReportes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoReporte(int id)
        {
            var tipoReporte = await _context.TiposReporte.FindAsync(id);
            if (tipoReporte == null)
            {
                return NotFound();
            }

            _context.TiposReporte.Remove(tipoReporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoReporteExists(int id)
        {
            return _context.TiposReporte.Any(e => e.Id == id);
        }
    }
}
