using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using ImageRecognitionLibrary;
using System.Text;

public class LevelBuilder : MonoBehaviour
{
    public int[,] matrix;
    public int unit = 8;
    public RoomPropertiesPanelScript RoomPropertiesPanel;
    static Quaternion constQuaternion;
    public bool isSaved;
    private Graph graph = new Graph();
    public Floor[,] floors;
    public Wall[,] walls;
    public Room[] Rooms;
    public Material DefaultFloorMaterial;
    static private System.Random rnd = new System.Random();

    void Start()
    {
        matrix = new int[,] {
            { 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, 9, 5, 5, 8, 8, 0 }, 
            { 0, 9, 1, 1, 1, 1, 0 }, 
            { 0, 9, 2, 2, 2, 1, 0 }, 
            { 0, 9, 3, 3, 3, 4, 0 }, 
            { 0, 9, 7, 7, 7, 6, 0 }, 
            { 0, 9, 12, 11, 10, 10, 0 }, 
            { 0, 9, 13, 13, 10, 10, 0 }, 
            { 0, 9, 9, 9, 9, 9, 0 }, 
            { 0, 0, 0, 0, 0, 0, 0 } 
        };
        BuildLevel(new Board(10, 10));
    }
    
    public void BuildLevel(Board board)
    {
        //matrix = PrepareMatrix(board.Grid);
        int n=13;
        floors = new Floor[matrix.GetLength(0)-2,matrix.GetLength(1)-2];
        walls = new Wall[matrix.GetLength(0)-1, matrix.GetLength(1)-1];
        Rooms = new Room[n];
        for (int i = 0; i < n; ++i)
        {
            Rooms[i] = new Room(i+1, DefaultFloorMaterial);
        }
        RoomPropertiesPanel.InitializePanel(n);
        Vector3 shift = new Vector3(((matrix.GetLength(1) - 2) * unit) / 2, 0f, ((matrix.GetLength(0) - 2) * -unit) / 2);
        this.transform.position = shift;
        SpawnWalls();
        SpawnFloors();
        SpawnDoors();
        foreach (var room in Rooms)
        {
            room.SetRoomMaterial();
        }
        isSaved = false;
    }
    public void RespawnDoors()
    {
        foreach (var wall in walls)
        {
            if (wall != null)
            {
                if (wall.sides.Left == MultiWallScript.Mode.Half)
                    wall.sides.Left = MultiWallScript.Mode.Full;
                if (wall.sides.Bottom == MultiWallScript.Mode.Half)
                    wall.sides.Bottom = MultiWallScript.Mode.Full;
                if (wall.sides.Right == MultiWallScript.Mode.Half)
                    wall.sides.Right = MultiWallScript.Mode.Full;
                if (wall.sides.Top == MultiWallScript.Mode.Half)
                    wall.sides.Top = MultiWallScript.Mode.Full;
            }
        }
        SpawnDoors();
        this.isSaved = false;
    }
    private void SpawnDoors()
    {
        var roomsWithDoors = graph.Kruskal();
        foreach (var edge in roomsWithDoors)
        {
            SpawnDoor(FindPlaceForDoor(Rooms[edge.U - 1], Rooms[edge.V - 1]));
        }
    }

    private void SpawnFloors()
    {
        for (int x = 1; x < (matrix.GetLength(0) - 1); x++)
        {
            for (int y = 1; y < (matrix.GetLength(1) - 1); y++)
            {
                if (matrix[x, y] > 0)
                {
                    //Debug.Log("Trying to create floor in " + (x-1) + "," + (y-1) + ".");
                    var floor = new Floor(x - 1, y - 1, unit);
                    floor.gameObject.transform.parent = this.transform;
                    //Debug.Log("Putting into array floor in " + (x-1) + "," + (y-1) + ".");
                    floors[x - 1, y - 1] = floor;
                    Rooms[matrix[x, y] - 1].floors.Add(floor);
                }
            }
        }
    }

    private void SpawnWalls()
    {
        for (int x = 0; x < (matrix.GetLength(0) - 1); x++)
        {
            for (int y = 0; y < (matrix.GetLength(1) - 1); y++)
            {
                //Debug.Log("Trying to create wall in " + x + "," + y + ".");
                var wall = SpawnWall(MakeHash(matrix[x, y], matrix[x + 1, y], matrix[x + 1, y + 1], matrix[x, y + 1]), unit, x, y);
                if (wall != null)
                    wall.gameObject.transform.parent = this.transform;
                //Debug.Log("Putting into array wall in " + x + "," + y + ".");
                walls[x, y] = wall;
                AddWallToRoom(x, y, wall);
                AddWallToRoom(x + 1, y, wall);
                AddWallToRoom(x, y + 1, wall);
                AddWallToRoom(x + 1, y + 1, wall);
            }
        }
    }

    private void AddWallToRoom(int x, int y, Wall wall)
    {
        if (x>=0 && y>=0 && matrix[x,y]!=null && matrix[x, y] > 0)
            Rooms[matrix[x, y]-1].Walls.Add(wall);
    }

