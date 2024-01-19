using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    DungeonGenerator Generator;
    // Start is called before the first frame update
    void Start()
    {
        Generator = GameObject.FindObjectOfType<DungeonGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Generator.SetNextLevel();
    }
}
