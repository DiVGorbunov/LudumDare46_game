using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    public int number = 10;
    public GameObject client;
    public BoxCollider surface;
    public Vector3 clientTransform = new Vector3(1f, 1f, 1f);

    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            var newClient = Instantiate(client,
                new Vector3(surface.center.x + (Random.value - 0.5f) * (surface.bounds.size.x - clientTransform.x),
                    surface.center.y + surface.bounds.extents.y + clientTransform.y,
                    surface.center.z + (Random.value - 0.5f) * (surface.bounds.size.z - clientTransform.z)), Quaternion.identity);
            newClient.transform.localScale = clientTransform;
        }
    }
}
