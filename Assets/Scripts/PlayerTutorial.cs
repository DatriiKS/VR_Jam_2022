using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI turnModeText;
    [SerializeField] private Transform blueSpherePosition;
    [SerializeField] private Transform redSpherePosition;
    [SerializeField] private GameObject blueSphere;
    [SerializeField] private GameObject redSphere;

    private void Start()
    {
        tutorialText.gameObject.SetActive(true);
        StartCoroutine(TurnModeChoseCountdown());
    }

    IEnumerator TurnModeChoseCountdown()
    {
        yield return new WaitForSeconds(12);
        tutorialText.gameObject.SetActive(false);
        turnModeText.gameObject.SetActive(true);
        var blueSphereObj = Instantiate(blueSphere, blueSpherePosition);
        var redSphereObj = Instantiate(redSphere, redSpherePosition);
        yield return new WaitForSeconds(8);
        blueSphereObj.SetActive(false);
        redSphereObj.SetActive(false);
        gameObject.SetActive(false);
    }
}
