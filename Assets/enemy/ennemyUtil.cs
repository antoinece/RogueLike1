using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesUtil : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private ennemyGen _ennemyGen;

    public void Wander(Rigidbody2D enemyRb,float speed)
    {
        switch (Random.Range(1, 5))
        {
            case 1:
                enemyRb.velocity = Vector2.up * speed;
                break;
            case 2:
                enemyRb.velocity = Vector2.down * speed;
                break;
            case 3:
                enemyRb.velocity = Vector2.left * speed;
                break;
            case 4:
                enemyRb.velocity = Vector2.right * speed;
                break;
            default:
                enemyRb.velocity = Vector2.up * speed;
                break;
        }
    }

    public void Patrol(List<Transform> patrolPoints,Rigidbody2D enemyRb,float speed,int startingPosition)
    {
        Vector3 direction = patrolPoints[startingPosition].position - enemyRb.transform.position;
        enemyRb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    public bool StayAtDistance(Rigidbody2D enemyRb,float speed,float distance)
    {
        Vector3 direction = player.transform.position - enemyRb.transform.position;
        if (direction.magnitude>distance)
        {
            enemyRb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        }
        else if (direction.magnitude< distance-0.1 )
        {
            enemyRb.velocity = new Vector2(-direction.x, -direction.y).normalized * speed;
        }
        else
        {
            enemyRb.velocity = new Vector2(0,0);
            return true;
        }

        return false;
    }

    public void Rush(Rigidbody2D enemyRb,float speed)
    {
        Vector3 direction = player.transform.position - enemyRb.transform.position;
        enemyRb.velocity = new Vector2(direction.x, direction.y).normalized * speed * 4;
    }

    public bool Chase(Rigidbody2D enemyRb,float speed,float distance)
    {
        Vector3 direction = player.transform.position - enemyRb.transform.position;
        if (direction.magnitude<distance)
        {
            enemyRb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
            return true;
        }

        return false;

    }

    public void Flee(Rigidbody2D enemyRb,float speed)
    {
        Vector3 direction = enemyRb.transform.position - player.transform.position;
        enemyRb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
    }

    public void MeleeAttack(GameObject enemy)
    {
        
    }

    public void RangeAttack(GameObject weapon,Transform enemy)
    {
        Instantiate(weapon,enemy.transform.position,Quaternion.identity);
    }

    public void Spawn(GameObject spawning,Vector3 position)
    {
        Instantiate(spawning, position, Quaternion.identity);
        _ennemyGen.nbEnemies++;
    }
    
    
    public void Kiled()
    {
        _ennemyGen.nbEnemies--;
    }
    
}
