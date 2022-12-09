using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponImpactStrategyBase : ScriptableObject
{
    public abstract void HitTarget(BaseEnemy enemy);
}
