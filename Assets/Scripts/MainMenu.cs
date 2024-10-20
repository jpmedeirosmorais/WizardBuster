using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] protected GameObject pauseMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }
}
