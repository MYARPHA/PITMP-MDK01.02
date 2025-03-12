namespace ErrorsApp
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        // Ошибка: пароли хранятся в открытом виде (без хеширования)
        static Dictionary<string, string> userCredentials = new Dictionary<string, string>();

        static List<User> users = new List<User>(); // Список для хранения пользователей

        static void Main(string[] args)
        {
            // Ошибка проектирования: пустой словарь, никто не может авторизоваться, если не зарегистрируется
            // Предварительно зарегистрированных пользователей нет

            bool exit = false;
            bool isAuthenticated = false;

            while (!exit)
            {
                Console.WriteLine("\nМеню:");
                if (!isAuthenticated)
                {
                    Console.WriteLine("1. Авторизоваться");
                    Console.WriteLine("2. Зарегистрироваться");
                    Console.WriteLine("3. Выйти из программы");
                }
                else
                {
                    Console.WriteLine("1. Добавить пользователяя");//Опечатка в пользовательском интерфейсе
                    Console.WriteLine("2. Удалить пользователя");
                    Console.WriteLine("2. Найти пользователя по имени"); //Опечатка в пользовательском интерфейсе
                    Console.WriteLine("4. Вывести всех пользователей");
                    Console.WriteLine("5. Выйти из учетной записи");
                    Console.WriteLine("6. Выйти из программы");
                }
                
                Console.Write("Выберите опцию: ");

                string choice = Console.ReadLine();

                if (!isAuthenticated)
                {
                    switch (choice)
                    {
                        case "1":
                            isAuthenticated = Authorize();
                            break;
                        case "2":
                            Register();
                            break;
                        case "3":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case "1":
                            AddUser(); 
                            break;
                        case "2":
                            RemoveUser(); 
                            break;
                        case "3":
                            FindUser(); 
                            break;
                        case "4":
                            DisplayUsers();
                            break;
                        case "5":
                            isAuthenticated = false;
                            Console.WriteLine("Вы вышли из учетной записи.");
                            break;
                        case "6":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
            }
        }


        // Метод для авторизации пользователя
        static bool Authorize()
        {
            Console.WriteLine("Введите имя пользователя:");
            string username = Console.ReadLine();

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            // Ошибка времени выполния: нет проверки на наличие пользователя
            if (userCredentials[username] == password) // Если пользователя нет, произойдет исключение
            {
                Console.WriteLine("Успешная авторизация!");
                return true;
            }
            else
            {
                Console.WriteLine("Неверное имя пользователя или пароль.");
                return false;
            }
        }

        // Метод для регистрации нового пользователя
        static void Register()
        {
            Console.WriteLine("Введите имя пользователя для регистрации:");
            string username = Console.ReadLine();

            // Логическая ошибка: можно зарегистрировать пользователя с пустым именем
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Имя пользователя не может быть пустым."); // Проверка есть, но продолжаем регистрацию даже при пустом имени
            }

            if (userCredentials.ContainsKey(username))
            {
                Console.WriteLine("Пользователь с таким именем уже существует."); // Проверка есть, но продолжаем регистрацию даже при таком же имени
            }

            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            if (password.Length < 8)
            {
                Console.WriteLine("Пароль слишком короткий. Минимум 8 символов."); // Эта проверка работает, но снова продолжаем регистрацию даже при коротком пароле
            }

            // Ошибка безопасности: пароли не хешируются
            // Ошибка времени выполенения добавление пользователя с таким же именем
            userCredentials.Add(username, password);
            Console.WriteLine("Пользователь успешно зарегистрирован.");
        }


        // Метод для добавления пользователя
        static void AddUser()
        {
            Console.WriteLine("Введите имя пользователя:");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст пользователя:");

            // Ошибка времени выполнения: неправильный ввод числа
            int age = Convert.ToInt32(Console.ReadLine()); 

            // Логическая ошибка: возраст может быть меньше 0, но добавление не прерывается
            if (age < 0)
            {
                Console.WriteLine("Возраст не может быть отрицательным.");
            }

            // Ошибка времени выполнения: попытка добавить пользователя с пустым именем
            users.Add(new User(name, age));

            Console.WriteLine("Пользователь добавлен.");
        }

        // Метод для удаления пользователя
        static void RemoveUser()
        {
            Console.WriteLine("Введите имя пользователя для удаления:");
            string name = Console.ReadLine();

            // Логическая ошибка: игнорируется ситуация, когда в списке несколько пользователей с одинаковыми именами
            User userToRemove = users.Find(u => u.Name == name); // Не учитываются регистр и пробелы

            if (userToRemove != null)
            {
                users.Remove(userToRemove);
                Console.WriteLine("Пользователь удален.");
            }
            else
            {
                Console.WriteLine("Пользователь не найден.");
            }
        }

        // Метод для поиска пользователя
        static void FindUser()
        {
            Console.WriteLine("Введите имя пользователя для поиска:");
            string name = Console.ReadLine();

            // Логическая ошибка: игнорируется ситуация, когда в списке несколько пользователей с одинаковыми именами
            User userFound = users.Find(u => u.Name == name);

            if (userFound != null)
            {
                // Ошибка в выводе: неправильное обращение к полям объекта, неверная интерполяция
                Console.WriteLine("Найден пользователь: {userFound.Name}, возраст {userFound.age}"); // Ошибка доступа к полям
            }
            else
            {
                Console.WriteLine("Пользователь не найден.");
            }
        }

        // Метод для вывода всех пользователей
        static void DisplayUsers()
        {
            if (users.Count == 0)
            {
                Console.WriteLine("Список пользователей пуст.");
                return //Синтаксическая ошибка нет ;
            }

            // Ошибка времени выполнения: выход за пределы списка при некорректной индексации
            for (int i = 0; i <= users.Count; i++)  // Логическая ошибка: цикл выполняется на одну итерацию больше
            {
                Console.WriteLine($"Имя: {users[i].Name}, Возраст: {users[i].Age}");
            }
        }
    }

    // Класс для хранения данных пользователя
    class User
    {
        // Ошибка проектирования: поля класса сделаны public вместо private
        public string Name;
        public int Age;

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
