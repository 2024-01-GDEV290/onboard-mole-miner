using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

    public Animator animator;
    public ParticleSystem particles;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip wallBreak;
    public AudioClip wallHit;
    public CameraShake cameraShake;
    //public float volume = 0.5f;

    private int hitCount = 0;
    private float hitTime = 0;

    // Update is called once per frame
    void Update()
    {

        // When this hits 0, you will be able to hit the wall again
        if (hitTime > 0)
        {
            hitTime -= 15 * Time.deltaTime;
            //animator.SetBool("hit", true);
        }

        // If hit three times, The wall will be destroyed!
        if (hitCount >=3)
        {
            audioSource.PlayOneShot(wallBreak, 0.5f);
            particles.Play();
            particles.gameObject.transform.parent = null;
            cameraShake.StartShake();
            Destroy(gameObject);
        }

        // Allows the wall to be hit again when this hits 0
        if (hitTime <= 0)
        {
            hitTime = 0;
            //animator.SetBool("hit", false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {   
        // Drill Collision
        if(other.gameObject.tag == "Drill" && hitTime <= 0)
        {
            audioSource2.PlayOneShot(wallHit, 0.4f);
            hitTime = 10;
            hitCount += 1;
            animator.SetInteger("wallState", hitCount);
        }
    }
}
