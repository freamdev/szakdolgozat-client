using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    // TODO Link:
    // https://cp-algorithms.com/data_structures/disjoint_set_union.html

    [SerializeField]
    GameObject portalReplacement;

    [SerializeField]
    int mapSize;
    [SerializeField]
    List<RoomController> mapPrefabs;
    RoomController[,] map;

    List<RoomIndex> rooms;
    List<int> parent;
    List<RoomBorder> borders;
    List<RoomBorder> selectedDoors;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        rooms = new List<RoomIndex>();
        parent = new List<int>();
        borders = new List<RoomBorder>();
        selectedDoors = new List<RoomBorder>();

        int minRandom = 0;
        int maxRandom = 1000000;

        map = new RoomController[mapSize, mapSize];

        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                var newRoom = Instantiate(SelectRoomPrefab());
                rooms.Add(new RoomIndex { CoordX = x, CoordY = y });

                int currentIndex = parent.Count;
                parent.Add(currentIndex);
                map[x, y] = newRoom;

                newRoom.transform.position = new Vector3(x * 42, 0, y * 42);

                if ((x + 1) < mapSize)
                {
                    borders.Add(new RoomBorder
                    {
                        a = new RoomIndex { CoordX = x, CoordY = y },
                        b = new RoomIndex { CoordX = x + 1, CoordY = y },
                        Weight = Random.Range(minRandom, maxRandom)
                    });
                }

                if ((y + 1) < mapSize)
                {
                    borders.Add(new RoomBorder
                    {
                        a = new RoomIndex { CoordX = x, CoordY = y },
                        b = new RoomIndex { CoordX = x, CoordY = y + 1 },
                        Weight = Random.Range(minRandom, maxRandom)
                    });
                }
            }
        }

        foreach(var border in borders.OrderBy(o => o.Weight))
        {
            int a = rooms.FindIndex(r => r.CoordX == border.a.CoordX && r.CoordY == border.a.CoordY);
            int b = rooms.FindIndex(r => r.CoordX == border.b.CoordX && r.CoordY == border.b.CoordY);

            var wasUnion = UnionSets(a, b);

            if (wasUnion)
            {
                selectedDoors.Add(border);
                if(border.b.CoordX > border.a.CoordX)
                {
                    var roomA = map[border.a.CoordX, border.a.CoordY];
                    var roomB = map[border.b.CoordX, border.b.CoordY];

                    var newPortal = Instantiate(portalReplacement);
                    newPortal.transform.position = roomA.right.transform.position;
                    newPortal.transform.rotation = roomA.right.transform.rotation;

                    Destroy(roomA.right.gameObject);
                    Destroy(roomB.left.gameObject);
                }

                if(border.b.CoordY > border.a.CoordY)
                {
                    var roomA = map[border.a.CoordX, border.a.CoordY];
                    var roomB = map[border.b.CoordX, border.b.CoordY];

                    var newPortal = Instantiate(portalReplacement);
                    newPortal.transform.position = roomA.bottom.transform.position;
                    newPortal.transform.rotation = roomA.bottom.transform.rotation;

                    Destroy(roomA.bottom.gameObject);
                    Destroy(roomB.top.gameObject);
                }
            }
            else
            {
                //Possible to add extra connections here to create loops
            }
        }
    }

    int FindSetParent(int v)
    {
        if (v != parent[v])
        {
            parent[v] = FindSetParent(parent[v]);
        }
        return parent[v];
    }

    bool UnionSets(int a, int b)
    {
        a = FindSetParent(a);
        b = FindSetParent(b);
        if (a == b)
        {
            return false;
        }
        parent[b] = a;
        return true;
    }

    RoomController SelectRoomPrefab()
    {
        return mapPrefabs.OrderBy(o => System.Guid.NewGuid()).First();
    }
}

public class RoomBorder
{
    public RoomIndex a { get; set; }
    public RoomIndex b { get; set; }
    public int Weight { get; set; }
}

public class RoomIndex
{
    public int CoordX { get; set; }
    public int CoordY { get; set; }
}