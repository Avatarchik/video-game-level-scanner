using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageRecognitionLibrary
{
    public enum ColorSetting { BlueMin, BlueMax, RedMin, RedMax, RedMin2, RedMax2, GreenMin, GreenMax, YellowMin, YellowMax }
    
    public interface ISettingsManager
    {
        string Read(ColorSetting setting);
        void Save(ColorSetting setting, string value);
        bool DoesValuesExist();
    }
}
