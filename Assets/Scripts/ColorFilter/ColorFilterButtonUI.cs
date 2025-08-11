using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using ColorBlindMode = ColorFilterSO.ColorBlindMode;

public class ColorFilterButtonUI : MonoBehaviour
{
    private const float DefaultFilterStrength = 1;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color selectedColor = new Color(.9f,0.55f,0);
    [SerializeField] private Color deSelectedColor = new Color(1f,1f,1f);
    [SerializeField] private ColorBlindMode filterMode;
    [SerializeField] private GameObject sliderContainer;
    [SerializeField] private TMP_Text buttonTextLabel;
    [SerializeField] private TMP_Text sliderTextLabel;
    [SerializeField] private Slider slider;
    
    private ColorFiltersUI _uiController;
    private float _currentStrength;
    private bool _isSelected;

    private void Start()
    {
        _uiController= GetComponentInParent<ColorFiltersUI>();
        _currentStrength = _uiController.GetIntensity(filterMode);
        _isSelected = false;
        
        slider.value = _currentStrength;
    }

    private void OnValidate()
    {
        gameObject.name = filterMode.ToString() + "Btn";
        backgroundImage.color = deSelectedColor;
        string[] nameParts =  Regex.Split(filterMode.ToString(), @"(?<!^)(?=[A-Z])");
        buttonTextLabel.text = string.Join(" ", nameParts);
        _currentStrength = _uiController != null? _uiController.GetIntensity(filterMode) : DefaultFilterStrength;
        slider.value = _currentStrength;
        _isSelected = false;
        
        switch (filterMode)
        {
            case ColorBlindMode.None:
            case ColorBlindMode.Protanopia:
            case ColorBlindMode.Deuteranopia:
            case ColorBlindMode.Tritanopia:
            case ColorBlindMode.Achromatopsia:
                sliderContainer.gameObject.SetActive(false);
                break;
            case ColorBlindMode.Protanomaly:
            case ColorBlindMode.Deuteranomaly:
            case ColorBlindMode.Tritanomaly:
                sliderContainer.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnSliderValueChanged()
    {
        if (_isSelected)
        {
            _currentStrength =  slider.value;
            _currentStrength = Mathf.Round(_currentStrength * 100f) / 100f;
            _uiController.UpdateFilterData(filterMode, _currentStrength);
            ApplyFilter();
        }
        else
        {
            slider.value = _currentStrength;
        }
        
        sliderTextLabel.text = _currentStrength.ToString("0.00");
    }
    
    public void ApplyFilter()
    {
        if (!_isSelected)
        {
            _uiController.ClearFilterSelection();

            if (filterMode != ColorBlindMode.None)
            {
                SelectButton();
            }
        }

        if (ColorBlindSimulator.Instance == null) return;
        ColorBlindSimulator.Instance.ApplyFilter(filterMode, _currentStrength);
    }

    public void SelectButton()
    {
        backgroundImage.color = selectedColor;
        _isSelected = true;
    }
    
    public void DeselectButton()
    {
        backgroundImage.color = deSelectedColor;
        _isSelected = false;
    }
}
