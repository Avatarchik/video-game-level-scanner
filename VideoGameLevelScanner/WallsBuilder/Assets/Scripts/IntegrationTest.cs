﻿using UnityEngine;
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

    //public static bool CameraOn = false;
    public FilteringSliders FilteringSliders;
    public UnityEngine.UI.Image CameraImageUI;
    public UnityEngine.UI.Image LookupUI;
    public LevelBuilder LevelCreator;

    private bool newFrame = true;
    private bool processingFrame = false;
    private Capture capture;
    private Image<Bgr,byte> frame;
    private Image<Bgr, byte> lookupImage;
    private Board board;
    private Timer cameraTimer;
    private FilteringParameters filteringParameters;
    private EditedRangeIndex editedRangeIndex;
    public Range<Hsv> EditedRange;
	private Texture2D cameraTex;
    private Texture2D lookupTex;

    private void Awake()
    {
		capture = new Capture ();
		//CameraOn = true;
        filteringParameters = new FilteringParameters(new UnityAppSettingsManager());
        cameraTimer = new Timer(40);
        cameraTimer.Elapsed += new ElapsedEventHandler(OnCameraFrame);
        capture.SetCaptureProperty(CAP_PROP.CV_CAP_PROP_FPS, 25);
	}

	private void Start()
	{
        ChangeEditedRange(0);
        cameraTimer.Enabled = true;
        cameraTimer.Start();
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
        newFrame = true;
    }

    private void AnalyseFrame()
    {
        if(frame!=null)
            frame.Dispose();
        frame = capture.QueryFrame();
        if (frame != null)
        {
            GameObject.Destroy(cameraTex);
            cameraTex = TextureConvert.ImageToTexture2D<Bgr, byte>(frame, true);
            Sprite.DestroyImmediate(CameraImageUI.GetComponent<UnityEngine.UI.Image>().sprite);
            CameraImageUI.sprite = Sprite.Create(cameraTex, new Rect(0, 0, cameraTex.width, cameraTex.height), new Vector2(0.5f, 0.5f));
        }
        if (true)
        //if (!processingFrame) 
        {
            processingFrame = true;
            
            board = ImageTools.ReadFromFrame(frame.Clone(),filteringParameters);

            if (lookupImage != null)
                lookupImage.Dispose();

            if (board != null)
                lookupImage = ImageTools.DrawRooms(320, 240, board.Grid);
            else
                lookupImage = new Image<Bgr, byte>(320, 240, new Bgr(0, 0, 0));

            if (lookupImage != null)
            {
                GameObject.Destroy(lookupTex);
                lookupTex = TextureConvert.ImageToTexture2D<Bgr, byte>(lookupImage, true);
                Sprite.DestroyImmediate(LookupUI.GetComponent<UnityEngine.UI.Image>().sprite);
                LookupUI.sprite = Sprite.Create(lookupTex, new Rect(0, 0, lookupTex.width, lookupTex.height), new Vector2(0.5f, 0.5f));
            }
            processingFrame = false;
		}
    }

    public float EditedHueMax 
    { 
        get { return (float)this.EditedRange.Max.Hue; } 
        set { this.EditedRange.Max.Hue = value; } 
    }
    public float EditedSaturationMax 
    { 
        get { return (float)this.EditedRange.Max.Satuation; } 
        set { EditedRange.Max.Satuation = value; } 
    }
    public float EditedValueMax 
    {
        get { return (float)this.EditedRange.Max.Value; } 
        set { EditedRange.Max.Value = value; } 
    }
    public float EditedHueMin 
    { 
        get { return (float)this.EditedRange.Min.Hue; } 
        set { EditedRange.Min.Hue = value; } 
    }
    public float EditedSaturationMin
    { 
        get { return (float)this.EditedRange.Min.Satuation; } 
        set { EditedRange.Min.Satuation = value; } 
    }
    public float EditedValueMin 
    { 
        get { return (float)this.EditedRange.Min.Value; } 
        set { EditedRange.Min.Value = value; } 
    }
    
    public void GoToBuilding()
    {
        cameraTimer.Stop();
        capture.Stop();
        LevelCreator.BuildLevel(board);
    }
    public void SaveParameters()
    {
        filteringParameters.SaveNewValues();
    }
    public void ResetParameters()
    {
        filteringParameters.ResetValues();
        ChangeEditedRange((int)editedRangeIndex);
    }

    public void ToogleAnalysis()
    {
        if (cameraTimer.Enabled)
            cameraTimer.Stop();
        else
            cameraTimer.Start();
    }

    public enum EditedRangeIndex {Blue=0, Red=1, Red2=2, Green=3, Yellow=4}
    public void ChangeEditedRange(int range)
    {
        editedRangeIndex = (EditedRangeIndex)range;
        switch (editedRangeIndex)
        {
            case EditedRangeIndex.Blue:
                EditedRange = filteringParameters.BlueRange[0];
                break;
            case EditedRangeIndex.Red:
                EditedRange = filteringParameters.RedRange[0];
                break;
            case EditedRangeIndex.Red2:
                EditedRange = filteringParameters.RedRange[1];
                break;
            case EditedRangeIndex.Green:
                EditedRange = filteringParameters.GreenRange[0];
                break;
            case EditedRangeIndex.Yellow:
                EditedRange = filteringParameters.YellowRange[0];
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        FilteringSliders.SetRanges(EditedRange);

    }
    private void OnDestroy()
    {
        cameraTimer.Stop();
        capture.Stop();
    }
}



