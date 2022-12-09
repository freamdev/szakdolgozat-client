using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : EnemyAttackBehaviourBase
{
    public override void Attack(BaseEnemy attacker)
    {
        attacker.GetAnimator().SetTrigger("Attack");
    }

    public override void AttackHappened(BaseEnemy attacker, PlayerController player)
    {
        player.LooseHealth(attacker.GetDamage());
    }
}
