using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Weapons/New Weapon Effect")]
public class WeaponEffect : ScriptableObject
{
    public WeaponFireStrategyBase weaponFireStrategy;
    public WeaponProjectileStrategyBase weaponProjectileStrategy;
    public WeaponImpactStrategyBase weaponImpactStrategy;

    public void Activate(PlayerWeaponController pwc)
    {
        var projectiles = weaponFireStrategy.CreateProjectile(pwc); 
        projectiles.ForEach(f => f.AddProjectileLogic(weaponProjectileStrategy));
        projectiles.ForEach(f => f.AddImpactLogic(weaponImpactStrategy));
    }
}
