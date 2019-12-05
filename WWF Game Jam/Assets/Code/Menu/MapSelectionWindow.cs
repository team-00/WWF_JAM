using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelectionWindow : MenuWindow
{
    [SerializeField]
    private TickBox[] levelSelections;
    [SerializeField]
    private Button start;

    private int currentSelected = 0;

    public override void Disable()
    {
        gameObject.SetActive(false);
        for (int boxID = 0; boxID < levelSelections.Length; boxID++)
        {
            levelSelections[boxID].Denitialize();
        }
        currentSelected = 0;
    }

    public override void Enable()
    {
        for (int boxID = 0; boxID < levelSelections.Length; boxID++)
        {
            levelSelections[boxID].Initialize(boxID, this, boxID == currentSelected);
        }
        gameObject.SetActive(true);
    }

    public override void TickBox(bool on, int boxID)
    {
        if (on)
        {
            int oldBoxID = currentSelected;
            currentSelected = boxID;
            if(oldBoxID != -1)
                levelSelections[oldBoxID].Tick();
            else
                start.interactable = true;
        }
        else if(boxID == currentSelected)
        {
            currentSelected = -1;
            start.interactable = false;
        }
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(1);

        Debug.Log("Start Game: Level "+ (currentSelected +1));
    }
}
