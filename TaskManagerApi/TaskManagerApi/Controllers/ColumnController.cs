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

    }
}
