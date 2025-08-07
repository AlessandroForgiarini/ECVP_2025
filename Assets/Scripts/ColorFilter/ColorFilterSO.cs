using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ColorFilterSO : ScriptableObject
{
    [Serializable]
    public enum ColorBlindMode
    {
        None,
        Protanopia,
        Deuteranopia,
        Tritanopia,
        Achromatopsia,
        Protanomaly,
        Deuteranomaly,
        Tritanomaly,
    }

    [Serializable]
    public struct ColorFilterData
    {
        public ColorBlindMode mode;
        [Range(0f,1f)]
        public float intensity;
    }
    
    public List<ColorFilterData> colorFilters;

    public ColorFilterData GetColorFilterData(ColorBlindMode mode)
    {
        return colorFilters.Find(x => x.mode == mode);
    }

    public void UpdateFilterData(ColorBlindMode filterMode, float currentStrength)
    {
        for (int i = 0; i < colorFilters.Count; i++)
        {
            if (colorFilters[i].mode == filterMode)
            {
                ColorFilterData updatedData = colorFilters[i];
                updatedData.intensity = Mathf.Clamp01(currentStrength); // Ensure within [0,1] range
                colorFilters[i] = updatedData;
                return;
            }
        }
    }
}