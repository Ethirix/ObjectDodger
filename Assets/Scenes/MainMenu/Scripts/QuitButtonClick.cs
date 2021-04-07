using UnityEngine;
using UnityEngine.UI;

public class QuitButtonClick : MonoBehaviour
{
    [SerializeField] private Button quitButton;

    private void Start()
    {
        quitButton.onClick.AddListener(CloseGame);
    }

    private static void CloseGame()
    {
        Application.Quit();
    }

}
