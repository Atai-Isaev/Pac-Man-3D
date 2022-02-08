using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    private float gravity = -9.81f;
    public Transform teleportOne;
    public Transform teleportTwo;
    public Transform respawnPosition;
    public Transform ghostRespawn;

    private CharacterController controller;
    public AudioClip eatSound;
    public AudioClip dieSound;
    public AudioClip boosterSound;
    public AudioClip introSound;
    private Vector3 moveDirection;
    private Vector3 velocity;
    public Slider volumeSlider;
    private AudioSource playerAudio;
    public RawImage[] lifes;
    private int initLifes;
    public GameObject gameOverText;
    public GameObject winText;



    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);
        gameOverText.SetActive(false);
        controller = GetComponent<CharacterController>();
        playerAudio = GetComponent<AudioSource>();
        playerAudio.PlayOneShot(introSound, 1f);
        initLifes = lifes.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (initLifes < 0)
        {
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        } 
		else if (GameObject.Find("Coin") == null && GameObject.Find("Booster") == null)
        {
            Time.timeScale = 0;
            winText.SetActive(true);
        }
        if (controller.isGrounded)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
            moveDirection = moveDirection.normalized;
            moveDirection = transform.TransformDirection(moveDirection);
            Physics.SyncTransforms();
            controller.Move(moveDirection * Time.deltaTime * speed);
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
            // velocity = transform.TransformDirection(velocity);
            Physics.SyncTransforms();
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Teleport_1"))
        {
            transform.position = teleportTwo.transform.position - new Vector3(1, 0, 0);
        }
        if (other.gameObject.tag.Equals("Teleport_2"))
        {
            transform.position = teleportOne.position + new Vector3(1, 0, 0);
        }
        if (other.tag.Equals("Enemy"))
        {
            GhostController ghostContoroller = other.GetComponent<GhostController>();
            if (ghostContoroller.IsScared()) {
                ghostContoroller.StartDeadMode();
                ScoreManager.instance.AddGhostPoint();
            }
            else if(!ghostContoroller.IsDead())
            {
                Debug.Log("Game Over!");
                playerAudio.PlayOneShot(dieSound, 1.0f);
                transform.position = respawnPosition.position;
                ResetManager.instance.Reset();
                Destroy(lifes[initLifes]);
                initLifes -= 1;
            }
            
        }
        if (other.gameObject.tag.Equals("Coin"))
        {
			Debug.Log("Coin");
            Destroy(other.gameObject);
            ScoreManager.instance.AddPoint();
            if (!playerAudio.isPlaying)
            {
                playerAudio.PlayOneShot(eatSound,0.7f);

            }
		}
        if (other.gameObject.tag.Equals("Booster"))
        {
			Debug.Log("Booster");
            _ = BoosterManager.instance.StartBoosterModeAsync();
            Destroy(other.gameObject);
            ScoreManager.instance.AddBoosterPoint();
            playerAudio.PlayOneShot(boosterSound, 0.8f);
        }
    }
}