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
        public FilteringParameters(ISettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;
            this.InitializeRanges();
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
        private Hsv blueMin = new Hsv(90, 90, 90);
        private Hsv blueMax = new Hsv(120, 255, 255);
        private Hsv redMin = new Hsv(0,85,80);
        private Hsv redMax = new Hsv(12,255,255);
        private Hsv redMin2 = new Hsv(150,85,80);
        private Hsv redMax2 = new Hsv(179,255,255);
        private Hsv greenMin = new Hsv(35, 70, 35);
        private Hsv greenMax = new Hsv(90, 255, 255);
        private Hsv yellowMin = new Hsv(10, 70, 127);
        private Hsv yellowMax = new Hsv(35, 255, 255);

        private KeyValuePair<Hsv, Hsv>[] blueRange;
        private KeyValuePair<Hsv, Hsv>[] redRange;
        private KeyValuePair<Hsv, Hsv>[] greenRange;
        private KeyValuePair<Hsv, Hsv>[] yellowRange;
        private List<KeyValuePair<Hsv, Hsv>[]> colorRanges;

        private void InitializeRanges()
        {
            blueRange = new KeyValuePair<Hsv, Hsv>[] { new KeyValuePair<Hsv, Hsv>(blueMin, blueMax) };
            redRange = new KeyValuePair<Hsv, Hsv>[] { new KeyValuePair<Hsv, Hsv>(redMin, redMax), new KeyValuePair<Hsv, Hsv>(redMin2, redMax2) };
            greenRange = new KeyValuePair<Hsv, Hsv>[] { new KeyValuePair<Hsv, Hsv>(greenMin, greenMax) };
            yellowRange = new KeyValuePair<Hsv, Hsv>[] { new KeyValuePair<Hsv, Hsv>(yellowMin, yellowMax) };
            colorRanges = new List<KeyValuePair<Hsv, Hsv>[]> { blueRange, redRange, greenRange, yellowRange };
        }

        #region Single Parameters
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
        #endregion
        public Hsv BlueMin { get { return blueMin; } private set { blueMin.Hue = value.Hue; blueMin.Satuation = value.Satuation; blueMin.Value = value.Value; } }
        public Hsv BlueMax { get { return blueMax; } private set { blueMax.Hue = value.Hue; blueMax.Satuation = value.Satuation; blueMax.Value = value.Value; } }
        public Hsv RedMin { get { return redMin; } private set { redMin.Hue = value.Hue; redMin.Satuation = value.Satuation; redMin.Value = value.Value; } }
        public Hsv RedMax { get { return redMax; } private set { redMax.Hue = value.Hue; redMax.Satuation = value.Satuation; redMax.Value = value.Value; } }
        public Hsv RedMin2 { get { return redMin2; } private set { redMin2.Hue = value.Hue; redMin2.Satuation = value.Satuation; redMin2.Value = value.Value; } }
        public Hsv RedMax2 { get { return redMax2; } private set { redMax2.Hue = value.Hue; redMax2.Satuation = value.Satuation; redMax2.Value = value.Value; } }
        public Hsv GreenMin { get { return greenMin; } private set { greenMin.Hue = value.Hue; greenMin.Satuation = value.Satuation; greenMin.Value = value.Value; } }
        public Hsv GreenMax { get { return greenMax; } private set { greenMax.Hue = value.Hue; greenMax.Satuation = value.Satuation; greenMax.Value = value.Value; } }
        public Hsv YellowMin { get { return yellowMin; } private set { yellowMin.Hue = value.Hue; yellowMin.Satuation = value.Satuation; yellowMin.Value = value.Value; } }
        public Hsv YellowMax { get { return yellowMax; } private set { yellowMax.Hue = value.Hue; yellowMax.Satuation = value.Satuation; yellowMax.Value = value.Value; } }

        public KeyValuePair<Hsv, Hsv>[] BlueRange { get { return blueRange; } }
        public KeyValuePair<Hsv, Hsv>[] RedRange { get { return redRange; } }
        public KeyValuePair<Hsv, Hsv>[] GreenRange { get { return greenRange; } }
        public KeyValuePair<Hsv, Hsv>[] YellowRange { get { return yellowRange; } }
        public List<KeyValuePair<Hsv, Hsv>[]> ColorsRanges { get { return colorRanges; } } 
    }
}
