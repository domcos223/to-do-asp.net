using TaskManagerApi.Data;

namespace TaskManagerApi.Services
{
    public class TodoService
    {
        private readonly TaskManagerContext _dbContext;

        public TodoService(TaskManagerContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
