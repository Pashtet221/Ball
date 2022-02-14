using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyrb;
    private GameObject player;

    void Start()
    {
        enemyrb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

   
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyrb.AddForce(lookDirection * speed);
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
