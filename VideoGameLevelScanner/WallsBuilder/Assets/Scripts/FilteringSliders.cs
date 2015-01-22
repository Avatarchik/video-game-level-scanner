using UnityEngine;
using System.Collections;
using Emgu.CV.Structure;
using ImageRecognitionLibrary;

public class FilteringSliders : MonoBehaviour
{
    public RangeSlider HueSlider;
    public RangeSlider SaturationSlider;
    public RangeSlider ValueSlider;

    public void SetRanges(Range<Hsv> range)
    {
        HueSlider.SetRange((float)range.Min.Hue, (float)range.Max.Hue);
        SaturationSlider.SetRange((float)range.Min.Satuation, (float)range.Max.Satuation);
        ValueSlider.SetRange((float)range.Min.Value, (float)range.Max.Value);
    }
}
