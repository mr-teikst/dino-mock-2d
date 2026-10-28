using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreText;

    void Start()
    {
        highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();   
        Debug.Log("Quit");
    }
}
