using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarUpdateTextScript : MonoBehaviour {
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text text;
    [SerializeField] private InitialiseAfterLoadingScript loadingScript;

    private void Start() {
        SetText(slider.value);
    }

    private void OnEnable() {
        slider.onValueChanged.AddListener(UpdateText);
    }

    private void OnDisable() {
        slider.onValueChanged.RemoveListener(UpdateText);
    }

    private void UpdateText(float value) {
        SetText(value);
        if (Math.Abs(value - 1f) < 0.001f) {
            loadingScript.RunFullyLoaded();
        }
    }

    private void SetText(float value) {
        text.text = (value * 100f).ToString("0") + "%";
    }

    public float SliderValue() {
        return slider.value;
    }

    public void UpdateLoadValue(float value) {
        if (slider.value + value > 1f) { value = 1f; }  
        slider.value = value;
    }
}
