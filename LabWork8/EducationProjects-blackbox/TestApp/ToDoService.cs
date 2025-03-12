using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class ToDoService
    {
        private static List<ToDoItem> items = new List<ToDoItem>
        {
            new ToDoItem(){ Title = "Task 1", IsDone = true , Description = "Description 1", DueDate = DateTime.Now.AddDays(1) },
            new ToDoItem(){ Title = "Task 2", IsDone = false , Description = "Description 2", DueDate = DateTime.Now.AddDays(2) },
            new ToDoItem(){ Title = "Task 3", IsDone = false , Description = "Description 3", DueDate = DateTime.Now.AddDays(3) },
        };

        public static IEnumerable<ToDoItem> GetToDoItems()
        {
            return items;
        }

        public static void AddToDoItem(ToDoItem item)
        {
            items.Add(item);
        }

        public static void RemoveAllTasks()
        {
            items.Clear();
        }

        public static void GetToDoItemById(Guid guid)
        {
            var initialCount = ToDoService.GetToDoItems().Count();
            Console.WriteLine($"Initial count: {initialCount}");

            ToDoService.RemoveAllTasks();

            var finalCount = ToDoService.GetToDoItems().Count();
            Console.WriteLine($"Final count after removing all tasks: {finalCount}");
        }
    }
}
