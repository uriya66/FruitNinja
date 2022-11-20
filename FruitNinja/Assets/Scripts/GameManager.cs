using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // The Header of the objects or variables below
    [Header("Score Elements")]
    public int score;
    public int highScore;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    [Header("Game Over Elements")]
    public GameObject gameOverPanel;
    [Header("Sounds Elements")]
    public AudioClip[] sounds;
    // AudioSource for Game Manager
    private AudioSource audioSource;
    public AudioClip bombSound;


    private void Start()
    {
        // Using the AudioSource inside the Game Manager
        audioSource = GetComponent<AudioSource>();
        GetHighScore();
    }

    // Get the value by key ("HighScore")
    private void GetHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "Best: " + highScore.ToString();
    }

    // Add score for fruit
    public void IncreaseScore(int addedPoints)
    {
        score += addedPoints;
        scoreText.text = score.ToString();
        
        // Set the highScore
        if (score > highScore)
        {
            // The values we are storing in PlayerPrefs going to be stored permanently on pc
            // And we can load them once we replay the game
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "Best: " + score.ToString();
        }
    }

    // Bomb hit
    public void OnBombHit()
    {
        audioSource.PlayOneShot(bombSound);
        gameOverPanel.SetActive(true);
        // Stop any movement in the game
        Time.timeScale = 0;
    }


    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Play random sounds
    public void PlayRandomSound()
    {
        // Choose a random sound from the array
        AudioClip randomSound = sounds[Random.Range(0, sounds.Length)];
        // play the random sound
        audioSource.PlayOneShot(randomSound);
    }
}
