using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IELTS_Centre.Data;
using IELTS_Centre.Models;

namespace IELTS_Centre.Controllers
{
    [Produces("application/json")]
    [Route("api/Teachers_API")]
    public class Teachers_APIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Teachers_APIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Teachers_API
        [HttpGet]
        public IEnumerable<Teacher> GetTeachers()
        {
            return _context.Teachers;
        }

        // GET: api/Teachers_API/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.ID == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // PUT: api/Teachers_API/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher([FromRoute] int id, [FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.ID)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers_API
        [HttpPost]
        public async Task<IActionResult> PostTeacher([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacher", new { id = teacher.ID }, teacher);
        }

        // DELETE: api/Teachers_API/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teachers.SingleOrDefaultAsync(m => m.ID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.ID == id);
        }
    }
}