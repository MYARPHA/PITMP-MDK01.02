using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp.Tests
{
    public class ToDoListViewModelTests
    {
        [TestClass]
        public class ToDoServiceTests
        {
            private static List<ToDoItem> items = new List<ToDoItem>
            {
                new ToDoItem(){ Title = "Task 1", IsDone = true , Description = "Description 1", DueDate = DateTime.Now.AddDays(1) },
                new ToDoItem(){ Title = "Task 2", IsDone = false , Description = "Description 2", DueDate = DateTime.Now.AddDays(2) },
                new ToDoItem(){ Title = "Task 3", IsDone = false , Description = "Description 3", DueDate = DateTime.Now.AddDays(3) },
            };

            [TestMethod]
            public void GetToDoItems_ReturnsAllTasks()
            {
                var result = ToDoService.GetToDoItems();
                Assert.AreEqual(3, result.Count());
            }

            [TestMethod]
            public void AddToDoItem_AddsNewTaskToList()
            {
                var newItem = new ToDoItem { Title = "Test Task" };
                ToDoService.AddToDoItem(newItem);

                var result = ToDoService.GetToDoItems();
                Assert.IsTrue(result.Contains(newItem));
            }


            [TestMethod]
            public void AddToDoItemWithEmptyData_ThrowsException()
            {
                Assert.ThrowsException<ArgumentException>(() => ToDoService.AddToDoItem(new ToDoItem()));
            }

            [TestMethod]
            public void AddToDoItemWithInvalidData_ThrowsException()
            {
                Assert.ThrowsException<ArgumentException>(() => ToDoService.AddToDoItem(new ToDoItem { Title = null }));
            }

            [TestMethod]
            public void GetToDoItemsCount_ReturnsCorrectNumber()
            {
                var count = ToDoService.GetToDoItems().Count();
                Assert.AreEqual(3, count);
            }

            [TestMethod]
            public void RemoveAllTasks_EmptyListResult()
            {
                ToDoService.RemoveAllTasks();
                var result = ToDoService.GetToDoItems();
                Assert.IsTrue(result.Count() == 0);
            }

            [TestMethod]
            public void GetToDoItemById_ReturnsFirstTask()
            {
                var item = ToDoService.GetToDoItems().FirstOrDefault();
                var result = ToDoService.GetToDoItemById(item.Id);

                Assert.IsNotNull(result);
                Assert.AreEqual(item.Id, result.Id);
                Assert.AreEqual(item.Title, result.Title);
            }

            [TestMethod]
            public void GetToDoItemById_ReturnsNullForNonExistentId()
            {
                var result = ToDoService.GetToDoItemById(Guid.NewGuid());
                Assert.IsNull(result);
            }
        }

    }
}
