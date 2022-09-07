using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour
{
    Vector3 startPos;
    int x, y, z;
    public float xRange, yRange, zRange;
    public Vector3 lastPos;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        xRange = Random.Range(0, 100) / 100f;
        yRange = Random.Range(0,100) / 100f;
        zRange = Random.Range(0,100) / 100f;
        speed = Random.Range(0,60) / 30f;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Sin(Time.time * speed);
        float dx = remap(t, -1, 1, -xRange, xRange);
        float dy = remap(t, -1, 1, -yRange, yRange);
        float dz = remap(t, -1, 1, -zRange, zRange);

        Vector3 updatedPos = new Vector3(dx, dy, dz);
        lastPos = updatedPos;
        transform.localPosition = updatedPos; 
    }
    // Remap value from one range to another
    float remap(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
