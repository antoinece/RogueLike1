using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaser : MonoBehaviour
{
    private EnemiesUtil _enemiesUtil;
    private Rigidbody2D _Rb;
    private float time;
    public int health = 2;
    [SerializeField] private GameObject explosion;
    [SerializeField]private float speed=2;
    private float canDamage;
    [SerializeField] private float chaseDistance = 5;
    // Start is called before the first frame update
    void Start()
    {
        _enemiesUtil = FindFirstObjectByType<EnemiesUtil>();
        _Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        canDamage += Time.deltaTime;
        if (_enemiesUtil.Chase(_Rb,speed,chaseDistance) == false)
        {
            if (time > 0.2)
            {
                time = 0;
                _enemiesUtil.Wander(_Rb,speed);
            }        
        }

        if (health<1)
        {
            _enemiesUtil.Kiled();
            Destroy(gameObject);
        }
    }
    public void damage()
    {
        health--;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (canDamage > 0.1)
            {
                canDamage = 0;
                Instantiate(explosion, gameObject.transform.position,Quaternion.identity );
                other.gameObject.GetComponent<playerControler>().life--;
            }   
        }
    }
}
