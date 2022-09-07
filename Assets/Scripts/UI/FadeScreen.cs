using System.IO;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 2;
    public Color fadeColor;
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if(fadeOnStart) {
            FadeIn();
        }
    }

    public void FadeIn()
    {
        Fade(1,0);
    }
    public void FadeOut()
    {
        Fade(0,1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn,alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            SetColor(
                fadeColor,
                Mathf.Lerp(
                    alphaIn,
                    alphaOut,
                    timer / fadeDuration
                )
            );

            timer += Time.deltaTime;
            yield return null;
        }

        SetColor(fadeColor, alphaOut);
    }

    void SetColor(Color color, float alpha)
    {
        Color newColor = color;
        newColor.a = alpha;
        rend.material.SetColor("_BaseColor", newColor);
    }
}
