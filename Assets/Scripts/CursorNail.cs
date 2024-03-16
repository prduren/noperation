using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorNail : MonoBehaviour
{

    public GameObject nail;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: get this uh.. working? why not working?
        // something to do with the camera?
        // stale Unity sesh?
        // things locked somehow? cursor locked? object locked?
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = -13.49f;
        nail.transform.position = mousePos;
    }
}
