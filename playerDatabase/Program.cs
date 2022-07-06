Database database = new Database();
bool isWork = true;
string userInput;

Console.WriteLine("Добро пожаловать в базу данных игроков!");

while (isWork)
{
    Console.SetCursorPosition(0, 1);
    Console.WriteLine("Выбери команду:\n" +
        "1. Добавить игрока.\n" +
        "2. Забанить игрока по уникальному номеру.\n" +
        "3. Вывести из бана игрока по уникальному номеру.\n" +
        "4. Удалить игрока.\n" +
        "5. Выход.\n");

    userInput = Console.ReadLine();

    switch (userInput)
    {
        case "1":
            database.CreatePlayer();
            break;
        case "2":
            database.BanPlayer();
            break;
        case "3":
            database.UnbanPlayer();
            break;
        case "4":
            database.DeletePlayer();
            break;
        case "5":
            isWork = false;
            break;
        default:
            Console.WriteLine("Такой команды нет!");
            break;
    }
    Console.Clear();
    Console.SetCursorPosition(0, 20);
    database.ShowInfoPlayers();
}

class Database
{
    private List<Player> _players = new List<Player>();
    private Player _player;

    public void CreatePlayer()
    {
        Console.WriteLine("Придумайте уникальный номер:");
        int uniqueNumber = AssignUniqueNumber();

        Console.Write("Введите имя игрока: ");
        string name = Console.ReadLine();

        Console.Write("\nВведите уровень игрока: ");
        int level = GetNumber();

        _players.Add(new Player(uniqueNumber, name, level));
    }

    public void BanPlayer()
    {
        bool wasPlayerReceived;

        Console.WriteLine("Введите уникальный номер игрока:");

        int uniqueNumber = GetNumber();

        wasPlayerReceived = TryGetPlayer(out _player, uniqueNumber);

        if (wasPlayerReceived == true)
        {
            _player.Ban();
            Console.WriteLine("Игрок забанен!");
        }
        else
        {
            Console.WriteLine("Игрок с таким id не найден.");
        }
    }

    public void UnbanPlayer()
    {
        bool wasPlayerReceived;

        Console.WriteLine("Введите уникальный номер игрока:");

        int uniqueNumber = GetNumber();

        wasPlayerReceived = TryGetPlayer(out _player, uniqueNumber);

        if (wasPlayerReceived == true)
        {
            _player.Unban();
            Console.WriteLine("Игрок забанен!");
        }
        else
        {
            Console.WriteLine("Игрок с таким id не найден.");
        }
    }

    public void DeletePlayer()
    {
        Console.WriteLine("Введите уникальный номер игрока для удаления:");
        int uniqueNumber = GetNumber();

        foreach (Player player in _players)
        {
            if (player.UniqueNumber == uniqueNumber)
            {
                _players.Remove(player);
                break;
            }
        }
    }

    public void ShowInfoPlayers()
    {
        foreach (Player player in _players)
        {
            player.ShowInfo();
        }
    }

    private int GetNumber()
    {
        bool isNumberWork = true;
        int playerNumber = 0;

        while (isNumberWork)
        {
            bool isNumber = true;
            string userInput = Console.ReadLine();

            if (isNumber = int.TryParse(userInput, out int number))
            {
                playerNumber = number;
                isNumberWork = false;
            }
            else
            {
                Console.WriteLine($"Не правильный ввод данных!!!  Повторите попытку");
            }
        }
        return playerNumber;
    }

    private int AssignUniqueNumber()
    {
        bool isNumberUnique = false;
        int uniqueNumberPlayer = 0;

        while (isNumberUnique == false)
        {
            uniqueNumberPlayer = GetNumber();

            if (_players.Count == 0)
            {
                isNumberUnique = true;
            }

            foreach (Player player in _players)
            {
                if (player.UniqueNumber == uniqueNumberPlayer)
                {
                    Console.WriteLine("Такой номер занят! Попробуйте снова.");
                }
                else
                {
                    Console.WriteLine("Номер записан!");
                    isNumberUnique = true;
                }
            }
        }

        return uniqueNumberPlayer;
    }

    private bool TryGetPlayer(out Player player, int uniqueNumber)
    {
        player = null;

        foreach(Player player1 in _players)
        {
            if (uniqueNumber == player1.UniqueNumber)
            {
                player = player1;
                return true;
            }
        }
        return false;
    }

}

class Player
{
    public int UniqueNumber { get; private set; }
    public string NickName { get; private set; }
    public int Level { get; private set; }
    public bool IsBanned { get; private set; }

    public Player(int uniqueNumber, string nickName, int level)
    {
        UniqueNumber = uniqueNumber;
        NickName = nickName;
        Level = level;
        IsBanned = false;
    }

    public void ShowInfo()
    {
        string isBannedComment;

        if (IsBanned == true)
        {
            isBannedComment = "Игрок забанен.";
        }
        else
        {
            isBannedComment = "Игрок не забанен.";
        }

        Console.WriteLine($"Уникальный номер: {UniqueNumber} | Никнейм: {NickName} | ур.{Level} | {isBannedComment}");
    }

    public void Ban()
    {
        IsBanned = true;
    }

    public void Unban()
    {
        IsBanned = false;
    }
}