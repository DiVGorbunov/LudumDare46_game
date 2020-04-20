using UnityEngine;

[RequireComponent(typeof(Objective))]
public class ObjectiveUnsatisfiedClients : MonoBehaviour
{
    public int maxUnsatisfiedClients = 5;

    private int _unsatisfiedClients = 0;

    Objective m_Objective;

    void Start()
    {
        m_Objective = GetComponent<Objective>();
        DebugUtility.HandleErrorIfNullGetComponent<Objective, ObjectiveUnsatisfiedClients>(m_Objective, this, gameObject);

        var clientGenerator = FindObjectOfType<ClientGenerator>();
        DebugUtility.HandleErrorIfNullFindObject<ClientGenerator, ObjectiveUnsatisfiedClients>(clientGenerator, this);
        clientGenerator.onClientUnsatisfy += OnClientUnsatisfy;

        if (string.IsNullOrEmpty(m_Objective.title))
            m_Objective.title = $"Don't get more than {maxUnsatisfiedClients} unsatisfied {GetClientByNumber(maxUnsatisfiedClients)}.";

        if (string.IsNullOrEmpty(m_Objective.description))
            m_Objective.description = GetUpdatedCounterAmount();
    }

    void OnClientUnsatisfy()
    {
        if (m_Objective.isCompleted)
        {
            return;
        }

        _unsatisfiedClients++;

        if (_unsatisfiedClients == maxUnsatisfiedClients)
        {
            m_Objective.CompleteObjective(string.Empty, GetUpdatedCounterAmount(), "Objective failed : " + m_Objective.title);
        }
        else
        {
            string notificationText = $"Another client was unsatisfied. Only {maxUnsatisfiedClients - _unsatisfiedClients} remained.";
            m_Objective.UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
        }
    }

    string GetUpdatedCounterAmount()
    {
        return $"{_unsatisfiedClients} / {maxUnsatisfiedClients}";
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
