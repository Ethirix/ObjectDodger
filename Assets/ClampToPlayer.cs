using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ClampToPlayer : MonoBehaviour
{
    [SerializeField] private Rigidbody player;
    [SerializeField] private Transform postfx;
    private Volume _pfx;
    private ColorAdjustments _colorAdjustments;
    private VolumeProfile _vP;
    private bool _doingRestart = false;
    private void OnEnable() {
        PlayerCollision.PlayerLostEvent += DoLossBlur;
        RespawnScript.RespawnEvent += DoLossBlur;
        PlayerCollision.RestartEvent += RemoveAllEffects;
        PlayerCollision.WinEvent += DoWinEffect;
        _pfx = postfx.gameObject.GetComponent<Volume>();
        _vP = _pfx.profile;
        _colorAdjustments = _vP.components[1] as ColorAdjustments;
    }

    private void OnDisable() {
        PlayerCollision.PlayerLostEvent -= DoLossBlur;
        RespawnScript.RespawnEvent -= DoLossBlur;
        PlayerCollision.RestartEvent -= RemoveAllEffects;
        PlayerCollision.WinEvent -= DoWinEffect;
    }

    private void DoWinEffect() {
        LeanTween.value(gameObject, UpdateBlur, _pfx.weight, 1.0f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
        
        LeanTween.value(gameObject, UpdateColorR, _colorAdjustments.colorFilter.value.r, 0.9f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.value(gameObject, UpdateColorB, _colorAdjustments.colorFilter.value.b, 0.9f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
        
    }

    private void DoLossBlur() {
        if (!_doingRestart) {
            _doingRestart = true;
            LeanTween.value(gameObject, UpdateBlur, _pfx.weight, 1.0f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.value(gameObject, UpdateColorR, _colorAdjustments.colorFilter.value.r, 0f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.value(gameObject, UpdateColorG, _colorAdjustments.colorFilter.value.g, 0f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.value(gameObject, UpdateColorB, _colorAdjustments.colorFilter.value.b, 0f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
        }
    }

    private void RemoveAllEffects() {
        _doingRestart = false;
        LeanTween.value(gameObject, UpdateBlur, _pfx.weight, 0.0f, 0.25f).setEase(LeanTweenType.easeInOutQuad);

        LeanTween.value(gameObject, UpdateColorR, _colorAdjustments.colorFilter.value.r, 1.0f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.value(gameObject, UpdateColorG, _colorAdjustments.colorFilter.value.g, 1.0f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.value(gameObject, UpdateColorB, _colorAdjustments.colorFilter.value.b, 1.0f, 1.0f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void UpdateBlur(float newVal) {
        _pfx.weight = newVal;
    }
    private void UpdateColorR(float newVal) {
        _colorAdjustments.colorFilter.Override(new Color(newVal, _colorAdjustments.colorFilter.value.g, _colorAdjustments.colorFilter.value.b));
    }
    private void UpdateColorG(float newVal) {
        _colorAdjustments.colorFilter.Override(new Color(_colorAdjustments.colorFilter.value.r, newVal, _colorAdjustments.colorFilter.value.b));
    }
    private void UpdateColorB(float newVal) {
        _colorAdjustments.colorFilter.Override(new Color(_colorAdjustments.colorFilter.value.r, _colorAdjustments.colorFilter.value.g, newVal));
    }
    
    private void FixedUpdate() {
        postfx.position = player.position;
    }
}
