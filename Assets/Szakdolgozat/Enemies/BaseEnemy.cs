using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    AIBase AI;
    [SerializeField]
    Material death;

    [SerializeField]
    float health;
    [SerializeField]
    float damage;
    [SerializeField]
    bool isRanged;

    SkinnedMeshRenderer skinnedMeshRenderer;
    PlayerController weaponController;
    Animator animator;

    EnemyAttackBehaviourBase attackBehaviour;

    bool dying;

    public float GetDamage()
    {
        return damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attackBehaviour = GetComponent<EnemyAttackBehaviourBase>();
        weaponController = FindObjectOfType<PlayerController>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        AI = Instantiate(AI);
        AI.Initialize(weaponController, this);
    }

    public void MoveTo(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void OnAttack()
    {
        print("on attack");
        attackBehaviour.AttackHappened(this, weaponController);
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            if (agent.remainingDistance > 2.4f)
            {
                animator.SetBool("Chasing", true);
            }
            else
            {
                animator.SetBool("Chasing", false);
            }

            if (Vector3.Distance(weaponController.transform.position, transform.position) < 3f)
            {
                print(attackBehaviour);
                attackBehaviour.Attack(this);
            }
            else
            {
                AI.Move();
            }

            if (isRanged)
            {
                transform.LookAt(weaponController.transform.position);
            }
        }
        else
        {
            MoveTo(transform.position);
        }
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            other.GetComponent<Projectile>().HitSomething(this);
        }
    }

    public void LooseHealth(float v)
    {
        health -= v;
        if (health <= 0 && !dying)
        {
            StartCoroutine(DeathCR());
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    IEnumerator DeathCR()
    {
        var t = .0f;
        GameObject.FindObjectOfType<PlayerController>().KilledOne();
        GetComponent<CapsuleCollider>().enabled = false;
        dying = true;
        animator.SetBool("Chasing", false);
        animator.SetBool("Dead", true);
        skinnedMeshRenderer.sharedMaterial = death;
        while (t < 1.5f)
        {
            t += Time.deltaTime;
            skinnedMeshRenderer.sharedMaterial.SetFloat("_ClippingValue", t);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        Destroy(gameObject);
    }
}
