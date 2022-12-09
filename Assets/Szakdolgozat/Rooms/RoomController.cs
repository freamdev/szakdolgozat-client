using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    TextMeshProUGUI roomProgressText;

    public PortalCandidate top;
    public PortalCandidate bottom;
    public PortalCandidate left;
    public PortalCandidate right;

    public int maxEnemies;
    public List<BaseEnemy> enemyPrefabs;
    public List<Transform> spawnPositions;
    public bool firstRoom;

    public WeaponPikcup weaponPickupPrefab;
    public GameObject weaponDropPosition;

    List<BaseEnemy> enemies;

    float spawnTime;
    bool isPlayerInside;

    private void Awake()
    {
        roomProgressText = GameObject.Find("RoomProgressionText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {

        enemies = new List<BaseEnemy>();
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            if (enemies.Count < maxEnemies)
            {
                if (enemies.Where(w => w != null).Count() == 0 || spawnTime <= 0)
                {
                    spawnTime = 6;
                    SpawnEnemies();
                }
            }
            roomProgressText.text = $"Progress: {enemies.Where(w => w == null).Count()}/{maxEnemies}";
            spawnTime -= Time.deltaTime;

            if (enemies.Where(w => w == null).Count() == maxEnemies && !lootTrigger)
            {
                lootTrigger = true;
                SpawnLoot();
            }
        }
    }

    bool lootTrigger = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInside = false;
        }
    }

    protected void SpawnEnemies()
    {
        foreach (var spawn in spawnPositions)
        {
            if (enemies.Count < maxEnemies)
            {
                var selectedEnemyPrefab = enemyPrefabs.OrderBy(o => System.Guid.NewGuid()).First();
                var instance = Instantiate(selectedEnemyPrefab, spawn.position, Quaternion.identity);
                instance.transform.position = spawn.position;
                enemies.Add(instance);
            }
            else
            {
                break;
            }
        }
    }

    protected void SpawnLoot()
    {
        for (int i = 0; i < 5; i++)
        {
            var loot = GameObject.FindObjectOfType<LocalDataStore>().GetWeaponGenerator().GenerateWeapon();
            var newPickup = Instantiate(weaponPickupPrefab, weaponDropPosition.transform);
            newPickup.transform.position += Vector3.left * i;
            newPickup.Initialize(loot);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(weaponDropPosition.transform.position, 1);
    }
}
