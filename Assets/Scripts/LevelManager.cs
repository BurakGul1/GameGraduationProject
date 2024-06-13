using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    private int currentLevel;
    private GameManager _gameManager;

    private void Awake()
    {
        SingletonThis();
    }

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        // Eğer MainMenu sahnesindeysek, GameManager'ı arama
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            FindGameManager();
        }
    }

    void Update()
    {
        if (_gameManager != null && _gameManager.levelCompleted)
        {
            SaveCurrentLevel();
        }
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

    private void SaveCurrentLevel()
    {
        MainMenuManager mainMenuManager = FindObjectOfType<MainMenuManager>();
        if (mainMenuManager != null)
        {
            mainMenuManager.SaveLevel(currentLevel + 1);
        }
        else
        {
            Debug.LogError("MainMenuManager not found in the scene.");
        }
    }

    public void FindGameManager()
    {
        _gameManager = FindObjectOfType<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }
    }
}