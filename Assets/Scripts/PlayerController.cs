using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    private float gravity = -9.81f;

    private CharacterController controller;
    public AudioClip eatSound;
    public AudioClip dieSound;
    public AudioClip boosterSound;
    public AudioClip introSound;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAudio = GetComponent<AudioSource>();
        playerAudio.PlayOneShot(introSound,1f);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Debug.Log("Game Over!");
            playerAudio.PlayOneShot(dieSound,1.0f);
        }
        if (other.gameObject.tag.Equals("Coin"))
        {
            Destroy(other.gameObject);
            ScoreManager.instance.AddPoint();
            if (!playerAudio.isPlaying)
            {
                playerAudio.PlayOneShot(eatSound,0.7f);

            }

        }

        if (other.gameObject.tag.Equals("Booster"))
        {
            Destroy(other.gameObject);
            ScoreManager.instance.AddBoosterPoint();
            playerAudio.PlayOneShot(boosterSound, 0.8f);
        }
    }
}