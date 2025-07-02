using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public GameObject titleScream;

    public Text scoreText;
    public int score;

    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;

        scoreText.text = "Score: " + score;
    }

    public void ShowTitleScream()
    {
        titleScream.SetActive(true);
    }

    public void HideTitleScream()
    {
        titleScream.SetActive(false);
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
