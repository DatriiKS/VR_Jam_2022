using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public enum PlayerState {  Dead, Alive }
    public GameManager gameManager;
    public PlayerState state = PlayerState.Alive;
    public UnityEvent onPlayerKilled = new UnityEvent();
    public GameObject deathEffect;
    public XROrigin rig;
    public Transform playerBehindTransform;
    private void Start()
    {
        if(!gameManager)
            gameManager = GameManager.GetGameManager();
    }

    public Transform GetPlayerTransform()
    {
        return rig.CameraFloorOffsetObject.transform;
    }

    public void Kill(GameObject go)
    {
        if (state != PlayerState.Dead)
        {
            Debug.Log("Player killed by: " + go.name);
            state = PlayerState.Dead;
            onPlayerKilled.Invoke();

        }
    }

    public bool IsAlive()
    {
        return (state == PlayerState.Alive);
    }

    public void SpawnDeathEffect()
    {
        GameObject de = GameObject.Instantiate(deathEffect);
        de.transform.localScale = Vector3.one * 5f;
        de.transform.position = rig.CameraFloorOffsetObject.transform.position;

    }
}
