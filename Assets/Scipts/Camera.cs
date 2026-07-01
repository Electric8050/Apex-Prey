using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("Target & Distance")]
    public Transform target;
    public float distance = 10.0f;

    [Header("Sensors")]
    public float mouseSensitivity = 5.0f;
    public float minY = -20f;
    public float maxY = 80f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Gather mouse input
        currentX += Input.GetAxis("Mouse X") * mouseSensitivity;
        currentY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // 2. Rotate the player horizontally with the mouse
        // This locks the character's forward direction to the camera's horizontal view
        target.rotation = Quaternion.Euler(0, currentX, 0);

        // 3. Calculate camera position using the updated target rotation
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        transform.position = target.position + rotation * direction;
        transform.LookAt(target.position);
    }
}