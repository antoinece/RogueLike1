using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class dasher : MonoBehaviour
{
    private EnemiesUtil _enemiesUtil;
    private Rigidbody2D _Rb;
    public int health = 1;
    [SerializeField]private float speed=2;
    [SerializeField] private float shootDistance = 2;
    [SerializeField] private GameObject explosion;
    private float canDamage;

    private float time;
    private float timeR;
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
        timeR += Time.deltaTime;
        if (time > 2)
        {
            if (_enemiesUtil.StayAtDistance(_Rb, speed, shootDistance))
            {
                if (timeR>1)
                {
                    timeR = 0;
                    if (Random.Range(1, 3) == 2)
                    {
                        time = 0;
                        _enemiesUtil.Rush(_Rb, speed);
                    }
                }
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
                Instantiate(explosion, gameObject.transform.position,quaternion.identity );
                other.gameObject.GetComponent<playerControler>().life--;
            }
        }
    }
}