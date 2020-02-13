using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private GameObject titleScreen;
    
    private float spawnRate = 1.0f;
    private int score;
    private int lives = 3;
    public bool gameActive;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;

    // Start is called before the first frame update
    void Start()
    {
        titleScreen = GameObject.Find("Title Screen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTargets()
    {
        while (gameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToSub)
    {
        lives -= livesToSub;
        livesText.text = "Lives: " + lives;

        if (lives == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        
        gameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        //Called when any of the difficulty buttons are clicked
        StartCoroutine(SpawnTargets());
        UpdateScore(0);
        UpdateLives(0);

        //Changes difficulty as per button pressed
        spawnRate /= difficulty;

        gameActive = true;

        //Removes title screen as game starts
        titleScreen.gameObject.SetActive(false);
    }
}
