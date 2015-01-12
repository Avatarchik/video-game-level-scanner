using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Room {
    public List<Floor> floors = new List<Floor>();
    public List<Wall> Walls = new List<Wall>(); 
    public int N;
    public Material WallMaterial;
    public Material FloorMaterial;

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
    public void SetRoomMaterial() {
        
    }
}
