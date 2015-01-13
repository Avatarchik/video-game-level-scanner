using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ImageRecognitionLibrary;

public class UnityAppSettingsManager : ISettingsManager
{
    private static Dictionary<ColorSetting, string> KeysNames = new Dictionary<ColorSetting, string>{
        {ColorSetting.BlueMin, "BlueMin"},
        {ColorSetting.RedMin, "RedMin"},
        {ColorSetting.RedMin2, "RedMin2"},
        {ColorSetting.GreenMin, "GreenMin"},
        {ColorSetting.YellowMin, "YellowMin"},
        {ColorSetting.BlueMax, "BlueMax"},
        {ColorSetting.RedMax, "RedMax"},
        {ColorSetting.RedMax2, "RedMax2"},
        {ColorSetting.GreenMax, "GreenMax"},
        {ColorSetting.YellowMax, "YellowMax"}
    };
    
    public string Read(ColorSetting setting)
    {
        return PlayerPrefs.GetString(KeysNames[setting]);
    }

    public void Save(ColorSetting setting, string value)
    {
        PlayerPrefs.SetString(KeysNames[setting], value);
        PlayerPrefs.Save();
    }

    public bool DoesValuesExist()
    {
        foreach (var pair in KeysNames)
        {
            if (!PlayerPrefs.HasKey(pair.Value))
                return false;
        }
        return true;
    }
}
