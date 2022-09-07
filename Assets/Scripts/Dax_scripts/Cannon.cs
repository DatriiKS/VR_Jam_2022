using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Cannon : MonoBehaviour,ICannon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject pedestal;
    [SerializeField] private GameObject indicator;
    [SerializeField] private ControllerMain controller1;
    [SerializeField] private ControllerMain controller2;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform startingPoint;
    [SerializeField] private int amountOfPartsToPutAway;
    [SerializeField] private bool doDebug = false;
    public UnityEvent onFireCannon = new UnityEvent(); 

    private bool[] inserts;
    private int currentInsert = 0;

    private bool notFiredOnce = true;

    public delegate void OncannonPoweredUp();
    public event OncannonPoweredUp onPoweredUp;

    private void OnEnable()
    {
        controller1.CannonFire += Fire;
        controller2.CannonFire += Fire;
    }
    private void OnDisable()
    {
        controller1.CannonFire -= Fire;
        controller2.CannonFire -= Fire;
    }
    private protected virtual void Start()
    {
        var sockets = pedestal.GetComponentsInChildren<XRSocketInteractor>();
        inserts = new bool[sockets.Length - amountOfPartsToPutAway];
        if(doDebug) Debug.Log(inserts.Length);
        TurnOnSockets(sockets);
    }
    public void Fire()
    {
        if (notFiredOnce)
        {
            var projectile = Instantiate(projectilePrefab, startingPoint.position, startingPoint.rotation);
            Rigidbody projectileBody = projectile.GetComponent<Rigidbody>();
            projectileBody.AddForce(projectile.transform.forward * 50, ForceMode.Impulse);
            onFireCannon.Invoke();
            notFiredOnce = false;
        }
    }
    public void PowerUpCannon()
    {
        if (doDebug) Debug.Log("All insetrts true");
        var indicatorColor = indicator.GetComponentInChildren<MeshRenderer>().material;
        indicatorColor.color = Color.green;
        line.gameObject.SetActive(true);
        onPoweredUp.Invoke();
    }
    public void TurnOnSockets(XRSocketInteractor[]sockets)
    {
        for (int i = 0; i < amountOfPartsToPutAway; i++)
        {
            sockets[i].gameObject.SetActive(false);
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
}
