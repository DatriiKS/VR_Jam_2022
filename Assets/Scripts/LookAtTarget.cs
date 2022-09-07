using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    Transform playerTransform;
    bool canSeePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponentInParent<SpawnableEnemy>().playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if(canSeePlayer)
        {
            transform.LookAt(playerTransform);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy collider triggered.");
        if (other.TryGetComponent<Camera>(out Camera cam))
        {
            canSeePlayer = true;
        }
    }
}
