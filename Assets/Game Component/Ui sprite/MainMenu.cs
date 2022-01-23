using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
    [SerializeField] GameObject mainMenu;

    bool openOption;

    void Awake()
    {

        openOption = false;
    }
    public void startGame()
    {
        Debug.Log("Start Game");
        SceneManager.LoadScene("open_sence");
    }

    public void exitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void optionMenuController()
    {
        if (!openOption)
        {
            openOption = true;
            optionMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
        else if (openOption)
        {
            openOption = false;
            optionMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void toogleFullScreen(bool isToggle)
    {
        if (isToggle)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

    }
    public void toogleVsync(bool isToggle)
    {
        if (isToggle)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }
}
