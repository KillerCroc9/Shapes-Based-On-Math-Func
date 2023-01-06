using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // The object to rotate around
    public float distance = 10.0f;  // The distance from the object
    public float xSpeed = 250.0f;  // The speed of rotation around the x-axis
    public float ySpeed = 120.0f;  // The speed of rotation around the y-axis

    float x = 0.0f;  // The current x-axis rotation
    float y = 0.0f;  // The current y-axis rotation

    void Update()
    {
        // Check if the right mouse button is held down
        
            // Get the mouse movement since the last frame
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Add the mouse movement to the current rotation values
            x += mouseX * xSpeed * Time.deltaTime;
            y -= mouseY * ySpeed * Time.deltaTime;

            // Clamp the y-axis rotation to avoid flipping the camera
            y = Mathf.Clamp(y, -90, 90);
        

        // Calculate the camera's position based on the current rotation
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

        // Set the camera's position and rotation
        transform.rotation = rotation;
        transform.position = position;
    }
}
