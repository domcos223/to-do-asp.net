using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Models
{
    public class Column
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Todo> Todos { get; set; } = new List<Todo>();

    }
}
