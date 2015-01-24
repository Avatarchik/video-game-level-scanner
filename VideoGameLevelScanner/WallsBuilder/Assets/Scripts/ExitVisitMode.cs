using UnityEngine;
using System.Collections;

public class ExitVisitMode : MonoBehaviour {
    public LevelBuilder Level;
    public GameObject VisitModeUI;
    public GameObject BuildModeUI;
    public GameObject Player;
    public Camera PlayerCamera;
    public Camera MainCamera;

	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Player.SetActive(false);
            PlayerCamera.enabled = false;
            Level.transform.localScale = new Vector3(1f,1f,1f);
            MainCamera.enabled = true;
            BuildModeUI.SetActive(true);
            VisitModeUI.SetActive(false);
        }
	}
}
