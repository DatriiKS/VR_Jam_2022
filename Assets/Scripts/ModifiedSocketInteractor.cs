using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public class ModifiedSocketInteractor : XRSocketInteractor
{
    public void ToggleInteractableCollider()
    {
        var currentInteractable = interactablesSelected.First();

        if (currentInteractable.colliders != null)
        {
            foreach (var collider in currentInteractable.colliders)
            {
                collider.enabled = false;
            }
        }
    }
}
