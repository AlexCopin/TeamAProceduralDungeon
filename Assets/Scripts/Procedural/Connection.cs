using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public bool hasLock;

    public Node firstNode;
    public Node secondNode;

    public Connection (Node firstNode, Node secondNode)
    {
        this.firstNode = firstNode;
        this.secondNode = secondNode;

        hasLock = false;
    }

    public Connection(Node firstNode, Node secondNode, bool hasLock)
    {
        this.firstNode = firstNode;
        this.secondNode = secondNode;

        this.hasLock = hasLock;
    }
}
