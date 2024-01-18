using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public Dictionary<Vector2, Node> nodes = new();

    public Tree()
    {
        nodes = new();
    }

    public Tree(Tree baseTree)
    {
        nodes = new(baseTree.nodes);
    }

    public bool GeneratePath(int lenght, Vector2 startingNodePos, RoomTag endRoomTag)
    {
        Vector2 lastNodePos = startingNodePos;

        for (int i = 1; i < lenght; i++)
        {
            #region Direction
            Vector2 newDir = Vector2.zero;

            List<Vector2> directions = new();
            directions.Add(Vector2.up);
            directions.Add(Vector2.down);
            directions.Add(Vector2.right);
            directions.Add(Vector2.left);

            for (int j = 4; j > 0; j--)
            {
                Vector2 tempDir;
                int rand = Random.Range(0, j);
                tempDir = directions[rand];

                //Check if direction is occupied
                if (nodes.ContainsKey(tempDir + lastNodePos))
                {
                    directions.RemoveAt(rand);
                    continue;
                }

                newDir = tempDir;
                break;
            }

            //Fail if all directions are occupied
            if(newDir == Vector2.zero)
            {
                nodes.Clear();
                return (false);
            }
            #endregion

            Node tempNode = new();
            Vector2 newPos = newDir + lastNodePos;
            tempNode.position = newPos;

            switch (newDir)
            {
                case Vector2 v when v.Equals(Vector2.right):
                    {
                        Connection connection = new(nodes[lastNodePos], tempNode);
                        nodes[lastNodePos].rightConnection = connection;
                        nodes[lastNodePos].doors.Add(DoorPos.Right);
                        tempNode.leftConnection = connection;
                        tempNode.doors.Add(DoorPos.Left);
                        break;
                    }

                case Vector2 v when v.Equals(Vector2.left):
                    {
                        Connection connection = new(nodes[lastNodePos], tempNode);
                        nodes[lastNodePos].leftConnection = connection;
                        nodes[lastNodePos].doors.Add(DoorPos.Left);
                        tempNode.rightConnection = connection;
                        tempNode.doors.Add(DoorPos.Right);
                        break;
                    }

                case Vector2 v when v.Equals(Vector2.up):
                    {
                        Connection connection = new(nodes[lastNodePos], tempNode);
                        nodes[lastNodePos].upConnection = connection;
                        nodes[lastNodePos].doors.Add(DoorPos.Up);
                        tempNode.downConnection = connection;
                        tempNode.doors.Add(DoorPos.Down);
                        break;
                    }

                case Vector2 v when v.Equals(Vector2.down):
                    {
                        Connection connection = new(nodes[lastNodePos], tempNode);
                        nodes[lastNodePos].downConnection = connection;
                        nodes[lastNodePos].doors.Add(DoorPos.Down);
                        tempNode.upConnection = connection;
                        tempNode.doors.Add(DoorPos.Up);
                        break;
                    }

                default:
                    {
                        return false;
                    }
            }

            if(i == lenght - 1)
            {
                tempNode.tags.Add(endRoomTag);
            }

            nodes.Add(newPos, tempNode);
            lastNodePos = newPos;
        }

        return true;
    }
    
    public Vector2 GetRandomNodePos()
    {
        List<Vector2> values = nodes.Keys.ToList();
        int size = nodes.Count;
        return values[Random.Range(0, size)];
    }

    public List<Vector2> GetRandomNodes(int quantity)
    {
        if(quantity > nodes.Count)
        {
            Debug.LogError("Not enough nodes in tree");
            return null;
        }

        List<Vector2> tempNodes = new();
        for(int i = 0; i < quantity; i++)
        {
            while (true)
            {
                Vector2 temp = GetRandomNodePos();

                //Same node check and exclude special rooms
                if (tempNodes.Contains(temp) || nodes[temp].tags.Count > 0)
                {
                    Debug.LogError("Picked excluded node");
                    continue;
                }

                tempNodes.Add(temp);
                break;
            }
        }

        return tempNodes;
    }
}
