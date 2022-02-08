using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    private float gravity = -9.81f;
    public Transform teleportOne;
    public Transform teleportTwo;
    public Transform respawnPosition;
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
    public MainMenuController menu;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAudio = GetComponent<AudioSource>();
        playerAudio.PlayOneShot(introSound, 1f);
        initLifes = lifes.Length - 1;

        if (MainMenuController.FirstStart)
        {
            menu.StartGameMenu();
        }
        else
        {
            Time.timeScale = 1;
               
            MainMenuController.GameIsOver = false;
            MainMenuController.GameIsPaused = false;
            MainMenuController.GameIsWon = false;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        if (initLifes < 0)
        {
            Time.timeScale = 0; 
            menu.GameOverMenu();
            MainMenuController.GameIsOver = true;
        }
        else if (GameObject.Find("Coin") == null && GameObject.Find("Booster") == null)
        {
            MainMenuController.GameIsWon = true;
            Time.timeScale = 0;
            menu.GameOverMenu();
            
        }
        if (controller.isGrounded)
        {
            float horizontalMovement = Input.GetAxis("Horizontal");
            float verticalMovement = Input.GetAxis("Vertical");

            moveDirection = new Vector3(horizontalMovement, 0, verticalMovement);
            moveDirection = transform.TransformDirection(moveDirection);
            Physics.SyncTransforms();
            controller.Move(moveDirection * Time.deltaTime * speed);
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
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
                //Debug.Log("Game Over!");
                playerAudio.PlayOneShot(dieSound, 1.0f);
                transform.position = respawnPosition.position;
                ResetManager.instance.Reset();
                Destroy(lifes[initLifes]);
                initLifes -= 1;
            }
            
        }
        if (other.gameObject.tag.Equals("Coin"))
        {
			//Debug.Log("Coin");
            Destroy(other.gameObject);
            ScoreManager.instance.AddPoint();
            if (!playerAudio.isPlaying)
            {
                playerAudio.PlayOneShot(eatSound, 0.7f);
            }

        }

        if (other.gameObject.tag.Equals("Booster"))
        {
			//Debug.Log("Booster");
            _ = BoosterManager.instance.StartBoosterModeAsync();
            Destroy(other.gameObject);
            ScoreManager.instance.AddBoosterPoint();
            playerAudio.PlayOneShot(boosterSound, 0.8f);
        }
        
        if (other.gameObject.tag.Equals("Cherry"))
        { 
            
            other.gameObject.SetActive(false);
            ScoreManager.instance.AddCherryPoint();

            playerAudio.PlayOneShot(boosterSound, 0.8f);
        }
    }
}