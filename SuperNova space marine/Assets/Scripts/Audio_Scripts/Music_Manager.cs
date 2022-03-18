using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music_Manager : MonoBehaviour
{
    Sound s;
    Scene scene;
    public Sound[] sounds;
    public int rand;
    public static Music_Manager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.group;
        }
    }
    void Start()
    {
        
        rand = UnityEngine.Random.Range(0, sounds.Length);
        AssignScene();
        if (SceneManager.GetActiveScene().buildIndex == 0)//checks if secne is equal to main menu scene
        {
            sounds[rand].source.Stop();//stop the random song from playing
            sounds[2].source.Play();//play menu music
            AssignScene();
        }

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != scene.buildIndex)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2) //checks if scene is equal to Cutscene scene
            {
                sounds[2].source.Stop();//stops main menu music - change in the future
                AssignScene();
            }
            if (SceneManager.GetActiveScene().buildIndex == 0)//checks if secne is equal to main menu scene
            {
                sounds[rand].source.Stop();//stop the random song from playing
                sounds[2].source.Play();//play menu music
                AssignScene();
            }
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                sounds[2].source.Stop();
                RandomSong();
                if (rand == 2)
                {
                    sounds[2].source.Stop();
                    RandomSong();
                }
                AssignScene();
            }
        }  
    }

    public void Play(string name)
    {
       
         s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
            return;
    
        s.source.Play();
        
    }

    public void RandomSong()
    {     
        sounds[rand].source.Play();
    }

    void AssignScene()
    {
        scene = SceneManager.GetActiveScene();
    }

}
