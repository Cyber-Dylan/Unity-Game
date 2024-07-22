using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject endScreen;
    public Text currentScoreText;

    public int currentScore;
    public AudioSource backgroundMusic;

    public int amountOfCollectables;




    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        currentScore = 0;
        endScreen.SetActive(false);

        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay();
        SetFinalScore();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    void ScoreDisplay()
    {
        currentScoreText.text = currentScore.ToString() + " / " + amountOfCollectables.ToString();
    }

    void SetFinalScore()
    {

        if(currentScore == amountOfCollectables)
        {
            endScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void RestartApplication()
    {
        SceneManager.LoadScene("_mainGame");
    }
}
