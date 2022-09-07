using UnityEngine;
public class Belt : MonoBehaviour
{
    [SerializeField] private Transform positionSource;
    [SerializeField] private float offset;

    void Update()
    {
        RotateWithHead();
        PositionUnderHead();
    } 

    private void PositionUnderHead()
    {
        Vector3 adjustedHeight = positionSource.localPosition;
        adjustedHeight.y = Mathf.Lerp(0.0f, adjustedHeight.y, offset);

        transform.localPosition = adjustedHeight;
    }

    private void RotateWithHead()
    {
        Vector3 adjustedRotation = positionSource.localEulerAngles;
        adjustedRotation.x = 0;
        adjustedRotation.z = 0;
        transform.localEulerAngles = adjustedRotation;
    }
}
