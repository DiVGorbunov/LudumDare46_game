using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLauncherWeaponController : WeaponController
{
    List<Fruit> LastGatheredCart = new List<Fruit>();

    protected override void Update()
    {
        base.Update();

        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddItem((Fruit)0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddItem((Fruit)1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddItem((Fruit)2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddItem((Fruit)3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddItem((Fruit)4);
        }
    }

    public void AddItem(Fruit newItem)
    {
        LastGatheredCart.Add(newItem);
    }

    public void RemoveItem(Fruit item)
    {
        LastGatheredCart.Remove(item);
    }

    override public void SpawnBullets()
    {
        // spawn all bullets with random direction
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 shotDirection = GetShotDirectionWithinSpread(weaponMuzzle);
            ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirection));
            ProjectileCart cart = newProjectile.GetComponent<ProjectileCart>();
            cart.SetItems(LastGatheredCart);
            LastGatheredCart = new List<Fruit>();
            newProjectile.Shoot(this);
        }
    }
}