    /// <summary>
    /// Gets pairs of coordinates of neighbouring floors where a passage can be made.
    /// </summary>
    /// <param name="roomA"></param>
    /// <param name="roomB"></param>
    /// <returns>List of pairs of coordinates.</returns>
    private KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>> FindPlaceForDoor(Room roomA, Room roomB)
    {
        //Debug.Log("Connecting rooms " + roomA.N + " " + roomB.N);
        var neighbouringFloors = new List<KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>>>();
        foreach (var floorA in roomA.floors) {
            foreach (var floorB in roomB.floors) {
                if(floorA.IsNextTo(floorB))    
                   neighbouringFloors.Add(new KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>>(new KeyValuePair<int,int>(floorA.X,floorA.Y), new KeyValuePair<int,int>(floorB.X,floorB.Y)));   
            }
        }
        var place = rnd.Next(neighbouringFloors.Count);
        //Debug.Log(neighbouringFloors.Count);
        //foreach (var pair in neighbouringFloors)
        //{
        //    Debug.Log("Pair (" + pair.Key + "),(" + pair.Value + ")");
        //}
        //Debug.Log("Pair (" + neighbouringFloors[place].Key + "),(" + neighbouringFloors[place].Value + ")");
        return neighbouringFloors[place];
    }

    void SpawnDoor(KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>> floorsCoordinates)
    {
        List<KeyValuePair<int, int>> commonWalls = floors[floorsCoordinates.Key.Key, floorsCoordinates.Key.Value].WallsCoordinates().Intersect(floors[floorsCoordinates.Value.Key, floorsCoordinates.Value.Value].WallsCoordinates()).ToList();
        Wall a = walls[commonWalls[0].Key, commonWalls[0].Value];
        Wall b = walls[commonWalls[1].Key, commonWalls[1].Value];
        Wall.ChangeToDoor(a, b);
    }

    void AddEdgesToGraph(int[] rooms, int i)
    {
        Edge edge;
        if (rooms[i] != 0 && rooms[i - 1] != 0 && rooms[i - 1] != rooms[i])
        {
            edge = new Edge(rooms[i - 1], rooms[i]);
            if (!graph.Edges.Exists(e => e.Equals(edge)))
                graph.Edges.Add(edge);
        }
    }

    //function that generates a unique hash, given 4 cells of a room-matrix
    int MakeHash(int a, int b, int c, int d)
    {
        int hash = 0;
        int[] rooms = new int[] { a, b, c, d };
        int[] dict = new int[] { a, -1, -1, -1 };
        for (int i = 1; i < rooms.Length; i++)
        {
            int j = 0;
            while (j < 4 && (dict[j] != -1 || dict[j] != rooms[i]))
            {
                if (dict[j] == -1)
                {
                    dict[j] = rooms[i];
                    hash = hash * 10 + j;
                    j = 4;
                }
                else if (dict[j] == rooms[i])
                {
                    hash = hash * 10 + j;
                    j = 4;
                }
                j++;
            }
            AddEdgesToGraph(rooms, i);
        }
        return hash;
    }
    //function that matches a hash with the correct part of a wall and displays it
    Wall SpawnWall(int hash, int unit, int x, int y)
    {
        Wall wall; 
        switch (hash)
        {
            case 0000: //no wall
                wall = null;
                break;
            case 0100: //corner
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full);
                break;
            case 0010:
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty);
                break;
            case 0001:
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Empty);
                break;
            case 0111:
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full);
                break;
            case 0110: //2wall
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full);
                break;
            case 0011:
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty);
                break;
            case 0112: //3wall
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full);
                break;
            case 0120:
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full);
                break;
            case 0012:
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty);
                break;
            case 0122:
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full);
                break;
            case 0101: //4wall
            case 0121:
            case 0123:
                wall = new Wall(x, y, unit, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full);
                break;
            default:
                Debug.LogWarning("Error reading hash, incorrect h23ash");
                throw new ArgumentException("Inproper hash code");
        }
        return wall;
    }

    private int[,] PrepareMatrix(int[,] matrix)
    {
        LogMatrix(matrix);
        int[,] result = new int[matrix.GetLength(0)+2, matrix.GetLength(1)+2];
        for (int x = 0; x < matrix.GetLength(0); ++x)
            for (int y = 0; y < matrix.GetLength(1); ++y)
                result[x + 1, y + 1] = matrix[x, y];
        LogMatrix(result);
        return result;
    }
    private void LogMatrix(int[,] matrix)
    {
        StringBuilder sb = new StringBuilder("Matrix("+matrix.GetLength(0)+","+matrix.GetLength(1)+") \n");
        for (int x = 0; x < matrix.GetLength(0); ++x)
        {
            for (int y = 0; y < matrix.GetLength(1); ++y)
            {
                sb.Append(matrix[x, y] + ",");
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }
}
