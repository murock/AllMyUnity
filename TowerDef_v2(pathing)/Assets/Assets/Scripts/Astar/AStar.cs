using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar  {

    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        //run through all tiles in game
        foreach  (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));       //add nodes to dictionary
        }
    }

    public static void GetPath(Point start)
    {
        if (nodes == null)  //if nodes have not been made create them
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();       //open list

        Node currentNode = nodes[start];    //insitalise starting path

        openList.Add(currentNode);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y); //get neighbour tiles positions

                if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].WalkAble && neighbourPos != currentNode.GridPosition)   //neighbour different to current pos?
                {
                    Node neighbour = nodes[neighbourPos];   //get the node of that tile

                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }

                    neighbour.CalcValues(currentNode);
                 //   neighbour.TileRef.SpriteRenderer.color = Color.blue;
                }
            }
        }

        //ONLY FOR DEBUGGING REMOVE LATER
        GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList); 
    }
}
