using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectPopulateGrid : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
        
    private void Start()
    { 
        Populate();
    }

    private void Populate()
    { 
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] scenes = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++) { 
            scenes[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }

        for (int i = 0; i < sceneCount; i++) {
            if (scenes[i].Contains("L_Level")) {
                int levelNo = int.Parse(scenes[i][scenes[i].Length - 7].ToString());
                GameObject newGameObject = Instantiate(prefab, transform);
                newGameObject.transform.Find("Text").GetComponent<TMP_Text>().text = levelNo.ToString();
                newGameObject.GetComponent<LoadLevel>().level = levelNo;
                newGameObject.GetComponent<LoadLevel>().btn = newGameObject.GetComponentInChildren<Button>();
            }
        }
    }

}
