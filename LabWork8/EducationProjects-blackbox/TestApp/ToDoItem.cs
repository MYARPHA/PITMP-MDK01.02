using System;

namespace TestApp
{
    public class ToDoItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime DueDate { get; set; } = DateTime.Today;
    }
}