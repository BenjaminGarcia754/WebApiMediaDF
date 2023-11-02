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
    public class MateriasController : ControllerBase
    {
        private readonly WebApiMediaDbContex _context;
        private readonly IMapper mapper;

        public MateriasController(WebApiMediaDbContex context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Materias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaDTO>>> GetMaterias()
        {
            var materias = await _context.Materias.ToListAsync();
            return mapper.Map<List<MateriaDTO>>(materias);
        }

        // GET: api/Materias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDTO>> GetMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);

            if (materia == null)
            {
                return NotFound();
            }

            return mapper.Map<MateriaDTO>(materia);
        }

        // PUT: api/Materias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(int id, MateriaDTO materiaDTO)
        {
            var materia = mapper.Map<Materia>(materiaDTO);
            if (id != materiaDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(id))
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

        // POST: api/Materias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Materia>> PostMateria(MateriaDTO materiaDTO)
        {
            var materia = mapper.Map<Materia>(materiaDTO);  
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateria", new { id = materia.Id }, materia);
        }

        // DELETE: api/Materias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound();
            }

            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriaExists(int id)
        {
            return _context.Materias.Any(e => e.Id == id);
        }
    }
}
