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
            Destroy(hit.transform.gameObject);
            breakSound.Play(0);
            //staticSoundDistortion.distortionLevel = staticSoundDistortion.distortionLevel + amountToIncrementStatic;
        } else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objEnd") {
            Debug.Log("end trace");
            SM.startObjTraceFlag = false;
            staticSound.Stop();
            hit.transform.parent.gameObject.SetActive(false);            
            SM.puzzleProgCounter++;
            SM.beginNewPuzzle = true;
            // amountToIncrementStatic = 0;
            Debug.Log(SM.beginNewPuzzle);
        } else if (Physics.Raycast(ray, out hit) && !hit.transform.tag.Contains("obj") && SM.startObjTraceFlag) {
            Suspicion.SuspicionHandler();
            Debug.Log("fail! " + SM.suspicionIncrementer);
            
            SM.startObjTraceFlag = false;
        }

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
