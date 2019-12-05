using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UIState { STANDARD, OPTIONS, INFO, EXIT }

public class UIManager : MonoBehaviour
{
    private UIState currentUIState;

    [SerializeField]
    private TurretManager turretManager;
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
        turretManager.RequestTurretPlacement((TurretType)id);
    }

    public void OpenGeneralTurretInfo(int id)
    {
        ChangeMenuState(2);
        var wndInfo = turretManager.GetTurretInfo((TurretType)id);
        // TODO make <name> + <info> on turretStats
        iWindow.SetUp("Firemage Panda-"+ id, "The Firemage Panda is a super efficient single target turret with medium attackspeed and cool looking attacks.\n\nAlso: Jan sucks");
        //iWindow.SetUp(wndInfo[0], wndInfo[1]);
    }

    public void StartNextRound()
    {
        Debug.Log("Start next Round");
        // TODO wave manager next round
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
