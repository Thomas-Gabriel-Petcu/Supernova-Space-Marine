using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlap_Terrain_Check : MonoBehaviour
{
    private Collider2D collInfo;
    public int radius;

    void Start()
    {
        collInfo = Physics2D.OverlapCircle(transform.position, radius);
    }
    void Update()
    {


        if (collInfo != null)
        {
            Destroy(gameObject);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
