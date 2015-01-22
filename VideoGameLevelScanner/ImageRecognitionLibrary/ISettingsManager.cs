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
        void ChangeValue(ColorSetting setting, string value);
        void Save();
        bool DoValuesExist();
    }
}
