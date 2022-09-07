using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlowEffect : MonoBehaviour
{
    public GameManager gameManager;
    public float speedMod = -1f;

    void Start()
    {
        if (!gameManager)
            gameManager = GameManager.GetGameManager();
        ActionBasedContinuousMoveProvider cmp = gameManager.player.GetComponent<ActionBasedContinuousMoveProvider>();
        cmp.moveSpeed += speedMod;
    }

    private void OnDestroy()
    {
        ActionBasedContinuousMoveProvider cmp = gameManager.player.GetComponent<ActionBasedContinuousMoveProvider>();
        cmp.moveSpeed -= speedMod;
    }

}
