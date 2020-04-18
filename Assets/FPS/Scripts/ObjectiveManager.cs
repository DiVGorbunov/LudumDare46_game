using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    List<Objective> m_Winning_Objectives = new List<Objective>();
    List<Objective> m_Failing_Objectives = new List<Objective>();

    public bool AreAllObjectivesCompleted()
    {
        if (m_Winning_Objectives.Count == 0)
            return false;

        for (int i = 0; i < m_Winning_Objectives.Count; i++)
        {
            // pass every objectives to check if they have been completed
            if (m_Winning_Objectives[i].isBlocking())
            {
                // break the loop as soon as we find one uncompleted objective
                return false;
            }
        }

        // found no uncompleted objective
        return true;
    }

    public bool IsAnyFailingObjectiveCompleted()
    {
        if (m_Failing_Objectives.Count == 0)
            return false;

        for (int i = 0; i < m_Failing_Objectives.Count; i++)
        {
            if (m_Failing_Objectives[i].isCompleted)
            {
                return true;
            }
        }

        return false;
    }

    public void RegisterObjective(Objective objective)
    {
        if (objective.isFailing)
        {
            m_Failing_Objectives.Add(objective);
        }
        else
        {
            m_Winning_Objectives.Add(objective);
        }
    }
}
