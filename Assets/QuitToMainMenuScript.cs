using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitToMainMenuScript : MonoBehaviour
{
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(ExitToMenu);
    }

    public static void ExitToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu").allowSceneActivation = true;
    }
}
