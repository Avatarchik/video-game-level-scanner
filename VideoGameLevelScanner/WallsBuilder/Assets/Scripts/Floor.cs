using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Floor 
{
    public int X;
    public int Y;
    public int Unit;
    public GameObject gameObject;
    public Floor(int x, int y, int unit)
    {
        X = x;
        Y = y;
        Unit = unit;
        var instance = Resources.Load("Prefabs/floor") as GameObject;
        gameObject = (GameObject)Object.Instantiate(instance, new Vector3(x * unit, 0, y * unit), Quaternion.identity);
        gameObject.transform.rotation = instance.transform.rotation;
    }

    public bool IsNextTo(Floor otherFloor)
    {
        int diffX = this.X - otherFloor.X; 
        int diffY = this.Y - otherFloor.Y;
        if (diffX == 0 || diffY == 0)
            return (System.Math.Abs(diffX + diffY) == 1);
        return false; 
    }
    public List<KeyValuePair<int, int>> WallsCoordinates()
    {
        List<KeyValuePair<int, int>> wallCoordinates = new List<KeyValuePair<int, int>>();
        wallCoordinates.Add(new KeyValuePair<int,int>(this.X, this.Y));
        wallCoordinates.Add(new KeyValuePair<int,int>(this.X+1, this.Y));
        wallCoordinates.Add(new KeyValuePair<int,int>(this.X, this.Y+1));
        wallCoordinates.Add(new KeyValuePair<int,int>(this.X+1, this.Y+1));
        return wallCoordinates;
    }
}
