using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Stats : MonoBehaviour
{
    public string Name;

    [TextArea]
    public string Description;

    public int Health;
    public int Damage;
    public int FireRate;
    public int MovementSpeed;

    public bool RangedAttack;
    public bool SpikeImmune;

}
