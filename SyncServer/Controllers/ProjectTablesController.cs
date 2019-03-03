using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SyncServer.Models;

namespace SyncServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTablesController : ControllerBase
    {
        private readonly SyncServerContext _context;

        public ProjectTablesController(SyncServerContext context)
        {
            _context = context;
        }

        // GET: api/ProjectTables
        [HttpGet]
        public IEnumerable<ProjectTable> GetProjectTable()
        {
            return _context.ProjectTable;
        }

        // GET: api/ProjectTables/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectTable([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTable = await _context.ProjectTable.FindAsync(id);

            if (projectTable == null)
            {
                return NotFound();
            }

            return Ok(projectTable);
        }

        // PUT: api/ProjectTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTable([FromRoute] string id, [FromBody] ProjectTable projectTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTable.Name)
            {
                return BadRequest();
            }

            _context.Entry(projectTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTableExists(id))
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

        // POST: api/ProjectTables
        [HttpPost]
        public async Task<IActionResult> PostProjectTable([FromBody] ProjectTable projectTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProjectTable.Add(projectTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectTable", new { id = projectTable.Name }, projectTable);
        }

        // DELETE: api/ProjectTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTable([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTable = await _context.ProjectTable.FindAsync(id);
            if (projectTable == null)
            {
                return NotFound();
            }

            _context.ProjectTable.Remove(projectTable);
            await _context.SaveChangesAsync();

            return Ok(projectTable);
        }

        private bool ProjectTableExists(string id)
        {
            return _context.ProjectTable.Any(e => e.Name == id);
        }
    }
}