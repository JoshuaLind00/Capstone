using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{

    private Text availablePoints;
    public static int MaxPoints = 10;
    public static int StrengthStat;
    public static int MagicStat;
    public static int StaminaStat;

    // Start is called before the first frame update
    void Start()
    {
        availablePoints = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        availablePoints.text = MaxPoints.ToString();
        StrengthStat = StatA.pointAmount;
    }

}
