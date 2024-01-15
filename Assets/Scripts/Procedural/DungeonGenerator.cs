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
        startingNode.tags.Add(RoomTag.StartRoom);

        for (int i = 0; i < 4; i++)
        {
            if (temp.GeneratePath(m_dungeonSize, startingNode))
            {
                _tree = temp;
                return;
            }
        }

        Debug.LogError("failed generation");
    }

    void InstanciateDungeon()
    {
        foreach(KeyValuePair< Vector2, Node > node in _tree.nodes)
        {
            GameObject objectToInstantiate = GetPrefabOfType(node.Value);
            if(objectToInstantiate == null)
            {
                Debug.LogError("NoFittingRoomFound");
                return;
            }
            Instantiate(objectToInstantiate, node.Key * m_tileSize, objectToInstantiate.transform.rotation, transform);
        }
    }

    GameObject GetPrefabOfType(Node node)
    {
        List<PoolRoom> validRooms = m_pool.rooms;

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

            foreach (PoolRoom room in temp)
            {
                if (!room.doors.Contains(pos))
                {
                    validRooms.Remove(room);
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
                    int index = Random.Range(0, validRooms.Count);

                    return validRooms[index].prefab;

                }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(_tree != null)
        {
            foreach (KeyValuePair<Vector2, Node> pos in _tree.nodes)
            {
                Gizmos.DrawCube(pos.Key, Vector3.one);
            }
        }
    }
#endif
}
