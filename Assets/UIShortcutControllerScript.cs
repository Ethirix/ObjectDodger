using UnityEngine;
using UnityEngine.UI;

public class UIShortcutControllerScript : MonoBehaviour
{
    [SerializeField] private Button btn;
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            QuitToMainMenuScript.ExitToMenu();
        }

        if (Input.GetKey(KeyCode.R)) {
            btn.onClick.Invoke();
        }
    }
}
