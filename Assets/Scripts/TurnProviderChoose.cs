using UnityEngine;

public class TurnProviderChoose : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HandCollided");
        var sphere = other.GetComponent<TurnProviderSphere>();
        if (sphere != null)
        {
            Debug.Log("ChangeStyleCallde");
            sphere.ChangeTurnStyle();
        }
    }
}
