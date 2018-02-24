using UnityEngine;
using System.Collections.Generic;
using System;

public class ColorPresetLibrary : MonoBehaviour {
    public ColorPresetData[] ColorPresetList = new ColorPresetData[0];
    public Dictionary<string, ColorPresetData> ColorPresetDictionary = new Dictionary<string, ColorPresetData>();

    public void Awake(){
        UpdateDictionary();
    }

    public void UpdateDictionary(){
        ColorPresetDictionary.Clear();
        for (int i = 0; i < ColorPresetList.Length; i++){
            if (ColorPresetList[i]){
                if (!ColorPresetDictionary.ContainsKey(ColorPresetList[i].ColorPresetName)){
                    ColorPresetDictionary.Add(ColorPresetList[i].ColorPresetName, ColorPresetList[i]);
                }
            }
        }
    }

    public void AddColorPreset(ColorPresetData ColorPreset)
    {
        for (int i = 0; i < ColorPresetList.Length; i++)
        {
            if (ColorPresetList[i].ColorPresetName == ColorPreset.ColorPresetName)
            {
                ColorPresetList[i] = ColorPreset;
                return;
            }
        }
        var list = new ColorPresetData[ColorPresetList.Length + 1];
        Array.Copy(ColorPresetList, list, ColorPresetList.Length );
        list[ColorPresetList.Length] = ColorPreset;
        ColorPresetList = list;
        ColorPresetDictionary.Add(ColorPreset.ColorPresetName, ColorPreset);
    }

    internal ColorPresetData GetColorPreset(string ColorPresetName)
    {
        return ColorPresetDictionary[ColorPresetName];
    }
}
