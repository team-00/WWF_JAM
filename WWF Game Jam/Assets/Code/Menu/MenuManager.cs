using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuState { START, MAPSELECTION, OPTIONS, EXIT}

public class MenuManager : MonoBehaviour
{

    private static MenuManager instance;
    public static MenuManager Instance { get => instance; }

    private MenuState currentMenuState;

    [SerializeField]
    private MapSelectionWindow msWindow;
    [SerializeField]
    private MainMenuWindow mmWindow;
    [SerializeField]
    private OptionsWindow oWindow;
    [SerializeField]
    private ExitWindow eWindow;

    public void Awake()
    {
        instance = this;
    }

    public void OnEnable()
    {
        currentMenuState = MenuState.START;
        mmWindow.Enable();
        msWindow.Disable();
        oWindow.Disable();
        eWindow.Disable();
    }

    public void ChangeMenuState(int newState)
    {
        switch (currentMenuState)
        {
            case MenuState.START:
                mmWindow.Disable();
                break;
            case MenuState.MAPSELECTION:
                msWindow.Disable();
                break;
            case MenuState.OPTIONS:
                oWindow.Disable();
                break;
            case MenuState.EXIT:
                eWindow.Disable();
                break;
            default:
                break;
        }

        switch ((MenuState)newState)
        {
            case MenuState.START:
                mmWindow.Enable();
                break;
            case MenuState.MAPSELECTION:
                msWindow.Enable();
                break;
            case MenuState.OPTIONS:
                oWindow.Enable();
                break;
            case MenuState.EXIT:
                eWindow.Enable();
                break;
            default:
                break;
        }

        currentMenuState = (MenuState)newState;
        AudioManager.PlayButtonSound();
    }
}
