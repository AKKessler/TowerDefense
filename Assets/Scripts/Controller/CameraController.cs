using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float horizontalSpeed;
    public float verticalSpeed;
    public float rotationSpeed;
    public float zoomSpeed;
    
	void Update () {

        float horizontal = Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Rotation") * rotationSpeed * Time.deltaTime;
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime; 

        
        transform.Translate(Vector3.forward * vertical);
        transform.Translate(Vector3.right * horizontal);
        transform.Translate(Vector3.up * -zoom); // negative so scroll down is zoom out
        transform.Rotate(Vector3.up, rotation, Space.World);
    }
}
