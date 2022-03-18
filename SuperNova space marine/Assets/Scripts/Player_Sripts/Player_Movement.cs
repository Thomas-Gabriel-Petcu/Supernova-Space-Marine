using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Movement : MonoBehaviour
{
    #region Variables
    public float speed;
    private Rigidbody2D rb;
    public float moveInput;
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private int extraJumps;
    public int extraJumpsValue;
    private bool facingRight;//Useless variable ATM

    public bool moving = false;

    Scene loadedScene;
    #endregion

    #region Scene Change Detection
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//called when a scene is changed.
    {
        loadedScene = SceneManager.GetActiveScene();//returns the active scene
        if (loadedScene.name == "Level01")//change the player's movement characteristics to match the game movement
        {
            speed = 9;
            jumpForce = 17;
            jumpTime = 0.3f;
            extraJumps = 1;
            extraJumpsValue = 1;
            rb.mass = 1;
            rb.gravityScale = 4;
        }
        else if(loadedScene.name == "Station_LVL")//change the player movement characteristics to match station movement
        {
            speed = 8;
            jumpForce = 3;
            jumpTime = 0.1f;
            extraJumpsValue = 0;
            rb.mass = 1;
            rb.gravityScale = 1;
        }
    }
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    void Start()
    {
        extraJumps = extraJumpsValue;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
    void Update()
    {
        PlayerMovement();
    }
   private void PlayerMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (true && Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            extraJumps--;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (jumpTimeCounter > 0 && isJumping == true)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;

            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
   


}
