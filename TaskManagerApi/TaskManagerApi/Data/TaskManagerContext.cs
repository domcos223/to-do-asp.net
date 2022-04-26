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
            modelBuilder.Entity<Todo>().HasKey(t => t.Id);
            modelBuilder.Entity<Todo>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Todo>().ToTable(nameof(Todo))
                                       .HasOne(t => t.Column)
                                       .WithMany(c => c.Todos)
                                       .HasForeignKey(t => t.ColumnId);

            modelBuilder.Entity<Column>().HasKey(c => c.Id);
            modelBuilder.Entity<Column>().ToTable(nameof(Column));
                                       
           
        }
    }
}
