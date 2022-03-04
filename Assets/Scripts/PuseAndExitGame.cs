using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuseAndExitGame: MonoBehaviour
{
    public GameObject PusePanel;

    public void GamePuse()
    {
        PusePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        PusePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
