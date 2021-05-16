using TMPro;
using UnityEngine;

public class InitialiseAfterLoadingScript : MonoBehaviour {
    [SerializeField] private Canvas creditsCanvas;
    [SerializeField] private Canvas loadingCanvas;
    [SerializeField] private TMP_Text loadingDelayText;
    public delegate void LoadedEvent();
    public static event LoadedEvent Loaded;

    public void RunFullyLoaded() {
        Loaded?.Invoke();
        loadingCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
        loadingDelayText.gameObject.SetActive(false);
    }
}
