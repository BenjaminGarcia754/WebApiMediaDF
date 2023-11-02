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
    public class VideoTypesController : ControllerBase
    {
        private readonly WebApiMediaDbContex _context;

        public VideoTypesController(WebApiMediaDbContex context)
        {
            _context = context;
        }

        // GET: api/VideoTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoType>>> GetVideoTypes()
        {
            return await _context.VideoTypes.ToListAsync();
        }

        // GET: api/VideoTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoType>> GetVideoType(int id)
        {
            var videoType = await _context.VideoTypes.FindAsync(id);

            if (videoType == null)
            {
                return NotFound();
            }

            return videoType;
        }

        // PUT: api/VideoTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideoType(int id, VideoType videoType)
        {
            if (id != videoType.Id)
            {
                return BadRequest();
            }

            _context.Entry(videoType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoTypeExists(id))
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

        // POST: api/VideoTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VideoType>> PostVideoType(VideoType videoType)
        {
            _context.VideoTypes.Add(videoType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVideoType", new { id = videoType.Id }, videoType);
        }

        // DELETE: api/VideoTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideoType(int id)
        {
            var videoType = await _context.VideoTypes.FindAsync(id);
            if (videoType == null)
            {
                return NotFound();
            }

            _context.VideoTypes.Remove(videoType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoTypeExists(int id)
        {
            return _context.VideoTypes.Any(e => e.Id == id);
        }
    }
}
