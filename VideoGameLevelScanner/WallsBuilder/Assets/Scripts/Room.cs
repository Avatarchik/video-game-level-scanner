using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Room {
    public List<Floor> floors = new List<Floor>(); 
    public int N;
    public Room(int n)
    {
        N = n;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var floor in floors)
            sb.Append("(" + floor.X + "," + floor.Y + ") ");
        return "Room" + N + ": " + sb.ToString();
    }
}
