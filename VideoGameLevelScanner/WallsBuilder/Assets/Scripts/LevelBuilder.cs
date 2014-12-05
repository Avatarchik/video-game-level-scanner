using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {

	public Transform wall;
	public Transform corner;
	public Transform tricorner;
	public Transform cross;
    public Transform floor;
	public int [,] matrix;
    public int unit = 8;
    static Quaternion constQuaternion;
    
    void Start () {
        //example matrix
		matrix = new int [,] {{0,0,0,0,0,0,0},{0,9,3,5,8,8,0},{0,9,1,1,1,1,0},{0,9,2,2,2,3,0},{0,9,3,3,2,4,0},{0,9,9,9,9,9,0},{0,0,0,0,0,0,0}};
		for (int x=0; x<(matrix.GetLength(0)-1); x++) {
			for (int y=0; y<(matrix.GetLength(1)-1); y++) {
                MatchHash(MakeHash(matrix[x, y], matrix[x + 1, y], matrix[x+1, y+1], matrix[x, y+1]), unit, x, y);
                if (y < (matrix.GetLength(1) - 2) && x!=0)
                {
                    Spawn(floor, 0, unit, x, y);
                }
			}
		}
	}
	

	void Update () {	
	}

    void Spawn(Transform obj, float rotation, int unit, int x, int y)
    {
        Transform t = (Transform)Instantiate(obj, new Vector3(x * unit, 0, y * unit), Quaternion.Euler(270f, rotation, 0f));
        t.parent = GameObject.Find("LevelCreator").transform;
    }

    //function that generates a unique hash, given 4 cells of a room-matrix
	int MakeHash(int a, int b, int c, int d) {
		int hash = 0;
		int [] rooms = new int[] {a, b, c, d};
		int [] dict = new int[] {a, -1, -1, -1};
		for (int i = 1; i < rooms.Length; i++) {
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

        }
		return hash;		
	}
    //function that matches a hash with the correct part of a wall and displays it
    void MatchHash(int hash, int unit, int x, int y)
    {
        switch (hash)
        {
            case 0000: //no wall
                break;
            case 0100: //corner
                Spawn(corner, 180, unit, x, y);
                break;
            case 0010:
                Spawn(corner, 90, unit, x, y);
                break;
            case 0001:
                Spawn(corner, 0, unit, x, y);
                break;
            case 0111:
                Spawn(corner, 270, unit, x, y);
                break;
            case 0110: //2wall
                Spawn(wall, 90, unit, x, y);
                break;
            case 0011:
                Spawn(wall, 0, unit, x, y);
                break;
            case 0112: //3wall
                Spawn(tricorner, 90, unit, x, y);
                break;
            case 0120:
                Spawn(tricorner, 270, unit, x, y);
                break;
            case 0012:
                Spawn(tricorner, 180, unit, x, y);
                break;
            case 0122:
                Spawn(tricorner, 0, unit, x, y);
                break;
            case 0101: //4wall
            case 0121:
            case 0123:
                Spawn(cross, 0, unit, x, y);
                break;
            default:
                Debug.LogWarning("Error reading hash, incorrect h23ash");
                break;
        }
    }


}
