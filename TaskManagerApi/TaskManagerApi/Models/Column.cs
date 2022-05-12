using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagerApi.Models
{   
    public class Column
    {   
        public Column()
        {
            Todos = new HashSet<Todo>();   
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColumnId { get; set; }
        [StringLength(20)]
        public string Title { get; set; }    
        public ICollection<Todo> Todos { get; set; }
    }
}
