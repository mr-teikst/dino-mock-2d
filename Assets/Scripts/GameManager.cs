using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public InputActionReference pauseAction;   
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text finalScoreText;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public float pointsPerSecond = 10f;         

    const string HighScoreKey = "HighScore";

    float score;
    int highScore;
    bool isPaused;
    bool isOver;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;                  
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    void OnEnable()  { pauseAction.action.Enable(); }
    void OnDisable() { pauseAction.action.Disable(); }

    void Update()
    {
        if (pauseAction.action.WasPressedThisFrame() && !isOver)
        {
            if (isPaused) Resume();
            else Pause();
        }

        if (!isPaused && !isOver)
            score += pointsPerSecond * Time.deltaTime;

        scoreText.text = "Score: " + (int)score;
        highScoreText.text = "Best: " + highScore;
    }

    public void AddCoin(int value)
    {
        if (!isOver) score += value;
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void GameOver()
    {
        isOver = true;
        Time.timeScale = 0f;

        int final = (int)score;
        if (final > highScore)
        {
            highScore = final;
            PlayerPrefs.SetInt(HighScoreKey, highScore);
            PlayerPrefs.Save();
        }

        finalScoreText.text = "Score: " + final + "\nBest: " + highScore;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
