using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Walkspeed;
    public float health;
    public float maxHealth = 100;
    
    [Header("Attack Stats")]
    [Range(0,100)] public int attackDamage;
    [Range(0,100)] public int attackRange;

    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleWalking()
    {
        
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
