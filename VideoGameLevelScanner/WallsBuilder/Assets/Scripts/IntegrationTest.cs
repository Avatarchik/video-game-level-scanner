using UnityEngine;
using System.Collections;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using System;
using System.Drawing;
using ImageRecognitionLibrary;

public class IntegrationTest : MonoBehaviour {

    public static bool CameraOn = false;
    private Capture capture;
    private Board board;
	private Texture2D cameraTex;
    private Texture2D lookupTex;

    void Awake()
    {
		capture = new Capture ();
		CameraOn = true;
	}
	void Start()
	{
		//var img = new Image<Bgr,byte>("E:\\image.bmp");
		//renderer.material.mainTexture = TextureConvert.ImageToTexture2D(img, true);
    }

    void Update()
    {
		if (CameraOn) {
			Image<Bgr, byte> frame = capture.QueryFrame ();
			Debug.Log ("Board");
			board = ImageTools.ReadFromFrame (frame);

			var img = ImageTools.DrawRooms (320,240, board.Grid);

			cameraTex = TextureConvert.ImageToTexture2D<Bgr,byte> (frame, true);
			GameObject.Find ("CameraImage").GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (cameraTex, new Rect (0, 0, cameraTex.width, cameraTex.height), new Vector2 (0.5f, 0.5f));
			if(img != null){
				lookupTex = TextureConvert.ImageToTexture2D<Bgr,byte> (img, true);
				GameObject.Find ("Lookup").GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (lookupTex, new Rect (0, 0, lookupTex.width, lookupTex.height), new Vector2 (0.5f, 0.5f));
			}
		}
    }

    void OnGUI()
    {
        if (CameraOn)
        {
            if (GUILayout.Button("Pause"))
            {
                capture.Pause();
            }
            if (GUILayout.Button("Stop"))
            {
                capture.Stop();
                CameraOn = false;
            }
        }
        else
        {
            if (GUILayout.Button("Play"))
            {
                capture.Start();
                CameraOn = true;
            }
        }
    }

    void OnDestroy()
    {
        capture.Stop();
    }
}



