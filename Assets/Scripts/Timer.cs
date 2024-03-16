using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public Canvas timerTextCanvas; // only referencing for DontDestroyOnLoad bc requires root GameObject
    public float myTimer;
    private float initTimer;
    public GameObject currentPuzzle;
    public Suspicion Suspicion;

    // public float timeLeft = 30;

    void Start() {
        initTimer = myTimer;
    }

    void Update() {

        if (currentPuzzle.activeSelf) {
            if (myTimer > 0) {
                myTimer -= Time.deltaTime * 2;

                string minutesLeft = Mathf.FloorToInt(myTimer / 60).ToString();
                string seconds = (myTimer % 60).ToString("F0");
                seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;

                timerText.text = minutesLeft + ":" + seconds;
            } else {
                // TODO: reset mouse to beginning of puzzle?
                myTimer = initTimer;
                Suspicion.SuspicionHandler();
            }
        }

    }
}