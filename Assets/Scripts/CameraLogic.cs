using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour {
    public Transform target;
    public Camera newCamera;
    
    private float initHeightAtDist;
    private bool dzEnabled;
    private float initFieldOfView;
    public float duration = 1;
    // private bool initFOVReturnFlag = false;
    public Vector3 paintingCameraLoc;
    public Quaternion paintingCameraRot;
    public Vector3 kitchenCameraLoc;
    public Quaternion kitchenCameraRot;
    public Vector3 windowCameraLoc;
    public Quaternion windowCameraRot;
    public Vector3 tvCameraLoc;
    public Quaternion tvCameraRot;
    public Vector3 doorCameraLoc;
    public Quaternion doorCameraRot;

    // Calculate the frustum height at a given distance from the camera.
    float FrustumHeightAtDistance(float distance) {
        return 2.0f * distance * Mathf.Tan(newCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance.
    float FOVForHeightAndDistance(float height, float distance) {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    // Start the dolly zoom effect.
    void StartDZ() {
        var distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(distance);
        dzEnabled = true;
    }
    
    // Turn dolly zoom off.
    void StopDZ() {
        dzEnabled = false;
    }
    
    void Start() {
        StartDZ();
        // this should be 60
        initFieldOfView = newCamera.fieldOfView;
    }

    void Update () {
        if (dzEnabled) {
            // Measure the new distance and readjust the FOV accordingly.
            var currDistance = Vector3.Distance(transform.position, target.position);
            newCamera.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
            
            if (newCamera.transform.position.z > -22f && SM.beginGamePostIntro) { // this IF used to be in the Update() root. I moved it to ensure camera position translation does not occur when we move the camera to view other objects.
                transform.Translate(Vector3.back * Time.deltaTime * 28f);
            }
        }
        
        // Simple control to allow the camera to be moved in and out using the up/down arrows.
        // transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * 28f);

        if (newCamera.transform.position.z < -21f) {
            dzEnabled = false;
            newCamera.fieldOfView = Mathf.MoveTowards(newCamera.fieldOfView, initFieldOfView, 100f * Time.deltaTime);
            if (newCamera.fieldOfView > 70) {
                StartCoroutine(IntroZoomDone());
            } 
        }
    }

    public void GoToPainting() {
        newCamera.transform.position = paintingCameraLoc;
        newCamera.transform.rotation = paintingCameraRot;
    }

    public void GoToKitchen() {
        newCamera.transform.position = kitchenCameraLoc;
        newCamera.transform.rotation = kitchenCameraRot;
    }

    public void GoToWindow() {
        newCamera.transform.position = windowCameraLoc;
        newCamera.transform.rotation = windowCameraRot;
    }

    public void GoToTV() {
        newCamera.transform.position = tvCameraLoc;
        newCamera.transform.rotation = tvCameraRot;
    }

    public void GoToDoor() {
        newCamera.transform.position = doorCameraLoc;
        newCamera.transform.rotation = doorCameraRot;
    }

    IEnumerator IntroZoomDone() {
        yield return new WaitForSeconds(3);
        SM.introZoomDone = true;
        StopDZ();
    }
}