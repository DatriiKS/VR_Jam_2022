using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetScore : MonoBehaviour
{
    public GameObject gameManager;

    public void SetScore()
    {
        GameManager gm = gameManager.GetComponent<GameManager>();
        int currentScore = gm.score;
        Debug.Log(gm.score);
        Debug.Log(gm.state);

        TextMeshProUGUI textmeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        if (gm.state == GameManager.GameState.Win)
        {
            textmeshPro.SetText("Score: {0}", gm.score);
        } else {
            textmeshPro.SetText("No Score");
        }

    }
}
