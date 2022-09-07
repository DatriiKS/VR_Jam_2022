using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTweaks : MonoBehaviour
{
    //public bool detectCollisions = true;

    // Start is called before the first frame update
    void Start()
    {
       // GetComponent<CharacterController>().detectCollisions = detectCollisions;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Interactable collided with: " + collision.gameObject.name);
    }

}
