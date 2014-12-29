using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {

    public Transform floor;
    public Transform multiwall;
	public int [,] matrix;
    public int unit = 8;
    static Quaternion constQuaternion;

    void Start () {
        //example matrix
		matrix = new int [,] {{0,0,0,0,0,0,0},{0,9,3,5,8,8,0},{0,9,1,1,1,1,0},{0,9,2,2,2,3,0},{0,9,3,3,2,4,0},{0,9,9,9,9,9,0},{0,0,0,0,0,0,0}};
        Vector3 shift = new Vector3(((matrix.GetLength(0)-2)*unit)/2, 0f, ((matrix.GetLength(1)-2)*unit)/2);
        GameObject.Find("LevelCreator").transform.position = shift;
		for (int x=0; x<(matrix.GetLength(0)-1); x++) {
			for (int y=0; y<(matrix.GetLength(1)-1); y++) {
                MatchHash(MakeHash(matrix[x, y], matrix[x + 1, y], matrix[x+1, y+1], matrix[x, y+1]), unit, x, y);
                if (y < (matrix.GetLength(1) - 2) && x!=0)
                {
                    var t = (Transform)Instantiate(floor, new Vector3(x * unit, 0, y * unit), Quaternion.identity);
                    t.rotation = floor.rotation;
                    t.parent = this.transform;
                }
			}
		}
	}
	


	void Update () {	
	}

    void Spawn(MultiWallScript.Mode topMode, MultiWallScript.Mode leftMode, MultiWallScript.Mode bottomMode, MultiWallScript.Mode rightMode, int unit, int x, int y)
    {
        var obj = (Transform)Instantiate(multiwall, new Vector3(x * unit, 0, y * unit), Quaternion.identity);
        MultiWallScript sides = obj.GetComponent<MultiWallScript>();
        sides.Top = topMode;
        sides.Right = rightMode;
        sides.Bottom = bottomMode;
        sides.Left = leftMode;
        obj.transform.parent = GameObject.Find("LevelCreator").transform;
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
                Spawn(MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Empty, unit, x, y);
                break;
            case 0010:
                Spawn(MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, unit, x, y);
                break;
            case 0001:
                Spawn(MultiWallScript.Mode.Empty, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, unit, x, y);
                break;
            case 0111:
                Spawn(MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, unit, x, y);
                break;
            case 0110: //2wall
                Spawn(MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, unit, x, y);
                break;
            case 0011:
                Spawn(MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, unit, x, y);
                break;
            case 0112: //3wall
                Spawn(MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, unit , x, y);
                break;
            case 0120:
                Spawn(MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, unit, x, y);
                break;
            case 0012:
                Spawn(MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, unit, x, y);
                break;
            case 0122:
                Spawn(MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Empty, MultiWallScript.Mode.Full, unit, x, y);
                break;
            case 0101: //4wall
            case 0121:
            case 0123:
                Spawn(MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, MultiWallScript.Mode.Full, unit, x, y);
                break;
            default:
                Debug.LogWarning("Error reading hash, incorrect h23ash");
                break;
        }
    }


}
