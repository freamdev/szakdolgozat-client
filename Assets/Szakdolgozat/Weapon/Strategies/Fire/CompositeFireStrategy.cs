using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Weapons/Strategies/Composite Weapon Fire Strategy")]
public class CompositeFireStrategy : WeaponFireStrategyBase
{
    public List<WeaponFireStrategyBase> strategies;

    public override List<Projectile> CreateProjectile(PlayerWeaponController pwc)
    {
        var projectiles = new List<Projectile>();

        foreach (var strategy in strategies)
        {
            projectiles.AddRange(strategy.CreateProjectile(pwc));
        }

        return projectiles;
    }

    public override void Generate()
    {
        foreach(var strategy in strategies)
        {
            strategy.Generate();
        }
    }
}
