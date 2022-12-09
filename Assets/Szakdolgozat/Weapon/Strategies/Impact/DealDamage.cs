using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "FPS/Weapons/Projectile Strategies/Deal Damage Strategy")]
public class DealDamage : WeaponImpactStrategyBase
{
    public override void HitTarget(BaseEnemy enemy)
    {
        enemy.LooseHealth(1);
    }
}
