namespace TaskManagerApi.Models
{
    public class Column
    {
        public int Id { get; set; }
        public string Title { get; set; }    
        public List<Todo> TodoList { get; set; }
    }
}
