using TaskManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Data
{

    public class TaskManagerContext : DbContext, ITaskManagerContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Column> Columns { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            
           
        }

    }
}
