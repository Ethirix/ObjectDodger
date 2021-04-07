using UnityEngine;
using UnityEngine.UI;

public class QuitToMainMenu : MonoBehaviour
{
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas currentCanvas;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        quitButton.onClick.AddListener(OpenMainMenu);
    }

    private void OpenMainMenu()
    {
        currentCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }
}
