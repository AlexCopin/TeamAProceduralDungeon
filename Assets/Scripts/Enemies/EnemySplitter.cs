using System;
using UnityEngine;
using System;

public class EnemySplitter : Enemy
{
    [Header("Splitter")]
    [SerializeField]
    GameObject PrefabLittle;
    [SerializeField]
    int Count;
    [SerializeField]
    float offset;
    [SerializeField]
    float offsetRandom;
    protected override void Death()
    {
        base.Death();
        for(int i = 0; i < Count; i++)
        {
            float OffsetX = UnityEngine.Random.Range(0, offsetRandom) + offset;
            float OffsetY = UnityEngine.Random.Range(0, offsetRandom) + offset;
            int XSign = UnityEngine.Random.Range(0,2);
            int YSign = UnityEngine.Random.Range(0,2);
            OffsetX = XSign == 0 ? OffsetX * 1 : OffsetX * -1;
            OffsetY = YSign == 0 ? OffsetY * 1 : OffsetY * -1;
            Vector3 pos = new Vector3(transform.position.x + OffsetX, transform.position.y + OffsetY, transform.position.z);
            Instantiate(PrefabLittle, pos, transform.rotation);
        }
    }
}
