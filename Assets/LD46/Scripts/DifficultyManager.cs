public class DifficultyManager
{
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

    public bool IsMaxLevel => CurrentLevel == 10;

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

    public float[] GetClientFruitNumberProbs()
    {
        switch (CurrentLevel)
        {
            case 0:
                return new[] { 1f, 0f, 0f};
            case 1:
                return new[] { 0.8f, 0.2f, 0f };
            case 2:
                return new[] { 0.6f, 0.4f, 0f };
            case 3:
                return new[] { 0.5f, 0.5f, 0f };
            case 4:
                return new[] { 0.4f, 0.4f, 0.2f };
            case 5:
                return new[] { 0.2f, 0.4f, 0.4f };
            case 6:
                return new[] { 0.0f, 0.75f, 0.25f };
            case 7:
                return new[] { 0f, 0.5f, 0.5f };
            case 8:
                return new[] { 0f, 0.3f, 0.7f };
            case 9:
                return new[] { 0f, 0f, 1f };
            default:
                return new[] { 0f, 0.0f, 1f };
        }
    }

    public int GetNumberOfClients()
    {
        switch (CurrentLevel)
        {
            case 0:
                return 5;
            case 1:
                return 5;
            case 2:
                return 5;
            case 3:
                return 10;
            case 4:
                return 10;
            case 5:
                return 14;
            case 6:
                return 14;
            case 7:
                return 20;
            case 8:
                return 20;
            case 9:
                return 20;
            default:
                return 20;
        }
    }

    public int GetClientsToSatisfy()
    {
        switch (CurrentLevel)
        {
            case 0:
                return 1;
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 2;
            case 4:
                return 3;
            case 5:
                return 3;
            case 6:
                return 4;
            case 7:
                return 4;
            case 8:
                return 4;
            case 9:
                return 4;
            default:
                return 4;
        }
    }

    public int GetMaxClientsToBeUnsatisfied()
    {
        switch (CurrentLevel)
        {
            case 0:
                return 1;
            case 1:
                return 1;
            case 2:
                return 1;
            case 3:
                return 1;
            case 4:
                return 4;
            case 5:
                return 4;
            case 6:
                return 3;
            case 7:
                return 2;
            case 8:
                return 2;
            case 9:
                return 2;
            default:
                return 1;
        }
    }

    public (float, float) GetClientTimelifeGaussian()
    {
        switch (CurrentLevel)
        {
            case 0:
                return ( 60f, 10f );
            case 1:
                return ( 45f, 5f);
            case 2:
                return ( 45f, 5f);
            case 3:
                return ( 43f, 5f);
            case 4:
                return (45f, 5f);
            case 5:
                return (45f, 5f);
            case 6:
                return (45f, 5f);
            case 7:
                return (45f, 5f);
            case 8:
                return (45f, 5f);
            case 9:
                return (45f, 5f);
            default:
                return (45f, 5f);
        }
    }
}