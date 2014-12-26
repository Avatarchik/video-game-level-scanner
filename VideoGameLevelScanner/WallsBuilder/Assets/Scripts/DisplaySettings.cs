using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DisplaySettings : MonoBehaviour {

    public enum Mode { Half, Full, Empty }
	public enum Side { Top, Bottom, Left, Right }
	//private static Dictionary<Side,string> SidesNames = new Dictionary<Side, string>{ {Side.Top,"Top"}, {Side.Top,""}, {Side.Left,"Left"}, {Side.Right,"Right"},  };
	private static string[] SidesNames = Enum.GetNames(typeof(Side));
	private static Dictionary<Mode,Mesh> modesMeshes;
	public Mesh fullMesh;
	public Mesh halfMesh;
	public Mesh emptyMesh;

	[SerializeField]
	private Mode top = Mode.Empty;
	public Mode Top { get { return top; } set { top = value; ChangeVisibility(Side.Top,value); } }
	[SerializeField]
	private Mode bottom = Mode.Empty;
	public Mode Right { get { return bottom; } set { bottom = value; } }
	[SerializeField]
	private Mode left = Mode.Empty;
	public Mode Bottom { get { return left; } set { left = value; } }
	[SerializeField]
	private Mode right = Mode.Empty;
	public Mode Left { get { return right; } set { right = value; } }

	void Start () {
		modesMeshes= new Dictionary<Mode, Mesh>(){ {Mode.Empty,emptyMesh} , {Mode.Half,halfMesh}, {Mode.Full,fullMesh}};
   	}

    public void ChangeVisibility(Side side, Mode mode)
    {
		MeshFilter meshFilter = this.transform.FindChild(SidesNames [(int)side]).GetComponent<MeshFilter>();
		meshFilter.mesh = modesMeshes [mode];
      
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow))
			Top = Mode.Full;
	}

}
