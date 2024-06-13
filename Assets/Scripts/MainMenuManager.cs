using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        int level = PlayerPrefs.HasKey("Level") ? PlayerPrefs.GetInt("Level") : 1;
        SceneManager.LoadScene(level);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void SaveLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
    }
}