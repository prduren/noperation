using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceObject : MonoBehaviour
{
    Vector3 originalPos;
    public GameObject objStartPoint;
    public GameObject objEndPoint;
    public float speed;
    private float startTime;
    private float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(objStartPoint.transform.position, objEndPoint.transform.position);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, objEndPoint.transform.position, fractionOfJourney);
    }
}
