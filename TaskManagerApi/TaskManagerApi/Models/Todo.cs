using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class Todo
    {
        public int TodoId { get; set; }
        public int ColumnId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

    }
}
