using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrrainGeneration : MonoBehaviour
{
   public GameObject[] terrain;
    public float time;
    int rand;
    int numOfTerSpawned;
    public int maxNumOfTer = 20;
    public bool canSpawnArena;
    public GameObject arena;
    public bool arenaSpawned = false;
    public int maxLevelHeight;
    public GameObject[] levelLimiters = new GameObject[2];

    //=========
    public GameObject destroyer;
    Vector3 offset = new Vector3(0, 10, 0);

    //=========

   void Start()
    {
        StartCoroutine(Terrain()); //starts Terrain() coroutine
        
    }
   IEnumerator Terrain()
    {
        while (numOfTerSpawned <= maxNumOfTer && transform.position.y <= maxLevelHeight) //checks for number of platforms generated and of y (height) value
        {
            yield return new WaitForSeconds(time);
            transform.position = new Vector2(transform.position.x, transform.position.y + 12.5f); //sets current transform.positon to the new position
            rand = Random.Range(0, terrain.Length); //grabs a random platform
            Instantiate(terrain[rand], transform.position, Quaternion.identity); //stores platform prefab into Gameobject a
            numOfTerSpawned++; //increments tracker of spawned platforms
            
            if (transform.position.y >= maxLevelHeight) //checks for y position of the generator
            {
                if (canSpawnArena && arenaSpawned == false)
                {
                    transform.position = new Vector3(transform.position.x , transform.position.y + 12.5f, 0); //moves up before instantiating the arena to avoid overlaping
                    Instantiate(arena, transform.position, Quaternion.identity); //instantiates the arena
                    Instantiate(destroyer, transform.position + offset, Quaternion.identity); //use Vector3 (x,y,z) to add offset to transform.position
                    arenaSpawned = true;
                    foreach (var limiter in levelLimiters)
                    {
                       BoxCollider2D col = limiter.GetComponent<BoxCollider2D>();
                       col.size = new Vector2(col.size.x, transform.position.y);
                       col.offset = new Vector2(col.offset.x, transform.position.y * 0.33f);
                    }
                    StopCoroutine("Terrain");
                }
            }
        }
    }
    }

