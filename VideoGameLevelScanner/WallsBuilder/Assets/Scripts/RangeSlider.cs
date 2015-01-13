using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RangeSlider : MonoBehaviour {
    Slider minSlider;
    Slider maxSlider;
    Text minText;
    Text maxText;
 
    public float min;
    public float Min
    {
        get { return min; }
        set 
        {
            this.min = value;
            minSlider.value = value;
            minText.text = value.ToString();
            if (value > this.max)
                this.Max = value;
        }
    }

    public float max;
    public float Max
    {
        get { return max; }
        set 
        { 
            max = value;
            maxSlider.value = value;
            maxText.text = value.ToString();
            if (value < this.min)
                this.Min = value;
        }
    }

    public float minimalValue;
    public float MinValue
    {
        get { return minimalValue; }
        set 
        { 
            minimalValue = value;
            minSlider.minValue = value;
            maxSlider.minValue = value;
        }
    }
    public float maximalValue;
    public float MaxValue
    {
        get { return maximalValue; }
        set 
        { 
            maximalValue = value;
            minSlider.maxValue = value;
            maxSlider.maxValue = value;
        }
    }

    private void Start()
    {
        minSlider = transform.FindChild("Background/MinSlider").GetComponent<Slider>();
        maxSlider = transform.FindChild("Background/MaxSlider").GetComponent<Slider>();
        minText = transform.FindChild("MinText").GetComponent<Text>();
        maxText = transform.FindChild("MaxText").GetComponent<Text>();
        this.Min = min;
        this.Max = max;
        this.MaxValue = maximalValue;
        this.MinValue = minimalValue;
    }


    
}
