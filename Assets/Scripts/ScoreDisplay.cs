using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{

    TextMeshProUGUI playerScoreText;
    GameSession gameSession;

    private void Start()
    {
        playerScoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = gameSession.GetScore().ToString();
    }
}
