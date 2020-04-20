using UnityEngine;

public class CartLauncherWeaponController : WeaponController
{
    private FruitBucket _fruitBucket;

    public void RegisterFruitBucket(FruitBucket fruitBucket)
    {
        _fruitBucket = fruitBucket;
    }

    override public void SpawnBullets()
    {
        // spawn all bullets with random direction
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Vector3 shotDirection = GetShotDirectionWithinSpread(weaponMuzzle);
            ProjectileBase newProjectile = Instantiate(projectilePrefab, weaponMuzzle.position, Quaternion.LookRotation(shotDirection));
            ProjectileCart cart = newProjectile.GetComponent<ProjectileCart>();
            cart.SetItems(_fruitBucket.GetBucketFruits());
            cart.onClientSatisfied += () =>
            {
                _fruitBucket.removeAllFruits();
            };

            newProjectile.Shoot(this);
        }
    }
}
