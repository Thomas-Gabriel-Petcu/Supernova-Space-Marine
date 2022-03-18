using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;

public class Station_Video_Player : MonoBehaviour
{
    [SerializeField]
    VideoPlayer videoPlayer;
    [SerializeField]
    AudioSource source;
    bool playing = false;
    double timer;

    void Start()
    {
        timer = videoPlayer.length;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 )
        {
            videoPlayer.Stop();
            if (playing == false)
            {
                source.Play();
                playing = true;
            }       
        }
    }
}
