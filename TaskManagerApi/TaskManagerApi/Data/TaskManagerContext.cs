using TaskManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Data
{

    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        {
        }

        public DbSet<Column> Columns { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Task");
            modelBuilder.Entity<Column>().ToTable("Column");
        }

    }
}
