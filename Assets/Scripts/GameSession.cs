using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerScore = 100;

    void Awake()
    {
        SetUpScoreSingleton();
    }

    private void SetUpScoreSingleton()
    {

        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }

    public int GetScore()
    {
        return playerScore;
    }

    public void AddToScore(int scoreValue)
    {
        playerScore += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }


}
