using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponProjectileStrategyBase : ScriptableObject
{

   public abstract void Iterate(Projectile p);
}
