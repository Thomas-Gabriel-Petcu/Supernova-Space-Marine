using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    #region Variables
    public Rigidbody2D rb;
    bool canIdle = true;
    bool canRun = true;
    bool canJump = true;
    private bool[] booleans;
    [SerializeField]
    private LookAround lookAroundCS;

    Vector3 mouse_pos; //stores a position value
    public Transform target; //Assign to the object you want to rotate
    Vector3 object_pos; //stores a position value
    public float angle; //single floating point used for storring the angle
    public Transform weaponPositioningHelper;
    #endregion

    void Awake()
    {
        booleans = new bool[3] {canIdle, canRun, canJump};
        weaponPositioningHelper = transform.Find("WeaponPositioningHelper");
    }

    void Update()
    {
        mouse_pos = Input.mousePosition; //assigns the mouse position value the variable
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        ChangeDirection(lookAroundCS.facingRight);//checks if the bool has changed and changes the faced direction accordingly
    }

    public void ChangeDirection(bool x)
    {
        if (x == true)//facing right
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else//facing left
        {
            transform.rotation = Quaternion.Euler(new Vector3(180, 0, -angle));
        }
    }

    //The functions down here might be for making the arm move up and down while the player walks.
    private void HolderFunction()
    {
        Debug.Log(rb.velocity.x);
        Debug.Log(rb.velocity.y);
        Debug.Log(canRun);

        if (rb.velocity.x != 0.0f && rb.velocity.y == 0.0f && canRun == true) //is running
        {
            Debug.Log("We're running");
            foreach (var item in booleans)
            {
                item.Equals(true);
            }
            StopAllCoroutines();
            StartCoroutine("Running");
            //the player is running 0.667 delay
        }
        else if (rb.velocity.y != 0.0f && canJump == true) //is jumping or falling
        {
            foreach (var item in booleans)
            {
                item.Equals(true);
            }
            StopAllCoroutines();

        }
        else if (rb.velocity == new Vector2(0.0f, 0.0f) && canIdle == true)
        {
            foreach (var item in booleans)
            {
                item.Equals(true);
            }
            StopAllCoroutines();
            StartCoroutine("Idling");
            Debug.Log("We're standing still and starting coroutine for idling");
        }

    }

    IEnumerator Idling()
    {
        canIdle = false;
        while (canIdle == false)
        {
            //Debug.Log("Moving down");
            //gameObject.transform.position += new Vector3(0, -0.06f, 0);
            yield return new WaitForSeconds(0.1953f);
            //gameObject.transform.position += new Vector3(0, 0.06f, 0);
            yield return new WaitForSeconds(0.1953f);
        }
        
    }
    IEnumerator Running()
    {
        canRun = false;
        while (canRun == false)
        {
            Debug.Log("Moving down");
            gameObject.transform.position += new Vector3(0, -0.06f, 0);
            yield return new WaitForSeconds(0.667f);
            gameObject.transform.position += new Vector3(0, 0.06f, 0);
            yield return new WaitForSeconds(0.667f);
        }
    }
   
}
