using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    public int number = 10;
    public GameObject client;
    public BoxCollider[] surfaces;
    public Vector3 clientTransform = new Vector3(1f, 1f, 1f);

    private const string ClientTag = "Client";

    void Start()
    {
        var overlapRadius = clientTransform.y * 2 + 0.5f;

        for (int i = 0; i < number; i++)
        {
            var newClientPosition = GetNewClientPosition(overlapRadius);
            var newClient = Instantiate(client, newClientPosition, Quaternion.identity);
            newClient.transform.localScale = clientTransform;
            newClient.tag = ClientTag;
        }
    }

    private Vector3 GetNewClientPosition(float overlapRadius)
    {
        do
        {
            var surface = GetRandomSurface();
            var position = new Vector3(surface.bounds.center.x + (Random.value - 0.5f) * (surface.bounds.size.x - clientTransform.x),
                   surface.bounds.center.y + surface.bounds.extents.y + clientTransform.y,
                   surface.bounds.center.z + (Random.value - 0.5f) * (surface.bounds.size.z - clientTransform.z));

            var overlaps = Physics.OverlapCapsule(position, clientTransform, overlapRadius);
            if (HasClient(overlaps))
            {
                continue;
            }

            return position;
        } while (true);
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
