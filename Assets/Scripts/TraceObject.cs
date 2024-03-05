using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceObject : MonoBehaviour
{
    public TextLogic TextLogic;
    Vector3 originalPos;
    public GameObject objStartPoint;
    public GameObject objEndPoint;
    public GameObject objPlayerPoint;
    public float speed;
    private float startTime;
    private float journeyLength;
    private bool hitObjEndPoint = false;
    private WaitForSeconds shortWait;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(objStartPoint.transform.position, objEndPoint.transform.position);
        shortWait = new WaitForSeconds(3 + Time.deltaTime);
        
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        if (SM.introZoomDone) {
            if (!hitObjEndPoint) {
                transform.position = Vector3.Lerp(transform.position, objEndPoint.transform.position, fractionOfJourney);
            }
            // mess with the < x number for how long til player grabs note
            if ((Vector3.Distance(transform.position, objEndPoint.transform.position)) < .00004f) {
                hitObjEndPoint = true;
                // TextLogic.IntroTextDisplay();
                // text box
                transform.position = Vector3.Lerp(transform.position, objPlayerPoint.transform.position, 1f);
            }
        }
    }
}
