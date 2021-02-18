using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{

    Player player;
    TextMeshProUGUI playerHealthText;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerHealthText = FindObjectOfType<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = player.PublishHealthRemaining().ToString();
        Debug.Log("The Health text should have linked up");
    }
}
