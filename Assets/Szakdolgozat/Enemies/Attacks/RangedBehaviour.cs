using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : EnemyAttackBehaviourBase
{

    [SerializeField]
    GameObject projectilePrefab;
    [SerializeField]
    float projectileSpeed;
    [SerializeField]
    float projectileLifetime;

    public override void Attack(BaseEnemy attacker)
    {
        attacker.GetAnimator().SetTrigger("Shoot");
    }

    public override void AttackHappened(BaseEnemy attacker, PlayerController target)
    {
        var newInstance = GameObject.Instantiate(projectilePrefab);
        newInstance.GetComponent<EnemyProjectile>().Initialize(attacker.GetDamage());
        attacker.StartCoroutine(ProjectileMovement(newInstance));
    }

    IEnumerator ProjectileMovement(GameObject projectile)
    {
        var t = .0f;
        while(t < projectileLifetime && projectile != null)
        {
            var deltaTime = Time.deltaTime;
            projectile.transform.position += projectile.transform.forward * deltaTime * projectileSpeed;
            yield return new WaitForSeconds(deltaTime);
        }

        if(projectile != null)
        {
            Destroy(projectile.gameObject);
        }
    }
}
