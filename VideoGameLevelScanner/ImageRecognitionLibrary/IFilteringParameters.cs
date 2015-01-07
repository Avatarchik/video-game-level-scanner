using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageRecognitionLibrary
{
    public interface IFilteringParameters
    {
        void ResetValues(); 
        void SaveNewValues();      
        
        double BlueMinH { get; set; }
        double BlueMinS { get; set; }
        double BlueMinV { get; set; }
        double BlueMaxH { get; set; }
        double BlueMaxS { get; set; }
        double BlueMaxV { get; set; }

        double RedMinH { get; set; }
        double RedMinS { get; set; }
        double RedMinV { get; set; }
        double RedMaxH { get; set; }
        double RedMaxS { get; set; }
        double RedMaxV { get; set; }

        double RedMin2H { get; set; }
        double RedMin2S { get; set; }
        double RedMin2V { get; set; }
        double RedMax2H { get; set; }
        double RedMax2S { get; set; }
        double RedMax2V { get; set; }

        double GreenMinH { get; set; }
        double GreenMinS { get; set; }
        double GreenMinV { get; set; }
        double GreenMaxH { get; set; }
        double GreenMaxS { get; set; }
        double GreenMaxV { get; set; }

        double YellowMinH { get; set; }
        double YellowMinS { get; set; }
        double YellowMinV { get; set; }
        double YellowMaxH { get; set; }
        double YellowMaxS { get; set; }
        double YellowMaxV { get; set; }
        
        Hsv BlueMin { get; set; }
        Hsv BlueMax { get; set; }
        Hsv RedMin { get; set; }
        Hsv RedMax { get; set; }
        Hsv RedMin2 { get; set; }
        Hsv RedMax2 { get; set; }
        Hsv GreenMin { get; set; }
        Hsv GreenMax { get; set; }
        Hsv YellowMin { get; set; }
        Hsv YellowMax { get; set; }

        List<KeyValuePair<Hsv, Hsv>> RedRange { get; }
    }
}
