using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    Text accuracyStat;

    [SerializeField]
    Text scoresText;

    [SerializeField]
    Image fillBar;

    [SerializeField]
    Text timeText;

    public static int points = 0;

    public static int scores = 0;
    public static int hits = 0;
    public static int shots = 0;
    static int timeRemains = 30;

    void Awake()
    {
        if (gameManager == null)
            gameManager = this;
    }

    void Start()
    {
        scores = 0;
        hits = 0;
        shots = 0;
        timeRemains = 30;
        points = 0;
        StartCoroutine(ShowTime());
    }

    // Start is called before the first frame update
    static public void Lost()
    {
        scores--;
        points = 0;
        gameManager.RefreshStat();
    }

    static public void Shot(bool good)
    {
        if (good)
        {
            hits++;
            points++;
        }
        shots++;
        if (points == 5) 
        { 
            points = 0; 
            timeRemains += 3;
        }
        gameManager.RefreshStat();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void RefreshStat()
    {
        scoresText.text = "Очки: " + scores.ToString();
        accuracyStat.text = "Точность: " + (100f * hits/Mathf.Max(1f,shots)).ToString("#0.0");
        fillBar.fillAmount = 0.2f*points;
        if (scores < 0)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }

    }

    IEnumerator ShowTime()
    {
        yield return new WaitForSeconds(1f);
        timeRemains--;
        if (timeRemains == 0)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
        timeText.text = timeRemains.ToString();
        StartCoroutine(ShowTime());
    }

}
