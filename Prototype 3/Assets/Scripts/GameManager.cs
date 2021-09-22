using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text startText;
    public Text scoreText;
    public Image gameOverTextBg;
    public Button restartButton;

    // stop time
    private void Awake()
    {
        Time.timeScale = 0;
    }

    // start time when space pressed 
    public void StartGame()
    {
        Time.timeScale = 1;

        startText.gameObject.SetActive(false);
        gameOverTextBg.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    // game over
    public void EndGame()
    {
        gameOverTextBg.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
