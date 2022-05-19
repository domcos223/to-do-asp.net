using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerApi.Controllers;
using TaskManagerApi.Data;
using TaskManagerApi.Models;
using Xunit;

namespace Tests
{
    public class TodoControllerTest
    {

        [Fact]
        public void TodoTest()
        {
            //Assert
            var data = new List<Todo>
            {
                new Todo { TodoId=1, Title = "First test todo", Description = "Test description 1", DueDate = DateTimeOffset.Now, ColumnId = 2, OrderId = 1 },
                new Todo { TodoId=2, Title = "Second test todo", Description = "Test description 2", DueDate = DateTimeOffset.Now, ColumnId = 2, OrderId = 2 },
                new Todo { TodoId=3, Title= "Third test todo", Description = "Test description 3", DueDate = DateTimeOffset.Now, ColumnId = 2, OrderId = 3 },
            };
      
            var mockTestTodos = MockDbSet(data);
            var mockContext = new Mock<TaskManagerContext>();
            mockContext.Setup(c => c.Todos).Returns(mockTestTodos.Object);
            //Act
            var todoController = new TodoController(mockContext.Object);
            todoController.MoveTodo(2, 3, 1, 2);
            int newColumnId = mockContext.Object.Todos.Where(t => t.TodoId == 1).Single().ColumnId;
            
            Assert.Equal(3, newColumnId);

        }
        Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> mockDbset = new Mock<DbSet<T>>();
            mockDbset.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            mockDbset.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            mockDbset.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            mockDbset.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());
            mockDbset.Setup(d => d.Add(It.IsAny<T>()));


            return mockDbset;
        }



    }
      
}
