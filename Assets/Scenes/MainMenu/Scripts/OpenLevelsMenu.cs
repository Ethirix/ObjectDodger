using UnityEngine;
using UnityEngine.UI;

public class OpenLevelsMenu : MonoBehaviour
{
    [SerializeField] private Button levelsButton;
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas levelsMenuCanvas;
    
    private void Start()
    {
        levelsButton.onClick.AddListener(LoadLevelUI);
    }

    private void LoadLevelUI()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        levelsMenuCanvas.gameObject.SetActive(true);
    }
}
