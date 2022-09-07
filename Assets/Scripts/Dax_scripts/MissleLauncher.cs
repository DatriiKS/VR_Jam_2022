using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MissleLauncher : MonoBehaviour, ICannon
{
    [SerializeField] private List<Transform> pointers;
    [SerializeField] private List<Transform> startingPoints;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject pedestal;
    [SerializeField] private GameObject indicator;
    [SerializeField] private int amountOfPartsToPutAway;

    private bool[] inserts;
    private int currentInsert = 0;
    private void Start()
    {
        var sockets = pedestal.GetComponentsInChildren<XRSocketInteractor>();
        inserts = new bool[sockets.Length - amountOfPartsToPutAway];
        Debug.Log(inserts.Length);
        TurnOnSockets(sockets);
    }
    public void Fire()
    {
        for (int i = 0; i < pointers.Count; i++)
        {
            var rocket = Instantiate(projectilePrefab, startingPoints[i].position, startingPoints[i].rotation * Quaternion.Euler(-90,0,0)).GetComponent<Rocket>();
            rocket.TurnOnRocket();
            rocket.Pointer = pointers[i];
        }
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
        Debug.Log("All insetrts true");
        var indicatorColor = indicator.GetComponent<MeshRenderer>().material;
        indicatorColor.color = Color.green;
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
