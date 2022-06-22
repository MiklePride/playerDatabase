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
            database.AntiBanPlayer();
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

    public void CreatePlayer()
    {
        int uniqueNumber = AssignUniqueNumber();
        int parseLevel = 0;

        Console.Write("Введите имя игрока: ");
        string name = Console.ReadLine();

        Console.Write("\nВведите уровень игрока: ");
        int level = VerificationNumber();

        _players.Add(new Player(uniqueNumber, name, level));
    }

    public void BanPlayer()
    {
        Console.WriteLine("Введите уникальный номер игрока:");

        int uniqueNumber = VerificationNumber();
        bool isBanned = true;
        SearchByUniqueNumberForBan(uniqueNumber, isBanned);

        Console.Clear();
        Console.WriteLine("Игрок забанен!");
        Console.ReadKey();
    }

    public void AntiBanPlayer()
    {
        Console.WriteLine("Введите уникальный номер игрока:");

        int uniqueNumber = VerificationNumber();
        bool isBanned = false;
        SearchByUniqueNumberForBan(uniqueNumber, isBanned);

        Console.Clear();
        Console.WriteLine("Игрок разабанен!");
        Console.ReadKey();
    }

    public void DeletePlayer()
    {
        Console.WriteLine("Введите уникальный номер игрока для удаления:");
        int uniqueNumber = VerificationNumber();

        foreach (Player player in _players)
        {
            if (player.UniqueNumber == uniqueNumber)
            {
                _players.Remove(player);
                break;
            }
        }
    }

    private void SearchByUniqueNumberForBan(int number, bool isBanned)
    {
        foreach (Player player in _players)
        {
            if (player.UniqueNumber == number)
            {
                player.Ban(isBanned);
            }
        }
    }

    private int VerificationNumber()
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
        Random random = new Random();

        int randomNumber = random.Next(_players.Count, _players.Count + 1) + 1;
        
        return randomNumber;
    }

    public void ShowInfoPlayers()
    {
        foreach (Player player in _players)
        {
            player.ShowInfo();
        }
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

    public void Ban(bool isBanned)
    {
        IsBanned = isBanned;
    }
}