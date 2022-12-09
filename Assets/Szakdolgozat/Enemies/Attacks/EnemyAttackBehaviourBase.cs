using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackBehaviourBase : MonoBehaviour
{
    public abstract void Attack(BaseEnemy attacker);
    public abstract void AttackHappened(BaseEnemy attacker, PlayerController target);
}
