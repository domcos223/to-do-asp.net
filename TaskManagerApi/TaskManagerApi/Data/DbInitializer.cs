using TaskManagerApi.Models;

namespace TaskManagerApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TaskManagerContext context)
        {
            // Look for any students.
            if (context.Todos.Any())
            {
                return;   // DB has been seeded
            }

            var columns = new Column[]
            {
                new Column{ColumnId=1,Title="TODO"},
                new Column{ColumnId=2,Title="In progress"},
                new Column{ColumnId=3,Title="Done"},
                new Column{ColumnId=4,Title="Postponed"},

            };

            context.Columns.AddRange(columns);
            context.SaveChanges();

            var todos = new Todo[]
            {
                new Todo{ColumnId=1, Title="Do laundry",Description="add laundry detergent and pod, wash with allergy care",DueDate=DateTime.Parse("2022-04-25")},
                new Todo{ColumnId=2, Title="Wash dishes",Description="just put them in the washer",DueDate=DateTime.Parse("2022-04-26")},
                new Todo{ColumnId=3, Title="Walk the dog",Description="leash is at the front door",DueDate=DateTime.Parse("2022-05-10")},
                new Todo{ColumnId=4, Title="Make dinner",Description="hamburger with fries",DueDate=DateTime.Parse("2022-05-11")},
            };

            context.Todos.AddRange(todos);
            context.SaveChanges();

            

        }
    }
}