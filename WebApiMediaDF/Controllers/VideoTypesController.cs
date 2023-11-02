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
    public class VideoTypesController : ControllerBase
    {
        private readonly WebApiMediaDbContex _context;
        private readonly IMapper mapper;

        public VideoTypesController(WebApiMediaDbContex context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/VideoTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoTypeDTO>>> GetVideoTypes()
        {
            var videoTypes = await _context.VideoTypes.ToListAsync();
            return mapper.Map<List<VideoTypeDTO>>(videoTypes);
        }

        // GET: api/VideoTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoTypeDTO>> GetVideoType(int id)
        {
            var videoType = await _context.VideoTypes.FindAsync(id);

            if (videoType == null)
            {
                return NotFound();
            }

            return mapper.Map<VideoTypeDTO>(videoType);
        }

        // PUT: api/VideoTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideoType(int id, VideoTypeDTO videoTypeDTO)
        {
            var videoType = mapper.Map<VideoType>(videoTypeDTO);
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
        public async Task<ActionResult<VideoType>> PostVideoType(VideoTypeDTO videoTypeDTO)
        {
            var videoType = mapper.Map<VideoType>(videoTypeDTO);
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
