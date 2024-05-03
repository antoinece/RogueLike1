using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{ 
    [SerializeField] private GameObject player;
    private Vector3 centreCamera = new Vector3(0.25f, 0.25f, -10);
    
    void Start()
    {
        transform.position = centreCamera;
    }

    void Update()
    {
        if (player.transform.position.x > centreCamera.x+6.7f)
        {
            centreCamera = new Vector3(centreCamera.x+13.22f,centreCamera.y,centreCamera.z);
            transform.position = centreCamera;
        }
        if (player.transform.position.x < centreCamera.x-6.7f)
        {
            centreCamera = new Vector3(centreCamera.x-13.22f,centreCamera.y,centreCamera.z);
            transform.position = centreCamera;
        }
        if (player.transform.position.y > centreCamera.y+4)
        {
            centreCamera = new Vector3(centreCamera.x,centreCamera.y+7.35f,centreCamera.z);
            transform.position = centreCamera;
        }
        if (player.transform.position.y < centreCamera.y-3.5)
        {
            centreCamera = new Vector3(centreCamera.x,centreCamera.y-7.35f,centreCamera.z);
            transform.position = centreCamera;
        }
        
    }
}
