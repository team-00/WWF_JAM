using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickBox : MonoBehaviour
{
    public Image TickIcon;

    private int boxId = 0;
    private WindowBase parent;

    public void Initialize(int boxId, WindowBase parent, bool startTicked)
    {
        this.boxId = boxId;
        this.parent = parent;
        Tick(startTicked);
    }

    public void Denitialize()
    {
        boxId = 0;
        parent = null;
        Tick(false);
    }

    private void Tick(bool on)
    {
        TickIcon.enabled = on;
    }

    public void Tick()
    {
        if (parent == null)
            return;
        Tick(!TickIcon.enabled);
        parent.TickBox(TickIcon.enabled, boxId);
    }
}
