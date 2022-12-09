using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponFireStrategyBase : ScriptableObject
{
    [SerializeField]
    protected Projectile bulletPrefab;

    [SerializeField]
    [Range(0.1f,1.5f)]
    protected float cooldown;

    public abstract List<Projectile> CreateProjectile(PlayerWeaponController pwc);
    public abstract void Generate();
}
