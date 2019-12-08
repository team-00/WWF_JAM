using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitWindow : WindowBase
{
    public override void Disable()
    {
        gameObject.SetActive(false);
    }

    public override void Enable()
    {
        gameObject.SetActive(true);
    }

    public override void TickBox(bool on, int boxID)
    {
        throw new System.NotImplementedException();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
