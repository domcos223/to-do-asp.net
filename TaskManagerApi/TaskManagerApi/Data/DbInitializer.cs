using TaskManagerApi.Models;

namespace TaskManagerApi.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TaskManagerContext context)
        {
            if (context.Columns.Any())
            {
                return;
            }

            var columns = new List<Column>();

            columns.Add(new Column { Title = "Todo" });
            columns.Add(new Column { Title = "In progress" });
            columns.Add(new Column { Title = "Done" });
            columns.Add(new Column { Title = "Postponed" });

            context.Columns.AddRange(columns);
            context.SaveChanges();


            if (context.Todos.Any())
            {
                return;   // DB has been seeded
            }


            var todos = new List<Todo>();

            todos.Add(new Todo { Title = "Do laundry", Description = "add laundry detergent and pod, wash with allergy care", DueDate = DateTime.Parse("2022-04-25"), OrderId = 1, ColumnId = 1 });
            todos.Add(new Todo { Title = "Wash dishes", Description = "just put them in the washer", DueDate = DateTime.Parse("2022-04-26"), OrderId = 2, ColumnId = 1 });
            todos.Add(new Todo { Title = "Walk the dog", Description = "leash is at the front door", DueDate = DateTime.Parse("2022-05-10"), OrderId = 3, ColumnId = 1 });

            context.Todos.AddRange(todos);
            context.SaveChanges();






        }
    }
}