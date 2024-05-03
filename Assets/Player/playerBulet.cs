using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class playerBulet : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ennemy"))
        {
            chaser chaser = other.GetComponent<chaser>();
            chaser.damage();
            Instantiate(explosion,gameObject.transform.position,quaternion.identity);
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("enemy1"))
        {
            dasher dasher = other.GetComponent<dasher>();
            dasher.damage();
            Instantiate(explosion,gameObject.transform.position,quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("enemy2"))
        {
            shooter shooter = other.GetComponent<shooter>();
            shooter.damage();
            Instantiate(explosion,gameObject.transform.position,quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("enemy3"))
        {
            spawner spawner = other.GetComponent<spawner>();
            spawner.damage();
            Instantiate(explosion,gameObject.transform.position,quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
    }
}
