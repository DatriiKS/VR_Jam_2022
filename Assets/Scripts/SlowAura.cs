using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class SlowAura : MonoBehaviour
{
    GameManager gameManager;
    public Player player;
    public bool slowed = false;
    public SlowEffect mySlowEffect;
    public GameObject visualEffect;
    public UnityEvent onSlow = new UnityEvent();
    public float speedMMod = -1f;
    public float minDistance = 10f;

    private void Start()
    {
        if (!gameManager)
            gameManager = GameManager.GetGameManager();
    }

    private void Update()
    {
        CheckPlayerDistance();   
    }


    private void OnDestroy()
    {
        if(mySlowEffect)
        {
            Destroy(mySlowEffect);
        }
    }

    void CheckPlayerDistance()
    {
        if(!slowed)
        {
            if(Mathf.Abs(Vector3.Distance(transform.position, gameManager.player.transform.position)) < minDistance)
            {
                AddEffect();
            } 
        } else if(slowed)
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, gameManager.player.transform.position)) > minDistance)
            {
                RemoveEFfect();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Slow aura triggered by: " + other.name);
        Player player = other.gameObject.GetComponentInParent<Player>();
        {

            if (player != null)
            {
                this.player = player;
                Debug.Log("Adding slow effect to player.");
                AddEffect();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {

        Player player = other.gameObject.GetComponentInParent<Player>();
        {
            if (player != null)
            {
                this.player = player;
                Debug.Log("Removing slow effect from player.");
                RemoveEFfect();
            }
        }
    }

    public void AddEffect()
    {
        Debug.Log("AddingEffect (Slow)");
        slowed = true;
        mySlowEffect = gameManager.player.gameObject.AddComponent<SlowEffect>();
        mySlowEffect.speedMod = speedMMod;
        if(visualEffect)
            visualEffect.SetActive(true);
        onSlow.Invoke();
    }
    public void RemoveEFfect()
    {
        Debug.Log("Removing Effect (Slow)");

        slowed = false;

        if (visualEffect)
            StartCoroutine(ResetVfx());
        if (mySlowEffect != null)
        {
            Destroy(mySlowEffect);
        }
    }

    IEnumerator ResetVfx()
    {
        yield return new WaitForSeconds(0.3f);
        visualEffect.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }

}
