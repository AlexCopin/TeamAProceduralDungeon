using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    public Dictionary<Vector2, Node> nodes = new();

    public bool GeneratePath(int lenght, Vector2 startingPoint)
    {
        Node lastNode = new();
        lastNode.position = startingPoint;
        nodes.Add(startingPoint, lastNode);

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
                if (nodes.ContainsKey(tempDir + lastNode.position))
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
            Vector2 newPos = newDir + lastNode.position;
            tempNode.position = newPos;

            switch (newDir)
            {
                case Vector2 v when v.Equals(Vector2.right):
                    {
                        Connection connection = new(lastNode, tempNode);
                        lastNode.rightConnection = connection;
                        tempNode.leftConnection = connection;
                        break;
                    }

                case Vector2 v when v.Equals(Vector2.left):
                    {
                        Connection connection = new(lastNode, tempNode);
                        lastNode.leftConnection = connection;
                        tempNode.rightConnection = connection;
                        break;
                    }

                case Vector2 v when v.Equals(Vector2.up):
                    {
                        Connection connection = new(lastNode, tempNode);
                        lastNode.upConnection = connection;
                        tempNode.downConnection = connection;
                        break;
                    }

                case Vector2 v when v.Equals(Vector2.down):
                    {
                        Connection connection = new(lastNode, tempNode);
                        lastNode.downConnection = connection;
                        tempNode.upConnection = connection;
                        break;
                    }

                default:
                    {
                        return false;
                    }
            }

            nodes.Add(newPos, tempNode);
            lastNode = tempNode;
        }

        return true;
    }

    
}
