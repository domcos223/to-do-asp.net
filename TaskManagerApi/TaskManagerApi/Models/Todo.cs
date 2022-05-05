using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManagerApi.Models
{
    public class Todo
    {   

        public int Id { get; set; }
        [Required(ErrorMessage = "You didn't provide enough data")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You didn't provide enough data")]
        public string Description { get; set; }

        [Required(ErrorMessage = "You didn't provide enough data")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        public int OrderId { get; set; }
        public int ColumnId { get; set; }

    }
}
