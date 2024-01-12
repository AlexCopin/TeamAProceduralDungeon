using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    int m_dungeonSize = 15;

    Tree _tree;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateDungeon();
        }
    }

    void GenerateDungeon()
    {
        for(int i = 0; i < 4; i++)
        {
            Tree temp = new();
            if (temp.GeneratePath(m_dungeonSize, Vector2.zero))
            {
                _tree = temp;
                return;
            }
        }

        Debug.LogError("failed generation");
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
