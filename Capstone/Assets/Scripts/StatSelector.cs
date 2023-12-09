using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatSelector : MonoBehaviour
{
    [SerializeField] private AudioSource audioM;
    [SerializeField] private AudioClip buttonPress;
    [SerializeField] private AudioClip statPress;

    private Text score;
    //public int defaultPoints = 5;
    public static int pointAmountA;
    public static int pointAmountB;
    public static int pointAmountC;

    private Text availablePoints;
    public static int MaxPoints = 20;
    public int Strength;
    public int Magic;
    public int Stamina;


    //Determin what stat the box uses
    public bool statA;
    public bool statB;
    public bool statC;
    public bool points;
    public bool gameScore;


    // Start is called before the first frame update
    void Start()
    {
        //Sets the default values for the stats
        /*pointAmountA = defaultPoints;
        pointAmountB = defaultPoints;
        pointAmountC = defaultPoints;*/
        score = GetComponent<Text>();
        availablePoints = GetComponent<Text>();
        audioM = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the text depending on the box selected
        if (statA)
        {
            score.text = pointAmountA.ToString();
        }
        else if (statB)
        {
            score.text = pointAmountB.ToString();
        }
        else if (statC)
        {
            score.text = pointAmountC.ToString();
        }
        else if (points)
        {
            availablePoints.text = MaxPoints.ToString();
        }
        else
        {
            Strength = pointAmountA;
            Magic = pointAmountB;
            Stamina = pointAmountC;
        }

        //Updates score to equal the points in stat A, B, or C
        Strength = pointAmountA;
        Magic = pointAmountB;
        Stamina = pointAmountC;
    }

    //Alter A Stat
    public void AddScoreA()
    {
        if (MaxPoints > 0 && pointAmountA < 15)
        {
            audioM.clip = statPress;
            audioM.Play();
            pointAmountA += 1;
            MaxPoints -= 1;
        }
    }

    public void ReduceScoreA()
    {
        if (MaxPoints < 25 && pointAmountA >= 1)
        {
            audioM.clip = statPress;
            audioM.Play();
            pointAmountA -= 1;
            MaxPoints += 1;
        }
    }

    //Alter B Stat
    public void AddScoreB()
    {
        if (MaxPoints > 0 && pointAmountB < 15)
        {
            audioM.clip = statPress;
            audioM.Play();
            pointAmountB += 1;
            MaxPoints -= 1;
        }
    }

    public void ReduceScoreB()
    {
        if (MaxPoints < 25 && pointAmountB >= 1)
        {
            audioM.clip = statPress;
            audioM.Play();
            pointAmountB -= 1;
            MaxPoints += 1;
        }
    }

    //Alter C Stat
    public void AddScoreC()
    {
        if (MaxPoints > 0 && pointAmountC < 15)
        {
            audioM.clip = statPress;
            audioM.Play();
            pointAmountC += 1;
            MaxPoints -= 1;
        }
    }

    public void ReduceScoreC()
    {
        if (MaxPoints < 25 && pointAmountC >= 1)
        {
            audioM.clip = statPress;
            audioM.Play();
            pointAmountC -= 1;
            MaxPoints += 1;
        }
    }



    //Menu Controlls

    public void StartGame()
    {
        audioM.clip = buttonPress;
        audioM.Play();
        StatManager.StrengthStat = Strength;
        StatManager.MagicStat = Magic;
        StatManager.StaminaStat = Stamina;
    }

    public void TestGame()
    {
        audioM.clip = buttonPress;
        audioM.Play();
        ScoreScript.scoreValue = 0;
        StatManager.StrengthStat = Strength;
        StatManager.MagicStat = Magic;
        StatManager.StaminaStat = Stamina;
        SceneManager.LoadScene("TestingScene");
    }

    public void LoadStatSelection()
    {
        audioM.clip = buttonPress;
        audioM.Play();
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene("StatScene");
    }

    public void LoadHowToPlay()
    {
        audioM.clip = buttonPress;
        audioM.Play();
        ScoreScript.scoreValue = 0;
       SceneManager.LoadScene("InstructScene");
    }

    public void LoadMainMenu()
    {
        audioM.clip = buttonPress;
        audioM.Play();
        ScoreScript.scoreValue = 0;
        SceneManager.LoadScene("OpeningScene");
    }

    public void Quit()
    {
        Application.Quit();
       // UnityEditor.EditorApplication.isPlaying = false;
    }


}