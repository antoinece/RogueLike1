using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    private EnemiesUtil _enemiesUtil;
    private Rigidbody2D _Rb;
    public int health = 1;
    
    [SerializeField]private float speed=3;
    [SerializeField] private float shootDistance = 4;
    [SerializeField] private GameObject bullet;
    
    private float time;
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
        if (_enemiesUtil.StayAtDistance(_Rb,speed,shootDistance)||_Rb.velocity.magnitude<0.1)
        {
            if (time > 0.5)
            {
                time = 0;
                _enemiesUtil.RangeAttack(bullet,transform);
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
}
