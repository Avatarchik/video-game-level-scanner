using Emgu.CV.Structure;
using ImageRecognitionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryTestingProgram
{
    public class UserSettings : IFilteringParameters
    {
        public static readonly UserSettings instance = new UserSettings();
        public IFilteringParameters Instance { get { return instance as IFilteringParameters; } }
        private UserSettings()
        {
            ReadValuesFromSettings();
        }

        public void ResetValues()
        {
            ReadValuesFromSettings();
        }

        private void ReadValuesFromSettings()
        {
            blueMin = ParseSettingToColor(Properties.Settings.Default.blueMin);
            blueMax = ParseSettingToColor(Properties.Settings.Default.blueMax);
            redMin = ParseSettingToColor(Properties.Settings.Default.redMin);
            redMax = ParseSettingToColor(Properties.Settings.Default.redMax);
            redMin2 = ParseSettingToColor(Properties.Settings.Default.redMin2);
            redMax2 = ParseSettingToColor(Properties.Settings.Default.redMax2);
            greenMin = ParseSettingToColor(Properties.Settings.Default.greenMin);
            greenMax = ParseSettingToColor(Properties.Settings.Default.greenMax);
            yellowMin = ParseSettingToColor(Properties.Settings.Default.yellowMin);
            yellowMax = ParseSettingToColor(Properties.Settings.Default.yellowMax);
        }

        private string HsvToString(Hsv color)
        {
            return (int)color.Hue + "," + (int)color.Satuation + "," + (int)color.Value;
        }

        public void SaveNewValues()
        {
            Properties.Settings.Default.blueMin = HsvToString(blueMin);
            Properties.Settings.Default.blueMax = HsvToString(blueMax);
            Properties.Settings.Default.redMin = HsvToString(redMin);
            Properties.Settings.Default.redMax = HsvToString(redMax);
            Properties.Settings.Default.redMin2 = HsvToString(redMin2);
            Properties.Settings.Default.redMax2 = HsvToString(redMax2);
            Properties.Settings.Default.greenMin = HsvToString(greenMin);
            Properties.Settings.Default.greenMax = HsvToString(greenMax);
            Properties.Settings.Default.yellowMin = HsvToString(yellowMin);
            Properties.Settings.Default.yellowMax = HsvToString(yellowMax);
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
        public Hsv blueMin;
        public Hsv blueMax;
        public Hsv redMin;
        public Hsv redMax;
        public Hsv redMin2;
        public Hsv redMax2;
        public Hsv greenMin;
        public Hsv greenMax;
        public Hsv yellowMin;
        public Hsv yellowMax;

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
        
        public List<KeyValuePair<Hsv, Hsv>> RedRange 
        { 
            get 
            { 
                return new List<KeyValuePair<Hsv,Hsv>>
                { 
                    new KeyValuePair<Hsv,Hsv>(redMin,redMax), 
                    new KeyValuePair<Hsv,Hsv>(redMin2,redMax2)
                };
            } 
        }
    }
}
