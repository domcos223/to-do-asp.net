using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Data
{
    public interface ITaskManagerContext
    {
        public abstract DbSet<Todo> Todos { get; set; }
        public abstract DbSet<Column> Columns { get; set; }
     
    }
}
