public class DifficultyManager
{
    private static DifficultyLevel[] _levels =
    {
        new DifficultyLevel { FruitNumberProbs = new[] { 1f, 0f, 0f }, NumberOfClients = 1, ClientsToSatisfy = 1, FruitTypes = 1, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (60f, 0f), WinningMessage = "It's time to fulfill your first client order!" },
        new DifficultyLevel { FruitNumberProbs = new[] { 1f, 0f, 0f }, NumberOfClients = 5, ClientsToSatisfy = 2, FruitTypes = 1, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (60f, 0f), WinningMessage = "Sometimes more people are waiting for their orders." },
        new DifficultyLevel { FruitNumberProbs = new[] { 1f, 0f, 0f }, NumberOfClients = 5, ClientsToSatisfy = 2, FruitTypes = 2, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (60f, 0f), WinningMessage = "Clients may have different tastes." },
        new DifficultyLevel { FruitNumberProbs = new[] { 1f, 0f, 0f }, NumberOfClients = 2, ClientsToSatisfy = 1, FruitTypes = 2, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (10f, 0f), WinningMessage = "Some clients are impatient." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.25f, 0.75f, 0f }, NumberOfClients = 5, ClientsToSatisfy = 2, FruitTypes = 2, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (60f, 0f), WinningMessage = "Or may have complicated requests." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.5f, 0.5f, 0f }, NumberOfClients = 5, ClientsToSatisfy = 3, FruitTypes = 3, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (40f, 5f), WinningMessage = "Now you're ready for real life situations." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.5f, 0.5f, 0f }, NumberOfClients = 7, ClientsToSatisfy = 4, FruitTypes = 3, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (40f, 5f), WinningMessage = "More people need your service this time!" },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.5f, 0.5f, 0f }, NumberOfClients = 7, ClientsToSatisfy = 4, FruitTypes = 3, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (35f, 5f), WinningMessage = "And some of them need it faster!" },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.5f, 0.5f, 0f }, NumberOfClients = 7, ClientsToSatisfy = 4, FruitTypes = 5, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (35f, 5f), WinningMessage = "New ingredients are on the map!" },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.5f, 0.5f, 0f }, NumberOfClients = 9, ClientsToSatisfy = 5, FruitTypes = 5, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (40f, 5f), WinningMessage = "Your delivery becomes popular!" },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.5f, 0.5f, 0f }, NumberOfClients = 15, ClientsToSatisfy = 5, FruitTypes = 5, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (40f, 5f), WinningMessage = "Time to move to a bigger city." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.5f, 0.5f, 0f }, NumberOfClients = 15, ClientsToSatisfy = 5, FruitTypes = 7, MaxClientsToBeUnsatisfied = 3, ClientLifetime = (60f, 5f), WinningMessage = "Big cities always provide better food choices." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.2f, 0.5f, 0.3f }, NumberOfClients = 15, ClientsToSatisfy = 5, FruitTypes = 7, MaxClientsToBeUnsatisfied = 3, ClientLifetime = (60f, 5f), WinningMessage = "And there are some foodies in this city." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0f, 0.5f, 0.5f }, NumberOfClients = 15, ClientsToSatisfy = 5, FruitTypes = 6, MaxClientsToBeUnsatisfied = 5, ClientLifetime = (70f, 5f), WinningMessage = "Simple orders are in the past." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0f, 0.5f, 0.5f }, NumberOfClients = 15, ClientsToSatisfy = 6, FruitTypes = 6, MaxClientsToBeUnsatisfied = 5, ClientLifetime = (70f, 5f), WinningMessage = "Top company should coop with top loads." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0f, 0.25f, 0.75f }, NumberOfClients = 15, ClientsToSatisfy = 6, FruitTypes = 6, MaxClientsToBeUnsatisfied = 5, ClientLifetime = (75f, 5f), WinningMessage = "The hardest day has come!" }
    };

    private static DifficultyManager _difficultyManager;

    public static DifficultyManager Instance
    {
        get
        {
            if (_difficultyManager == null)
            {
                _difficultyManager = new DifficultyManager();
            }
            return _difficultyManager;
        }
    }

    public int CurrentLevel { get; private set; }

    public bool IsMaxLevel => CurrentLevel == _levels.Length - 1;

    private DifficultyManager()
    {
        ResetLevel();
    }

    public void MoveToNextLevel()
    {
        if (!IsMaxLevel)
        {
            CurrentLevel++;
        }
    }

    public void ResetLevel()
    {
        CurrentLevel = 0;
    }

    public float[] GetClientFruitNumberProbs()
    {
        return _levels[CurrentLevel].FruitNumberProbs;
    }

    public int GetNumberOfClients()
    {
        return _levels[CurrentLevel].NumberOfClients;
    }

    public int GetClientsToSatisfy()
    {
        return _levels[CurrentLevel].ClientsToSatisfy;
    }

    public int GetMaxClientsToBeUnsatisfied()
    {
        return _levels[CurrentLevel].MaxClientsToBeUnsatisfied;
    }

    public (float, float) GetClientTimelifeGaussian()
    {
        return _levels[CurrentLevel].ClientLifetime;
    }

    public int GetFruitTypes()
    {
        return _levels[CurrentLevel].FruitTypes;
    }

    public string GetWinningMessage()
    {
        return _levels[CurrentLevel].WinningMessage;
    }
}

public class DifficultyLevel
{
    public int NumberOfClients { get; set; }
    public int ClientsToSatisfy { get; set; }
    public int MaxClientsToBeUnsatisfied { get; set; }
    public float[] FruitNumberProbs { get; set; }
    public (float, float) ClientLifetime { get; set; }
    public int FruitTypes { get; set; }
    public string WinningMessage { get; set; }
}