using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDungeonLevel", menuName = "ScriptableObject/DungeonLevel")]
public class DungeonLevel : ScriptableObject
{
    public int mainDungeonSize = 15;
    public int lockedRoomsNb = 3;
    public int secondaryPathsSize = 3;
}
