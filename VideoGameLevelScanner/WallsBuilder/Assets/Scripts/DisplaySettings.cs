using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DisplaySettings : MonoBehaviour {

    public enum Mode { Half, Full, Empty }

    public Mode Top = Mode.Empty;
    public Mode Right = Mode.Empty;
    public Mode Bottom = Mode.Empty;
    public Mode Left = Mode.Empty;

	void Start () {
        ToggleVisibility(Top, 0);
        ToggleVisibility(Right, 1);
        ToggleVisibility(Bottom, 2);
        ToggleVisibility(Left, 3);
   	}

    public void ToggleVisibility(Mode side, int n)
    {
        //Renderer r0 = this.transform.Find("Middle").renderer.enabled = true;
       Renderer r1 = null;
       Renderer r2 = null;
       if (n == 0) {
            r2 = this.transform.Find("Top").renderer;
            r1 = this.transform.Find("TopHalf").renderer;
       } else if (n == 1) {
            r2 = this.transform.Find("Right").renderer;
            r1 = this.transform.Find("RightHalf").renderer;
       } else if (n == 2) {
            r2 = this.transform.Find("Bottom").renderer;
            r1 = this.transform.Find("BottomHalf").renderer;
       } else if (n == 3) {
            r2 = this.transform.Find("Left").renderer;
            r1 = this.transform.Find("LeftHalf").renderer;
       }
       if (side == Mode.Empty) {
           r2.enabled = false;
           r1.enabled = false;
       }
       else if (side == Mode.Half)
       {
           r2.enabled = false;
           r1.enabled = true;
       } 
       else if (side == Mode.Full)
           r2.enabled = true;
           r1.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
