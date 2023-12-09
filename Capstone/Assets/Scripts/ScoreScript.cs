using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;
    private Text playerScore;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "VictoryScene")
        {
            playerScore.text = "Score: " + scoreValue;
        }
        else
        {
            playerScore.text = " " + scoreValue + " ";
        }
        
    }
}
