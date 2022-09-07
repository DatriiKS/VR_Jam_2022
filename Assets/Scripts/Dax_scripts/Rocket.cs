using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float flightSpeed;
    [SerializeField] private GameObject explodeEffect;
    private GameManager gameManager;
    private Rigidbody rigidbody;
   

    private bool isTurnedOn = false;

    public Transform Pointer { get; set; }

    private void Awake()
    {
        gameManager = GameManager.GetGameManager();
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (isTurnedOn)
        {
            rigidbody.velocity = transform.forward * flightSpeed;

            var heading = Pointer.position - transform.position;
            var rotation = Quaternion.LookRotation(heading);

            rigidbody.MoveRotation(rotation);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        List<SpawnableEnemy> enemies = new List<SpawnableEnemy>();
        var objectsHit = Physics.OverlapSphere(transform.position, 10f);
        
        foreach (var hitTarget in objectsHit)
        {
            var enemy = hitTarget.gameObject.GetComponent<SpawnableEnemy>();
            if (enemy != null)
            {
                Debug.Log("Rocket hit enemy: " + enemy.transform.name);
                enemies.Add(enemy);
            }
        }
        foreach (var item in enemies)
        {
            item.Kill();
        }
        gameManager.effectManager.ReparentEffect(explodeEffect.transform);
        gameManager.CheckGameEndConditions();
        Destroy(transform.gameObject);
    }
    public void TurnOnRocket()
    {
        StartCoroutine(RocketFlight());
    }

    IEnumerator RocketFlight()
    {
        rigidbody.AddForce(transform.forward * 50, ForceMode.Impulse);
        yield return new WaitForSeconds(2);
        isTurnedOn = true;
    }
    
}
