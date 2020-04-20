﻿public class DifficultyManager
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

    private DifficultyManager()
    {
        CurrentLevel = 0;
    }

    public void MoveToNextLevel()
    {
        CurrentLevel++;
    }

    public float[] GetClientFruitNumberProbs()
    {
        switch (CurrentLevel)
        {
            case 0:
                return new[] { 1f, 0f, 0f};
            case 1:
                return new[] { 0.5f, 0.5f, 0f };
            case 2:
                return new[] { 0.34f, 0.33f, 0.33f };
            case 3:
                return new[] { 0f, 0.5f, 0.5f };
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
                return 7;
            case 2:
                return 7;
            case 3:
                return 12;
            default:
                return 12;
        }
    }

    public int GetClientsToSatisfy()
    {
        switch (CurrentLevel)
        {
            case 0:
                return 2;
            case 1:
                return 4;
            case 2:
                return 4;
            case 3:
                return 6;
            default:
                return 6;
        }
    }
}