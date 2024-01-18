using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMediaDF.Controllers.Services;

namespace WebApiMediaDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly WebApiMediaDbContex _context;
        private readonly IMapper mapper;

        public VideosController(WebApiMediaDbContex context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Videos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetVideos()
        {
            var videos = await _context.Videos.ToListAsync();
            return mapper.Map<List<VideoDTO>>(videos);
        }

        // GET: api/Videos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoDTO>> GetVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video == null)
            {
                return NotFound();
            }

            return mapper.Map<VideoDTO>(video);
        }

        [HttpGet("/favoritos/{id}")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetFavoritos(int id)
        {
            VideoServices videoServices = new VideoServices(_context, mapper);
            var videos = await videoServices.GetFavoritos(id);
            return videos;

        }

        [HttpGet("/mas-tarde/{id}")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetMasTarde(int id)
        {
            VideoServices videoServices = new VideoServices(_context, mapper);
            var videos = await videoServices.GetMasTarde(id);
            return videos;
        }

        [HttpGet("/materia/{id}")]
        public async Task<ActionResult<IEnumerable<VideoDTO>>> GetPorMateria(int id)
        {
            VideoServices videoServices = new VideoServices(_context, mapper);
            var videos = await videoServices.GetPorMateria(id);
            return videos;
        }

        // PUT: api/Videos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideo(int id, VideoDTO videoDTO)
        {
            var video = mapper.Map<Video>(videoDTO);
            if (id != videoDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(video).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
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

        // POST: api/Videos
        [HttpPost]
        public async Task<ActionResult<Video>> PostVideo(VideoDTO videoDTO)
        {
            Console.WriteLine("video: " + videoDTO.Imagen);
            var video = mapper.Map<Video>(videoDTO);
            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVideo", new { id = video.Id }, video);
        }

        // DELETE: api/Videos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
    }
}
