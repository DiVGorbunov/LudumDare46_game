using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartLauncherWeaponController : WeaponController
{
    List<int> LastGatheredCart = new List<int>();

    protected override void Update()
    {
        base.Update();

        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            AddItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddItem(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddItem(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddItem(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AddItem(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            AddItem(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AddItem(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AddItem(9);
        }
    }

    public void AddItem(int newItem)
    {
        LastGatheredCart.Add(newItem);
    }

    public void RemoveItem(int item)
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
            LastGatheredCart = new List<int>();
            newProjectile.Shoot(this);
        }
    }
}
