using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : WindowBase
{
    [SerializeField]
    private Button start, options, exit;

    public override void Disable()
    {
        start.interactable = false;
        options.interactable = false;
        exit.interactable = false;
    }

    public override void Enable()
    {
        start.interactable = true;
        options.interactable = true;
        exit.interactable = true;
    }

    public override void TickBox(bool on, int boxID)
    {
        throw new System.NotImplementedException();
    }
}
