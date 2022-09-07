using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SyncNavAgentWithAnimator : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator anim;
    public bool doSync = false;
    public float speed = 1f;

    void Start()
    {
        if(agent != null && anim != null)
        {
            doSync = true;
        }
    }

    void Update()
    {
        agent.updatePosition = false;
        agent.updateRotation = true;


    }

    
    void OnAnimatorMove()
    {

        if (doSync)
        {
            agent.velocity = anim.deltaPosition / Time.deltaTime;
            agent.speed = anim.velocity.magnitude;
        }
    //    agent.speed =(anim.deltaPosition / Time.deltaTime).magnitude;
    }
    
}
