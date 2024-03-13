using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour
{
    public AudioSource breakSound;
    public AudioSource staticSound;
    private GameObject[] objTraceList;
    // private int objTraceCounter = 0;
    private float amountToIncrementStatic;
    public AudioDistortionFilter staticSoundDistortion;
    public Suspicion Suspicion;
    public GameObject navButtons;

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objBegin") {
            Debug.Log("begin trace");
            // GetDistortionLevel();
            SM.startObjTraceFlag = true;
            if (!staticSound.isPlaying) {
                staticSound.Play(0);
            }
        } else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objTrace" && SM.startObjTraceFlag) {
            // Destroy(hit.transform.gameObject);
            hit.transform.gameObject.SetActive(false);
            breakSound.Play(0);
            //staticSoundDistortion.distortionLevel = staticSoundDistortion.distortionLevel + amountToIncrementStatic;
        } else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objEnd" && SM.startObjTraceFlag) {
            Debug.Log("end trace");
            SM.startObjTraceFlag = false;
            staticSound.Stop();
            hit.transform.parent.gameObject.SetActive(false);            
            SM.puzzleProgCounter++;
            if (SM.puzzleProgCounter == 5) {
                SM.dayOneComplete = true;
            }
            SM.beginNewPuzzle = true;
            // amountToIncrementStatic = 0;
        } else if (Physics.Raycast(ray, out hit) && !hit.transform.tag.Contains("obj") && SM.startObjTraceFlag) {
            Suspicion.SuspicionHandler();
            foreach (Transform child in hit.transform.parent.transform) {
                child.gameObject.SetActive(true);
            }
            SM.startObjTraceFlag = false;
        }

        if (SM.dayOneComplete) {
            navButtons.SetActive(true);
            transform.gameObject.SetActive(false);
        }

        // we're done doing traces, completed all 5 and continuing forward
        

    }

    /*
    void GetDistortionLevel() {
        objTraceList = GameObject.FindGameObjectsWithTag("objTrace");
        foreach(GameObject obj in objTraceList) {
            objTraceCounter = objTraceCounter + 1;
        }
        objTraceCounter = objTraceCounter + 1; // one more increment to make the distortion NOT 100% on the last objTrace (too loud)
        amountToIncrementStatic = 1f / objTraceCounter;
        Debug.Log(amountToIncrementStatic);
    }
    */
}
