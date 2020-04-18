using UnityEngine;

public class ObjectiveUnsatisfiedClients : MonoBehaviour
{
    public ClientGenerator clientGenerator;
    public int maxUnsatisfiedClients = 5;

    private int _unsatisfiedClients = 0;

    void Start()
    {
        clientGenerator.onClientDie += () =>
        {
            _unsatisfiedClients++;
        };
    }

    public bool IsCompleted()
    {
        return _unsatisfiedClients < maxUnsatisfiedClients;
    }
}
