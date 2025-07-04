using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] S_UI_lives;
    public Image livesImageDisplay;

    public GameObject titleScreamPanel;

    public Text scoreText;
    public int score = 0;

    public void UpdateLives(int currentLives)
    {
        //Debug.Log("Player lives: " + currentLives);
        livesImageDisplay.sprite = S_UI_lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScream()
    {
        titleScreamPanel.SetActive(true);
    }

    public void HideTitleScream()
    {
        titleScreamPanel.SetActive(false);
        ResetScore();
    }

    private void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
