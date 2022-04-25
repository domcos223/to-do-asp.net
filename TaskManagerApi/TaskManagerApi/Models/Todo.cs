namespace TaskManagerApi.Models
{
    public class Todo
    {
        public int TodoId { get; set; }
        public int ColumnId { get; set; }
        public string Title { get; set; }    
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Column Column { get; set; }

    }
}
