using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    public int number = 10;
    public GameObject client;
    public BoxCollider[] surfaces;
    public Vector3 clientTransform = new Vector3(1f, 1f, 1f);

    private const string ClientTag = "Client";
    private const int PlacementAttempts = 5;

    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            Vector3 newClientPosition;
            if (TryGetNewClientPosition(out newClientPosition))
            {
                var newClient = Instantiate(client, newClientPosition, Quaternion.identity);
                newClient.transform.localScale = clientTransform;
                newClient.tag = ClientTag;
            }
        }
    }

    private bool TryGetNewClientPosition(out Vector3 position)
    {
        int attempts = 0;
        do
        {
            var surface = GetRandomSurface();
            position = new Vector3(surface.bounds.center.x + (Random.value - 0.5f) * (surface.bounds.size.x - clientTransform.x),
                   surface.bounds.center.y + surface.bounds.extents.y + clientTransform.y,
                   surface.bounds.center.z + (Random.value - 0.5f) * (surface.bounds.size.z - clientTransform.z));

            var overlaps = Physics.OverlapCapsule(position, clientTransform, clientTransform.y / 2f);
            if (HasClient(overlaps))
            {
                attempts++;
                continue;
            }

            return true;
        } while (attempts < PlacementAttempts);

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
            if (overlaps[i].gameObject.CompareTag(ClientTag))
            {
                return true;
            }
        }

        return false;
    }
}
