using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    int m_mainDungeonSize = 15;
    [SerializeField]
    int m_lockedRoomsNb = 3;
    [SerializeField]
    int m_secondaryPathsSize = 3;

    [SerializeField]
    int m_tries = 20;
    [SerializeField]
    Vector2 m_tileSize = Vector2.zero;

    [SerializeField]
    RoomsPool m_pool;
    [SerializeField]
    List<DungeonLevel> m_levels;

    [SerializeField]
    TilesetSwapper Swapper;
    [SerializeField]
    List<string> TilesetIds;
    Tree _tree;

    int _curLevelIndex = 0;

    private void Start()
    {
        GameObject temp = Instantiate(m_pool.rooms[0].prefab);
        m_tileSize = temp.GetComponent<Room>().GetWorldBounds().size;
        Destroy(temp);

        CreateLevel(_curLevelIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateDungeon();
            InstanciateDungeon();
        }
    }

    void CreateLevel(int index)
    {
        SetLevelData(m_levels[index]);
        GenerateDungeon();
        InstanciateDungeon();
    }

    void GenerateDungeon()
    {
        //Base Path
        _tree = new();

        Node startingNode = new();
        startingNode.position = Vector2.zero;
        startingNode.tags.Add(RoomTag.StartRoom);
        _tree.nodes.Add(startingNode.position, startingNode);

        for (int i = 0; i < m_tries; i++)
        {
            Tree temp = new(_tree);

            if (temp.GeneratePath(m_mainDungeonSize, startingNode.position, RoomTag.EndRoom))
            {
                _tree = temp;
                break;
            }

            if(i == m_tries - 1)
            {
                Debug.LogError("failed generation");
                return;
            }
        }

        #region LockedRooms
        List<Vector2> nodesPos = _tree.GetRandomNodes(m_lockedRoomsNb);

        foreach (Vector2 nodePos in nodesPos)
        {
            for (int i = 0; i < m_tries; i++)
            {
                Tree temp = new(_tree);

                if (temp.GeneratePath(m_secondaryPathsSize, nodePos, RoomTag.KeyRoom))
                {
                    _tree = temp;
                    Node node = _tree.nodes[nodePos];
                    foreach (DoorPos doors in node.doors)
                    {
                        bool hasFoundDoorToLock = false;

                        switch (doors)
                        {
                            case DoorPos.Up:
                                {
                                    if (node.upConnection != null && node.upConnection.prevNode == node)
                                    {
                                        node.upConnection.hasLock = true;
                                        hasFoundDoorToLock = true;
                                    }
                                    break;
                                }
                            case DoorPos.Right:
                                {
                                    if (node.rightConnection != null && node.rightConnection.prevNode == node)
                                    {
                                        node.rightConnection.hasLock = true;
                                        hasFoundDoorToLock = true;
                                    }
                                    break;
                                }
                            case DoorPos.Down:
                                {
                                    if (node.downConnection != null && node.downConnection.prevNode == node)
                                    {
                                        node.downConnection.hasLock = true;
                                        hasFoundDoorToLock = true;
                                    }
                                    break;
                                }
                            case DoorPos.Left:
                                {
                                    if (node.leftConnection != null && node.leftConnection.prevNode == node)
                                    {
                                        node.leftConnection.hasLock = true;
                                        hasFoundDoorToLock = true;
                                    }
                                    break;
                                }
                        }

                        if (hasFoundDoorToLock)
                        {
                            break;
                        }
                    }

                    break;
                }

                if (i == m_tries - 1)
                {
                    Debug.LogError("failed generation");
                    return;
                }
            }
        }
        #endregion

    }

    void InstanciateDungeon()
    {
        GameObject prevRoom = null;

        foreach(KeyValuePair< Vector2, Node > node in _tree.nodes)
        {
            Node nodeValue = node.Value;
            GameObject objectToInstantiate = GetPrefabOfType(nodeValue, prevRoom);
            prevRoom = objectToInstantiate;
            if(objectToInstantiate == null)
            {
                Debug.LogError("NoFittingRoomFound");
                return;
            }
            Debug.Log("passed");
            GameObject room = Instantiate(objectToInstantiate, node.Key * m_tileSize, objectToInstantiate.transform.rotation, transform);

            #region Set locks
            if (room.TryGetComponent(out Room roomScript))
            {
                foreach(DoorPos door in nodeValue.doors)
                {
                    switch (door)
                    {
                        case DoorPos.Up :
                            {
                                if(nodeValue.upConnection != null && nodeValue.upConnection.hasLock)
                                {
                                    roomScript.GetDoor(Utils.ORIENTATION.NORTH).SetState(Door.STATE.CLOSED);
                                }
                                break;
                            }
                        case DoorPos.Right:
                            {
                                if (nodeValue.rightConnection != null && nodeValue.rightConnection.hasLock)
                                {
                                    roomScript.GetDoor(Utils.ORIENTATION.EAST).SetState(Door.STATE.CLOSED);
                                }
                                break;
                            }
                        case DoorPos.Down:
                            {
                                if (nodeValue.downConnection != null && nodeValue.downConnection.hasLock)
                                {
                                    roomScript.GetDoor(Utils.ORIENTATION.SOUTH).SetState(Door.STATE.CLOSED);
                                }
                                break;
                            }
                        case DoorPos.Left:
                            {
                                if (nodeValue.leftConnection != null && nodeValue.leftConnection.hasLock)
                                {
                                    roomScript.GetDoor(Utils.ORIENTATION.WEST).SetState(Door.STATE.CLOSED);
                                }
                                break;
                            }
                    }
                }
            }
            #endregion
        }
    }

    public void SetNextLevel()
    {
        _curLevelIndex += 1;
        if(_curLevelIndex >= m_levels.Count)
        {
            Debug.LogError("No next level to load");
            return;
        }
        Swapper.SetVariation(TilesetIds[_curLevelIndex]);
        DeleteDungeon();
        CreateLevel(_curLevelIndex);
    }

    public void DeleteDungeon()
    {
        int childNb = transform.childCount;
        for(int i = 0; i < childNb; i++)
        {
            Destroy(transform.GetChild(0));
        }
    }

    void SetLevelData(DungeonLevel level)
    {
        m_mainDungeonSize = level.mainDungeonSize;
        m_lockedRoomsNb = level.lockedRoomsNb;
        m_secondaryPathsSize = level.secondaryPathsSize;
    }

    GameObject GetPrefabOfType(Node node, GameObject prevRoom)
    {
        List<PoolRoom> validRooms = new(m_pool.rooms);

        #region Tags
        foreach (RoomTag tag in node.tags)
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
        #endregion

        #region Doors
        foreach (DoorPos pos in node.doors)
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
        #endregion

        #region Room Select
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
        #endregion
    }
}
