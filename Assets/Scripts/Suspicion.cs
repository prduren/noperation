using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspicion : MonoBehaviour
{

    public GameObject suspicionObj;

    // execute this every time you want to increment Suspicion
    public void SuspicionHandler() {
        suspicionObj.transform.GetChild(0).transform.GetChild(SM.suspicionIncrementer).gameObject.SetActive(true);
        SM.suspicionIncrementer++;
    }
}
