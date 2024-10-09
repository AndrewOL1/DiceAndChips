using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    private int scoreCount;
    private userSettings settings;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        settings=GameObject.Find("Settings").GetComponent<userSettings>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void updateScore(int score)
    {
        scoreCount += score;
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }
    public void Reset()
    {
        scoreText.text = "Score: " + 0;
        scoreCount = 0;
    }
    public void UpdateHighScore()
    {
        settings.newScore(scoreCount);
        settings.toggleHighScore();
        scoreText.text = "";
    } 
}
