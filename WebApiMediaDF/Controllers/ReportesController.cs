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
    public class ReportesController : ControllerBase
    {
        private readonly WebApiMediaDbContex _context;
        private readonly IMapper mapper;

        public ReportesController(WebApiMediaDbContex context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Reportes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReporteDTO>>> GetReportes()
        {
            var reportes = await _context.Reportes.ToListAsync();
            return mapper.Map<List<ReporteDTO>>(reportes);
        }

        // GET: api/Reportes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReporteDTO>> GetReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);

            if (reporte == null)
            {
                return NotFound();
            }

            return mapper.Map<ReporteDTO>(reporte);
        }

        // PUT: api/Reportes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReporte(int id, ReporteDTO reporteDTO)
        {
            var reporte = mapper.Map<Reporte>(reporteDTO);
            if (id != reporteDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(reporte).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReporteExists(id))
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

        // POST: api/Reportes
        [HttpPost]
        public async Task<ActionResult<Reporte>> PostReporte(ReporteDTO reporteDTO)
        {
            var reporte = mapper.Map<Reporte>(reporteDTO);
            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReporte", new { id = reporte.Id }, reporte);
        }

        // DELETE: api/Reportes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReporte(int id)
        {
            var reporte = await _context.Reportes.FindAsync(id);
            if (reporte == null)
            {
                return NotFound();
            }

            _context.Reportes.Remove(reporte);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReporteExists(int id)
        {
            return _context.Reportes.Any(e => e.Id == id);
        }
    }
}
