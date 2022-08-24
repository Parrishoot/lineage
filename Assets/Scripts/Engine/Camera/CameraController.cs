using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : Singleton<CameraController>
{
    public float followSpeed = .005f;

    public float xOffset = 0f;
    public float yOffset = 0f;

    public float xBuffer = 0f;
    public float yBuffer = 0f;

    public bool smoothFollow = false;

    public GameObject cameraAnchor;

    public const float TRANSITION_TIME = .4f;

    private float targetZoom;

    private float currentTransitionTime = 0f;
    private float totalTransitionTime;
    private float startingZoom;

    private float defaultZoom;

    public void Start()
    {
        defaultZoom = Camera.main.orthographicSize;
        targetZoom = defaultZoom;
    }

    // Update is called once per frame
    void Update()
    {
        // If there is an object to follow set
        if (cameraAnchor != null)
        {

            // Find the current position and the object that you are following's position
            float currentX = gameObject.transform.position.x;
            float currentY = gameObject.transform.position.y;

            float followX = cameraAnchor.transform.position.x;
            float followY = cameraAnchor.transform.position.y;

            float xDiff = followX + xOffset - currentX;
            float yDiff = followY + yOffset - currentY;

            if (smoothFollow)
            {
                if (Mathf.Abs(xDiff) > xBuffer)
                {
                    gameObject.transform.Translate(xDiff * followSpeed * Time.deltaTime,
                                                   0,
                                                   0);
                }

                if (Mathf.Abs(yDiff) > yBuffer)
                {
                    gameObject.transform.Translate(0,
                                                   yDiff * followSpeed * Time.deltaTime,
                                                   0);
                }
            }

            else
            {
                transform.position = new Vector3(followX, followY, transform.position.z);
            }
        }

        // Set the zoom if it is not equal to the target
        if (Camera.main.orthographicSize != targetZoom)
        {
            currentTransitionTime += Time.deltaTime / totalTransitionTime;
            Camera.main.orthographicSize = Mathf.Lerp(startingZoom, targetZoom, Mathf.SmoothStep(0f, 1f, currentTransitionTime));
        }
    }

    // Function to be called by the follow object (typically Local player)
    public void SetFollowObject(GameObject follow)
    {
        cameraAnchor = follow;
    }

    public void SetConversation(GameObject characterOne, GameObject characterTwo, float zoomSpeed = TRANSITION_TIME)
    {
        // Find the location to move the camera anchor
        Vector3 targetLocation = characterOne.transform.position + ((characterTwo.transform.position - characterOne.transform.position) / 2);

        // Move the camera anchor to that location over time
        DetachCameraAnchor();
        cameraAnchor.GetComponent<CameraAnchorController>().MoveToTarget(targetLocation, TRANSITION_TIME);

        // Set the targetZoom and transitionTime;
        SetZoom(defaultZoom / 2, zoomSpeed);

    }

    public void SetHardPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
        this.cameraAnchor.transform.position = newPosition;
    }

    public static Vector2 GetVectorToMouse(Vector2 objectPosition)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition - objectPosition;
    }

    public static float getAngleToMouse(Vector2 objectPosition)
    {

        Vector3 mouseVector = GetVectorToMouse(objectPosition);

        return Mathf.Atan2(mouseVector.x, mouseVector.y) * Mathf.Rad2Deg;
    }

    public void ResetZoom()
    {
        SetZoom(defaultZoom);
    }

    private void SetZoom(float newTargetZoom, float zoomSpeed = TRANSITION_TIME)
    {
        currentTransitionTime = 0f;
        totalTransitionTime = zoomSpeed;
        targetZoom = newTargetZoom;
        startingZoom = Camera.main.orthographicSize;
        // GetComponent<UnityEngine.Experimental.Rendering.Universal.PixelPerfectCamera>().enabled = false;
    }

    public void AttachCameraAnchor(GameObject gameObject, bool shouldSmoothFollow = true)
    {
        cameraAnchor.transform.parent = gameObject.transform;
        cameraAnchor.transform.localPosition = Vector3.zero;
        ResetZoom();
        smoothFollow = shouldSmoothFollow;
    }

    public void DetachCameraAnchor(bool shouldSmoothFollow = true)
    {
        cameraAnchor.transform.parent = null;
        smoothFollow = shouldSmoothFollow;
    }

}
