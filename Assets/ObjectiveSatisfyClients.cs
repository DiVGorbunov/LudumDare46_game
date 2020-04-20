using UnityEngine;

public class ObjectiveSatisfyClients : MonoBehaviour
{
    public int minSatisfiedClients = 5;
    public bool useDifficultyManager = true;

    private int _satisfiedClients = 0;

    Objective m_Objective;

    void Start()
    {
        minSatisfiedClients = useDifficultyManager ? DifficultyManager.Instance.GetClientsToSatisfy() : minSatisfiedClients;

        m_Objective = GetComponent<Objective>();
        DebugUtility.HandleErrorIfNullGetComponent<Objective, ObjectiveSatisfyClients>(m_Objective, this, gameObject);

        var clientGenerator = FindObjectOfType<ClientGenerator>();
        DebugUtility.HandleErrorIfNullFindObject<ClientGenerator, ObjectiveSatisfyClients>(clientGenerator, this);
        clientGenerator.onClientSatisfy += OnClientSatisfy;

        if (string.IsNullOrEmpty(m_Objective.title))
            m_Objective.title = $"Satisfy {minSatisfiedClients} ${GetClientByNumber(minSatisfiedClients)} on the map.";

        if (string.IsNullOrEmpty(m_Objective.description))
            m_Objective.description = GetUpdatedCounterAmount();
    }

    void OnClientSatisfy()
    {
        if (m_Objective.isCompleted)
        {
            return;
        }

        _satisfiedClients++;

        if (_satisfiedClients == minSatisfiedClients)
        {
            m_Objective.CompleteObjective(string.Empty, GetUpdatedCounterAmount(), "Objective completed : " + m_Objective.title);
        }
        else
        {
            string notificationText = $"Another client was satisfied. Only {minSatisfiedClients - _satisfiedClients} remained.";
            m_Objective.UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        }
    }

    string GetUpdatedCounterAmount()
    {
        return $"{_satisfiedClients} / {minSatisfiedClients}";
    }

    string GetClientByNumber(int number)
    {
        if (number == 1)
        {
            return "client";
        }
        return "clients";
    }
}
