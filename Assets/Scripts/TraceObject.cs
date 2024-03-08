using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceObject : MonoBehaviour
{
    public TextLogic TextLogic;
    public GameObject objStartPoint;
    public GameObject objEndPoint;
    public GameObject objPlayerPoint;
    public float speed;
    public GameObject traceObjParent;
    public AudioSource introNoise;
    private float startTime;
    private float journeyLength;
    private bool hitObjEndPoint = false;
    private WaitForSeconds shortWait;
    private Vector3 originalPos;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(objStartPoint.transform.position, objEndPoint.transform.position);
        shortWait = new WaitForSeconds(3 + Time.deltaTime);
        originalPos = transform.position;
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        if (SM.introZoomDone || SM.beginNewPuzzle) {
            if (!hitObjEndPoint) {
                transform.position = Vector3.Lerp(transform.position, objEndPoint.transform.position, fractionOfJourney);
            }
            // mess with the < x number for how long til player grabs note
            if ((Vector3.Distance(transform.position, objEndPoint.transform.position)) < .00004f) {
                introNoise.Play(0);
                hitObjEndPoint = true;
                // TextLogic.IntroTextDisplay();
                // text box
                transform.position = Vector3.Lerp(transform.position, objPlayerPoint.transform.position, 1f);
                if ((Vector3.Distance(transform.position, objPlayerPoint.transform.position)) < .00004f) {
                    Debug.Log("move back to OG pos");
                    // reset stuff to prep for new puzzle and slide-in
                    transform.position = originalPos;
                    SM.beginNewPuzzle = false;
                    // init puzzle
                    // Transform currentPuzzle = this.transform.GetChild(SM.puzzleProgCounter);
                    traceObjParent.transform.GetChild(SM.puzzleProgCounter).gameObject.SetActive(true);
                    // currentPuzzle.parent.SetActive(true);
                    // set intro zoom to false for the first and only time otherwise we'll keep looping transform logic
                    SM.introZoomDone = false;
                    hitObjEndPoint = false;
                }
            }
        }
    }
}
