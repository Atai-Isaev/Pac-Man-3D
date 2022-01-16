using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    private float gravity = -9.81f;

    private CharacterController controller;

    private Vector3 moveDirection;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");


            moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
            moveDirection = moveDirection.normalized;
            moveDirection = transform.TransformDirection(moveDirection);

            controller.Move(moveDirection * Time.deltaTime * speed);
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            // velocity = transform.TransformDirection(velocity);
            controller.Move(velocity * Time.deltaTime);
        }
    }
}