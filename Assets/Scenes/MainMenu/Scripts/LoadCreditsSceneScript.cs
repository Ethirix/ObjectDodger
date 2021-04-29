using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCreditsSceneScript : MonoBehaviour {

    [SerializeField] private Button btn;

    private void OnEnable() {
        btn.onClick.AddListener(OpenCreditsScene);
    }

    private void OnDisable() {
        btn.onClick.RemoveListener(OpenCreditsScene);
    }

    private static void OpenCreditsScene() {
        SceneManager.LoadSceneAsync("CreditsScene").allowSceneActivation = true;
    }
}
