using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct SpawnerData
{
    [SerializeField]
    public byte _Count;
    [SerializeField]
    public GameObject EnemyType;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    public List<SpawnerData> _datas;

    //USE THE DIFFICULTY CURVE LATER
    [SerializeField, Range(0, 1)]
    public float _difficulty;

    [SerializeField]
    public List<Transform> _spawnPoints;

    private bool _isSpawned = false;

    // Dungeon location
    private Room _room = null;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Room room in Room.allRooms)
        {
            if (room.Contains(transform.position))
            {
                _room = room;
            }
        }
        _room.OnRoomEntered += OnRoomActivation;
        //Apply difficulty for number of enemies
        ApplyDifficulty(_difficulty);
    }

    public void ApplyDifficulty(float difficulty)
    {
        for (int i = 0; i < _datas.Count; i++)
        {
            SpawnerData data = _datas[i];
            data._Count += (byte)Mathf.Floor(data._Count * difficulty);
        }
    }

    void OnRoomActivation()
    {
        if(_isSpawned )
            return;

        foreach (SpawnerData data in _datas)
        {
            for (int i = 0; i < data._Count; ++i)
            {
                int RandomSpawn = UnityEngine.Random.Range(0, _spawnPoints.Count);
                Enemy enemy = Instantiate<GameObject>(data.EnemyType, _spawnPoints[RandomSpawn].position, Quaternion.identity, _room.transform).GetComponent<Enemy>();
                enemy.ApplyDifficulty(_difficulty);
            }
        }
        _isSpawned = true;
    }

    void OnDrawGizmos()
    {
        foreach(Transform t in _spawnPoints)
        {
            Gizmos.DrawIcon(t.position, "GizmoIcon", true);
        }
    }
}
