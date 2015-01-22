using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class Room{
    public List<Floor> floors = new List<Floor>();
    public List<Wall> Walls = new List<Wall>(); 
    public int N;
    public Material WallMaterial;
    public Material FloorMaterial;

    public Room(int n, Material floorMaterial)
    {
        N = n;
        FloorMaterial = new Material(floorMaterial);
        FloorMaterial.name = "floorMat";
        FloorMaterial.color = Color.gray;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var floor in floors)
            sb.Append("(" + floor.X + "," + floor.Y + ") ");
        return "Room" + N + ": " + sb.ToString();
    }
    public void SetWallMaterial() 
    {
        
    }
    public void SetRoomMaterial()
    {
        foreach (var floor in floors)
        {
            floor.gameObject.renderer.material = FloorMaterial;
        } 
    }
    public void ChangeFloorColor(Color color)
    {
        FloorMaterial.color = color;
    }
    public void ChangeWallMaterial()
    {

    }
}
