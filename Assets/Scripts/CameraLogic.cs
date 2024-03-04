using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour {
    public Transform target;
    public Camera newCamera;
    
    private float initHeightAtDist;
    private bool dzEnabled;
    private float initFieldOfView;
    public float duration = 1;
    

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
        }
        
        // Simple control to allow the camera to be moved in and out using the up/down arrows.
        // transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * 28f);
        if (newCamera.transform.position.z > -22f) {
            transform.Translate(Vector3.back * Time.deltaTime * 28f);
        }
        if (newCamera.transform.position.z < -21f) {
            // newCamera.fieldOfView = initFieldOfView;
            // newCamera.fieldOfView = Mathf.Lerp(newCamera.fieldOfView, initFieldOfView, t / duration);
            if (duration < 0){
                duration = 0.001f;
            }
            dzEnabled = false;
            newCamera.fieldOfView = Mathf.MoveTowards(newCamera.fieldOfView, initFieldOfView, 100f * Time.deltaTime);
        }
    }
}