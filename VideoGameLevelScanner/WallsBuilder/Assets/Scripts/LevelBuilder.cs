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
                    Instantiate(floor, new Vector3(x * unit, 0, y * unit), Quaternion.Euler(270f, 0f, 0f));
                }
			}
		}
	}
	

	void Update () {	
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
                Instantiate(corner, new Vector3(x*unit, 0, y*unit), Quaternion.Euler(270f,180f,0f));
                break;
            case 0010:
                Instantiate(corner, new Vector3(x*unit, 0, y*unit), Quaternion.Euler(270f,90f,0f));
                break;
            case 0001:
                Instantiate(corner, new Vector3(x*unit, 0, y*unit), Quaternion.Euler(270f,0f,0f));
                break;
            case 0111:
                Instantiate(corner, new Vector3(x*unit, 0, y*unit), Quaternion.Euler(270f,270f,0f));
                break;
            case 0110: //2wall
                Instantiate(wall, new Vector3(x*unit, 0, y*unit), Quaternion.Euler(270f,90f,0f));
                break;
            case 0011:
                Instantiate(wall, new Vector3(x * unit, 0, y * unit), Quaternion.Euler(270f, 0f, 0f));
                break;
            case 0112: //3wall
                Instantiate(tricorner, new Vector3(x*unit, 0, y*unit), Quaternion.Euler(270f,90f,0f));
                break;
            case 0120:
                Instantiate(tricorner, new Vector3(x*unit, 0, y*unit), Quaternion.Euler(270f,270f,0f));
                break;
            case 0012:
                Instantiate(tricorner, new Vector3(x * unit, 0, y * unit), Quaternion.Euler(270f, 180f, 0f));
                break;
            case 0122:
                Instantiate(tricorner, new Vector3(x*unit, 0, y*unit), Quaternion.Euler(270f,0f,0f));
                break;
            case 0101: //4wall
            case 0121:
            case 0123:
                Instantiate(cross, new Vector3(x * unit, 0, y * unit), Quaternion.Euler(270f, 0f, 0f));
                break;
            default:
                Debug.LogWarning("Error reading hash, incorrect h23ash");
                break;
        }

    }	
}
