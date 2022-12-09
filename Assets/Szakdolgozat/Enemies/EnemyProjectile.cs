using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    float damage;

    public void Initialize(float d)
    {
        damage = d;
    }
}
