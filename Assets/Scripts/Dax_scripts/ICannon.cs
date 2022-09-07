using UnityEngine.XR.Interaction.Toolkit;

public interface ICannon
{
    public void Fire();
    void PowerUpCannon();
    void TurnOnSockets(XRSocketInteractor[] sockets);
    void OnSocketTriggered(bool IsTriggered);
}
