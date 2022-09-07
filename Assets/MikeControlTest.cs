using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using static UnityEngine.InputSystem.InputAction;

public class MikeControlTest : MonoBehaviour
{
    public InputActionReference turnAction;
    public Vector2 turnValue;

    void Start()
    {
        turnAction.action.performed += ctx => DoThing(ctx);
    }

    public void DoThing(CallbackContext ctx)
    {
        turnValue = ctx.ReadValue<Vector2>();
      
    }
}
