using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    WeaponProjectileStrategyBase projectileStrategy;
    WeaponImpactStrategyBase impactStrategy;

    public void AddProjectileLogic(WeaponProjectileStrategyBase weaponProjectileStrategy)
    {
        projectileStrategy = weaponProjectileStrategy;
    }

    public void AddImpactLogic(WeaponImpactStrategyBase weaponImpactStrategy)
    {
        impactStrategy = weaponImpactStrategy;
    }

    private void Update()
    {
        if(projectileStrategy != null)
        {
            projectileStrategy.Iterate(this);
        }
    }

    public void HitSomething(BaseEnemy enemy)
    {
        impactStrategy.HitTarget(enemy);
    }

}
