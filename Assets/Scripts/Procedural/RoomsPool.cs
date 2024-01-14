using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPool", menuName = "ScriptableObject/RoomsPool")]
public class RoomsPool : ScriptableObject
{
    public List<PoolRoom> rooms;
}

[Serializable]
public class PoolRoom
{
    public GameObject prefab;

    public List<DoorPos> doors;
    public List<RoomTag> tags;

    public int DoorsNumber()
    {
        if(doors != null)
        {
            return doors.Count;
        }

        return 0;
    }
}
