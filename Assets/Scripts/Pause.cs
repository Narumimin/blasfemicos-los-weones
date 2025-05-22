using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    private bool isInPenitentWindow = true;
    public GameObject pausePanel;
    public GameObject penitentWindow;
    public GameObject optionsWindow;

    public void TogglePause()
    {
        if(isInPenitentWindow)
        penitentWindow.SetActive(isInPenitentWindow);

        else if (!isInPenitentWindow)
        optionsWindow.SetActive(!isInPenitentWindow);

        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
    }

    public void OptionsWindow()
    {
        isInPenitentWindow = !isInPenitentWindow;
        penitentWindow.SetActive(isInPenitentWindow);
        optionsWindow.SetActive(!isInPenitentWindow);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
