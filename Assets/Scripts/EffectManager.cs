using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject bloodVfx;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameManager)
            gameManager = GameManager.GetGameManager();
        gameManager.effectManager = this;

    }

    // Spawn an object at the object's position.
    // Destroys after 2 seconds.
    public void ReparentEffect(Transform t) 
    {
        t.parent = transform;
        t.gameObject.SetActive(true);
        foreach(var p in t.gameObject.GetComponentsInChildren<ParticleSystem>()) {
            p.Play();
        }
        Destroy(t.gameObject, 2);
    }

    public void SpawnBlood(Transform t)
    {
        GameObject go = Instantiate(bloodVfx, t.transform.position, Quaternion.identity);
        Destroy(go, 10);
    }
}
