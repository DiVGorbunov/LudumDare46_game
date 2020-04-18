using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    public int number = 10;
    public Collider client;
    public BoxCollider surface;
    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(client,
                new Vector3(surface.center.x + (Random.value - 0.5f) * (surface.bounds.size.x - client.bounds.size.x),
                    1.5f,
                    surface.center.z + (Random.value - 0.5f) * (surface.bounds.size.z - client.bounds.size.z)), Quaternion.identity);
        }
    }
}
