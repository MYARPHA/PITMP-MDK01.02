Console.WriteLine("PR2");


static void Main(string[] args)
{
    // Основной цикл работы приложения
    while (true)
    {
        Console.WriteLine("1. Проверка простого числа");
        Console.WriteLine("2. Генерация случайного числа");
        Console.WriteLine("3. Проверка массива чисел");
        Console.WriteLine("4. Нагрузочное тестирование");
        Console.WriteLine("5. Проверка обработки ошибок");
        Console.WriteLine("6. Выход");

        Console.Write("Выберите действие: ");

        switch (Console.ReadLine())
        {
            case "1":
                CheckSinglePrime();
                break;
            case "2":
                GenerateRandomNumber();
                break;
            case "3":
                CheckPrimeArray();
                break;
            case "4":
                StressTesting();
                break;
            case "5":
                ErrorHandlingTest();
                break;
            case "6":
                return;
            default:
                Console.WriteLine("Пожалуйста, выберите правильный номер действия.");
                break;
        }
    }
}

static void CheckSinglePrime()
{
    Console.Write("Введите число для проверки: ");
    string input = Console.ReadLine();

    if (int.TryParse(input, out int number))
    {
        if (number <= 0)
        {
            Console.WriteLine("Пожалуйста, введите положительное число.");
        }
        else if (IsPrime(number))
        {
            Console.WriteLine($"{number} - простое число.");
        }
        else
        {
            Console.WriteLine($"{number} - составное число.");
        }
    }
    else
    {
        Console.WriteLine("Пожалуйста, введите корректное число.");
    }
}

static bool IsPrime(int number)
{
    if (number <= 1) return false;
    if (number == 2 || number == 3) return true;
    if (number % 2 == 0 || number % 3 == 0) return false;

    for (int i = 5; i <= Math.Sqrt(number); i += 6)
    {
        if (number % i == 0 || number % (i + 2) == 0) return false;
    }

    return true;
}

static void GenerateRandomNumber()
{
    Random random = new Random();
    int randomNumber = random.Next(1, 101);
    Console.WriteLine($"Сгенерировано случайное число: {randomNumber}");

    // Проверка простоты сгенерированного числа
    if (IsPrime(randomNumber))
    {
        Console.WriteLine($"{randomNumber} - простое число.");
    }
    else
    {
        Console.WriteLine($"{randomNumber} - составное число.");
    }
}

static void CheckPrimeArray()
{
    Console.Write("Введите количество элементов в массиве: ");
    string input = Console.ReadLine();

    if (int.TryParse(input, out int arraySize) && arraySize > 0)
    {
        int[] primeArray = new int[arraySize];
        int[] nonPrimeArray = new int[arraySize];

        Random random = new Random();
        for (int i = 0; i < arraySize; i++)
        {
            int num = random.Next(1, 101);
            primeArray[i] = num;
            nonPrimeArray[i] = num;

            if (IsPrime(num))
            {
                primeArray[i] = num;
            }
            else
            {
                nonPrimeArray[i] = num;
            }
        }

        Console.WriteLine("Простые числа:");
        PrintArray(primeArray);

        Console.WriteLine("\nСоставные числа:");
        PrintArray(nonPrimeArray);
    }
}

static void PrintArray(int[] array)
{
    foreach (var item in array)
    {
        Console.Write(item + " ");
    }
    Console.WriteLine();
}

static void StressTesting()
{
    const int batchSize = 10000;
    int totalNumbers = 0;
    int primeCount = 0;
    int compositeCount = 0;

    Random random = new Random();
    for (int i = 0; i < batchSize; i++)
    {
        int num = random.Next(1, 1000001);
        bool isPrime = IsPrime(num);

        if (isPrime)
        {
            primeCount++;
        }
        else
        {
            compositeCount++;
        }

        totalNumbers++;

        if (totalNumbers % 1000 == 0)
        {
            Console.WriteLine($"Обработано {totalNumbers} чисел");
        }
    }

    Console.WriteLine($"\nРезультаты нагрузочного тестирования:\n");
    Console.WriteLine($"Всего чисел: {totalNumbers}");
    Console.WriteLine($"Простых чисел: {primeCount}");
    Console.WriteLine($"Составных чисел: {compositeCount}");
    Console.WriteLine($"Процент простых чисел: {(double)primeCount / totalNumbers * 100:F2}%");
}

static void ErrorHandlingTest()
{
    Console.WriteLine("Тестирование обработки ошибок:");
    Console.WriteLine("1. Ввод строки вместо числа");
    Console.WriteLine("2. Ввод отрицательного числа");
    Console.WriteLine("3. Ввод числа за пределами диапазона");

    Console.Write("Выберите действие для тестирования: ");

    switch (Console.ReadLine())
    {
        case "1":
            CheckSinglePrime(); // Это вызовет обработку ошибки ввода строки
            break;
        case "2":
            CheckSinglePrime(); // Это вызовет обработку отрицательных чисел
            break;
        case "3":
            CheckSinglePrime(); // Это вызовет обработку чисел за пределами диапазона
            break;
        default:
            Console.WriteLine("Пожалуйста, выберите правильный номер действия.");
            break;
    }
}
