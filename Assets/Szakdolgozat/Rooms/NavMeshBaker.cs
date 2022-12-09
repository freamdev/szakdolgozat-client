using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;


public class NavMeshBaker : MonoBehaviour
{
    void Start()
    {
        var things = GetComponentsInChildren<NavMeshSurface>();
        foreach(var thing in things)
        {
            thing.BuildNavMesh();
        }
    }
}
