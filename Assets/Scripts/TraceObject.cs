using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceObject : MonoBehaviour
{
    Vector3 originalPos;
    public GameObject objStartPoint;
    public GameObject objEndPoint;
    public GameObject objPlayerPoint;
    public float speed;
    private float startTime;
    private float journeyLength;
    private bool hitObjEndPoint = false;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(objStartPoint.transform.position, objEndPoint.transform.position);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        if (!hitObjEndPoint) {
            transform.position = Vector3.Lerp(transform.position, objEndPoint.transform.position, fractionOfJourney);
        }
        if ((Vector3.Distance(transform.position, objEndPoint.transform.position)) < .00001f) {
            hitObjEndPoint = true;
            // text box
            transform.position = Vector3.Lerp(transform.position, objPlayerPoint.transform.position, 1f);
        }        
    }
}
