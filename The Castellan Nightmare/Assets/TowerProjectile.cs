using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    private Rigidbody2D _rb;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Spawned(Transform target)
    {
        _rb.velocity = target.position * projectileSpeed;
    }
}
