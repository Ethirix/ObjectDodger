using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectPopulateGrid : MonoBehaviour {
    [SerializeField] private GameObject prefab;

    private void Start() { 
        Populate();
    }

    private void Populate() {
        List<string> scenes = new List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) { 
            scenes.Add(SceneUtility.GetScenePathByBuildIndex(i));
        }
        
        List<string> levelScenes = scenes.Where(scene => scene.Contains("Level_")).ToList();

        foreach (string scene in levelScenes) {
            AddSceneToUI(scene);
        }
    }

    private void AddSceneToUI(string scene)
    {
        GameObject newGameObject = Instantiate(prefab, transform);
        LoadLevel loadLevelScript = newGameObject.GetComponent<LoadLevel>();
        string levelID = scene.Split("_.".ToCharArray())[1];

        newGameObject.transform.Find("Text").GetComponent<TMP_Text>().text = levelID;
        loadLevelScript.level = levelID;
        loadLevelScript.btn = newGameObject.GetComponentInChildren<Button>();
    }
}
