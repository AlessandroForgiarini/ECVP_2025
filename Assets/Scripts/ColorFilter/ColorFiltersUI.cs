using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using ColorBlindMode = ColorFilterSO.ColorBlindMode;

public class ColorFiltersUI : MonoBehaviour
{
    [SerializeField] private GameObject colorFiltersContainer;
    [SerializeField] private ColorFilterSO colorFilterSO;
    
    private void Awake()
    {
        #if !UNITY_EDITOR
            Destroy(gameObject);
        #endif
    }

    public void ToggleColorFilters()
    {
        if (colorFiltersContainer.activeSelf)
        {
            colorFiltersContainer.SetActive(false);
        }
        else
        {
            colorFiltersContainer.SetActive(true);
        }
    }

    public void ClearFilterSelection()
    {
        // retrieving all buttons;
        ColorFilterButtonUI[] buttons =  colorFiltersContainer.GetComponentsInChildren<ColorFilterButtonUI>();

        foreach (ColorFilterButtonUI button in buttons)
        {
            button.DeselectButton();   
        }
    }

    public float GetIntensity(ColorBlindMode filterMode)
    {
        return colorFilterSO.GetColorFilterData(filterMode).intensity;
    }
    
    public void UpdateFilterData(ColorBlindMode filterMode, float currentStrength)
    {
        colorFilterSO.UpdateFilterData(filterMode, currentStrength);
#if UNITY_EDITOR
        EditorUtility.SetDirty(colorFilterSO);
        AssetDatabase.SaveAssets();
#endif
    }
}
