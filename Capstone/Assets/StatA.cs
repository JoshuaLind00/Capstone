using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatA : MonoBehaviour
{

    private Text score;
    public int defaultPoints = 5;
    public static int pointAmount;

    // Start is called before the first frame update
    void Start()
    {
        pointAmount = defaultPoints;
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = pointAmount.ToString();
    }

    public void AddScore()
    {
        if (StatsManager.MaxPoints > 0 && pointAmount < 20)
        {
            pointAmount += 1;
            StatsManager.MaxPoints -= 1;
        }
    }

    public void ReduceScore()
    {
        if (StatsManager.MaxPoints < 25 && pointAmount > 1)
        {
            pointAmount -= 1;
            StatsManager.MaxPoints += 1;
        }
    }
}