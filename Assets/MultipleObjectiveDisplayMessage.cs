using UnityEngine;

public class MultipleObjectiveDisplayMessage : MonoBehaviour
{
    [Tooltip("Prefab for the message")]
    public GameObject messagePrefab;
    [Tooltip("Delay before displaying the message")]
    public float delayBeforeShowing;

    public ObjectiveSatisfyClients objectiveSatisfyClients;
    public ObjectiveUnsatisfiedClients objectiveUnsatisfiedClients;

    float m_InitTime = float.NegativeInfinity;
    bool m_WasDisplayed;
    DisplayMessageManager m_DisplayMessageManager;

    void Start()
    {
        m_InitTime = Time.time;
        m_DisplayMessageManager = FindObjectOfType<DisplayMessageManager>();
        DebugUtility.HandleErrorIfNullFindObject<DisplayMessageManager, DisplayMessage>(m_DisplayMessageManager, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_WasDisplayed)
            return;

        if (Time.time - m_InitTime > delayBeforeShowing)
        {
            var messageInstance = Instantiate(messagePrefab, m_DisplayMessageManager.DisplayMessageRect);
            var notification = messageInstance.GetComponent<NotificationToast>();
            if (notification)
            {
                notification.Initialize(GetMessage());
            }

            m_WasDisplayed = true;
        }
    }

    string GetMessage()
    {
        string message = "Satisfy " + objectiveSatisfyClients.minSatisfiedClients + " clients !";

        if (objectiveUnsatisfiedClients.maxUnsatisfiedClients > 0)
        {
            message += " And don't let " + objectiveUnsatisfiedClients.maxUnsatisfiedClients + " clients be unsatisfied !";
        }

        return message;
    }
}
