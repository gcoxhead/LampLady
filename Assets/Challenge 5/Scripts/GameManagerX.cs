using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    // Game Objects
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI highscoreText;

    public TextMeshProUGUI blueberryCount;
    public TextMeshProUGUI strawberryCount;
    public TextMeshProUGUI daisyCount;
    //public TextMeshProUGUI skullCount;
    //public TextMeshProUGUI toadStoolCount;

    public GameObject titleScreen;
    public GameObject titleBackground;
    public GameObject florenceImage;
    public GameObject itemCount;
    public Button restartButton; 
    public Button quitButton; 
    public List<GameObject> targetPrefabs;

    // Game Variables
    private int score;
    private float time; 
    private int health;
    private int blueberryScore = 0;
    private int strawberryScore = 0;
    private int daisyScore = 0;
    private int highScore = 0;
    //private int skullScore;
    //private int toadStoolScore;
    public bool isGameActive;

    //Static Game Settings
    private float spawnRate = 1.5f;
    private float spaceBetweenSquares = 2.5f; 
    private float minValueX = -3.75f; //  x value of the center of the left-most square
    private float minValueY = -3.75f; //  y value of the center of the bottom-most square
    
    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        highscoreText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        StartCoroutine(SpawnTarget());
        score = 0;
        time = 0;
        health = 100;
        ClearPreviousScore();

        UpdateScore(0);
        titleScreen.SetActive(false);
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        healthText.gameObject.SetActive(true);
        titleBackground.gameObject.SetActive(false);
        itemCount.gameObject.SetActive(true);
        florenceImage.gameObject.SetActive(true);
        
    }
    public void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (isGameActive)
        {
            time += Time.deltaTime;
            timerText.SetText("Time: " + Mathf.Round(time));

            if (time <0)
            {
                GameOver();
            }
        }
    }

    // While game is active spawn a random target
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);

            if (isGameActive)
            {
                Instantiate(targetPrefabs[index], RandomSpawnPosition(), targetPrefabs[index].transform.rotation);
            }
            
        }
    }

    // Generate a random spawn position based on a random index from 0 to 3
    Vector3 RandomSpawnPosition()
    {
        float spawnPosX = minValueX + (RandomSquareIndex() * spaceBetweenSquares);
        float spawnPosY = minValueY + (RandomSquareIndex() * spaceBetweenSquares);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;

    }

    // Generates random square index from 0 to 3, which determines which square the target will appear in
    int RandomSquareIndex()
    {
        return Random.Range(0, 4);
    }

    // Update score with value from target clicked
    public void UpdateScore(int scoreToAdd)
    {

        score += scoreToAdd;
        scoreText.text = "Score:"+ score;

        if (score>highScore)
        {
            highScore = score;
            highscoreText.text = "Highscore" + highScore;
        }
        
        if (scoreToAdd == 15 )
        {
            blueberryScore += 1 ;
            blueberryCount.text = "" + blueberryScore ;

        }
        
         if (scoreToAdd == 10 )
        {
            strawberryScore += 1 ;
            strawberryCount.text = "" + strawberryScore ;

        }

         if (scoreToAdd == 5 )
        {
            daisyScore += 1 ;
            daisyCount.text = "" + daisyScore ;

        }
        

        highscoreText.text = "Highscore: " + highScore;

        health += scoreToAdd;

        if (health >= 100)
        {
            health = 100;
            healthText.text = "Health:"+ health + "%";
        }
        else if (health <= 0)
        {
            health = 0;
            healthText.text = "Health:"+ health + "%";
            GameOver();
        }
        else
        healthText.text = "Health:"+ health;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        //gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        highscoreText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        isGameActive = false;
        
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayAgain()
    {
        StartGame(1);
    }

    public void ClearPreviousScore()
    {
    blueberryScore = 0;
    blueberryCount.text = "" + blueberryScore ;
    strawberryScore = 0;
    strawberryCount.text = "" + strawberryScore ;
    daisyScore = 0;
    daisyCount.text = "" + daisyScore ;

    }

}
