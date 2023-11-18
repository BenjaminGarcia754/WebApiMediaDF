using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper mapper;

        public TipoReportesController(WebApiMediaDbContex context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/TipoReportes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoReporteDTO>>> GetTiposReporte()
        {
            var reportes = await _context.TiposReporte.ToListAsync();
            return mapper.Map<List<TipoReporteDTO>>(reportes);
        }

        // GET: api/TipoReportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoReporteDTO>> GetTipoReporte(int id)
        {
            var tipoReporte = await _context.TiposReporte.FindAsync(id);

            if (tipoReporte == null)
            {
                return NotFound();
            }

            return mapper.Map<TipoReporteDTO>(tipoReporte);
        }

        // PUT: api/TipoReportes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoReporte(int id, TipoReporteDTO tipoReporteDTO)
        {
            var tipoReporte = mapper.Map<TipoReporte>(tipoReporteDTO);
            if (id != tipoReporteDTO.Id)
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
        [HttpPost]
        public async Task<ActionResult<TipoReporte>> PostTipoReporte(TipoReporteDTO tipoReporteDTO)
        {
            var tipoReporte = mapper.Map<TipoReporte>(tipoReporteDTO);
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
