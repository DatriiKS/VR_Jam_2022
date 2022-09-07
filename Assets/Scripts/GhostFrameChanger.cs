using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelImporter;

public class GhostFrameChanger : MonoBehaviour
{
    public int numFrames = 3;
    int currentFrame = 0;
    public string frameBase = "Ghostx_";
    public float changeDelay = 4f;
    public bool doBlink = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if(doBlink)
        {
            doBlink = false;
            StartCoroutine(ChangeFrame(0f));

        }
    }


    IEnumerator ChangeFrame(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<VoxelFrameAnimationObject>().ChangeFrame(GetNextFrame());

        StartCoroutine(ChangeFrame(0.1f));
    
    }



    private string GetNextFrame()
    {

        if(currentFrame == numFrames-1f)
        {
            currentFrame = 0;
            doBlink = false;
        } else
        {
            currentFrame++;
        }
        return frameBase + currentFrame.ToString();

    }

}
