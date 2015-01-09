using UnityEngine;
using System.Collections;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Timers;
using ImageRecognitionLibrary;

public class IntegrationTest : MonoBehaviour {

    public static bool CameraOn = false;
    private bool newFrame = true;
    private bool processingFrame = false;
    private Capture capture;
    private Image<Bgr,byte> frame;
    private Board board;
    private Timer cameraTimer;
    private FilteringParameters filteringParameters;
	private Texture2D cameraTex;
    private Texture2D lookupTex;

    private void Awake()
    {
		capture = new Capture ();
		//CameraOn = true;
        filteringParameters = new FilteringParameters(new UnityAppSettingsManager());
        cameraTimer = new Timer(80);
        cameraTimer.Elapsed += new ElapsedEventHandler(OnCameraFrame);
        capture.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_FPS, 25);
	}
	private void Start()
	{
        cameraTimer.Enabled = true;
        cameraTimer.Start();
        //var img = new Image<Bgr,byte>("E:\\image.bmp");
		//renderer.material.mainTexture = TextureConvert.ImageToTexture2D(img, true);
    }

    private void Update()
    {
        if (newFrame)
        {
            AnalyseFrame();
            newFrame = false;
        }    
    }

    private void OnCameraFrame(object source, ElapsedEventArgs e)
    {
        Debug.Log("TICK!");
        newFrame = true;
    }

    private void AnalyseFrame()
    {
        

        frame = capture.QueryFrame();
        cameraTex = TextureConvert.ImageToTexture2D<Bgr, byte>(frame, true);
        GameObject.Find("CameraImage").GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(cameraTex, new Rect(0, 0, cameraTex.width, cameraTex.height), new Vector2(0.5f, 0.5f));
        
        if (!processingFrame) {
            Debug.Log("Analysis fired");
            processingFrame = true;
            
            board = ImageTools.ReadFromFrame (frame,filteringParameters);
            var img = ImageTools.DrawRooms (320,240, board.Grid);

            if (img != null)
            {
                lookupTex = TextureConvert.ImageToTexture2D<Bgr, byte>(img, true);
                GameObject.Find("Lookup").GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(lookupTex, new Rect(0, 0, lookupTex.width, lookupTex.height), new Vector2(0.5f, 0.5f));
            }
            processingFrame = false;
		}
    }

    //private void OnGUI()
    //{
    //    if (CameraOn)
    //    {
    //        if (GUILayout.Button("Stop"))
    //        {
    //            capture.Stop();
    //            CameraOn = false;
    //        }
    //    }
    //    else
    //    {
    //        if (GUILayout.Button("Play"))
    //        {
    //            capture.Start();
    //            CameraOn = true;
    //        }
    //    }
    //}

    private void OnDestroy()
    {
        cameraTimer.Stop();
        capture.Stop();
    }
}



