using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private GameObject player;
    [SerializeField]private Rigidbody2D rbe;
    [SerializeField] private GameObject explosion;  

    private float force = 3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 positionPlayer = player.transform.position;
        Vector3 positionBullet = rbe.transform.position;
        Vector3 direction = positionPlayer - positionBullet;
        rbe.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot+90);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("wall"))
        {
            Destroy(gameObject);
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<playerControler>().life--;
            Instantiate(explosion,gameObject.transform.position,quaternion.identity);
            Destroy(gameObject);
        }
    }
}
