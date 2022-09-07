using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraSensor : MonoBehaviour
{
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameManager)
            gameManager = GameManager.GetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
