using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Examples;

public class TurnProviderSphere : MonoBehaviour
{
    private LocomotionSchemeManager schemeManager;
    private SnapTurnProviderBase snapTurnProvider;
    private ContinuousTurnProviderBase continuousTurnProvider;
    [SerializeField] private bool isContinious;

    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.GetGameManager();
        continuousTurnProvider = gameManager.player.gameObject.GetComponent<ContinuousTurnProviderBase>();
        snapTurnProvider = gameManager.player.gameObject.GetComponent<SnapTurnProviderBase>();
        schemeManager = gameManager.player.gameObject.GetComponent<LocomotionSchemeManager>();
    }
    public void ChangeTurnStyle()
    {
        if (isContinious)
        {
            continuousTurnProvider.enabled = true;
            snapTurnProvider.enabled = false;
            schemeManager.turnStyle = LocomotionSchemeManager.TurnStyle.Continuous;
            Debug.Log("CHANGED TO CONTINIOUS");
        }
        else
        {
            continuousTurnProvider.enabled = false;
            snapTurnProvider.enabled = true;
            schemeManager.turnStyle = LocomotionSchemeManager.TurnStyle.Snap;
            Debug.Log("CHANGED TO SNAP");
        }
    }
}
