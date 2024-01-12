using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Connection upConnection;
    public Connection downConnection;
    public Connection rightConnection;
    public Connection leftConnection;

    public Vector2 position;
}
