using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsWindow : WindowBase
{
    public override void Disable()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    public override void Enable()
    {
        Time.timeScale = 0.0f;
        gameObject.SetActive(true);
    }

    public override void TickBox(bool on, int boxID)
    {
        throw new System.NotImplementedException();
    }
}
