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
    public class DynamicEntitiesController : ControllerBase
    {
        private readonly SyncServerContext _context;

        public DynamicEntitiesController(SyncServerContext context)
        {
            _context = context;
        }

        // GET: api/DynamicEntities
        [HttpGet]
        public IEnumerable<DynamicEntity> GetDynamicEntities()
        {
            return _context.DynamicEntities;
        }

        // GET: api/DynamicEntities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDynamicEntity([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dynamicEntity = await _context.DynamicEntities.FindAsync(id);

            if (dynamicEntity == null)
            {
                return NotFound();
            }

            return Ok(dynamicEntity);
        }

        // PUT: api/DynamicEntities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDynamicEntity([FromRoute] string id, [FromBody] DynamicEntity dynamicEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dynamicEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(dynamicEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DynamicEntityExists(id))
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

        // POST: api/DynamicEntities/5
        [HttpPost("{maxSync}")]
        public async Task<IActionResult> PostDynamicEntity([FromRoute] int maxSync, [FromBody] ICollection<DynamicEntity> dynamicEntities)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int newRowVersion = GetNewMaxRowVersion() + 1;

            foreach (var dynamicEntity in dynamicEntities)
            {
                dynamicEntity.RowVersion = newRowVersion;

                if (_context.SchemaDefinitions.Any(e => e.Id == dynamicEntity.Id))
                {
                    _context.Entry(dynamicEntity).State = EntityState.Modified;
                }
                else
                {
                    var projTable = await _context.ProjectTable.SingleOrDefaultAsync(p => p.Name == dynamicEntity.ProjectTable.Name && p.ProjectId == dynamicEntity.ProjectTable.ProjectId);
                    if (projTable != null)
                    {
                        dynamicEntity.ProjectTable = projTable;
                    }
                    else
                    {
                        var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == dynamicEntity.ProjectTable.Project.Id);
                        if (project != null)
                        {
                            dynamicEntity.ProjectTable.Project = project;
                        }

                    }
                    await _context.DynamicEntities.AddAsync(dynamicEntity);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();

            var changes = _context.SchemaDefinitions
                .Where(e => e.RowVersion > maxSync)
                .Include(e => e.ProjectTable)
                .ThenInclude(p => p.Project);

            return Ok(changes);
        }

        private int GetNewMaxRowVersion()
        {
            return _context.DynamicEntities
                .Select(b => b.RowVersion)
                .DefaultIfEmpty(0)
                .Max();
        }

        // DELETE: api/DynamicEntities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDynamicEntity([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dynamicEntity = await _context.DynamicEntities.FindAsync(id);
            if (dynamicEntity == null)
            {
                return NotFound();
            }

            _context.DynamicEntities.Remove(dynamicEntity);
            await _context.SaveChangesAsync();

            return Ok(dynamicEntity);
        }

        private bool DynamicEntityExists(string id)
        {
            return _context.DynamicEntities.Any(e => e.Id == id);
        }
    }
}