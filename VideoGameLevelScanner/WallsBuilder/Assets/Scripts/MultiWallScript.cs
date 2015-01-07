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
        switch (mode)
        {
            case Mode.Full:
                sideObject.FindChild("LeftSide").GetComponent<MeshFilter>().mesh =  fullMeshLeft;
                sideObject.FindChild("RightSide").GetComponent<MeshFilter>().mesh = fullMeshRight;
                sideObject.FindChild("Middle").GetComponent<MeshFilter>().mesh = fullMesh;
                break;
            case Mode.Half:
                sideObject.FindChild("LeftSide").GetComponent<MeshFilter>().mesh = halfMeshLeft;
                sideObject.FindChild("RightSide").GetComponent<MeshFilter>().mesh = halfMeshRight;
                sideObject.FindChild("Middle").GetComponent<MeshFilter>().mesh = halfMesh;
                break;
            case Mode.Empty:
                sideObject.FindChild("LeftSide").GetComponent<MeshFilter>().mesh = null;
                sideObject.FindChild("RightSide").GetComponent<MeshFilter>().mesh = null;
                sideObject.FindChild("Middle").GetComponent<MeshFilter>().mesh = emptyMesh;
                break;
            default:
                throw new ArgumentException();
        }
    }
}
