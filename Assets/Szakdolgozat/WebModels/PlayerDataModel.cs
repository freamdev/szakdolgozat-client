using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataModel
{
    public string? id;
    public string playerId;
    public int passedFloor;
    public int killedEnemies;
    public float time;
}