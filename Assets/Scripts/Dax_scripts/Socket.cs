using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Socket : XRSocketInteractor
{
    public delegate void TurnedOn(bool IsTurned);

    public event TurnedOn OnTurnedOn; 

}
