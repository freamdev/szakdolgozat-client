using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/AI/Strategies/Case AI")]
public class ChaseAI : AIBase
{
    public override void Move()
    {
        self.MoveTo(controller.transform.position);
    }
}
