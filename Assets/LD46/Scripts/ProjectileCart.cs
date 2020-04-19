using UnityEngine;
using UnityEngine.Events;

public class ProjectileCart : ProjectileStandard
{
    private const string TargetTag = "Client";

    public UnityAction onClientSatisfied;

    private Fruit[] cartItems;

    public void SetItems(Fruit[] items)
    {
        cartItems = items;
    }

    override protected void OnHit(Vector3 point, Vector3 normal, Collider collider) 
    {

        if (collider.gameObject.tag == TargetTag)
        {
            ClientController controller = collider.gameObject.GetComponent<ClientController>();
            if (controller)
            {
                if (controller.CheckRequirement(cartItems))
                {
                    if (onClientSatisfied != null)
                    {
                        onClientSatisfied.Invoke();
                    }
                }
            }

            // Should check if our client is supplied by cart
        }

        // impact vfx
            if (impactVFX)
        {
            GameObject impactVFXInstance = Instantiate(impactVFX, point + (normal * impactVFXSpawnOffset), Quaternion.LookRotation(normal));
            if (impactVFXLifetime > 0)
            {
                Destroy(impactVFXInstance.gameObject, impactVFXLifetime);
            }
        }

        // impact sfx
        if (impactSFXClip)
        {
            AudioUtility.CreateSFX(impactSFXClip, point, AudioUtility.AudioGroups.Impact, 1f, 3f);
        }

        // Self Destruct
        Destroy(this.gameObject);
    }
}
