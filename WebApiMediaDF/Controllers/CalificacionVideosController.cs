using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMediaDF.Modelos;
using WebApiMediaDF.Modelos.DTOs;

namespace WebApiMediaDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionVideosController : ControllerBase
    {
        private readonly WebApiMediaDbContex _context;
        private readonly IMapper mapper;
        public CalificacionVideosController(WebApiMediaDbContex context)
        {
            _context = context;
        }

        // GET: api/CalificacionVideos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalificacionVideoDTO>>> GetCalificacionVideos()
        {
            var calificacionVideos = await _context.CalificacionVideos.ToListAsync();

            return mapper.Map<List<CalificacionVideoDTO>>(calificacionVideos);
        }

        // GET: api/CalificacionVideos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalificacionVideoDTO>> GetCalificacionVideo(int id)
        {
            CalificacionVideo calificacionVideo = await _context.CalificacionVideos.FindAsync(id);

            if (calificacionVideo == null)
            {
                return NotFound();
            }

            return mapper.Map<CalificacionVideoDTO>(calificacionVideo);
        }

        [HttpGet("/calificacionVideoPorUsuario")]
        public async Task<ActionResult<CalificacionVideoDTO>> GetCalificacionVideoPorUsuario(int idUsuario, int idVideo)
        {
            var calificacionVideo = await _context.CalificacionVideos.Where(x => x.UsuarioRelacionado == idUsuario && x.VideoRelacionado == idVideo).FirstOrDefaultAsync();

            if (calificacionVideo == null)
            {
                return NotFound();
            }

            return mapper.Map<CalificacionVideoDTO>(calificacionVideo);
        }

        //Obtiene el promedio de calificaciones de un video
        [HttpGet("/promedioPorVideo/{id}")]
        public async Task<ActionResult<double>> GetPromedioCalificacionVideo(int id)
        {
            var calificacionVideo = await _context.CalificacionVideos.Where(x => x.VideoRelacionado == id).ToListAsync();

            if (calificacionVideo == null)
            {
                return NotFound();
            }
            double promedio = 0;
            foreach (var item in calificacionVideo)
            {
                promedio += item.CalificacionUsuario;
            }
            promedio = promedio / calificacionVideo.Count;
            return promedio;
        }

        // PUT: api/CalificacionVideos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalificacionVideo(int id, CalificacionVideoDTO calificacionVideo)
        {
            if (id != calificacionVideo.Id)
            {
                return BadRequest();
            }
            CalificacionVideo calificacion = mapper.Map<CalificacionVideo>(calificacionVideo);
            _context.Entry(calificacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionVideoExists(id))
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

        // POST: api/CalificacionVideos
        [HttpPost]
        public async Task<ActionResult<CalificacionVideo>> PostCalificacionVideo(CalificacionVideoDTO calificacionVideo)
        {
            CalificacionVideo calificacion = mapper.Map<CalificacionVideo>(calificacionVideo);
            _context.CalificacionVideos.Add(calificacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalificacionVideo", new { id = calificacionVideo.Id }, calificacionVideo);
        }

        // DELETE: api/CalificacionVideos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalificacionVideo(int id)
        {
            var calificacionVideo = await _context.CalificacionVideos.FindAsync(id);
            if (calificacionVideo == null)
            {
                return NotFound();
            }

            _context.CalificacionVideos.Remove(calificacionVideo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalificacionVideoExists(int id)
        {
            return _context.CalificacionVideos.Any(e => e.Id == id);
        }
    }
}
