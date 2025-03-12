using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class ToDoListViewModel: ViewModelBase
    {
        public ToDoItem NewToDoItem { get; set; }

        public ToDoListViewModel()
        {
            
            NewToDoItem = new ToDoItem();
        }

        public ObservableCollection<ToDoItem> ToDoItems
        {
            get
            {
                return new ObservableCollection<ToDoItem>(ToDoService.GetToDoItems());
            }
        }

        public void AddToDoItem()
        {
            ToDoService.AddToDoItem(NewToDoItem);
            NewToDoItem = new ToDoItem();
            RaisePropertyChanged(nameof(ToDoItems));
            RaisePropertyChanged(nameof(NewToDoItem));
        }

        public RelayCommand AddToDoItemCommand
        {
            get
            {
                return new RelayCommand(x => AddToDoItem());
            }
        }
    }
}
