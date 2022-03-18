using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemey_Spawner : MonoBehaviour
{
    Vector2 newPos;
    float moveAmount;
    public Vector2 maxPos;
    public GameObject[] enemy;
    Vector2 screenBounds;
    float widthPos;
    int rand;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        maxPos =new Vector2(transform.position.x, transform.position.y);
        Invoke("Delayer", 0.5f);
    }
    IEnumerator Spawn()
    {
        while (transform.position.y < 100f)
        {
            yield return new WaitForSeconds(0.2f);
            widthPos = Random.Range(-10f, 10f);
            moveAmount = Random.Range(2f, 6f);
            newPos = new Vector2(transform.position.x + widthPos, transform.position.y + moveAmount);
            transform.position = newPos;
            rand = Random.Range(0, enemy.Length);
          GameObject a = Instantiate(enemy[rand],transform.position,Quaternion.identity);
            a.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), newPos.y);
        }
        
    }

    void Delayer()
    {
        StartCoroutine(Spawn());
    }
}
