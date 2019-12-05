using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoWindow : WindowBase
{
    [SerializeField]
    private TextMeshProUGUI nameText, infoText;

    public void SetUp(string name, string info)
    {
        nameText.text = name;
        infoText.text = info;
    }

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
