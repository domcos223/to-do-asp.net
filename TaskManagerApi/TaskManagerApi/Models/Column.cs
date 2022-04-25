namespace TaskManagerApi.Models
{
    public class Column
    {
        public int ColumnId { get; set; }
        public string Title { get; set; }
        public ICollection<Todo> Todos { get; set; }

    }
}
