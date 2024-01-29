using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour
{
    public AudioSource breakSound;
    public AudioSource staticSound;
    private GameObject[] objTraceList;
    private int objTraceCounter = 0;
    private float amountToIncrementStatic;
    public AudioDistortionFilter staticSoundDistortion;

    void Start()
    {
        objTraceList = GameObject.FindGameObjectsWithTag("objTrace");
        foreach(GameObject obj in objTraceList) {
            objTraceCounter = objTraceCounter + 1;
        }
        amountToIncrementStatic = 1f/objTraceCounter;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objBegin") {
            Debug.Log("begin trace");
            SM.startObjTraceFlag = true;
            if (!staticSound.isPlaying) {
                staticSound.Play(0);
            }
        } else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objTrace" && SM.startObjTraceFlag) {
            Destroy(hit.transform.gameObject);
            breakSound.Play(0);
            staticSoundDistortion.distortionLevel = staticSoundDistortion.distortionLevel + amountToIncrementStatic;
        } else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objEnd") {
            Debug.Log("end trace");
            SM.startObjTraceFlag = false;
            staticSound.Stop();
        } else if (Physics.Raycast(ray, out hit) && !hit.transform.tag.Contains("obj") && SM.startObjTraceFlag) {
            Debug.Log("fail!");
        }

    }
}
