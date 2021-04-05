using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public int level;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(RunLoad);
    }

    private void RunLoad()
    {
        SceneManager.LoadSceneAsync("L_Level" + level).allowSceneActivation = true;
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("L_Level" + level));
    }
}
