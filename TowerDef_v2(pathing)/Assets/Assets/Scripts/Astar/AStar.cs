using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static void GetPath(Point start, Point goal)
    {
        if (nodes == null)  //if nodes have not been made create them
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();       //open list

        HashSet<Node> closedList = new HashSet<Node>();       //closed list

        Stack<Node> finalPath = new Stack<Node>();       //backtrack for creeps so you can pop from start to end

        Node currentNode = nodes[start];    //insitalise starting path

        openList.Add(currentNode);
        while (openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y); //get neighbour tiles positions

                    if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.Tiles[neighbourPos].WalkAble && neighbourPos != currentNode.GridPosition)   //neighbour different to current pos?
                    {
                        int gCost = 0;

                        if (Math.Abs(x - y) == 1)   //if horizontal or vertical Absoulute value means -1 = 1
                        {
                            gCost = 10;
                        }
                        else //else diagonal
                        {
                            gCost = 14;
                        }

                        Node neighbour = nodes[neighbourPos];   //get the node of that tile



                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)    //check if new parent is actually a better parent based all new gScore value
                            {
                                neighbour.CalcValues(currentNode, nodes[goal], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbour))
                        {
                            openList.Add(neighbour);    //add new neighbour tile to openlist
                            neighbour.CalcValues(currentNode, nodes[goal], gCost);  //calc values for parent... might not be optimal to do this here
                        }
                    }
                }
            }

            // moves current node from open to closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.F).First();   //sorts list by F value and selects the first on the list
            }

            if(currentNode == nodes[goal])  //once goal is in list then go back and add to final path stack
            {
                while (currentNode.GridPosition != start)   //stop when you reach the start
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;  //we found the goal
            }
            //ONLY FOR DEBUGGING REMOVE LATER
            GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList, closedList);
        }
      
    }
}
