using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    public bool hasLock;

    public Node prevNode;
    public Node nextNode;

    public Connection (Node prevNode, Node nextNode)
    {
        this.prevNode = prevNode;
        this.nextNode = nextNode;

        hasLock = false;
    }

    public Connection(Node prevNode, Node nextNode, bool hasLock)
    {
        this.prevNode = prevNode;
        this.nextNode = nextNode;

        this.hasLock = hasLock;
    }
}
