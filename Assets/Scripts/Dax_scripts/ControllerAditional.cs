using UnityEngine;

public class ControllerAditional : ControllerMain
{
    [SerializeField] private float angleLimitUp;
    [SerializeField] private float angleLimitDown;
    [SerializeField] private Transform parent;
    private float xRotation;
    private float parentRotation;
    public override void TurnBigSphere()
    {
        parentRotation = parent.transform.eulerAngles.y;
        xRotation = sphere.rotation.eulerAngles.x;
        if (isActive && isInHands && xRotation < angleLimitUp || xRotation > angleLimitDown && schemeMoveProvider.moveSpeed == 0)
        {
            //Debug.Log(sphere.eulerAngles.x);
            var a = joystickMove.action.ReadValue<Vector2>();
            xRotation += (a.y * 30) * Time.deltaTime;
        }
        else if (xRotation > angleLimitUp && xRotation < 180f)
        {
            xRotation = angleLimitUp - 0.1f;
        }
        else if (xRotation < angleLimitDown && xRotation > 180f)
        {
            xRotation = angleLimitDown + 0.1f;
        }

        sphere.transform.rotation = Quaternion.Euler(xRotation, parentRotation, 0f);
    }
}
