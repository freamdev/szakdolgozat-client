using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Weapons/Strategies/Simple Weapon Strategy")]
public class SimpleFireStrategy : WeaponFireStrategyBase
{
    public override List<Projectile> CreateProjectile(PlayerWeaponController pwc)
    {
        var ret = new List<Projectile>();

        if (pwc.Canfire(cooldown))
        {
            var bullet = Instantiate(bulletPrefab, pwc.GetFirePositionTransform().position, pwc.GetFirePositionTransform().rotation);
            ret.Add(bullet.GetComponent<Projectile>());
            pwc.FireHappened();
        }

        return ret;
    }

    public override void Generate()
    {
        cooldown = Random.Range(0.1f, 1.5f);
    }
}
