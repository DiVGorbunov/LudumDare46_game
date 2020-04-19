using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCart : ProjectileStandard
{
    public string TargetTag = "Client";

    public List<Fruit> cartItems = new List<Fruit>();

    // Start is called before the first frame update
    void Start()
    {
        //cartItems = new List<int> { 1, 2, 3, 4 };
    }

    public void SetItems(List<Fruit> items)
    {
        cartItems = items;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    override protected void OnHit(Vector3 point, Vector3 normal, Collider collider) 
    {

        if (collider.gameObject.tag == TargetTag)
        {
            ClientController controller = collider.gameObject.GetComponent<ClientController>();
            if (controller)
            {
                controller.CheckRequirement(cartItems);
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
