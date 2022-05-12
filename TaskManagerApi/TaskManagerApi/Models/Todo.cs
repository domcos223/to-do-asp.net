using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagerApi.Models
{
    public class Todo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TodoId { get; set; }
        public int ColumnId { get; set; }

        [StringLength(20)]
        public string Title { get; set; }

        [StringLength(80)]
        public string Description { get; set; }

        public DateTimeOffset DueDate { get; set; }
        public int OrderId { get; set; }

        [JsonIgnore]
        public Column Column { get; set; }

    }
}
