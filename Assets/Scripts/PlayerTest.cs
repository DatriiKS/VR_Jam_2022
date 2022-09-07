using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public CharacterController cc;
    public float turnSpeed = 10f;
    public float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate around y - axis
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed, 0);

        // Move forward / backward
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = moveSpeed * Input.GetAxis("Vertical");
        cc.SimpleMove(forward * curSpeed);
    }
}
