using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Examples;

public class ControllerMain : MonoBehaviour
{
    [SerializeField] private Cannon cannon;
    [SerializeField] private protected Transform sphere;
    [SerializeField] private protected InputActionReference joystickMove;
    [SerializeField] private InputActionReference joystickPress;
    [SerializeField] private bool doDebug;
    [SerializeField] private AudioSource au;
    [SerializeField] private LocomotionSchemeManager schemeManager;

    private SnapTurnProviderBase schemeSnapTurnProvider;
    private ContinuousTurnProviderBase schemeContiniousTurnProvider;
    private protected ContinuousMoveProviderBase schemeMoveProvider;

    private protected bool isActive = false;
    private protected bool isInHands = false;

    private float baseMoveSpeed;
    private float baseTurnSpeed;
    private float baseTurnAmount;
    private float yRotation;

    public delegate void OnFire();
    public event OnFire CannonFire;
    void OnEnable()
    {
        schemeSnapTurnProvider = schemeManager.snapTurnProvider;
        schemeContiniousTurnProvider = schemeManager.continuousTurnProvider;
        schemeMoveProvider = schemeManager.continuousMoveProvider;

        baseMoveSpeed = schemeMoveProvider.moveSpeed;
        baseTurnAmount = schemeSnapTurnProvider.turnAmount;
        baseTurnSpeed = schemeContiniousTurnProvider.turnSpeed;

        joystickPress.action.performed += TogglePlayerMovement;
        cannon.onPoweredUp += PowerUp;
    }
    private void OnDisable()
    {
        joystickPress.action.performed -= TogglePlayerMovement;
        cannon.onPoweredUp -= PowerUp;
    }

    private  void Update()
    {
        TurnBigSphere();
        if(doDebug) Debug.Log(joystickMove.action.ReadValue<Vector2>());
    }
    public virtual void TurnBigSphere()
    {
        if (doDebug) Debug.Log(isActive);
        if (doDebug) Debug.Log(isInHands);
        yRotation = sphere.rotation.eulerAngles.y;
        if (isActive && isInHands && schemeMoveProvider.moveSpeed == 0)
        {
            var a = joystickMove.action.ReadValue<Vector2>();
            yRotation += (a.x * 30) * Time.deltaTime;
            au.volume = 1f;
        } else
        {
            au.volume = 0;
        }
        sphere.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void PowerUp()
    {
        isActive = true;
        if (doDebug) Debug.Log("Controller Activated");
    }

    //If you`re making a modified rig, then move this method in it
    public void TogglePlayerMovement(InputAction.CallbackContext context)
    {
        switch (schemeManager.turnStyle)
        {
            case LocomotionSchemeManager.TurnStyle.Snap:
                if (schemeMoveProvider.moveSpeed == baseMoveSpeed)
                {
                    schemeSnapTurnProvider.turnAmount = 0;
                    schemeMoveProvider.moveSpeed = 0;
                }
                else
                {
                    schemeSnapTurnProvider.turnAmount = baseTurnAmount;
                    schemeMoveProvider.moveSpeed = baseMoveSpeed;
                }
                break;
            case LocomotionSchemeManager.TurnStyle.Continuous:
                if (schemeMoveProvider.moveSpeed == baseMoveSpeed)
                {
                    schemeContiniousTurnProvider.turnSpeed = 0;
                    schemeMoveProvider.moveSpeed = 0;
                }
                else
                {
                    schemeContiniousTurnProvider.turnSpeed = baseTurnSpeed;
                    schemeMoveProvider.moveSpeed = baseMoveSpeed;
                }
                break;
            default:
                break;
        }
    }

    public void FireCannon()
    {
        if (isActive)
        {
            CannonFire.Invoke();
        }
    }
    public void InHandsToggle()
    {
        isInHands = !isInHands;
    }
}
