using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class RockController : MonoBehaviour
{
    public float forceMultiplier;
    GameObject player;
    public float collectionDistance = 0.9f;
    Rigidbody2D rb;
    public AudioSource audioSource;
    public AudioClip pickup;
    bool isSpawning;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        isSpawning = true;
        StartCoroutine(SpawnRock_IE());
    }

    private void Update()
    {
        if (!isSpawning)
        {
            float distance = Math.Abs(Vector2.Distance(player.transform.position, transform.position));
            Vector3 normalDirection = Vector3.Normalize(player.transform.position - transform.position);
            if (distance < collectionDistance)
            {
                rb.AddForce(normalDirection * forceMultiplier, ForceMode2D.Force);
            }
        } 
    }


    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (!isSpawning)
    //    {
    //        Debug.Log("Collision detected");

    //        if (collision.gameObject.tag == "Player")
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSpawning)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.GetComponent<PlayerMovement>().PlayPickupAudio();
                Destroy(gameObject);
            }
        }
    }

    IEnumerator SpawnRock_IE()
    {
        yield return new WaitForSeconds(0.4f);
        isSpawning = false;
    }
}
