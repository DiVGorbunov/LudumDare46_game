using UnityEngine;
using UnityEngine.Events;

public class ClientGenerator : MonoBehaviour
{
    public int number = 10;
    public GameObject client;
    public BoxCollider[] surfaces;
    public Vector3 clientTransformScale = new Vector3(1f, 1f, 1f);
    public int placementAttempts = 5;
    public bool randomizeHealth = false;
    public UnityAction onClientUnsatisfy;

    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 newClientPosition;
            if (TryGetNewClientPosition(out newClientPosition))
            {
                var newClient = Instantiate(client, newClientPosition, Quaternion.identity, gameObject.transform);

                newClient.transform.localScale = clientTransformScale;
                ConfigureNewClient(newClient);
            }
        }
    }

    private void ConfigureNewClient(GameObject newClient)
    {
        if (randomizeHealth)
        {
            var controller = newClient.GetComponent<ClientController>();
            if (controller != null)
            {
                controller.secondsToLive += Random.value * controller.secondsToLive;
            }
        }

        var health = newClient.GetComponent<Health>();
        if (health != null)
        {
            health.onDie += () =>
            {
                if (onClientUnsatisfy != null)
                {
                    onClientUnsatisfy.Invoke();
                }
                Destroy(newClient);
            };
        }
    }

    private bool TryGetNewClientPosition(out Vector3 position)
    {
        int attempts = 0;
        do
        {
            var surface = GetRandomSurface();
            position = new Vector3(surface.bounds.center.x + (Random.value - 0.5f) * (surface.bounds.size.x - clientTransformScale.x),
                   surface.bounds.center.y + surface.bounds.extents.y + clientTransformScale.y,
                   surface.bounds.center.z + (Random.value - 0.5f) * (surface.bounds.size.z - clientTransformScale.z));

            var overlaps = Physics.OverlapCapsule(position, clientTransformScale, clientTransformScale.y / 2f);
            if (HasClient(overlaps))
            {
                attempts++;
                continue;
            }

            return true;
        } while (attempts < placementAttempts);

        position = Vector3.zero;
        return false;
    }

    private BoxCollider GetRandomSurface()
    {
        var step = 1f / surfaces.Length;
        var value = Random.value;

        int i = 0;
        while (value > step * (i + 1))
        {
            i++;
        }

        return surfaces[i];
    }

    private bool HasClient(Collider[] overlaps)
    {
        for (int i = 0; i < overlaps.Length; i++)
        {
            if (overlaps[i].gameObject.CompareTag(client.tag))
            {
                return true;
            }
        }

        return false;
    }
}
