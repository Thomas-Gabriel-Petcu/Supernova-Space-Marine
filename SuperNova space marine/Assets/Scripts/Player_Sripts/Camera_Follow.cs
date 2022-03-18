using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target; //target to follow - set in inspector
    public float smoothSpeed;
    public bool hangarCam;
    public float distance;
    public bool cutScene = false;
    public float hangarVerticalOffset;
    public float gameVerticalOffset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()//Why the fuck did I use FixedUpdate()?? This is used for physics!
    {
        if (hangarCam == true && cutScene != true)
        {
            HangarCam();
        }
        else
        {
            GameCam();
        }
    }

    void HangarCam()
    {
        transform.position = new Vector3(target.position.x, target.position.y + hangarVerticalOffset, distance);
    }
    void GameCam()
    {
        transform.position = new Vector3(0, target.position.y + gameVerticalOffset, distance);
    }
    void CutSceneMove()
    {
        cutScene = true;
        Vector3 originalPos = transform.position; //at the end of the cutscene revert the position of the camera to this position
        GameObject sceneTarget = GameObject.FindGameObjectWithTag("Hangar_Cutscene_Target1");
        transform.position = Vector3.Slerp(transform.position, sceneTarget.transform.position, 1);
    }
    //Remember to create the trigger object and script for the cutscene and set all the variables:
    //cutscene target 1
    //cutscene target 2

}
