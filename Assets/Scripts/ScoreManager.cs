using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;

    private int score = 0;
    private int highscore = 0;

    private int coinEaten = 0; 

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = score.ToString() + "POINTS";
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }
    

    public void AddPoint()
    {
        score += 10;
        coinEaten++;
        scoreText.text = score.ToString() + " POINTS";
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }
    
    public void AddBoosterPoint()
    {
        score += 100;
        scoreText.text = score.ToString() + " POINTS";
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }

    public void AddCherryPoint()
    {
        score += 100;
        scoreText.text = score.ToString() + " POINTS";
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }
    public int getScore()
	{
		return score;
	}
    public void AddGhostPoint()
    {
        score += 200;
        scoreText.text = score.ToString() + " POINTS";
        if (highscore < score)
            PlayerPrefs.SetInt("highscore", score);
    }

    public int GetCoinsEaten() {
        return coinEaten;
    }


}
