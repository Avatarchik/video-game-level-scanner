using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MultiWallScript : MonoBehaviour {

    public enum Mode { Half, Full, Empty }
	public enum Side { Top, Bottom, Left, Right }
	//private static Dictionary<Side,string> SidesNames = new Dictionary<Side, string>{ {Side.Top,"Top"}, {Side.Top,""}, {Side.Left,"Left"}, {Side.Right,"Right"},  };
	private static string[] SidesNames = Enum.GetNames(typeof(Side));
	private static Dictionary<Mode,Mesh> modesMeshes;
    
    public Mesh fullMesh;
    public Mesh fullMeshLeft;
    public Mesh fullMeshRight;
    public Mesh halfMesh;
    public Mesh halfMeshLeft;
    public Mesh halfMeshRight;
    public Mesh emptyMesh;

	[SerializeField]
	private Mode top = Mode.Empty;
	public Mode Top { get { return top; } set { top = value; ChangeVisibility(Side.Top,value); } }
	
    [SerializeField]
	private Mode bottom = Mode.Empty;
	public Mode Bottom { get { return bottom; } set { bottom = value; ChangeVisibility(Side.Bottom,value); } }
	
    [SerializeField]
	private Mode left = Mode.Empty;
	public Mode Left { get { return left; } set { left = value; ChangeVisibility(Side.Left,value); } }
	
    [SerializeField]
	private Mode right = Mode.Empty;
	public Mode Right { get { return right; } set { right = value; ChangeVisibility(Side.Right,value); } }

	void Awake () {
        
   	}

    public void ChangeVisibility(Side side, Mode mode)
    {
		Transform sideObject = this.transform.FindChild(SidesNames [(int)side]);
        Mesh leftMesh, rightMesh, midMesh;
        switch (mode)
        {
            case Mode.Full:
                leftMesh =  fullMeshLeft;
                rightMesh = fullMeshRight;
                midMesh = fullMesh;
                break;
            case Mode.Half:
                leftMesh = halfMeshLeft;
                rightMesh = halfMeshRight;
                midMesh = halfMesh;
                break;
            case Mode.Empty:
                leftMesh = null;
                rightMesh = null;
                midMesh = emptyMesh;
                break;
            default:
                throw new ArgumentException();
        }
        sideObject.FindChild("LeftSide").GetComponent<MeshFilter>().sharedMesh= leftMesh;
        sideObject.FindChild("LeftSide").GetComponent<MeshCollider>().sharedMesh = leftMesh;
        sideObject.FindChild("RightSide").GetComponent<MeshFilter>().sharedMesh = rightMesh;
        sideObject.FindChild("RightSide").GetComponent<MeshCollider>().sharedMesh = rightMesh;
        sideObject.FindChild("Middle").GetComponent<MeshFilter>().sharedMesh = midMesh;
        sideObject.FindChild("Middle").GetComponent<MeshCollider>().sharedMesh = midMesh;
    }
}
