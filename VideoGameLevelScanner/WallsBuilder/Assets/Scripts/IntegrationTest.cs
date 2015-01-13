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
    private Image<Bgr, byte> lookupImage;
    private Board board;
    private Timer cameraTimer;
    private FilteringParameters filteringParameters;
    public Hsv EditedRangeMax;
    public Hsv EditedRangeMin;
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
        EditedRangeMax = new Hsv();
        EditedRangeMin = new Hsv();

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
        if (frame != null) { 
            cameraTex = TextureConvert.ImageToTexture2D<Bgr, byte>(frame, true);
            Sprite.DestroyImmediate(GameObject.Find("CameraImage").GetComponent<UnityEngine.UI.Image>().sprite);
            GameObject.Find("CameraImage").GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(cameraTex, new Rect(0, 0, cameraTex.width, cameraTex.height), new Vector2(0.5f, 0.5f));

        }
        if (!processingFrame) {
            Debug.Log("Analysis fired");
            processingFrame = true;
            
            board = ImageTools.ReadFromFrame (frame,filteringParameters);

            if (board != null)
                lookupImage = ImageTools.DrawRooms(320, 240, board.Grid);
            else
                lookupImage = new Image<Bgr, byte>(320, 240, new Bgr(0, 0, 0));

            if (lookupImage != null)
            {
                lookupTex = TextureConvert.ImageToTexture2D<Bgr, byte>(lookupImage, true);
                Sprite.DestroyImmediate(GameObject.Find("Lookup").GetComponent<UnityEngine.UI.Image>().sprite);
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

    public float EditedHueMax { get { return (float)this.EditedRangeMax.Hue; } set { EditedRangeMax.Hue = value; } }
    public float EditedSaturationMax { get { return (float)this.EditedRangeMax.Satuation; } set { EditedRangeMax.Satuation = value; } }
    public float EditedValueMax { get { return (float)this.EditedRangeMax.Value; } set { EditedRangeMax.Value = value; } }
    public float EditedHueMin { get { return (float)this.EditedRangeMin.Hue; } set { EditedRangeMin.Hue = value; } }
    public float EditedSaturationMin { get { return (float)this.EditedRangeMin.Satuation; } set { EditedRangeMin.Satuation = value; } }
    public float EditedValueMin { get { return (float)this.EditedRangeMin.Value; } set { EditedRangeMin.Value = value; } }
    
    public void GoToBuilding()
    {
        cameraTimer.Stop();
        capture.Stop();
        this.gameObject.SetActive(false);
        GameObject.Find("LevelCreator").GetComponent<LevelBuilder>().BuildLevel(board);
    }

    public enum EditedRange {Blue=0,Red=1, Red2=2, Green=3, Yellow=4}
    public void ChangeEditedRange(int range)
    {
        EditedRange selectedColor = (EditedRange)range;
        switch (selectedColor)
        {
            case EditedRange.Blue:
                EditedRangeMax = filteringParameters.BlueMax;
                EditedRangeMin = filteringParameters.BlueMin;
                break;
            case EditedRange.Red:
                EditedRangeMax = filteringParameters.RedMax;
                EditedRangeMin = filteringParameters.RedMin;
                break;
            case EditedRange.Red2:
                EditedRangeMax = filteringParameters.RedMax2;
                EditedRangeMin = filteringParameters.RedMin2;
                break;
            case EditedRange.Green:
                EditedRangeMax = filteringParameters.GreenMax;
                EditedRangeMin = filteringParameters.GreenMin;
                break;
            case EditedRange.Yellow:
                EditedRangeMax = filteringParameters.YellowMax;
                EditedRangeMin = filteringParameters.YellowMin;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void OnDestroy()
    {
        cameraTimer.Stop();
        capture.Stop();
    }
}



