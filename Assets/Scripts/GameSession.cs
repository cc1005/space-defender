using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] float playerScore = 100f;
    [SerializeField] float pointsPerKill = 10f;
    [SerializeField] TextMeshProUGUI playerScoreText;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpScoreSingleton();
    }

    private void SetUpScoreSingleton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Start Menu")
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            playerScoreText.text = playerScore.ToString();
        }
    }

    public void AddToScore()
    {
        playerScore += pointsPerKill;
        playerScoreText.text = playerScore.ToString();
    }
}
