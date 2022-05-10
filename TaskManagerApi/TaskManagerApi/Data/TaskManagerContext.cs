using TaskManagerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerApi.Data
{

    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Column> Columns { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().HasKey(t => t.Id);
            modelBuilder.Entity<Todo>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Todo>().ToTable(nameof(Todo));
            modelBuilder.Entity<Column>().HasKey(c => c.Id);
            modelBuilder.Entity<Todo>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Column>().ToTable(nameof(Column));
            
           
        }

    }
}
