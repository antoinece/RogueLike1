using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    private float t;
    private void Update()
    {
        t = t + Time.deltaTime;
        if (t>0.1)
        {
            Destroy(gameObject);
        }
    }
}
