using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWindow : MenuWindow
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
