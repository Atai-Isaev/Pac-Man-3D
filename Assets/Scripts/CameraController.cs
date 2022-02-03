using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    float xRotation = 0f;
    public float mouseSensitivity = 3.5f;
    public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
         Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        xRotation -= mouseY * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        
        transform.localEulerAngles = Vector3.right * xRotation;
        playerBody.Rotate(Vector3.up * mouseX * mouseSensitivity);
        
        if (MainMenuController.GameIsOver || MainMenuController.GameIsWon)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (MainMenuController.GameIsPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
