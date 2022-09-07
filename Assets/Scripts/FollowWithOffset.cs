using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWithOffset : MonoBehaviour
{
    public Transform followTransform;
    public Vector3 transformOffset = new Vector3(0, 10, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (followTransform == null)
            followTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = followTransform.position + transformOffset;
    }
}
