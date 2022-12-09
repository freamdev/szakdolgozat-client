using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AIBase : ScriptableObject
{
    protected PlayerController controller;
    protected BaseEnemy self;

    public void Initialize(PlayerController pwc, BaseEnemy be)
    {
        controller = pwc;
        self = be;
    }

    public abstract void Move();
}
