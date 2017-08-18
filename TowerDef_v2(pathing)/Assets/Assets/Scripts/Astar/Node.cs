using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  {

    public Point GridPosition {
        get;
        private set;
    }

    public TileScript TileRef { get;private set; }

    public Node Parent { get; private  set; }   //stores the parent tile

    public Node(TileScript tileRef)
    {
        this.TileRef = tileRef;
        this.GridPosition = TileRef.GridPosition;
    }

    public void CalcValues(Node parent)
    {
        this.Parent = parent;
    }
}
