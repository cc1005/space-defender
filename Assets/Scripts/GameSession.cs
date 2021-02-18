using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
        SetUpScoreSingleton();
    }

    private void SetUpScoreSingleton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (FindObjectsOfType(GetType()).Length > 1 || sceneName == "Start Menu")
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("I will print if the score singleton has loaded");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
