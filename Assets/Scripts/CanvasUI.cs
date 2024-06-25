using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasUI : MonoBehaviour
{
    private static CanvasUI instance;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private MainMenuManager mainMenuManager;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] public GameObject winPanel;
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public TextMeshProUGUI _LevelCount;
    private void Awake()
    {
        SingletonThis();
    }

    void Start()
    {
        mainMenuPanel.SetActive(true);
        loadingPanel.SetActive(false);
        gamePanel.SetActive(false);
    }
    void Update()
    {
        
    }

    void SingletonThis()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClickPlayButton()
    {
        StartCoroutine(LoadingPanel());
        Time.timeScale = 1f;
    }

    IEnumerator LoadingPanel()
    {
        mainMenuPanel.SetActive(false);
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        mainMenuManager.StartGame();
        yield return new WaitForSeconds(.1f);
        gamePanel.SetActive(true);
        loadingPanel.SetActive(false);
    }
    public void ClickPauseButton()
    {
        pausePanel.SetActive(true);
        gamePanel.SetActive(false);
    }
    public void ClickResumeButton()
    {
        pausePanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1f;
    }
    public void ClickBackButton()
    {
        pausePanel.SetActive(false);
        gamePanel.SetActive(false);
        StartCoroutine(BackToMainMenu());
    }
    IEnumerator BackToMainMenu()
    {
        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        mainMenuManager.ReturnToMainMenu(); // Ana menüye dönmek için bir yöntem
        mainMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        loadingPanel.SetActive(false);
    }

    public void ClickSettingsButton()
    {
        gamePanel.SetActive(false);
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void ClickExitButton()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        winPanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1f;
    }
}
