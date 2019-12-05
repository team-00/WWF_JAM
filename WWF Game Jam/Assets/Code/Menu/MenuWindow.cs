﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuWindow : MonoBehaviour
{
    public abstract void Disable();
    public abstract void Enable();

    public abstract void TickBox(bool on, int boxID);
}
