using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _camera;
    private Quaternion originalRotation;

    void Start()
    {
        // Automatically find and assign the main camera
        _camera = Camera.main;

        // Store the original rotation of the object
        originalRotation = transform.rotation;

        // If the main camera isn't found, log an error
        if (_camera == null)
        {
            Debug.LogError("No main camera found. Make sure your camera is tagged as 'MainCamera'.");
        }
    }

    void Update()
    {
        // If a camera has been assigned, update the rotation to face the camera
        if (_camera != null)
        {
            transform.rotation = _camera.transform.rotation;
        }
    }
}
