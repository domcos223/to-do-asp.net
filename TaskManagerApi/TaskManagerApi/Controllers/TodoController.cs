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
    public class TodoController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public TodoController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }


        [HttpPut("MoveTodo")]
        public void MoveTodo(int? sourceId, int destinationId, int draggableId, int orderId)
        {
            var draggedTodo = _context.Todos.Where(w => w.TodoId == draggableId).First();
            if (sourceId == null) //moving inside one column
            {
                var columnTodosBefore = _context.Todos.Where(t => t.ColumnId == destinationId).OrderBy(o => o.OrderId).ToList();
                columnTodosBefore.Remove(draggedTodo);
                columnTodosBefore.Insert(orderId - 1, draggedTodo);
                draggedTodo.OrderId = orderId;
                int id = 1;
                foreach (var todo in columnTodosBefore)
                {
                    todo.OrderId = id;
                    id++;
                }

                _context.Entry(draggedTodo).State = EntityState.Modified;
                _context.SaveChanges();

            }
            else
            {
                var todosFinish = _context.Todos.Where(t => t.ColumnId == destinationId)
                  .OrderBy(o => o.OrderId).ToList();

                var todosStart = _context.Todos.Where(t => t.ColumnId == sourceId)
                  .OrderBy(o => o.OrderId).ToList();
                todosStart.Remove(draggedTodo);
                _context.SaveChanges();
                int id = 1;
                foreach (var todo in todosStart)
                {
                    todo.OrderId = id;
                    id++;
                }
                todosFinish.Insert(orderId - 1, draggedTodo);
                draggedTodo.ColumnId = destinationId;
                draggedTodo.OrderId = orderId;
                int id2 = 1;
                foreach (var todo in todosFinish)
                {
                    todo.OrderId = id2;
                    id2++;
                }
                _context.Entry(draggedTodo).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        [HttpPut("EditTodo")]
        public void EditTodo(int id, string title, string description, string dueDate)
        {
            var editedTodo = _context.Todos.Where(w => w.TodoId == id).First();
            editedTodo.Title = title;
            editedTodo.Description = description;
            editedTodo.DueDate = DateTime.Parse(dueDate);
            _context.Entry(editedTodo).State = EntityState.Modified;
            _context.SaveChanges();

        }
        // POST: api/Todo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            var lastOrderId = _context.Todos.Where(t => t.ColumnId == todo.ColumnId).Max(t => (int?)t.OrderId) ?? 0;
            if (lastOrderId == 0)
            {
                todo.OrderId = 1;
            }
            else
            {
                todo.OrderId = lastOrderId + 1;
            }
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.TodoId }, todo);
        }

        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            var removedOrderId = todo.OrderId;
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            var todosAfter = _context.Todos.Where(t => t.ColumnId == todo.ColumnId)
                .Where(t => t.OrderId > removedOrderId)
              .OrderBy(o => o.OrderId).ToList();

            foreach (var item in todosAfter)
            {
                item.OrderId -= 1;

            }
            await _context.SaveChangesAsync();


            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.TodoId == id);
        }
    }
}
