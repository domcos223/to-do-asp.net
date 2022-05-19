using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public ColumnController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: api/Column
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> GetColumns()
        {   
            var columns = await _context.Columns.OrderBy(o => o.ColumnId).Include(c => c.Todos.OrderBy(o=> o.OrderId)).ToListAsync();
            return columns;
        }


        // GET: api/Column/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> GetColumn(int id)
        {
            var column = await _context.Columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return column;
        }

        //// PUT: api/Column/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutColumn(int id, Column column)
        //{
        //    if (id != column.ColumnId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(column).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ColumnExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //private bool ColumnExists(int id)
        //{
        //    return _context.Columns.Any(e => e.ColumnId == id);
        //}
    }
}
