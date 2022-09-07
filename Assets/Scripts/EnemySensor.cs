using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class EnemySensor : MonoBehaviour
{
    public UnityEvent<GameObject> onControllerTrigger = new UnityEvent<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered detected ");
        if(other.TryGetComponent<XRController>(out XRController controllerr))
        {
            onControllerTrigger.Invoke(controllerr.gameObject);
        }
    }
}
