using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point {
    public int X { get; set; }

    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static bool operator ==(Point first, Point second)    //adding functionality to the class to compare if 2 points match
    {
        return first.X == second.X && first.Y == second.Y;  //if both X and Y values match return true
    }


    public static bool operator !=(Point first, Point second)    //adding functionality to the class to compare if 2 points match
    {
        return first.X != second.X || first.Y != second.Y;  //if either value is different return true
    }

}
