using UnityEngine;

public class ClientGenerator : MonoBehaviour
{
    public int number = 10;
    public Transform prefab;
    public BoxCollider surface;
    void Start()
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(prefab,
                new Vector3(Random.value * surface.bounds.size.x - surface.bounds.size.x / 2f,
                    1.5f,
                    Random.value * surface.bounds.size.z - surface.bounds.size.z / 2f), Quaternion.identity);
        }
    }
}
