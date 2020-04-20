using UnityEngine;
using UnityEngine.Events;

public class ClientGenerator : MonoBehaviour
{
    public GameObject client;
    public BoxCollider[] surfaces;
    public Vector3 clientTransformScale = new Vector3(1f, 1f, 1f);
    public int placementAttempts = 5;

    public int number = 10;
    public float[] fruitNumberProbabilities = { 0.34f, 0.33f, 033f };
    public (float, float) clientTimelifeGaussian = (60f, 0f);
    public bool useDifficultyManager = true;

    public UnityAction onClientSatisfy;
    public UnityAction onClientUnsatisfy;

    void Start()
    {
        number = useDifficultyManager ? DifficultyManager.Instance.GetNumberOfClients() : number;
        clientTimelifeGaussian = useDifficultyManager ? DifficultyManager.Instance.GetClientTimelifeGaussian() : clientTimelifeGaussian;
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
        var newClientController = newClient.GetComponent<ClientController>();
        newClientController.fruitNumberProbabilities = useDifficultyManager ?
            DifficultyManager.Instance.GetClientFruitNumberProbs() :
            fruitNumberProbabilities;

        var health = NextGaussian(clientTimelifeGaussian.Item1, clientTimelifeGaussian.Item2);
        newClientController.SetHealth(health);

        newClientController.onUnsatisfy += () =>
        {
            if (onClientUnsatisfy != null)
            {
                onClientUnsatisfy.Invoke();
            }
        };
        newClientController.onSatisfy += () =>
        {
            if (onClientSatisfy != null)
            {
                onClientSatisfy.Invoke();
            }
        };
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

    public static float NextGaussian()
    {
        float v1, v2, s;
        do
        {
            v1 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            v2 = 2.0f * Random.Range(0f, 1f) - 1.0f;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1.0f || s == 0f);

        s = Mathf.Sqrt((-2.0f * Mathf.Log(s)) / s);

        return v1 * s;
    }

    public static float NextGaussian(float mean, float standardDeviation)
    {
        return mean + NextGaussian() * standardDeviation;
    }
}
