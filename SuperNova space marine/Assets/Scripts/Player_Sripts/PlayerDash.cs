using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //IMPLEMENT COOLDOWN
    private float timeSinceLastClick;
    private float lastClickTime;
    private const float double_Click_Time = 0.2f;
    public float dashTime;
    private float _dashTime;
    [SerializeField] private float dashForce;
    public float dashCoolDown;
    private float _dashCoolDown;
    private Rigidbody2D rb;

    private string lastInput;
    private bool readyToDetect = true;

    private Player_Movement playerMovement;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _dashCoolDown = dashCoolDown;
        _dashTime = dashTime;
        playerMovement = gameObject.GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        dashTime -= Time.deltaTime;
        dashCoolDown -= Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.W))
        {
            readyToDetect = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            readyToDetect = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            readyToDetect = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            readyToDetect = true;
        }

        switch (Input.inputString)
        {
            case "w":
                if (DetectDoubleTap(Input.inputString) == true)
                {
                    Dash(Vector3.up);
                }
                lastInput = Input.inputString;
                break;
            case "a":
                if (DetectDoubleTap(Input.inputString) == true)
                {
                    Dash(Vector3.left);
                }
                lastInput = Input.inputString;
                break;
            case "s":
                if (DetectDoubleTap(Input.inputString) == true)
                {
                    Dash(Vector3.down);
                }
                lastInput = Input.inputString;
                break;
            case "d":
                if (DetectDoubleTap(Input.inputString) == true)
                {
                    Dash(Vector3.right);
                }
                lastInput = Input.inputString;
                break;

            default:
                break;
        }
        StopDashing();
    }
    private bool DetectDoubleTap(string inputString)
    {
        if (readyToDetect == true)
        {
            readyToDetect = false;
            string inputSave = inputString;
            timeSinceLastClick = Time.time - lastClickTime;
            if (inputSave == lastInput && timeSinceLastClick <= double_Click_Time)
            {
                Debug.Log("Pressed same key twice");
                return true;
            }
            lastClickTime = Time.time;
            return false;
        }
        return false;
    }
    private void Dash(Vector3 direction)
    {
        dashTime = _dashTime;
        if (dashTime > 0 && timeSinceLastClick <= double_Click_Time && dashCoolDown <= 0)
        {
            Debug.Log("Dashed");
            playerMovement.enabled = false;
            rb.velocity = direction * Time.deltaTime * dashForce;
            dashCoolDown = _dashCoolDown;
        }
    }

    private void StopDashing()
    {
        if (dashTime <= 0 && playerMovement.enabled == false)
        {
            Debug.Log("Stopped dashing");
            rb.velocity = Vector3.zero;
            playerMovement.enabled = true;
        }
    }
}
