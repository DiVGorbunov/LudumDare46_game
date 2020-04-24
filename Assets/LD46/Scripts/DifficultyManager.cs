public class DifficultyManager
{
    private static DifficultyLevel[] _levels =
    {
        new DifficultyLevel { FruitNumberProbs = new[] { 1f, 0f, 0f }, NumberOfClients = 1, ClientsToSatisfy = 1, FruitTypes = 1, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (60f, 0f), WinningMessage = "It's time to fulfill your first client order!" },
        new DifficultyLevel { FruitNumberProbs = new[] { 1f, 0f, 0f }, NumberOfClients = 5, ClientsToSatisfy = 2, FruitTypes = 1, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (60f, 0f), WinningMessage = "Sometimes more people are waiting for their orders." },
        new DifficultyLevel { FruitNumberProbs = new[] { 1f, 0f, 0f }, NumberOfClients = 5, ClientsToSatisfy = 2, FruitTypes = 2, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (60f, 0f), WinningMessage = "Clients may have different tastes." },
        new DifficultyLevel { FruitNumberProbs = new[] { 1f, 0f, 0f }, NumberOfClients = 2, ClientsToSatisfy = 1, FruitTypes = 2, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (10f, 0f), WinningMessage = "Some clients are impatient." },
        new DifficultyLevel { FruitNumberProbs = new[] { 0.25f, 0.75f, 0f }, NumberOfClients = 5, ClientsToSatisfy = 2, FruitTypes = 2, MaxClientsToBeUnsatisfied = 1, ClientLifetime = (60f, 0f), WinningMessage = "Or may have complicated requests." }
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
        CurrentLevel = 0;
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