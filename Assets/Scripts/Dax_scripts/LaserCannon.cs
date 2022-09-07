using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserCannon : MonoBehaviour, ICannon
{
    [SerializeField] private GameObject pedestal;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Transform startingPoint;
    [SerializeField] private float baseSecondsCount;
    [SerializeField] private int amountOfPartsToPutAway;
    [SerializeField] private bool doDebug = false;
    [SerializeField] private TextMeshProUGUI text;

    private GameManager gameManager;

    private bool[] inserts;
    private int currentInsert = 0;

    private void Start()
    {
        text.text = baseSecondsCount.ToString();
        gameManager = GameManager.GetGameManager();
        var sockets = pedestal.GetComponentsInChildren<XRSocketInteractor>();
        inserts = new bool[sockets.Length - amountOfPartsToPutAway];
        if (doDebug) Debug.Log(inserts.Length);
        TurnOnSockets(sockets);
    }
    public void Fire()
    {
        List<SpawnableEnemy> enemies = new List<SpawnableEnemy>();
        RaycastHit[] sphereHits = Physics.SphereCastAll(startingPoint.position, 10f, startingPoint.right,100f);
        foreach (var hitCollider in sphereHits)
        {
            var enemy = hitCollider.collider.GetComponent<SpawnableEnemy>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
        foreach (var enemy in enemies)
        {
            enemy.Kill();
        }
        gameManager.CheckGameEndConditions();
    }

    public void OnSocketTriggered(bool IsTriggered)
    {
        if (IsTriggered)
        {
            inserts[currentInsert] = true;
            currentInsert++;
        }
        if (inserts.All(x => x == true))
        {
            PowerUpCannon();
        }
    }

    public void PowerUpCannon()
    {
        if (doDebug) Debug.Log("All insetrts true");
        var indicatorColor = indicator.GetComponentInChildren<MeshRenderer>().material;
        indicatorColor.color = Color.green;
        StartCoroutine(TextTimerStart());
    }

    IEnumerator TextTimerStart()
    {
        while (baseSecondsCount >= 0)
        {
            text.text = baseSecondsCount.ToString();
            yield return new WaitForSeconds(1);
            baseSecondsCount--;
        }
        Fire();
    }

    public void TurnOnSockets(XRSocketInteractor[] sockets)
    {
        for (int i = 0; i < amountOfPartsToPutAway; i++)
        {
            sockets[i].gameObject.SetActive(false);
        }
    }
}
