using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform mainCamera;
    public float cameraSpeed = 2f;

    private Vector3 initialCameraPosition;
    private Vector3 targetCameraPosition;
    private float lerpProgress = 0.01f;
    private bool isCameraMoving;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveCameraSmoothly(Vector3 initialPosition, Vector3 targetPosition)
    {
        initialCameraPosition = initialPosition;
        targetCameraPosition = targetPosition;
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        isCameraMoving = true;
        Time.timeScale = 0f;
        yield return null;

        lerpProgress = 0f;
        while (lerpProgress < 1f)
        {
            lerpProgress += Time.unscaledDeltaTime * cameraSpeed;
            mainCamera.position = Vector3.Lerp(initialCameraPosition, targetCameraPosition, lerpProgress);
            yield return null;
        }

        isCameraMoving = false;
        Time.timeScale = 1f;
    }
}
