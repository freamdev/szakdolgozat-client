using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/AI/Strategies/Distance AI")]
public class KeepDistanceAI : AIBase
{
    public float minDistance;

    public override void Move()
    {
        if(Vector3.Distance(controller.transform.position, self.transform.position) < minDistance)
        {
            var dir = (self.transform.position - controller.transform.position).normalized * minDistance;

            self.MoveTo(self.transform.position + dir);
        }
    }
}
