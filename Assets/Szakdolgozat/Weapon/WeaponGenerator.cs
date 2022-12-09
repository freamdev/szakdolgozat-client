using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/Data/Weapon Generator")]
public class WeaponGenerator : ScriptableObject
{
    List<WeaponFireStrategyBase> fireStrategies;
    List<WeaponProjectileStrategyBase> projectileStrategies;
    List<WeaponImpactStrategyBase> impactStrategies;

    public WeaponBase weaponPrefab;

    public List<GameObject> weaponGraphics;

    public void Load()
    {
        fireStrategies = Resources.LoadAll<WeaponFireStrategyBase>("Fire").ToList();
        projectileStrategies = Resources.LoadAll<WeaponProjectileStrategyBase>("Projectile").ToList();
        impactStrategies = Resources.LoadAll<WeaponImpactStrategyBase>("Impact").ToList();

        weaponPrefab = Resources.Load<WeaponBase>("PremadeWeapons\\BaseWeaponPrefab");
        weaponGraphics = Resources.LoadAll<GameObject>("WeaponGraphics").ToList();
    }

    public WeaponBase GenerateWeapon()
    {
        var newInstance = Instantiate(weaponPrefab);

        newInstance.infiniteWeapon = false;
        newInstance.weaponPrefab = weaponGraphics.OrderBy(o => System.Guid.NewGuid()).First();
        newInstance.clipSize = Random.Range(10, 100);

        var newWeaponEffect = new WeaponEffect
        {
            weaponFireStrategy = Instantiate(fireStrategies.First()),
            weaponProjectileStrategy = Instantiate(projectileStrategies.First()),
            weaponImpactStrategy = Instantiate(impactStrategies.First()),
        };

        newWeaponEffect.weaponFireStrategy.Generate();

        newInstance.weaponEffects.Add(newWeaponEffect);

        return newInstance;
    }
}
