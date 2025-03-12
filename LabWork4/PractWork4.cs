// Практическая 4

// Можно внедрить зависимости через конструктор:
public class HotelManagementSystem
{
    private readonly List<Customer> _customers;
    private readonly List<Room> _rooms;

    public HotelManagementSystem(List<Customer> customers, List<Room> rooms)
    {
        if (customers == null)
        {
            _customers = new List<Customer>();
        }
        else
        {
            _customers = customers;
        }

        if (rooms == null)
        {
            _rooms = new List<Room>();
        }
        else
        {
            _rooms = rooms;
        }
    }
}

// Проверка на корректность входных данных (Валидация данных)

public void BookRoom(string customerEmail, int roomNumber, DateTime startDate, DateTime endDate, string customerName)
{
    if (string.IsNullOrEmpty(customerEmail))
        throw new ArgumentException("Пустой email");

    var customer = GetCustomerByEmail(customerEmail);
    if (customer == null)
        throw new InvalidOperationException("Клиент не найден.");

    var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
    if (room == null)
        throw new InvalidOperationException("Номер не найден.");

    if (!IsRoomAvailable(room, startDate, endDate))
        throw new InvalidOperationException("Комната недоступна в указанный период.");

    var booking = new Booking(customer, room, startDate, endDate);
    customer.AddBooking(booking);

    Console.WriteLine($"Номер {roomNumber} успешно забронирован для {customer.Name}.");
}

// Для удобства можно использовать IRoomRepository

public interface IRoomRepository // Интерфейс
{
    Room AddRoom(int roomNumber, string type, decimal pricePerNight, int capacity);
    void UpdateRoom(Room room);
    void DeleteRoom(int roomNumber);
    Room GetRoomByNumber(int roomNumber);
    List<Room> GetAllRooms();
}

public class RoomRepository : IRoomRepository
{
    private List<Room> _rooms;

    public RoomRepository(List<Room> rooms)
    {
        if (rooms == null)
        {
            _rooms = new List<Room>();
        }
        else
        {
            _rooms = rooms;
        }
    }

    public Room AddRoom(int roomNumber, string type, decimal pricePerNight, int capacity)
    {
        var room = new Room(roomNumber, type, pricePerNight, capacity);
        _rooms.Add(room);
        return room;
    }
}

// Можно добавить систему логирования для отслеживания важных событий

using System;
using Microsoft.Extensions.Logging;

public class HotelManagementSystem
{
    private readonly ILogger<HotelManagementSystem> _logger;

    public HotelManagementSystem(ILogger<HotelManagementSystem> logger)
    {
        _logger = logger;
    }

    public void BookRoom(string customerEmail, int roomNumber, DateTime startDate, DateTime endDate, string customerName)
    {
        try
        {
            _logger.LogInformation("Комната {RoomNumber} забронирована для клиента {CustomerName}", roomNumber, customerName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при бронировании комнаты");
            throw;
        }
    }
}


// Проверка что бронирование не null 
public void AddBooking(Booking booking)
{
    if (booking != null)
    {
        BookingHistory.Add(booking);
    }
    else
    {
        throw new ArgumentNullException(nameof(booking), "Бронирование не может быть null.");
    }
}

// Проверка на корректность статуса

public void CancelBooking()
{
    if (!Status.Equals("Забронировано", StringComparison.OrdinalIgnoreCase))
    {
        throw new InvalidOperationException("Бронирование уже отменено или завершено.");
    }

    Status = "Отменено";
    Room.IsAvailable = true;
}

// Добавить в BookRoom() проверку на количество свободных мест
public void BookRoom(string customerEmail, int roomNumber, DateTime startDate, DateTime endDate, string customerName)
{
    // Поиск доступных комнат 
    var availableRooms = rooms.Where(r => r.RoomNumber == roomNumber && r.IsAvailable);
    
    // Использоввание LINQ для более эффективного поиска свободной комнаты (IsAvailable = true)
    if (!availableRooms.Any())
    {
        throw new InvalidOperationException("Номер комнаты недоступен или забронирован.");
    }

    // Остальной код
    var customer = GetCustomerByEmail(customerEmail);
            var room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (customer == null || room == null)
            {
                throw new InvalidOperationException("Клиент или номер не найден.");
            }

            var booking = new Booking(customer, room, startDate, endDate);
            customer.AddBooking(booking);

            Console.WriteLine($"Номер {roomNumber} успешно забронирован для {customer.Name}.");
}

// Проверку на корректность данных при создании комнаты
public Room(int roomNumber, string type, decimal pricePerNight, int capacity)
{
    // Проверка что не должен быть меньше или равны нулю
    if (roomNumber <= 0 || capacity <= 0 || pricePerNight < 0)
    {
        throw new ArgumentException("Неверные входные данные");
    }
    RoomNumber = roomNumber;
    Type = type;
    PricePerNight = pricePerNight;
    Capacity = capacity;
    IsAvailable = true; 
}

// Второй вариант проверки корректности данных при создании комнаты
public static bool IsValidRoomNumber(int roomNumber)
{
    return roomNumber > 0;
}

public void AddRoom(int roomNumber, string type, decimal pricePerNight, int capacity)
{
    if (!IsValidRoomNumber(roomNumber))
    {
        throw new ArgumentException("Номер комнаты должен быть положительным.");
    }
    // (...)
}


// РЕАЛИЗОВАТЬ UNIT-TEST!!!

[TestClass]
public class RoomTests
{
    [TestMethod]
    public void Constructor_ValidInput_CorrectInitialization()
    {
        var room = new Room(101, "Стандарт", 100, 2);
        Assert.AreEqual(101, room.RoomNumber);
        Assert.AreEqual("Стандарт", room.Type);
        Assert.AreEqual(100, room.PricePerNight);
        Assert.AreEqual(2, room.Capacity);
        Assert.IsTrue(room.IsAvailable);
    }
}

// Внедрить зависимости для класса HotelManagementSystem через конструктор 
public class HotelManagementSystem
{
    private readonly IRoomRepository _roomRepository;
    private readonly ICustomerRepository _customerRepository;

    public HotelManagementSystem(IRoomRepository roomRepository, ICustomerRepository customerRepository)
    {
        _roomRepository = roomRepository;
        _customerRepository = customerRepository;
    }
}

// Добавить XML-документацию к публичным методам и свойствам!!!

/// <summary>
/// Получает доступную комнату по типу и вместимости.
/// </summary>
/// <param name="type">Тип комнаты.</param>
/// <param name="capacity">Вместимость комнаты.</param>
/// <returns>Доступная комната или null, если не найдена.</returns>
public Room GetAvailableRoom(string type, int capacity)
{
    // Реализация метода..
}

// Добавить IRepositoty
public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T GetById(object id);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}

public class RoomRepository : IRepository<Room>
{
    private List<Room> _rooms;

    public RoomRepository()
    {
        _rooms = new List<Room>();
    }

    public IEnumerable<Room> GetAll()
    {
        return _rooms;
    }
}

public class HotelManagementSystem
{
    private IRoomRepository _roomRepository;
    private ICustomerRepository _customerRepository;

    public HotelManagementSystem(IRoomRepository roomRepository, ICustomerRepository customerRepository)
    {
        _roomRepository = roomRepository;
        _customerRepository = customerRepository;
    }
}
