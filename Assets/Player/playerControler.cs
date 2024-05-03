using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class playerControler : MonoBehaviour
{
    
    [SerializeField]private Animator animator;

    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxSpeed = 1f;
    
    [SerializeField] private GameObject bullet;
    public int life = 20;
    public int Maxlife = 20;
    
    private bool _up;
    private bool _down;
    private bool _left;
    private bool _right;
    private bool _isWalking;
    private bool _leftClick;
    private bool _cantShoot;
    
    
    private int _lastPos;

    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        life = Maxlife;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("up",_up);
        animator.SetBool("down",_down);
        animator.SetBool("side",_left);
        if (_left == false)
        {
            animator.SetBool("side",_right);
        }
        if (_left&&!_right)
        {
            _sr.flipX = false;
        }
        else if (_right&&!_left)
        {
            _sr.flipX = true;
        }

        Vector2 velocity = _rb.velocity;
        if (_up)
        {
            velocity += Vector2.up * (speed);
            _lastPos = 1;
        }
        if (_down)
        {
            velocity += Vector2.down*(speed);
            _lastPos = 2;
        }
        if (_left&&!_right)
        {
            velocity += Vector2.left*(speed);
            _lastPos = 3;
        }
        if (_right&&!_left)
        {
            velocity += Vector2.right*(speed);
            _lastPos = 4;
        }
        if (!_left&&!_right&&!_down&&!_up)
        {
            velocity = _rb.velocity / 1.5f;
        }

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
        
        _rb.velocity = velocity;
        
        if (_rb.velocity.magnitude>0.001)
        {
            animator.SetBool("walk",true);
        }
        else
        {
            animator.SetBool("walk",false);
        }

        if (_leftClick)
        {
            StartCoroutine(Shooting());
        }

        if (life<1)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    private void Shoot()
    {
       GameObject _bullet = Instantiate(bullet,transform.position,Quaternion.identity);

       Rigidbody2D rbBullet = _bullet.GetComponent<Rigidbody2D>();
       
       if (_rb.velocity.magnitude < 0.001)
       {
           switch (_lastPos)
           {
               case 1 : rbBullet.velocity = Vector2.up * (maxSpeed * 2);
                   rbBullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(-Vector2.up.y, -Vector2.up.x) * Mathf.Rad2Deg+90);
                   break;
               case 2 : rbBullet.velocity = Vector2.down * (maxSpeed * 2);
                   rbBullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(-Vector2.down.y, -Vector2.down.x) * Mathf.Rad2Deg+90);
                   break;
               case 3 : rbBullet.velocity = Vector2.left * (maxSpeed * 2);
                   rbBullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(-Vector2.left.y, -Vector2.left.x) * Mathf.Rad2Deg+90);
                   break;
               case 4 : rbBullet.velocity = Vector2.right * (maxSpeed * 2);
                   rbBullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(-Vector2.right.y, -Vector2.right.x) * Mathf.Rad2Deg+90);
                   break;
               default:
                   Debug.Log("player shoot");
                   rbBullet.velocity = Vector2.down * (maxSpeed * 2);
                   rbBullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(-Vector2.down.y, -Vector2.down.x) * Mathf.Rad2Deg+90);
                   break;
           }
       }
       else
       {
           rbBullet.velocity = _rb.velocity.normalized * (maxSpeed * 2);
           rbBullet.transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(-_rb.velocity.y, -_rb.velocity.x) * Mathf.Rad2Deg+90);
       }
      
    }

    public void OnUp(InputValue value)
    {
        _up = value.isPressed;
    }
    public void OnDown(InputValue value)
    {
        _down = value.isPressed;
    }
    public void OnLeft(InputValue value)
    {
        _left = value.isPressed;
    }
    public void OnRight(InputValue value)
    {
        _right = value.isPressed;
    }
    public void OnLeftClick(InputValue value)
    {
        _leftClick = value.isPressed;
    }
    
    IEnumerator Shooting()
    {
        if (_cantShoot)
        {
            yield break;
        }

        _cantShoot = true;
        Shoot();
        yield return new WaitForSeconds(0.5f);
        
        _cantShoot = false;
    }
}


