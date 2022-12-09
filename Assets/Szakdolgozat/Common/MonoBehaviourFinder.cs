using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MonoBehaviourFinder<T> where T : MonoBehaviour
{
    static List<T> behaviours;

    public static T FindBehaviour()
    {
        var behaviour = behaviours.FirstOrDefault(f => f != null && f.GetType() == typeof(T));
        if(behaviour == null)
        {
            behaviour = GameObject.FindObjectOfType<T>();
            behaviours.Add(behaviour);
        }
        return behaviour;
    }
}
