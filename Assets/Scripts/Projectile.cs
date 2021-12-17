// Student name: Phan Tan Dat
// Student ID: 18127078
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    AudioSource audioSource;
    public AudioClip projectileHitSound1, projectileHitSound2;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        int rand = Random.Range(0, 1);
        switch (rand)
        {
            case 0:
                audioSource.PlayOneShot(projectileHitSound1);
                break;
            default:
                audioSource.PlayOneShot(projectileHitSound2);
                break;
        }  
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }
        Destroy(gameObject);
    }
}
