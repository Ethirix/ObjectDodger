using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMainMenu : MonoBehaviour {
    
    private void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadSceneAsync("MainMenu").allowSceneActivation = true;
        }
    }
}
