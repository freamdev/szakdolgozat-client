using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Weapons/Projectile Strategies/Fly forward Strategy")]
public class FlyStraightProjectileStrategy : WeaponProjectileStrategyBase
{
    public override void Iterate(Projectile projectile)
    {
        projectile.transform.position += projectile.transform.forward * Time.deltaTime * 60;
    }
}
