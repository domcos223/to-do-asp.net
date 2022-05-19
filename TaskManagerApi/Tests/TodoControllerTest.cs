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
                new Todo {  Title = "Second test todo", Description = "Test description 2", DueDate = DateTimeOffset.Now, ColumnId = 2, OrderId = 2 },
                new Todo {  Title = "Third test todo", Description = "Test description 3", DueDate = DateTimeOffset.Now, ColumnId = 2, OrderId = 3 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ITaskManagerContext>();
            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);
            //Act
            var todoDescription = mockContext.Object.Todos.Where(t => t.TodoId == 1).Single().Description;
            Assert.Equal("Test description 1", todoDescription);

         
        }


    }
      
}
