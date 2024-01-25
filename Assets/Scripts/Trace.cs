using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trace : MonoBehaviour
{
    public AudioSource breakSound;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objBegin") {
            Debug.Log("begin trace");
            SM.startObjTraceFlag = true;
        } else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objTrace" && SM.startObjTraceFlag) {
            Destroy(hit.transform.gameObject);
            breakSound.Play(0);
        } else if (Physics.Raycast(ray, out hit) && hit.transform.tag == "objEnd") {
            Debug.Log("end trace");
            SM.startObjTraceFlag = false;
        } else if (Physics.Raycast(ray, out hit) && !hit.transform.tag.Contains("obj") && SM.startObjTraceFlag) {
            Debug.Log("fail!");
        }
    }
}
