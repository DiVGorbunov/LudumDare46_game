using UnityEngine;

public class CartLauncherWeaponController : WeaponController
{
    private FruitBucket fruitBucket;

    void Start()
    {
        fruitBucket = FindObjectOfType<FruitBucket>();
    }

    override public void SpawnBullets()
    {
        // spawn all bullets with random direction
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 shotDirection = GetShotDirectionWithinSpread(weaponMuzzle);
            ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirection));
            ProjectileCart cart = newProjectile.GetComponent<ProjectileCart>();
            cart.SetItems(fruitBucket.GetBucketFruits());
            cart.onClientSatisfied += () =>
            {
                fruitBucket.removeAllFruits();
            };

            newProjectile.Shoot(this);
        }
    }
}
