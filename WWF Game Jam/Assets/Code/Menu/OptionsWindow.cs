using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsWindow : MenuWindow
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
}
