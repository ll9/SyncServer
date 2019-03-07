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
    public class SchemaDefinitionsController : ControllerBase
    {
        private readonly SyncServerContext _context;

        public SchemaDefinitionsController(SyncServerContext context)
        {
            _context = context;
        }

        // GET: api/SchemaDefinitions
        [HttpGet]
        public IEnumerable<SchemaDefinition> GetSchemaDefinitions()
        {
            return _context.SchemaDefinitions;
        }

        // GET: api/SchemaDefinitions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchemaDefinition([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schemaDefinition = await _context.SchemaDefinitions.FindAsync(id);

            if (schemaDefinition == null)
            {
                return NotFound();
            }

            return Ok(schemaDefinition);
        }

        // PUT: api/SchemaDefinitions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchemaDefinition([FromRoute] string id, [FromBody] SchemaDefinition schemaDefinition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != schemaDefinition.Id)
            {
                return BadRequest();
            }

            _context.Entry(schemaDefinition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchemaDefinitionExists(id))
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

        // POST: api/SchemaDefinitions/5
        [HttpPost("{maxSync}")]
        public async Task<IActionResult> PostSchemaDefinition([FromRoute] int maxSync, [FromBody] ICollection<SchemaDefinition> schemaDefinitions)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int newRowVersion = GetNewMaxRowVersion() + 1;

            foreach (var schemaDefinition in schemaDefinitions)
            {
                schemaDefinition.RowVersion = newRowVersion;

                if (_context.SchemaDefinitions.Any(e => e.Id == schemaDefinition.Id))
                {
                    _context.Entry(schemaDefinition).State = EntityState.Modified;
                }
                else
                {
                    await _context.SchemaDefinitions.AddAsync(schemaDefinition);
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
            return _context.SchemaDefinitions
                .Select(b => b.RowVersion)
                .DefaultIfEmpty(0)
                .Max();
        }

        // DELETE: api/SchemaDefinitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchemaDefinition([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var schemaDefinition = await _context.SchemaDefinitions.FindAsync(id);
            if (schemaDefinition == null)
            {
                return NotFound();
            }

            _context.SchemaDefinitions.Remove(schemaDefinition);
            await _context.SaveChangesAsync();

            return Ok(schemaDefinition);
        }

        private bool SchemaDefinitionExists(string id)
        {
            return _context.SchemaDefinitions.Any(e => e.Id == id);
        }
    }
}