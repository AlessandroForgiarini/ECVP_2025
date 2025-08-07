using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using ColorBlindMode = ColorFilterSO.ColorBlindMode;

// LUT sources https://github.com/andrewwillmott/colour-blind-luts

[RequireComponent(typeof(Volume))]
public class ColorBlindSimulator : MonoBehaviour
{
    public static ColorBlindSimulator Instance { get; private set; }

    [SerializeField] private Texture lutNormal;
    [SerializeField] private Texture lutProtanopia;
    [SerializeField] private Texture lutDeuteranopia;
    [SerializeField] private Texture lutTritanopia;
    
    private Volume _volume;
    private ColorCurves _colorCurves;
    private ColorLookup _colorLookup;

    private ColorBlindMode _activeFilter;
    private float _activeIntensity;
    
    private void Awake()
    {
        Instance = this;
        _activeFilter = ColorBlindMode.None;
        
        _volume = GetComponent<Volume>();
        if (_volume == null)
        {
            Debug.LogError("Volume component is missing. Please add it to the GameObject.");
            return;
        }
        
        if (_volume.profile.TryGet<ColorCurves>(out ColorCurves curves))
        {
            _colorCurves = curves;
        }
        else
        {
            Debug.LogError("ColorCurves not found in the Volume profile. Please add it.");
        }
        
        if (_volume.profile.TryGet<ColorLookup>(out ColorLookup colorLookup))
        {
            _colorLookup = colorLookup;
        }
        else
        {
            Debug.LogError("ColorLookup not found in the Volume profile. Please add it.");
        }
    }

    public void ApplyFilter(ColorBlindMode filterMode, float intensity = 1f)
    {
        if (filterMode == _activeFilter)
        {
            if (Mathf.Approximately(intensity, _activeIntensity))
            {
                return;
            }
        }
        
        if (_activeFilter == ColorBlindMode.Achromatopsia)
        {
            UpdateColorCurves(GetDefaultColorCurves());
        }
        
        switch (filterMode)
        {
            case ColorBlindMode.None:
                UpdateColorLookup(lutNormal,0);
                break;
            case ColorBlindMode.Protanopia:
                UpdateColorLookup(lutProtanopia);
                break;
            case ColorBlindMode.Protanomaly:
                UpdateColorLookup(lutProtanopia, intensity);
                break;
            case ColorBlindMode.Deuteranopia:
                UpdateColorLookup(lutDeuteranopia);
                break;
            case ColorBlindMode.Deuteranomaly:
                UpdateColorLookup(lutDeuteranopia, intensity);
                break;
            case ColorBlindMode.Tritanopia:
                UpdateColorLookup(lutTritanopia);
                break;
            case ColorBlindMode.Tritanomaly:
                UpdateColorLookup(lutTritanopia, intensity);
                break;
            case ColorBlindMode.Achromatopsia:
                ColorCurves newColorCurves = GetDefaultColorCurves();
                AnimationCurve flatZeroCurve = GetFlatZeroAnimCurve();
                newColorCurves.hueVsSat.overrideState = true;
                newColorCurves.hueVsSat.value = new TextureCurve(flatZeroCurve, 0, true, Vector2.one);
                UpdateColorCurves(newColorCurves);
                break;
            default:
                break;
        }

        _activeIntensity = intensity;
        _activeFilter = filterMode;
    }
    
    private void UpdateColorCurves(ColorCurves newColorCurves)
    {
        _colorCurves.hueVsSat.overrideState = newColorCurves.hueVsSat.overrideState;
        _colorCurves.hueVsSat.value = newColorCurves.hueVsSat.value;
    }
    
    private void UpdateColorLookup(Texture lut, float contribution = 1f)
    {
        _colorLookup.texture.value = lut;
        _colorLookup.contribution.value = contribution;
    }

    private static ColorCurves GetDefaultColorCurves()
    {
        ColorCurves curves = ScriptableObject.CreateInstance<ColorCurves>();
        return curves;
    }
    
    private static AnimationCurve GetFlatZeroAnimCurve()
    {
        AnimationCurve flatCurve = new AnimationCurve();

        Keyframe key = new(0f, 0f, 0, 0, 1, 1);
        flatCurve.AddKey(key);

        key = new(1f, 0f, 0, 0, 1, 1);
        flatCurve.AddKey(key);
        return flatCurve;
    }
}
