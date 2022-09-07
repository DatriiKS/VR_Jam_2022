using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Examples;

public class OpenMenu : MonoBehaviour
{
    public InputActionAsset inputActions;
    public GameObject xrOrigin;
    public GameObject settingMenu;
    InputAction _menu;

    void Start()
    {
        Debug.Log(settingMenu.activeInHierarchy);
        Debug.Log(inputActions == null);
        if (inputActions)
        {
            Debug.Log("input action not null");
            _menu = inputActions.FindActionMap("XRI LeftHand").FindAction("Menu");
            Debug.Log(_menu);
            _menu.Enable();
            _menu.performed += MenuPress;
        }
    }


    public void MenuPress(InputAction.CallbackContext context)
    {
        Debug.Log("Menu Pressed");
        if (settingMenu.activeInHierarchy)
        {
            settingMenu.SetActive(false);
        } else {
            settingMenu.SetActive(true); 
        }
    }

    
    public void ChangeTurnStyle(string style)
    {
        LocomotionSchemeManager manager = xrOrigin.GetComponent<LocomotionSchemeManager>();
        switch (style)
        {
            case "snap":
            manager.SetTurnStyle(LocomotionSchemeManager.TurnStyle.Snap);
            break;
            default:
            manager.SetTurnStyle(LocomotionSchemeManager.TurnStyle.Continuous);
            break;
        }
    }
}
