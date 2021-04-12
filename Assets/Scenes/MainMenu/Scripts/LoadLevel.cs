using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public string level;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(RunLoad);
    }

    private void RunLoad()
    {
        SceneManager.LoadSceneAsync("Level_" + level).allowSceneActivation = true;
    }
}
