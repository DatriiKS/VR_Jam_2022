using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasRotator : MonoBehaviour
{
    private Player player;
    private void Start()
    {
        player = GameManager.GetGameManager().player;
    }
    void Update()
    {
        var resultedPosition = player.transform.position - transform.position;
        resultedPosition.y = 0;
        transform.rotation = Quaternion.LookRotation(resultedPosition);
    }
}
