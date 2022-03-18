
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New FireArm", menuName = "Inventory/FireArm")]
public class FireArmBaseClass : MonoBehaviour
{
    //THIS SYSTEM DOES NOT WORK WITH BURST FIRE AND BUTTON HOLD AT THE SAME TIME. BUTTON HOLD MUST BE DISABLED FOR BURST FIRE TO WORK
    //Gun stats
    public int ID;
    public int bulletsLeft;
    public int magSize, bulletsPerTap;
    public float roundsPM, reloadTime, timeBetweenShots;
    private float timeBetweenShooting;
    public bool allowedButtonHold;
    public float distance;
    //Bools
    bool shooting, reloading, readyToShoot;

    //References
    public Transform projSpawnPoint;
    public LayerMask canBeShot;
    public RaycastHit rayHit;
    private GameObject poolerObj;
    private ObjectPooler pooler;
    public string poolObjName; //the name of the Pool that sits as a child of the Game Manager
                               //this string will be the name of the pool to be searched.

    InventoryUI invUI;
    //Functions
    void Awake()
    {
        poolerObj = GameObject.Find(poolObjName);
        bulletsLeft = magSize;
        readyToShoot = true;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        timeBetweenShooting = 1 / (roundsPM/60);
        pooler = poolerObj.GetComponent<ObjectPooler>();
        invUI = InventoryUI.instance;
    }
    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (allowedButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        //reloading
        if (bulletsLeft <= 0 && !reloading && Input.GetKey(KeyCode.Mouse0))//reloads while holding left click down
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading)
        {
            Reload();
        }
        //shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && invUI.open == false)
        {
            Shoot();
            //Debug.Log("We're ready to shoot and shooting"); 
        }

    }

    private void Shoot()
    {
        readyToShoot = false;
        GameObject proj = pooler.GetPooledObject();
        if (proj == null) return;
        proj.transform.position = projSpawnPoint.position;
        proj.transform.rotation = projSpawnPoint.rotation;
        proj.SetActive(true);
        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);

        //if (bulletsLeft > 0)//in case of burst fire I think
        //{
        //    Invoke("Shoot", timeBetweenShots);
        //}
    }                                            
    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        //Debug.Log("Reloading");
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
    }
}
