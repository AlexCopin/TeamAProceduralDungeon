using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    int m_dungeonSize = 15;

    [SerializeField]
    RoomsPool m_pool;

    [SerializeField]
    Vector2 m_tileSize = Vector2.zero;

    Tree _tree;

    private void Start()
    {
        GameObject temp = Instantiate(m_pool.rooms[0].prefab);
        m_tileSize = temp.GetComponent<Room>().GetWorldBounds().size;
        Destroy(temp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateDungeon();
            InstanciateDungeon();
        }
    }

    void GenerateDungeon()
    {
        Tree temp = new();
        Node startingNode = new();
        startingNode.position = Vector2.zero;
        //startingNode.tags.Add(RoomTag.StartRoom);
        temp.nodes.Add(startingNode.position, startingNode);

        for (int i = 0; i < 4; i++)
        {
            //Base Path
            if (temp.GeneratePath(m_dungeonSize, startingNode.position))
            {
                _tree = temp;
                Debug.Log(_tree);
                return;
            }
        }

        Debug.LogError("failed generation");
    }

    void InstanciateDungeon()
    {
        GameObject prevRoom = null;

        foreach(KeyValuePair< Vector2, Node > node in _tree.nodes)
        {
            GameObject objectToInstantiate = GetPrefabOfType(node.Value, prevRoom);
            prevRoom = objectToInstantiate;
            if(objectToInstantiate == null)
            {
                Debug.LogError("NoFittingRoomFound");
                return;
            }
            Debug.Log("passed");
            Instantiate(objectToInstantiate, node.Key * m_tileSize, objectToInstantiate.transform.rotation, transform);
        }
    }

    GameObject GetPrefabOfType(Node node, GameObject prevRoom)
    {
        List<PoolRoom> validRooms = new(m_pool.rooms);

        foreach(RoomTag tag in node.tags)
        {
            List<PoolRoom> temp = new(validRooms);

            foreach (PoolRoom room in temp)
            {
                if (!room.tags.Contains(tag))
                {
                    validRooms.Remove(room);
                }
            }
        }

        foreach(DoorPos pos in node.doors)
        {
            List<PoolRoom> temp = new(validRooms);

            for (int i = 0; i < temp.Count; i++)
            {
                if(temp[i].DoorsNumber() != node.doors.Count)
                {
                    validRooms.Remove(temp[i]);
                    continue;
                }

                if (!temp[i].doors.Contains(pos))
                {
                    validRooms.Remove(temp[i]);

                    continue;
                }
            }
        }

        switch (validRooms.Count)
        {
            case 0:
                {
                    return null;
                }

            case 1:
                {
                    return validRooms[0].prefab;
                }

            default :
                {
                    if (prevRoom != null)
                    {
                        foreach (PoolRoom room in validRooms)
                        {
                            if (room.prefab == prevRoom)
                            {
                                validRooms.Remove(room);
                                break;
                            }
                        }
                    }

                    int index = Random.Range(0, validRooms.Count);

                    return validRooms[index].prefab;
                }
        }
    }
}
