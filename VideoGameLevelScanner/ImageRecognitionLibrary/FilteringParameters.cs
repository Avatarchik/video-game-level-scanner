using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageRecognitionLibrary
{
    public class FilteringParameters
    {
        ISettingsManager settingsManager;
        private FilteringParameters(ISettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;
            ReadValuesFromSettings();
        }

        public void ResetValues()
        {
            ReadValuesFromSettings();
        }

        private void ReadValuesFromSettings()
        {
            blueMin = ParseSettingToColor(settingsManager.Read(ColorSetting.BlueMin));
            blueMin = ParseSettingToColor(settingsManager.Read(ColorSetting.BlueMin));
            blueMax = ParseSettingToColor(settingsManager.Read(ColorSetting.BlueMax));
            redMin = ParseSettingToColor(settingsManager.Read(ColorSetting.RedMin));
            redMax = ParseSettingToColor(settingsManager.Read(ColorSetting.RedMax));
            redMin2 = ParseSettingToColor(settingsManager.Read(ColorSetting.RedMin2));
            redMax2 = ParseSettingToColor(settingsManager.Read(ColorSetting.RedMax2));
            greenMin = ParseSettingToColor(settingsManager.Read(ColorSetting.GreenMin));
            greenMax = ParseSettingToColor(settingsManager.Read(ColorSetting.GreenMax));
            yellowMin = ParseSettingToColor(settingsManager.Read(ColorSetting.YellowMin));
            yellowMax = ParseSettingToColor(settingsManager.Read(ColorSetting.YellowMax));
        }

        private string HsvToString(Hsv color)
        {
            return (int)color.Hue + "," + (int)color.Satuation + "," + (int)color.Value;
        }

        public void SaveNewValues()
        {
            settingsManager.Save(ColorSetting.BlueMin, HsvToString(blueMin));
            settingsManager.Save(ColorSetting.BlueMax, HsvToString(blueMax));
            settingsManager.Save(ColorSetting.RedMin, HsvToString(redMin));
            settingsManager.Save(ColorSetting.RedMax, HsvToString(redMax));
            settingsManager.Save(ColorSetting.RedMin2, HsvToString(redMin2));
            settingsManager.Save(ColorSetting.RedMax2, HsvToString(redMax2));
            settingsManager.Save(ColorSetting.GreenMin, HsvToString(greenMin));
            settingsManager.Save(ColorSetting.GreenMax, HsvToString(greenMax));
            settingsManager.Save(ColorSetting.YellowMin, HsvToString(yellowMin));
            settingsManager.Save(ColorSetting.YellowMax, HsvToString(yellowMax));
        }

        private Hsv ParseSettingToColor(string setting)
        {
            string[] values = setting.Split(',');
            byte h = 0;
            byte s = 0;
            byte v = 0;
            Byte.TryParse(values[0], out h);
            Byte.TryParse(values[1], out s);
            Byte.TryParse(values[2], out v);
            if (h > 179)
                h = 179;
            return new Hsv(h, s, v);
        }
        private Hsv blueMin;
        private Hsv blueMax;
        private Hsv redMin;
        private Hsv redMax;
        private Hsv redMin2;
        private Hsv redMax2;
        private Hsv greenMin;
        private Hsv greenMax;
        private Hsv yellowMin;
        private Hsv yellowMax;

        public double BlueMinH { get { return blueMin.Hue; } set { blueMin.Hue = value; } }
        public double BlueMinS { get { return blueMin.Satuation; } set { blueMin.Satuation = value; } }
        public double BlueMinV { get { return blueMin.Value; } set { blueMin.Value = value; } }
        public double BlueMaxH { get { return blueMax.Hue; } set { blueMax.Hue = value; } }
        public double BlueMaxS { get { return blueMax.Satuation; } set { blueMax.Satuation = value; } }
        public double BlueMaxV { get { return blueMax.Value; } set { blueMax.Value = value; } }

        public double RedMinH { get { return redMin.Hue; } set { redMin.Hue = value; } }
        public double RedMinS { get { return redMin.Satuation; } set { redMin.Satuation = value; } }
        public double RedMinV { get { return redMin.Value; } set { redMin.Value = value; } }
        public double RedMaxH { get { return redMax.Hue; } set { redMax.Hue = value; } }
        public double RedMaxS { get { return redMax.Satuation; } set { redMax.Satuation = value; } }
        public double RedMaxV { get { return redMax.Value; } set { redMax.Value = value; } }

        public double RedMin2H { get { return redMin2.Hue; } set { redMin2.Hue = value; } }
        public double RedMin2S { get { return redMin2.Satuation; } set { redMin2.Satuation = value; } }
        public double RedMin2V { get { return redMin2.Value; } set { redMin2.Value = value; } }
        public double RedMax2H { get { return redMax2.Hue; } set { redMax2.Hue = value; } }
        public double RedMax2S { get { return redMax2.Satuation; } set { redMax2.Satuation = value; } }
        public double RedMax2V { get { return redMax2.Value; } set { redMax2.Value = value; } }

        public double GreenMinH { get { return greenMin.Hue; } set { greenMin.Hue = value; } }
        public double GreenMinS { get { return greenMin.Satuation; } set { greenMin.Satuation = value; } }
        public double GreenMinV { get { return greenMin.Value; } set { greenMin.Value = value; } }
        public double GreenMaxH { get { return greenMax.Hue; } set { greenMax.Hue = value; } }
        public double GreenMaxS { get { return greenMax.Satuation; } set { greenMax.Satuation = value; } }
        public double GreenMaxV { get { return greenMax.Value; } set { greenMax.Value = value; } }

        public double YellowMinH { get { return yellowMin.Hue; } set { yellowMin.Hue = value; } }
        public double YellowMinS { get { return yellowMin.Satuation; } set { yellowMin.Satuation = value; } }
        public double YellowMinV { get { return yellowMin.Value; } set { yellowMin.Value = value; } }
        public double YellowMaxH { get { return yellowMax.Hue; } set { yellowMax.Hue = value; } }
        public double YellowMaxS { get { return yellowMax.Satuation; } set { yellowMax.Satuation = value; } }
        public double YellowMaxV { get { return yellowMax.Value; } set { yellowMax.Value = value; } }

        public Hsv BlueMin { get { return blueMin; } set { blueMin = value; } }
        public Hsv BlueMax { get { return blueMax; } set { blueMax = value; } }
        public Hsv RedMin { get { return redMin; } set { redMin = value; } }
        public Hsv RedMax { get { return redMax; } set { redMax = value; } }
        public Hsv RedMin2 { get { return redMin2; } set { redMin2 = value; } }
        public Hsv RedMax2 { get { return redMax2; } set { redMax2 = value; } }
        public Hsv GreenMin { get { return greenMin; } set { greenMin = value; } }
        public Hsv GreenMax { get { return greenMax; } set { greenMax = value; } }
        public Hsv YellowMin { get { return yellowMin; } set { yellowMin = value; } }
        public Hsv YellowMax { get { return yellowMax; } set { yellowMax = value; } }

        public KeyValuePair<Hsv, Hsv> BlueRange { get { return new KeyValuePair<Hsv, Hsv>(blueMin, blueMax); } }
        public List<KeyValuePair<Hsv, Hsv>> RedRange
        {
            get
            {
                return new List<KeyValuePair<Hsv, Hsv>>
                { 
                    new KeyValuePair<Hsv,Hsv>(redMin,redMax), 
                    new KeyValuePair<Hsv,Hsv>(redMin2,redMax2)
                };
            }
        }
        public KeyValuePair<Hsv, Hsv> GreenRange { get { return new KeyValuePair<Hsv, Hsv>(greenMin, greenMax); } }
        public KeyValuePair<Hsv, Hsv> YellowRange { get { return new KeyValuePair<Hsv, Hsv>(yellowMin, yellowMax); } }
    }
}
