using UnityEngine;
using System.Collections;

public class Wall {
   
    public int X;
    public int Y;
    public int Unit;
    public GameObject gameObject;
    public MultiWallScript sides;
    public Wall(int x, int y, int unit, MultiWallScript.Mode topMode, MultiWallScript.Mode rightMode, MultiWallScript.Mode bottomMode, MultiWallScript.Mode leftMode)
    {
        X = x;
        Y = y;
        Unit = unit;
        var instance = Resources.Load("Prefabs/wallSegment") as GameObject; 
        gameObject = (GameObject)Object.Instantiate(instance, new Vector3(x * unit, 0, y * unit), Quaternion.identity);
        gameObject.transform.rotation = instance.transform.rotation;

        sides = gameObject.GetComponent<MultiWallScript>();
        sides.Top = topMode;
        sides.Right = rightMode;
        sides.Bottom = bottomMode;
        sides.Left = leftMode;
    }

    public static void ChangeToDoor(Wall a, Wall b) 
    {
        //right and left mode to modify
        if (a.X == b.X) 
        {
            if (a.X < b.X)
            {
                a.sides.Top = MultiWallScript.Mode.Half;
                b.sides.Bottom = MultiWallScript.Mode.Half;
            }
            else 
            { 
                a.sides.Bottom = MultiWallScript.Mode.Half; 
                b.sides.Top = MultiWallScript.Mode.Half; 
            }
        }
        //top and bottom mode to modify
        else if (a.Y == b.Y) 
        {
            if (a.Y < b.Y) 
            { 
                a.sides.Right = MultiWallScript.Mode.Half; 
                b.sides.Left = MultiWallScript.Mode.Half; 
            }
            else { 
                a.sides.Left = MultiWallScript.Mode.Half; 
                b.sides.Right = MultiWallScript.Mode.Half; 
            }
        } 
        else 
            throw new System.ArgumentException("Inproper coordinates of the walls");

        //if (a.X == b.X)
        //{
        //    if (a.Y < b.Y) 
        //    {
        //        a.sides.Right = MultiWallScript.Mode.Half; 
        //        b.sides.Left = MultiWallScript.Mode.Half;
        //    }
        //    else 
        //    {
        //        a.sides.Left = MultiWallScript.Mode.Half; 
        //        b.sides.Right = MultiWallScript.Mode.Half;
        //    }
        //}
        //else if (a.Y == b.Y)
        //{
        //    if (a.X < b.X) {
        //        a.sides.Bottom = MultiWallScript.Mode.Half; 
        //        b.sides.Top = MultiWallScript.Mode.Half;
        //    }
        //    else 
        //    {
        //        a.sides.Top = MultiWallScript.Mode.Half; 
        //        b.sides.Bottom = MultiWallScript.Mode.Half;
        //    }
        //}
        //else
        //    throw new System.ArgumentException("Inproper coordinates of the walls");
    }

    public bool IsNextTo(Wall otherWall)
    {
        int diffX = this.X - otherWall.X;
        int diffY = this.Y - otherWall.Y;
        if (diffX == 0 || diffY == 0)
            return (System.Math.Abs(diffX + diffY) == 1);
        return false;
    }
}
