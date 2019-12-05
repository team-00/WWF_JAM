using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState { STANDARD, OPTIONS, INFO, EXIT }

public class UIManager : MonoBehaviour
{
    private UIState currentUIState;

    [SerializeField]
    private MainUIWindow muiWindow;
    [SerializeField]
    private InfoWindow iWindow;
    [SerializeField]
    private OptionsWindow oWindow;
    [SerializeField]
    private ExitWindow eWindow;

    public void OnEnable()
    {
        currentUIState = UIState.STANDARD;
        muiWindow.Enable();
        iWindow.Disable();
        oWindow.Disable();
        eWindow.Disable();
    }

    public void ChangeMenuState(int newState)
    {

        switch (currentUIState)
        {
            case UIState.STANDARD:
                muiWindow.Disable();
                break;
            case UIState.OPTIONS:
                oWindow.Disable();
                break;
            case UIState.INFO:
                iWindow.Disable();
                break;
            case UIState.EXIT:
                eWindow.Disable();
                break;
        }

        switch ((UIState)newState)
        {
            case UIState.STANDARD:
                muiWindow.Enable();
                break;
            case UIState.OPTIONS:
                oWindow.Enable();
                break;
            case UIState.INFO:
                iWindow.Enable();
                break;
            case UIState.EXIT:
                eWindow.Enable();
                break;
        }

        currentUIState = (UIState)newState;
    }

    public void SpawnTurret(int id)
    {
        //spawn turret based on id (0-3)
        Debug.Log("Spawn Turret:" + id);
    }

    public void OpenGeneralTurretInfo(int id)
    {
        ChangeMenuState(2);
        //Based on id (0-3)
        iWindow.SetUp("Firemage Panda-"+ id, "The Firemage Panda is a super efficient single target turret with medium attackspeed and cool looking attacks.\n\nAlso: Jan sucks");
    }

    public void StartNextRound()
    {
        Debug.Log("Start next Round");
        //Start next round
    }
}
