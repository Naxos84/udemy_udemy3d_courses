using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scoreboard : MonoBehaviour
{

    [SerializeField] int scorePerHit = 12;
    int currentScore = 0;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        this.scoreText = GetComponent<Text>();
        this.scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ScoreHit()
    {
        this.currentScore += this.scorePerHit;
        this.scoreText.text = currentScore.ToString();
    }

}
