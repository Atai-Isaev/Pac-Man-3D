using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 50f;
    public float turnSpeed = 50f;

    private Rigidbody playerRb;
   

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * forwardInput * speed);
        playerRb.AddForce(Vector3.right * horizontalInput * speed);

    }
}
