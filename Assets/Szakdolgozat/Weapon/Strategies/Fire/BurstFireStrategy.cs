using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Weapons/Strategies/Burst Weapon Strategy")]
public class BurstFireStrategy : WeaponFireStrategyBase
{
    public float minSpread;
    public float maxSpread;

    public int minBulletCount;
    public int maxBulletCount;

    float spread;
    int numberOfBullets;

    public override List<Projectile> CreateProjectile(PlayerWeaponController pwc)
    {
        var ret = new List<Projectile>();

        if (pwc.Canfire(cooldown))
        {
            for (int i = 0; i < numberOfBullets; i++)
            {
                var spawnPosition = Random.insideUnitSphere * spread + pwc.GetFirePositionTransform().position;
                var bullet = Instantiate(bulletPrefab, spawnPosition, pwc.GetFirePositionTransform().rotation);
                ret.Add(bullet.GetComponent<Projectile>());
            }
            pwc.FireHappened();
        }

        return ret;
    }

    public override void Generate()
    {
        cooldown = Random.Range(0.1f, 1.5f);
        numberOfBullets = Random.Range(minBulletCount, maxBulletCount);
        spread = Random.Range(minSpread, maxSpread);
    }
}
