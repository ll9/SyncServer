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
    public class ProjectTableChangeSetsController : ControllerBase
    {
        private readonly SyncServerContext _context;

        public ProjectTableChangeSetsController(SyncServerContext context)
        {
            _context = context;
        }

        // GET: api/ProjectTableChangeSets
        [HttpGet]
        public IEnumerable<ProjectTableChangeSet> GetProjectTableChangeSet()
        {
            return _context.ProjectTableChangeSet;
        }

        // GET: api/ProjectTableChangeSets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectTableChangeSet([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTableChangeSet = await _context.ProjectTableChangeSet.FindAsync(id);

            if (projectTableChangeSet == null)
            {
                return NotFound();
            }

            return Ok(projectTableChangeSet);
        }

        // PUT: api/ProjectTableChangeSets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProjectTableChangeSet([FromRoute] string id, [FromBody] ProjectTableChangeSet projectTableChangeSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTableChangeSet.Name)
            {
                return BadRequest();
            }

            _context.Entry(projectTableChangeSet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTableChangeSetExists(id))
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

        // POST: api/ProjectTableChangeSets
        [HttpPost]
        public async Task<IActionResult> PostProjectTableChangeSet([FromBody] ProjectTableChangeSet projectTableChangeSet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProjectTableChangeSet.Add(projectTableChangeSet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProjectTableChangeSet", new { id = projectTableChangeSet.Name }, projectTableChangeSet);
        }

        // DELETE: api/ProjectTableChangeSets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectTableChangeSet([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTableChangeSet = await _context.ProjectTableChangeSet.FindAsync(id);
            if (projectTableChangeSet == null)
            {
                return NotFound();
            }

            _context.ProjectTableChangeSet.Remove(projectTableChangeSet);
            await _context.SaveChangesAsync();

            return Ok(projectTableChangeSet);
        }

        private bool ProjectTableChangeSetExists(string id)
        {
            return _context.ProjectTableChangeSet.Any(e => e.Name == id);
        }
    }
}