using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    public int number = 10;
    public GameObject client;
    public BoxCollider[] surfaces;
    public Vector3 clientTransform = new Vector3(1f, 1f, 1f);

    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            var surface = GetRandomSurface();

            var newClient = Instantiate(client,
                new Vector3(surface.bounds.center.x + (Random.value - 0.5f) * (surface.bounds.size.x - clientTransform.x),
                    surface.bounds.center.y + surface.bounds.extents.y + clientTransform.y,
                    surface.bounds.center.z + (Random.value - 0.5f) * (surface.bounds.size.z - clientTransform.z)), Quaternion.identity);
            newClient.transform.localScale = clientTransform;
        }
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
}
