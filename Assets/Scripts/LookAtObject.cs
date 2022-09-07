using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{

    public Animator avatar;
    public Transform lookAtObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (avatar)
        {
            if (lookAtObj != null)
            {
                avatar.SetLookAtPosition(lookAtObj.position);
            }
        }

    }
}
