using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class RockController : MonoBehaviour
{
    public float forceMultiplier;
    GameObject player;
    public float collectionDistance = 0.5f;
    Rigidbody2D rb;
    public AudioSource audioSource;
    public AudioClip pickup;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Math.Abs(Vector2.Distance(player.transform.position, transform.position));
        Vector3 normalDirection = Vector3.Normalize(player.transform.position - transform.position);
        if (distance < collectionDistance)
        {
            rb.AddForce(normalDirection * forceMultiplier, ForceMode2D.Force);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");

        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
