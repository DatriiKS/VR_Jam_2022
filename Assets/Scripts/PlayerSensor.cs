using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSensor : MonoBehaviour
{
    public UnityEvent<GameObject> onSenseEnemy = new UnityEvent<GameObject>();
    public GameManager gameManager;

    public void Start()
    {
        if (!gameManager)
            gameManager = GameManager.GetGameManager();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player detected trigger with " + other.gameObject.name);
       
        if (other.TryGetComponent<EnemySensor>(out var enemySensor))
        {
            SenseEnemy(enemySensor.transform.parent.gameObject);
        }

    }



    void SenseEnemy(GameObject enemy)
    {
        if (enemy.TryGetComponent<EnemyWeapon>(out var weapon))
        {
            Debug.Log("Weapon detected.  Killing player.");
            gameManager.player.Kill(weapon.gameObject);
        }
        else if (enemy.TryGetComponent<SpawnableEnemy>(out var se))
        {
            Debug.Log("Player seen by: " + se.gameObject);
            se.SetSeenPlayer(transform.parent.gameObject);
        }
        onSenseEnemy.Invoke(gameObject);

    }
}
