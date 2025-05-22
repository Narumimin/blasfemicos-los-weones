using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsPanel;
    private bool controlsPanelUp = false;

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlsButton()
    {
        controlsPanelUp = !controlsPanelUp;
        controlsPanel.SetActive(controlsPanelUp);
        mainMenu.SetActive(!controlsPanelUp);
    }

    public void ExitButton()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
